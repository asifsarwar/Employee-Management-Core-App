function ConfirmDelete(uniqueId, isConfirmDelete) {
    debugger;
    var deletespan = 'deletespan_' + uniqueId;
    var confirmdelete = 'confirmdelete_' + uniqueId;

    if (isConfirmDelete) {
        $('#' + deletespan).hide();
        $('#' + confirmdelete).show();
    } else {
        $('#' + deletespan).show();
        $('#' + confirmdelete).hide();
    }
}