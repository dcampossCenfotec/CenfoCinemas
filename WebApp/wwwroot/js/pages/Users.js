
function usersViewController() {
    this.ViewName = "Users";
    this.ApiEndPointName = "User";

    this.initVIew = function () {
        this.loadTable();

        $('#btnCreate').click(function () {
            var vc = new usersViewController();
            vc.Create();
        })

        $('#btnUpdate').click(function () {
            var vc = new usersViewController();
            vc.Update();
        })

        $('#btnDelete').click(function () {
            var vc = new usersViewController();
            vc.Delete();
        })
    }

    this.loadTable = function () {
        //'https://localhost:7103/api/User/RetriveAll'
        var controlActions = new ControlActions();
        var service = this.ApiEndPointName + "/RetriveAll";
        var urlService = controlActions.GetUrlApiService(service);

        var columns = [];
        columns[0] = {'data': 'id'}
        columns[1] = { 'data': 'userCode' }
        columns[2] = { 'data': 'name' }
        columns[3] = { 'data': 'email' }
        columns[4] = { 'data': 'birth' }
        columns[5] = { 'data': 'status' }

        //Invoca a dataTable para convertir tabla HTML a DataTable
        $('#tblUsers').dataTable({
            "processing": true,
            "ajax": {
                "url": urlService,
                dataSrc: ''
            },
            "columns": columns
        });

    }

    this.Create = function () {
        var userDTO = {};

        userDTO.id = 0; // Default value for new user
        userDTO.created = new Date().toISOString(); // Set current date as created date
        userDTO.updated = new Date().toISOString(); // Set current date as updated date

        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();   
        userDTO.status = $('#txtStatus').val(); 
        userDTO.birth = $('#txtBirthDate').val();   
        userDTO.password = $('#txtPassword').val(); 

        var controlActions = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        controlActions.PostToAPI(urlService, userDTO, function (response) {
            $('#tblUsers').dataTable().ajax.reload(); // Reload the table after creating a user
        })
    }

    this.Update = function () {
        var userDTO = {};

        userDTO.id = $('#txtId').val(); 
        userDTO.created = new Date().toISOString(); // Set current date as created date
        userDTO.updated = new Date().toISOString(); // Set current date as updated date

        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birth = $('#txtBirthDate').val();
        userDTO.password = $('#txtPassword').val();

        var controlActions = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        controlActions.PutToAPI(urlService, userDTO, function (response) {
            $('#tblUsers').dataTable().ajax.reload(); // Reload the table after creating a user
        })
    }

    this.Delete = function () {
        var userDTO = {};

        userDTO.id = $('#txtId').val(); // Default value for new user
        userDTO.created = new Date().toISOString(); // Set current date as created date
        userDTO.updated = new Date().toISOString(); // Set current date as updated date

        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birth = $('#txtBirthDate').val();
        userDTO.password = $('#txtPassword').val();

        var controlActions = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete";

        controlActions.DeleteToAPI(urlService, userDTO, function (response) {
            $('#tblUsers').dataTable().ajax.reload(); // Reload the table after creating a user
        })
    }
}

$(document).ready(function () {
    var vc = new usersViewController();
    vc.initVIew();
})