var $dTable;

var dTable = '#dTable';
var thisUrl = 'employee';

!function () {
    getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.modal-dialog').addClass('modal-lg');
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

        BuildDatePicker('#OutDate');

        if ($('#Id').val() > 0) {
            let statusVal = $('#Status').val();

            if (statusVal == empStatus.Resign) {
                $('#rowOutDate').show();
                $('#rowActive').hide();
                $('#IsActive').prop('checked', false);
            } else {
                $('#rowOutDate').hide();
                $('#rowActive').show();
                $('#IsActive').prop('checked', true);
                $('#OutDate').val('');
            }
        }

        $('#CompanyId').on('change', function (e) {
            BuildMasterDataDDl('#BranchId', masterDataType.Branch, $(this).val());
            BuildMasterDataDDl('#JobGroupId', masterDataType.JobGroup, $(this).val());
        });

        $('#DivisionId').on('change', function (e) {
            BuildMasterDataDDl('#DepartmentId', masterDataType.Department, $(this).val());
        });

        $('#JobGroupId').on('change', function (e) {
            BuildMasterDataDDl('#JobTitleId', masterDataType.JobTitle, $(this).val());
        });

        $('#Status').on('change', function (e) {
            let thisVal = $(this).val();

            $('#OutDate').val('');

            if (thisVal == empStatus.Resign) {
                $('#rowOutDate').show();
                $('#rowActive').hide();
                $('#IsActive').prop('checked', false);
            } else {
                $('#rowOutDate').hide();
                $('#rowActive').show();
                $('#IsActive').prop('checked', true);
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

    $.get('employees').done(function (response) {
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
                    data: 'nrp',
                    className: 'text-center text-nowrap'
                },
                {
                    data: 'name',
                    className: 'text-nowrap'
                },
                {
                    data: 'companyName',
                    className: 'text-nowrap'
                },
                {
                    data: 'branchName',
                    className: 'text-nowrap'
                },
                {
                    data: 'divisionName',
                    className: 'text-nowrap'
                },
                {
                    data: 'departmentName',
                    className: 'text-nowrap'
                },
                {
                    data: 'jobGroupName',
                    className: 'text-nowrap'
                },
                {
                    data: 'jobTitleName',
                    className: 'text-nowrap'
                },
                {
                    data: 'status'
                },
                {
                    data: 'outDate',
                    className: 'text-center',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        } else {
                            return '';
                        }
                    }
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
                    className: 'text-center text-nowrap',
                    render: function (data, type, row) {
                        let editBtn = '';
                        let delBtn = '';

                        if (isAllowEdit()) {
                            editBtn = '<a href="' + thisUrl + '/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                        }

                        if (isAllowDelete()) {
                            delBtn = '<a href="' + thisUrl + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Employee" data-detail="name <b>' + row.name + '</b>">Delete</a>';
                        }

                        return '<div>' + editBtn + delBtn + '</div>';
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            scrollX: true,
            dom: '<"row"<"col text-start"<"#dtBtnAdd">><"col justify-content-end"f>>' +
                '<"row"<"col-sm-12"tr>>' +
                '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
        });

        if (isAllowCreate()) {
            $('#dtBtnAdd').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Add Employee</a>');
        }
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}
