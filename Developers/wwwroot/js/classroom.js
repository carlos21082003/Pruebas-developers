let datatable;
$(function () {
    loadDataTable();
});
function loadDataTable() {
    datatable = $('#table_classrooms').DataTable({
        language: {
            lengthMenu: "Mostrar _MENU_ registros por página",
            zeroRecords: "No hay registros disponibles.",
            info: "Pág. _PAGE_ de _PAGES_ - Mostrando del _START_ al _END_ de _TOTAL_ registros",
            infoEmpty: "No hay registros disponibles.",
            infoFiltered: "(filtrado de un total _MAX_ registros)",
            loadingRecords: "Cargando en curso...",
            emptyTable: "No hay registros disponibles.",
            search: "Buscar",
            paginate: {
                first: "Primero",
                last: "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        ajax: {
            url: "/Classrooms/ListarTodos"
        },
        columns: [
            { data: "classroomId", width: "10%", className: "text-center", orderable: false },            
            { data: "course.name" },
            {
                data: "trainer",
                render: function (data) { return data.firstName + ' ' + data.lastName }
            },
            {
                data: "sessionDate",
                render: function (data) { return new Date(data).toLocaleDateString(); }
            },
            { data: "hours", className: "text-center" },
            {
                data: "status",
                render: function (data) {
                    if (data == true) {
                        return `<span class="text-success">ACTIVO</span>`;
                    } else {
                        return `<span class="text-danger">INACTIVO</span>`;
                    }
                }, width: "10%", orderable: false, searchable: false, className: "text-center"
            },
            {
                data: "classroomId",
                render: function (data) {
                    return `
                        <a href="/Classrooms/Edit/${data}" class="btn btn-sm btn-success text-white" style="cursor:pointer;">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a href="/Classrooms/Details/${data}" class="btn btn-sm btn-info text-white" style="cursor:pointer;">
                            <i class="bi bi-list-check"></i>
                        </a>
                        <a onclick=Delete("/Classrooms/Delete/${data}") class="btn btn-sm btn-danger text-white" style="cursor:pointer;">
                            <i class="bi bi-trash3-fill"></i>
                        </a>
                    `;
                }, width: "10%", orderable: false, searchable: false, className: "text-center"
            },
        ]
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de eliminar este curso?",
        text: "Este registro no se podrá recuperar",
        html: true,
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url, // Courses/Delete/5
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload(); // Recargar el datatable
                    }
                    else {
                        toastr.error(data.message); // Notificaciones
                    }
                }
            });
        }
    });
}