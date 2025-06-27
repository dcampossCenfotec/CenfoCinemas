
function usersViewController() {
    this.ViewName = "Users";
    this.ApiEndPointName = "User";

    this.initVIew = function () {
        this.loadTable();
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

        /*
          {
    "userCode": "string",
    "name": "Daniel",
    "email": "dcamposs@ucenfotec.ac.cr",
    "password": "string",
    "birth": "2000-06-21T15:16:24.21",
    "status": "string",
    "id": 2,
    "created": "2025-06-21T15:16:46.517",
    "updated": "0001-01-01T00:00:00"
  }
        */

    }
}

$(document).ready(function () {
    var vc = new usersViewController();
    vc.initVIew();
})