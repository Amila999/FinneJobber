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
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Category/Edit?id=${data}"
                                class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a href="/Admin/Category/Delete?id=${data}
                                class="btn btn-danger mx-2"><i class="bi bi-trash3-fill"></i> Delete</a>
                        </div>

                        `
                },
                "width": "15%"
            },
        ]
    });
} 