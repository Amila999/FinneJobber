var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width":"15%"},
            { "data": "displayOrder", "width": "15%" },
            { "data": "imageUrl", "width": "15%" },
        ]
    });
} 