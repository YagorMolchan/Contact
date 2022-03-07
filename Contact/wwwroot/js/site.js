function checkForm(name, phoneNumber, jobTitle, birthDate) {

    let flag = true;

    if (name.value == "") {
        name.nextElementSibling.innerText = "Имя не заполнено!";
        flag = false;
    }
    if (jobTitle.value == "") {
        jobTitle.nextElementSibling.innerText = "Место работы не заполнено!";
        flag = false;
    }
    if (phoneNumber.value.indexOf("_") >= 0) {
        phoneNumber.nextElementSibling.innerText = "Номер телефона не заполнен!";
        flag = false;
    }
    if (birthDate.value == "") {
        birthDate.nextElementSibling.innerText = "Дата рождения не заполнена!";
        flag = false;
    }

    return flag;

}

function AddNewContact(ContactId) {
    $("#form")[0].reset();
    $("#Id").val(ContactId);
    $("#ModalTitle").html("Добавить новый контакт");
    const phoneNumber = document.getElementById("PhoneNumber");
    const maskOptions = {
        mask: "+375(00)000-00-00",
        lazy: false
    };

    var mask = new IMask(phoneNumber, maskOptions);
    $("#CreateEditModal").modal();
}

function EditContactRecord(ContactId) {
    const phoneNumber = document.getElementById("PhoneNumber");
    const maskOptions = {
        mask: "+375(00)000-00-00",
        lazy: false
    };

    var mask = new IMask(phoneNumber, maskOptions);
    var url = "/Contact/GetContactById?ContactId=" + ContactId;
    $("#ModalTitle").html("Изменить контакт");
    $("#CreateEditModal").modal();
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var obj = JSON.parse(data);
            var date = new Date(obj.BirthDate).toLocaleDateString();
            $("#Id").val(obj.Id);
            $("#Name").val(obj.Name);
            $("#PhoneNumber").val(obj.PhoneNumber);
            $("#JobTitle").val(obj.JobTitle);
            $("#BirthDate").val(date.split(".").reverse().join("-"));
        }
    });
}


function SaveContactRecord() {
    const name = document.getElementById("Name");

    const phoneNumber = document.getElementById("PhoneNumber");

    const jobTitle = document.getElementById("JobTitle");

    const birthDate = document.getElementById("BirthDate");

    let result = checkForm(name, phoneNumber, jobTitle, birthDate);

    if (!result) {
        event.preventDefault();
    }
    else {
        var data = $("#SubmitForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Contact/SaveContactInDatabase",
            data: data,
            success: function () {
                window.location.href = "/Contact/index";
                $("#CreateEditModal").modal("hide");
            }
        });
    }

}

var DeleteContactRecord = function (ContactId) {
    $("#Id").val(ContactId);
    $("#DeleteConfirmation").modal("show");
}

var ConfirmDelete = function () {
    var ContactId = $("#Id").val();
    $.ajax({
        type: "POST",
        url: "/Contact/DeleteContactRecord?ContactId=" + ContactId,
        success: function () {
            $("#DeleteConfirmation").modal("hide");
            location.reload();
        }
    })
}