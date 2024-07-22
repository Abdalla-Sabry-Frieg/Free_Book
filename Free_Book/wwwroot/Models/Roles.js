// add table style for search

$(document).ready(function () {
    $('#myTable').DataTable();
});
// replace the table id to new name (id="TableRole") to set the jquery function

function Delete(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href= `/Admin/Accounts/DeleteRole?Id=${id}`
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });

}

// Arrow function Method

Edit = (id,name) => {
    document.getElementById("title").innerHTML = "تعديل مجموعه المستخدمينن";
    document.getElementById("btnSave").value = "تعديل";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}

Rest = () => {
    document.getElementById("title").innerHTML = "اضف مجموعه مستخدمسن جديده";
    document.getElementById("btnSave").value = "حفظ";
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
}