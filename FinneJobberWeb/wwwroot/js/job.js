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
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Job/Upsert?id=${data}"
                                class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a href="/Admin/Job/Delete?id=${data}
                                class="btn btn-danger mx-2"><i class="bi bi-trash3-fill"></i> Delete</a>
                        </div>

                        `
                },
                "width": "15%"
            },
        ]
    });
} 