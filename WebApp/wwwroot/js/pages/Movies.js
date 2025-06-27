
function moviesViewController() {
    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    this.initVIew = function () {
        this.loadTable();
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
}

$(document).ready(function () {
    var vc = new moviesViewController();
    vc.initVIew();
})