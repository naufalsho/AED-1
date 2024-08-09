var $dtMenu;
var $dtGroup;

var dtMenu = '#dTableMenu';
var dtGroup = '#dTableMenuGroup';
var thisUrl = 'menu';

!function () {
    getDataMenu();
    let loadHandler = getDataMenu;

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

        ModalAction(loadHandler);
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
    });

    $('a[data-bs-toggle="tab"]').on('show.bs.tab', function () {
        let thisEle = $(this);
        let tabShow = thisEle.data('table');

        if (tabShow === 'menu') {
            getDataMenu();
            loadHandler = getDataMenu;
        } else if (tabShow === 'group') {
            getDataGroup();
            loadHandler = getDataGroup;
        }

        $(window).off('show.bs.modal').on('show.bs.modal', function () {
            $('.form-select').select2({
                dropdownParent: $("#FormInput")
            });

            ModalAction(loadHandler);
        });

        $('#app').on('click', '.btn-delete', function (e) {
            e.preventDefault();

            ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
        });
    });
}();

function getDataMenu() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/menu/getlist').done(function (response) {
        initDtMenu(response);
        panelHideLoader('#panelDiv', '#panelLoader')('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function initDtMenu(response) {
    if (!$.fn.DataTable.isDataTable(dtMenu)) {
        $dtMenu = $(dtMenu).DataTable({
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
                    data: 'menuGroupName'
                },
                {
                    data: 'order',
                    className: 'text-center'
                },
                {
                    data: 'name'
                },
                {
                    data: 'controller'
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
                            editBtn = '<a href="' + thisUrl + '/menu/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                        }

                        if (isAllowDelete()) {
                            delBtn = '<a href="' + thisUrl + '/menu/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Menu" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                        }

                        return '<div>' + editBtn + delBtn + '</div>';
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            dom: '<"row"<"col text-start"<"#dtBtnAddMenu">><"col justify-content-end"f>>' +
                '<"row"<"col-sm-12"tr>>' +
                '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
        });

        if (isAllowCreate()) {
            $('#dtBtnAddMenu').append('<a href="' + thisUrl + '/menu" class="btn btn-outline-primary btn-modal">Add Menu</a>');
        }
    } else {
        $dtMenu.clear().search('').draw();
        $dtMenu.rows.add(response).draw();
    }
}

function getDataGroup() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/group/getlist').done(function (response) {
        initDtGroup(response);
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function initDtGroup(response) {
    if (!$.fn.DataTable.isDataTable(dtGroup)) {
        $dtGroup = $(dtGroup).DataTable({
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
                    data: 'order',
                    className: 'text-center'
                },
                {
                    data: 'name'
                },
                {
                    data: 'icon'
                },
                {
                    data: 'isDirectMenu',
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
                            editBtn = '<a href="' + thisUrl + '/group/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                        }

                        if (isAllowDelete()) {
                            delBtn = '<a href="' + thisUrl + '/group/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Grup Menu" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                        }

                        return '<div>' + editBtn + delBtn + '</div>';
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            dom: '<"row"<"col text-start"<"#dtBtnAddGroup">><"col justify-content-end"f>>' +
                '<"row"<"col-sm-12"tr>>' +
                '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
        });

        if (isAllowCreate()) {
            $('#dtBtnAddGroup').append('<a href="' + thisUrl + '/group" class="btn btn-outline-primary btn-modal">Add Menu Group</a>');
        }
    } else {
        $dtGroup.clear().search('').draw();
        $dtGroup.rows.add(response).draw();
    }
}
