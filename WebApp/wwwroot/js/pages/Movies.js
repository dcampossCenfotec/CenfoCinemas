
function moviesViewController() {
    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    this.initVIew = function () {
        this.loadTable();

        $('#btnCreate').click(function () {
            console.log('create clicked')
            var mc = new moviesViewController();
            mc.Create();
        })

        $('#btnUpdate').click(function () {
            var mc = new moviesViewController();
            mc.Update();
        })

        $('#btnDelete').click(function () {
            var mc = new moviesViewController();
            mc.Delete();
        })
    }

    this.loadTable = function () {

        var controlActions = new ControlActions();
        var service = this.ApiEndPointName + "/RetriveAll";
        var urlService = controlActions.GetUrlApiService(service);

        var columns = [];
        columns[0] = {'data': 'id'}
        columns[1] = { 'data': 'title' }
        columns[2] = { 'data': 'description' }
        columns[3] = { 'data': 'genre' }
        columns[4] = { 'data': 'director' }
        columns[5] = { 'data': 'releaseDate' }

        //Invoca a dataTable para convertir tabla HTML a DataTable
        $('#tblMovies').dataTable({
            "processing": true,
            "ajax": {
                "url": urlService,
                dataSrc: ''
            },
            "columns": columns
        });

    }


    this.Create = function () {
        var movieDTO = {};

        movieDTO.id = 0; // Default value for new user
        movieDTO.created = new Date().toISOString(); // Set current date as created date
        movieDTO.updated = new Date().toISOString(); // Set current date as updated date

        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtReleaseDate').val();

        var controlActions = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        controlActions.PostToAPI(urlService, movieDTO, function (response) {
            $('#tblMovies').dataTable().ajax.reload(); // Reload the table after creating a user
        })
    }

    this.Update = function () {
        var movieDTO = {};

        movieDTO.id = $('#txtId').val();
        movieDTO.created = new Date().toISOString(); // Set current date as created date
        movieDTO.updated = new Date().toISOString(); // Set current date as updated date

        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtReleaseDate').val();

        var controlActions = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        controlActions.PutToAPI(urlService, movieDTO, function (response) {
            $('#tblMovies').dataTable().ajax.reload(); // Reload the table after creating a user
        })
    }

    this.Delete = function () {
        var movieDTO = {};

        movieDTO.id = $('#txtId').val(); // Default value for new user
        movieDTO.created = new Date().toISOString(); // Set current date as created date
        movieDTO.updated = new Date().toISOString(); // Set current date as updated date

        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtReleaseDate').val();

        var controlActions = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete";

        controlActions.DeleteToAPI(urlService, movieDTO, function (response) {
            $('#tblMovies').dataTable().ajax.reload(); // Reload the table after creating a user
        })
    }
}

$(document).ready(function () {
    var vc = new moviesViewController();
    vc.initVIew();
})