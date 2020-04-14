using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace EmployeeManagementCoreApp.Helper.Attributes
{
    public class AllowedDomainValidation : ValidationAttribute
    {
        private readonly string allowedDomain;
        public AllowedDomainValidation(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            if(strings != null && strings.Count() == 2 && strings[1].ToUpper() == allowedDomain.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
