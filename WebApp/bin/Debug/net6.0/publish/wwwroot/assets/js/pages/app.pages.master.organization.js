var dtGeneral = '#dTableGeneral';
var dtParent = '#dTableParent';

var thisUrl = 'organization';

!function () {
    getDataCompany();

    let loadHandler = getDataCompany;
    let lblParent = '';

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

        if (tabShow === 'company') {
            getDataCompany();
            loadHandler = getDataCompany;
        } else if (tabShow === 'branch') {
            $('#thParentName').text('Company')
            lblParent = 'Company';
            getDataBranch();
            loadHandler = getDataBranch;
        } else if (tabShow === 'division') {
            getDataDivision();
            loadHandler = getDataDivision;
        } else if (tabShow === 'department') {
            $('#thParentName').text('Division')
            lblParent = 'Division';
            getDataDepartment();
            loadHandler = getDataDepartment;
        } else if (tabShow === 'jobgroup') {
            $('#thParentName').text('Company')
            lblParent = 'Company';
            getDataJobGroup();
            loadHandler = getDataJobGroup;
        } else if (tabShow === 'jobtitle') {
            $('#thParentName').text('Job Group')
            lblParent = 'Job Group';
            getDataJobtitle();
            loadHandler = getDataJobtitle;
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

function getDataCompany() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/companies').done(function (response) {
        initDtParent(response, 'company');
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataBranch() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/branches').done(function (response) {
        initDtGeneral(response, 'branch');
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataDivision() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/divisions').done(function (response) {
        initDtParent(response, 'division');
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataDepartment() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/departments').done(function (response) {
        initDtGeneral(response, 'department');
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataJobGroup() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/jobgroups').done(function (response) {
        initDtGeneral(response, 'jobgroup');
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataJobtitle() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(thisUrl + '/jobtitles').done(function (response) {
        initDtGeneral(response, 'jobtitle');
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function initDtParent(response, type) {
    let mdType = '';
    let mdText = '';
    let mdBtn = '';

    switch (type) {
        case 'company':
            mdType = 'company';
            mdText = 'Company'
            mdBtn = '#dtBtnAddCompany';
            break;
        case 'division':
            mdType = 'division';
            mdText = 'Division'
            mdBtn = '#dtBtnAddDivision';
            break;
        default:
            mdType = '';
            mdText = ''
            mdBtn = '';
    }

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
                        editBtn = '<a href="' + thisUrl + '/' + mdType + '/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                    }

                    if (isAllowDelete()) {
                        delBtn = '<a href="' + thisUrl + '/' + mdType + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="' + mdText + '" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                    }

                    return '<div>' + editBtn + delBtn + '</div>';
                }
            }
        ],
        responsive: true,
        autoWidth: false,
        dom: '<"row"<"col text-start"<"' + mdBtn + '">><"col justify-content-end"f>>' +
            '<"row"<"col-sm-12"tr>>' +
            '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
    });

    if (isAllowCreate()) {
        $(mdBtn).append('<a href="' + thisUrl + '/' + mdType + '/create" class="btn btn-outline-primary btn-modal">Add ' + mdText + '</a>');
    }
}

function initDtGeneral(response, type) {
    let mdType = '';
    let mdText = '';
    let mdBtn = '';

    switch (type) {
        case 'branch':
            mdType = 'branch';
            mdText = 'Branch'
            mdBtn = '#dtBtnAddBranch';
            break;
        case 'department':
            mdType = 'department';
            mdText = 'Department'
            mdBtn = '#dtBtnAddDepartment';
            break;
        case 'jobgroup':
            mdType = 'jobgroup';
            mdText = 'Job Group'
            mdBtn = '#dtBtnAddJobGroup';
            break;
        case 'jobtitle':
            mdType = 'jobtitle';
            mdText = 'Job Title'
            mdBtn = '#dtBtnAddJobTitle';
            break;
        default:
            mdType = '';
            mdText = ''
            mdBtn = '';
    }

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
                        editBtn = '<a href="' + thisUrl + '/' + mdType + '/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                    }

                    if (isAllowDelete()) {
                        delBtn = '<a href="' + thisUrl + '/' + mdType + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="' + mdText + '" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                    }

                    return '<div>' + editBtn + delBtn + '</div>';
                }
            }
        ],
        responsive: true,
        autoWidth: false,
        dom: '<"row"<"col text-start"<"' + mdBtn + '">><"col justify-content-end"f>>' +
            '<"row"<"col-sm-12"tr>>' +
            '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
    });

    if (isAllowCreate()) {
        $(mdBtn).append('<a href="' + thisUrl + '/' + mdType + '/create" class="btn btn-outline-primary btn-modal">Add ' + mdText + '</a>');
    }
}
