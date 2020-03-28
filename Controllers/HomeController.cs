using EmployeeManagementCoreApp.Helper.Database;
using EmployeeManagementCoreApp.Models;
using EmployeeManagementCoreApp.Models.DbModels;
using EmployeeManagementCoreApp.Models.DbModelsRepo.Interfaces;
using EmployeeManagementCoreApp.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
namespace EmployeeManagementCoreApp.Controllers
{
    public class HomeController:Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IHostingEnvironment _hostingEnvironment;
        public HomeController(IEmployeeRepository employeeRepository,IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment; 
        }
        [Route("")]
        [Route("Home/Index")]
        public ActionResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return RedirectToAction("List");
        }
        public ViewResult Details(int? id)
        {
            _employeeRepository = null;
            DbEmployee employee = _employeeRepository.GetEmployee(id??1);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            ViewData["Employee"] = employee;
            ViewData["PageTitle"] = "Employee Details";
            ViewBag.Employee = employee;
            HomeDetailsViewModel vm = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            //return View("../../MyViews/Test",employee);
            return View(vm);
            //return this.Json(_employeeRepository.GeEmployee(1));
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                DbEmployee newEmployee = new DbEmployee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Department = model.Department,
                    //PhotoPath = uniqueFileName,
                    //Photo = data
                };
                _employeeRepository.AddEmployee(newEmployee);
                List<DbEmployeeImages> ImagesList = new List<DbEmployeeImages>();
                string uniqueFileName = null;
                if(model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (var photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        byte[] data;
                        BinaryReader reader = new BinaryReader(photo.OpenReadStream());
                        data = reader.ReadBytes((int)photo.Length);
                        DbEmployeeImages image = new DbEmployeeImages()
                        {
                            EmployeeID = newEmployee.EmployeeID,
                            FileContentType = photo.ContentType,
                            FileLength = photo.Length,
                            FileName = uniqueFileName,
                            FileData = data
                        };
                        ImagesList.Add(image);
                    }
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        db.DbEmployeeImages.AddRange(ImagesList);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("details", new { id = newEmployee.EmployeeID });
            }
            return View();
        }
        public ViewResult List()
        {
            IEnumerable<DbEmployee> employeeList = _employeeRepository.GetAllEmployee();
            return View(employeeList);
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            DbEmployee employee = _employeeRepository.GetEmployee(id);

            if (employee != null)
            {
                EmployeeEditViewModel model = new EmployeeEditViewModel()
                {
                    EmployeeID = employee.EmployeeID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Department = employee.Department,
                    ExistingPhotoPath = employee.PhotoPath
                };
                return View(model);
            }
            else
            {
                throw new Exception("Error");
            }
            
        }
        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                string uniqueName = null;
                byte[] photoDataT = null;
                List<DbEmployeeImages> ImagesList = new List<DbEmployeeImages>();
                if (model.Photos != null)
                {
                   
                    foreach(var photo in model.Photos)
                    {
                        uniqueName = Guid.NewGuid() + photo.FileName;
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        string photoPath = Path.Combine(uploadsFolder, uniqueName);
                        photo.CopyTo(new FileStream(photoPath, FileMode.Create));
                       
                        BinaryReader reader = new BinaryReader(photo.OpenReadStream());
                        byte[]photoData = reader.ReadBytes((int)photo.Length);
                        if (i == 0)
                        {
                            photoDataT = photoData;
                            i++;
                        }
                        DbEmployeeImages image = new DbEmployeeImages()
                        {
                            EmployeeID = model.EmployeeID,
                            FileContentType = photo.ContentType,
                            FileLength = photo.Length,
                            FileName = uniqueName,
                            FileData = photoData
                        };
                        ImagesList.Add(image);
                    }
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        db.DbEmployeeImages.AddRange(ImagesList);
                        db.SaveChanges();
                    }
                }
                DbEmployee employee = _employeeRepository.GetEmployee(model.EmployeeID);
                if (employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Department = model.Department;
                    employee.PhotoPath = uniqueName;
                    employee.Photo = photoDataT;
                    _employeeRepository.Update(employee);
                    //using (ApplicationDbContext db = new ApplicationDbContext())
                    //{
                    //    var entity = db.DbEmployee.Attach(employee);
                    //    entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //    db.SaveChanges(); 
                    //}
                }
                return RedirectToAction("Details", new { id = employee.EmployeeID });
            }
            return View();
        }
    }
}
