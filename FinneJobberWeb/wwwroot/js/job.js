var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Job/GetAll"
        },
        "columns": [
            { "data": "category.name", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "jobLocation", "width": "15%" },
            { "data": "date", "width": "15%" },
            { "data": "time", "width": "15%" },
            { "data": "budget", "width": "15%" },
        ]
    });
} 