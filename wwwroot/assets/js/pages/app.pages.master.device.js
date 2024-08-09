var dtGeneral = '#dTableGeneral';
var dtParent = '#dTableParent';

var thisUrl = 'device';

!function () {
    getDataGeneral();

    let loadHandler = getDataGeneral;
    let lblParent = 'Type';

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

        $('#lblParent').text(lblParent);
        ModalAction(loadHandler);
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
    });

    $('a[data-bs-toggle="tab"]').on('show.bs.tab', function () {
        let thisEle = $(this);
        let tabShow = thisEle.data('table');

        if (tabShow === 'general') {
            getDataGeneral();
            loadHandler = getDataGeneral;
        } else if (tabShow === 'parent') {
            getDataParent();
            loadHandler = getDataParent;
        }

        $(window).off('show.bs.modal').on('show.bs.modal', function () {
            $('.form-select').select2({
                dropdownParent: $("#FormInput")
            });

            $('#lblParent').text(lblParent);
            ModalAction(loadHandler);
        });

        $('#app').on('click', '.btn-delete', function (e) {
            e.preventDefault();

            ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
        });
    });
}();

function getDataGeneral() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/categories').done(function (response) {
        initDtGeneral(response);
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataParent() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/types').done(function (response) {
        initDtParent(response);
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function initDtGeneral(response) {
    $(dtGeneral).DataTable({
        destroy: true,
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
                data: 'code'
            },
            {
                data: 'name'
            },
            {
                data: 'parentName'
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
                        editBtn = '<a href="' + thisUrl + '/category/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                    }

                    if (isAllowDelete()) {
                        delBtn = '<a href="' + thisUrl + '/category/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Device Category" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                    }

                    return '<div>' + editBtn + delBtn + '</div>';
                }
            }
        ],
        responsive: true,
        autoWidth: false,
        dom: '<"row"<"col text-start"<"#dtBtnAddGeneral">><"col justify-content-end"f>>' +
            '<"row"<"col-sm-12"tr>>' +
            '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
    });

    if (isAllowCreate()) {
        $('#dtBtnAddGeneral').append('<a href="' + thisUrl + '/category/create" class="btn btn-outline-primary btn-modal">Add Device Category</a>');
    }
}

function initDtParent(response) {
    $(dtParent).DataTable({
        destroy: true,
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
                data: 'code'
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
                        editBtn = '<a href="' + thisUrl + '/type/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                    }

                    if (isAllowDelete()) {
                        delBtn = '<a href="' + thisUrl + '/type/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Device Type" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                    }

                    return '<div>' + editBtn + delBtn + '</div>';
                }
            }
        ],
        responsive: true,
        autoWidth: false,
        dom: '<"row"<"col text-start"<"#dtBtnAddParent">><"col justify-content-end"f>>' +
            '<"row"<"col-sm-12"tr>>' +
            '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
    });

    if (isAllowCreate()) {
        $('#dtBtnAddParent').append('<a href="' + thisUrl + '/type/create" class="btn btn-outline-primary btn-modal">Add Device Type</a>');
    }
}
