var $dTable;

var dTable = '#dTable';
var thisUrl = 'deploy';

var commonService = new webapp.CommonService();

!function () {
    getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.modal-dialog').addClass('modal-lg');
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

        let modalType = $('#modalType').val();

        if (modalType === 'Create') {
            $('#rowDeviceToken').hide();
            $('#BASTCompFormFileDl').hide();
            $('#BASTVendorFileDl').hide();
            $('#rowReturnForm').hide();
        } else if (modalType === 'Edit') {
            if ($('#BASTCompFileId').val()) {
                $('#BASTCompFormFileDl').show();
            }

            if ($('#BASTVendorFileId').val()) {
                $('#BASTVendorFileDl').show();
            }

            $('#NRP').attr('Disabled', 'Disabled');
            $('#EmpName').attr('Disabled', 'Disabled');
            $('#DeviceTypeId').attr('Disabled', 'Disabled');
            $('#DeviceCatId').attr('Disabled', 'Disabled');
            $('#ProductBrandId').attr('Disabled', 'Disabled');
            $('#ProductTypeId').attr('Disabled', 'Disabled');
            $('#AssetId').attr('Disabled', 'Disabled');
            $('#rowReturnForm').hide();
        } else {
            $('#NRP').attr('Disabled', 'Disabled');
            $('#EmpName').attr('Disabled', 'Disabled');
            $('#DeviceTypeId').attr('Disabled', 'Disabled');
            $('#DeviceCatId').attr('Disabled', 'Disabled');
            $('#ProductBrandId').attr('Disabled', 'Disabled');
            $('#ProductTypeId').attr('Disabled', 'Disabled');
            $('#AssetId').attr('Disabled', 'Disabled');
            $('#BASTCompDate').attr('Disabled', 'Disabled');
            $('#BASTVendorDate').attr('Disabled', 'Disabled');
            $('#BASTCompFormFile').hide();
            $('#BASTVendorFormFile').hide();
            $('#rowReturnForm').show();
        }

        BuildDatePicker('#BASTCompDate');
        BuildDatePicker('#BASTVendorDate');

        $('#NRP').off('change').on('change', function (e) {
            commonService.getEmployeeByNrp($('#NRP').val()).done(function (response) {
                $('#EmployeeId').val(response.id);
                $('#EmpName').val(response.name);
            });
        });

        $('#DeviceTypeId').on('change', function (e) {
            BuildMasterDataDDl('#DeviceCatId', masterDataType.DeviceCat, $(this).val());

            getAvailableAsset();
        });

        $('#DeviceCatId').on('change', function (e) {
            getAvailableAsset();
        });

        $('#ProductBrandId').on('change', function (e) {
            BuildMasterDataDDl('#ProductTypeId', masterDataType.ProductType, $(this).val());

            getAvailableAsset();
        });

        $('#ProductTypeId').on('change', function (e) {
            getAvailableAsset();
        });

        ModalActionWithFile(getData);
    });
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get('deployed').done(function (response) {
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
                    data: 'assetStatus',
                    className: 'text-nowrap'
                },
                {
                    data: 'nrp',
                    className: 'text-nowrap'
                },
                {
                    data: 'empName',
                    className: 'text-nowrap'
                },
                {
                    data: 'deviceTypeName',
                    className: 'text-nowrap'
                },
                {
                    data: 'deviceCatName',
                    className: 'text-nowrap'
                },
                {
                    data: 'productBrandName',
                    className: 'text-nowrap'
                },
                {
                    data: 'productTypeName',
                    className: 'text-nowrap'
                },
                {
                    data: 'serialNumber',
                    className: 'text-nowrap'
                },
                {
                    data: 'sapAssetNumber',
                    className: 'text-nowrap'
                },
                {
                    data: 'notes',
                    className: 'text-nowrap'
                },
                {
                    data: 'hostName',
                    className: 'text-nowrap'
                },
                {
                    data: 'bastCompDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        }

                        return '';
                    }
                },
                {
                    data: 'bastCompFileId',
                    className: 'text-center text-nowrap',
                    render: function (data, type, row) {
                        if (data) {
                            return '<a href="/download/' + data + '">BAST Company</a>';
                        }

                        return '';
                    }
                },
                {
                    data: 'bastVendorDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        }

                        return '';
                    }
                },
                {
                    data: 'bastVendorFileId',
                    className: 'text-center text-nowrap',
                    render: function (data, type, row) {
                        if (data) {
                            return '<a href="/download/' + data + '">BAST Vendor</a>';
                        }

                        return '';
                    }
                },
                {
                    data: 'deployDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        return FormatDateToString(data);
                    }
                },
                {
                    data: 'deployBy',
                    className: 'text-nowrap'
                },
                {
                    data: 'id',
                    className: 'text-center text-nowrap',
                    render: function (data, type, row) {
                        let editBtn = '';
                        let delBtn = '';
                        let iotBtn = '';

                        if (isAllowEdit()) {
                            editBtn = '<a href="' + thisUrl + '/' + data + '" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>';
                        }

                        if (isAllowDelete()) {
                            delBtn = '<a href="undeploy/' + data + '" type="button" class="btn btn-outline-info btn-sm me-2 btn-modal">Undeploy</a>';
                        }

                        if (!row.ioTDeviceToken) {
                            iotBtn = '<a href="sendtoiot/' + data + '" type="button" class="btn btn-outline-success btn-sm send-iot">Send To IoT</a>';
                        }

                        return '<div>' + editBtn + delBtn + iotBtn + '</div>';
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            scrollX: true,
            dom: '<"row"<"col text-start"<"#dtBtn"B>><"col justify-content-end"f>>' +
                '<"row"<"col-sm-12"tr>>' +
                '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            buttons: [
                {
                    extend: 'excel',
                    className: 'd-none',
                    exportOptions: {
                        columns: ':not(:last-child)'
                    }
                }
            ],
            drawCallback: function () {
                $('.send-iot').off().on('click', function (e) {
                    e.preventDefault();

                    $.post($(this).attr('href')).done(function (response) {
                        ShowAlert(alertStatus.Success, 'Data send to IoT!');
                    }).fail(function (response) {
                        HandleHttpRequestFail(response);
                    });
                });
            }
        });

        if (isAllowCreate()) {
            $('#dtBtn').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Add New Deployment</a>');
        }

        $('#dtBtn')
            .append(
                $('<span class="btn btn-secondary ms-1">Excel</span>')
                    .on('click', function () {
                        $('.buttons-excel').click();
                    }));
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}

function getAvailableAsset() {
    if ($('#EmployeeId').val() == 0) {
        ShowAlert(alertStatus.Error, 'Please fill NRP first!', null, null);
    } else if ($('#EmployeeId').val() != 0 && $('#DeviceTypeId').val() && $('#DeviceCatId').val() && $('#ProductBrandId').val() && $('#ProductTypeId').val()) {
        $('#AssetId').empty().append('<option selected="selected" value="">Please select one</option>');

        let data = {
            employeeId: $('#EmployeeId').val(),
            deviceTypeId: $('#DeviceTypeId').val(),
            deviceCatId: $('#DeviceCatId').val(),
            productBrandId: $('#ProductBrandId').val(),
            productTypeId: $('#ProductTypeId').val()
        }

        $.getJSON('/get/asset/available', data).done(function (response) {
            $.each(response, function (index, element) {
                $('#AssetId').append('<option value="' + element.value + '">' + element.text + '</option>');
            });
        }).fail(function (response) {
            HandleHttpRequestFail(response);
        });
    }
}