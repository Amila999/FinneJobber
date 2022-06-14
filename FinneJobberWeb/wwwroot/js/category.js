﻿var dataTable;

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
            {
                "data": "imageUrl",
                "render": function (data) {
                    return '<img height="75%" width="75%" src="' + data + '">'
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Category/Upsert?id=${data}"
                                class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a onClick=Delete('/Admin/Category/Delete?id=${data}')
                                class="btn btn-danger mx-2"><i class="bi bi-trash3-fill"></i> Delete</a>
                        </div>

                        `
                },
                "width": "15%"
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}