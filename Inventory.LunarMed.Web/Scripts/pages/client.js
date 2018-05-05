
$('#test').on('click', function () {
    BootstrapDialog.show({
        title: '',
        type: BootstrapDialog.TYPE_SUCCESS,
        message: 'Client successfully saved!'
    });
});

// Save New Client OnClick Event
$('#btnSaveClient').on('click', function (e) {
    e.preventDefault();

    addClient();
});

//Add Client Function
function addClient() {

    var clientObj = {
        ClientId: 0,
        Name: $('#name').val(),
        Address: $('#address').val(),
        ContactNumber: $('#contactNumber').val()
    };
    $.ajax({
        url: "Client/Create",
        data: JSON.stringify(clientObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}  