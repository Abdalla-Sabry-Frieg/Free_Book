// add table style for search

$(document).ready(function () {
    $('#myTable').DataTable({
        "autoWidth": false,
        "responsive": true
    });
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
            window.location.href = `/Admin/Accounts/DeleteUser?userId=${id}`
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });

}

// Arrow function Method

Edit = (id, roleName, name, email, image, activeUser) => {
    document.getElementById("title").innerHTML = "تعديل مجموعه المستخدم";
    document.getElementById("btnSave").value = "تعديل";
    document.getElementById("UserId").value = id;
    document.getElementById("UserName").value = name; 
    document.getElementById("UserRole").value = roleName; 
    document.getElementById("UserEmail").value = email;
    document.getElementById("Image").hidden = false;
    document.getElementById("Image").src = "/Images/Users/" + image;
   

    var active = document.getElementById("ActiveUser");
    if (activeUser == "True")
        active.Checked = true;
    else
        active.Checked = false;

  //to hide password in edit view because the password will change in seperated view 

    $('#grPassword').hide();
    $('#grComparPassword').hide();
    // becuse i have a validation side to password
    document.getElementById("userPassword").value = "$$$$$$";
    document.getElementById("userComparePassword").value = "$$$$$$";

   // document.getElementById("imageHide").value = image;

  
  
   
}

Rest = () => {
    document.getElementById("title").innerHTML = "اضف مجموعه مستخدمين جديده";
    document.getElementById("btnSave").value = "حفظ";
    document.getElementById("UserId").value = "";
    document.getElementById("UserName").value = "";
    document.getElementById("UserRole").value = "";
    document.getElementById("UserEmail").value = "";
    // document.getElementById("ImageUser").value = "";
    document.getElementById("ActiveUser").Checked = false;

    // to show the password in create step
    $('#grPassword').show();
    $('#grComparPassword').show();
    document.getElementById("userPassword").value = "";
    document.getElementById("userComparePassword").value = "";

    document.getElementById("Image").hidden = false;
}

function ChangePassword(id) {

    document.getElementById('userPasswordId').value = id;
}