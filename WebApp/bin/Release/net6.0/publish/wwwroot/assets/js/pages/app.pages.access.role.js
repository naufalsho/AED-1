var $dTable;

var dTable = '#dTable';
var thisUrl = 'role';

!function () {
    getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.modal-dialog').addClass('modal-lg');

        $('.form-check-input[name*="AllowView"]').each(function () {
            let thisEl = $(this);
            let thisElName = thisEl.attr('name');
            let thisElId = thisElName.split('.')[0];

            $(thisEl).off('change').on('change', function () {
                if (!this.checked) {
                    $('.form-check-input[name*="' + thisElId + '"]').each(function () {
                        $(this).prop('checked', false);
                    });
                }
            });
        });

        $('.form-check-input[name*="RoleMenus"]').each(function () {
            let thisEl = $(this);
            let thisElName = thisEl.attr('name');

            if (thisElName.indexOf('AllowCreate') > -1 || thisElName.indexOf('AllowEdit') > -1 || thisElName.indexOf('AllowDelete') > -1) {
                let thisElId = thisElName.split('.')[0];

                $(thisEl).off('change').on('change', function () {
                    if (this.checked) {
                        $('.form-check-input[name="' + thisElId + '.AllowView"]').prop('checked', true);
                    }
                });
            }
        });

        ModalAction(getData);
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get('roles').done(function (response) {
        initDt(response);
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function initDt(response) {
    if (!$.fn.DataTable.isDataTable(dTable)) {
        $dTable = $(dTable).DataTable({
            data: response,
            columns: [
                {
                    data: 'id',
                    className: 'text-center',
                    render: function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },
                {
                    data: 'name'
                },
                {
                    data: 'createdBy'
                },
                {
                    data: 'createdDate',
                    className: 'text-center',
                    render: function (data) {
                        return FormatDateToString(data);
                    }
                },
                {
                    data: 'isActive',
                    className: 'text-center',
                    render: function (data) {
                        var check = '';
                        if (data == true) {
                            check = 'checked="checked"';
                        }

                        return '<input class="form-check-input" type="checkbox" id="checkbox1" ' + check + ' disabled />';
                    }
                },
                {
                    data: 'id',
                    className: 'text-center',
                    render: function (data, type, row) {
                        let editBtn = '';
                        let delBtn = '';

                        if (isAllowEdit()) {
                            editBtn = '<a href="' + thisUrl + '/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                        }

                        if (isAllowDelete()) {
                            delBtn = '<a href="' + thisUrl + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Role" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                        }

                        return '<div>' + editBtn + delBtn + '</div>';
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            dom: '<"row"<"col text-start"<"#dtBtnAdd">><"col justify-content-end"f>>' +
                '<"row"<"col-sm-12"tr>>' +
                '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
        });

        if (isAllowCreate()) {
            $('#dtBtnAdd').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Add Role</a>');
        }
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}
