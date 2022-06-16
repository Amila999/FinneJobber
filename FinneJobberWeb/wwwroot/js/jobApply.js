var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/JobSeeker/Home/GetAll"
        },
        "columns": [
            {"data": "category",
                "render": function (data) {
                    return `
                        <div class="card">
                            <div class="col-12">
                                <div class="row">
                                    <img height="25%" width="25%" src="${data.imageUrl}" class="card-img-top rounded"/>
                                </div>
                                <div class="card-body" align="center">
                                    <div class="pl-1">
                                        <p class="card-title h4 text-primary">${data.name}</p>
                                    </div>
                                </div>
                            </div>
                        </div>`
                }, "width": "10%"
            },
            { "data": "description", "width": "15%" },
            { "data": "jobLocation", "width": "15%" },
            { "data": "budget", "width": "15%" },
            { "data": "date", "width": "15%" },
            { "data": "time", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Job/Upsert?id=${data}"
                                class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a onClick=Delete('/Admin/Job/Delete?id=${data}')
                                class="btn btn-danger mx-2"><i class="bi bi-trash3-fill"></i> Delete</a>
                        </div>

                        `
                },
                "width": "10%"
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