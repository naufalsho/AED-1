var $dTable;

var dTable = '#dTable';
var thisUrl = 'asset';

!function () {
    getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.modal-dialog').addClass('modal-lg');
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

        $('#rowBuyBackFile').hide();
        $('#rowVendorReturnFile').hide();

        BuildDatePicker('#PODate');
        BuildDatePicker('#BASTVendorDate');
        BuildDatePicker('#PeriodFrom');
        BuildDatePicker('#PeriodTo');
        BuildDatePicker('#StartBillingDate');
        BuildDatePicker('#BuyBackDate');
        BuildDatePicker('#BASTVendorReturnDate');

        if ($('#Id').val() > 0) {
            if ($('#PeriodFrom').val() && $('#PeriodTo').val()) {
                $('#TotalPeriod').val(GetIntervalMonth($('#PeriodFrom').val(), $('#PeriodTo').val()));
            }

            if ($('#IsAsset').attr('checked')) {
                $('#rowRentPrice').hide();
                $('#TotalPrice').removeAttr('readonly');
            } else {
                $('#rowRentPrice').show();
                $('#TotalPrice').attr('readonly', 'true');
            }

            if ($('#RentPrice').val()) {
                $('#RentPrice').val(FormatToCurrency($('#RentPrice').val()));
            }

            if ($('#TotalPrice').val()) {
                $('#TotalPrice').val(FormatToCurrency($('#TotalPrice').val()));
            }

            if ($('#AssetStatus').val() == assetStatus.Purchased) {
                $('#rowBuyBackFile').show();
            }

            if ($('#AssetStatus').val() == assetStatus.BackToVendor) {
                $('#rowVendorReturnFile').show();
            }

            $('#AssetStatus').on('change', function (e) {
                let statusVal = $('#AssetStatus').val();

                if (statusVal == assetStatus.Purchased) {
                    $('#iClearBASTVendorReturnDate').trigger('click');
                    $('#BASTVendorReturnFormFile').val('');

                    $('#BuyBackFileDl').hide();

                    $('#rowBuyBackFile').show();
                    $('#rowVendorReturnFile').hide();
                } else if (statusVal == assetStatus.BackToVendor) {
                    $('#iClearBuyBackDate').trigger('click');
                    $('#BuyBackFormFile').val('');

                    $('#BASTVendorReturnFileDl').hide();

                    $('#rowBuyBackFile').hide();
                    $('#rowVendorReturnFile').show();
                } else {
                    $('#rowBuyBackFile').hide();
                    $('#rowVendorReturnFile').hide();
                }
            });

            if ($('#BuyBackFileId').val()) {
                $('#BuyBackFileDl').hide();
            }

            $('#BuyBackFormFile').off('change').on('change', function (e) {
                $('#BuyBackFileDl').hide();
            });

            if ($('#BASTVendorReturnFileId').val()) {
                $('#BASTVendorReturnFileDl').hide();
            }

            $('#BASTVendorReturnFormFile').off('change').on('change', function (e) {
                $('#BASTVendorReturnFileDl').hide();
            });
        } else {
            $('#BASTVendorFileDl').hide();
        }

        $('#MacAddress').keyup(function (e) {
            var r = /([a-f0-9]{2})/i;
            var str = e.target.value.replace(/[^a-f0-9:]/ig, "");
            if (e.keyCode != 8 && r.test(str.slice(-2))) {
                str = str.concat(':')
            }
            e.target.value = str.slice(0, 17);
        });

        $('#DeviceTypeId').on('change', function (e) {
            BuildMasterDataDDl('#DeviceCatId', masterDataType.DeviceCat, $(this).val());
        });

        $('#ProductBrandId').on('change', function (e) {
            BuildMasterDataDDl('#ProductTypeId', masterDataType.ProductType, $(this).val());
        });

        $('#BASTVendorFormFile').off('change').on('change', function (e) {
            $('#BASTVendorFileDl').hide();
        });

        $('#IsAsset').off('change').on('change', function () {
            $('#RentPrice').val('');
            $('#TotalPrice').val('');

            if (this.checked) {
                $('#rowRentPrice').hide();
                $('#TotalPrice').removeAttr('readonly');
            } else {
                $('#rowRentPrice').show();
                $('#TotalPrice').attr('readonly', 'true');
            }
        });

        $('#PeriodFrom').off('change').on('change', function () {
            if ($('#PeriodFrom').val() && $('#PeriodTo').val()) {
                $('#TotalPeriod').val(GetIntervalMonth($('#PeriodFrom').val(), $('#PeriodTo').val()));

                if (!$('#IsAsset').checked) {
                    if ($('#RentPrice').val()) {
                        $('#TotalPrice').val(GetTotalPrice($('#RentPrice').val().replace(/\./g, ''), $('#TotalPeriod').val()));
                    }
                }
            }
        });

        $('#PeriodTo').off('change').on('change', function () {
            getStatus();

            if ($('#PeriodFrom').val() && $('#PeriodTo').val()) {
                $('#TotalPeriod').val(GetIntervalMonth($('#PeriodFrom').val(), $('#PeriodTo').val()));

                if (!$('#IsAsset').checked) {
                    if ($('#RentPrice').val()) {
                        $('#TotalPrice').val(GetTotalPrice($('#RentPrice').val().replace(/\./g, ''), $('#TotalPeriod').val()));
                    }
                }
            }
        });

        $('#RentPrice').off('change').on('change', function () {
            if ($('#TotalPeriod').val() && !$('#IsAsset').checked) {
                $('#TotalPrice').val(GetTotalPrice($('#RentPrice').val().replace(/\./g, ''), $('#TotalPeriod').val()));
            }
        });

        ModalActionWithFile(getData);
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get('assets').done(function (response) {
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
                    data: 'assetCompanyName',
                    className: 'text-nowrap'
                },
                {
                    data: 'assetBranchName',
                    className: 'text-nowrap'
                },
                {
                    data: 'vendorName',
                    className: 'text-nowrap'
                },
                {
                    data: 'poDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        return FormatDateToString(data);
                    }
                },
                {
                    data: 'poNumber',
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
                    data: 'macAddress',
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
                        let ret = '';

                        if (data) {
                            ret = '<a href="/download/' + data + '">BAST Vendor File</a>';
                        }

                        return ret;
                    }
                },
                {
                    data: 'periodFrom',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        }

                        return '';
                    }
                },
                {
                    data: 'periodTo',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        }

                        return '';
                    }
                },
                {
                    data: 'totalPeriod',
                    className: 'text-nowrap',
                    render: function (data, type, row) {
                        if (row.periodFrom && row.periodTo) {
                            return GetIntervalMonth(FormatDateToString(row.periodFrom), FormatDateToString(row.periodTo)).toString() + ' Months';
                        }

                        return '';
                    }
                },
                {
                    data: 'isAsset',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        var check = '';
                        if (data == true) {
                            check = 'checked="checked"';
                        }

                        return '<input class="form-check-input" type="checkbox" id="checkbox1" ' + check + ' disabled />';
                    }
                },
                {
                    data: 'rentPrice',
                    className: 'text-nowrap',
                    render: function (data, type, row) {
                        let ret = '';

                        if (row.rentPrice) {
                            ret = 'Rp. ' + row.rentPrice.toString().replace(/\D/g, '').replace(/\B(?=(\d{3})+(?!\d))/g, '.');
                        }

                        return ret;
                    }
                },
                {
                    data: 'totalPrice',
                    className: 'text-nowrap',
                    render: function (data, type, row) {
                        let ret = '';

                        if (row.rentPrice) {
                            ret = 'Rp. ' + GetTotalPrice(row.rentPrice, GetIntervalMonth(FormatDateToString(row.periodFrom), FormatDateToString(row.periodTo)));
                        } else if (row.totalPrice) {
                            ret = 'Rp. ' + row.totalPrice.toString().replace(/\D/g, '').replace(/\B(?=(\d{3})+(?!\d))/g, '.');
                        }

                        return ret;
                    }
                },
                {
                    data: 'startBillingDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        }

                        return '';
                    }
                },
                {
                    data: 'isEndPeriod',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        var check = '';
                        if (data == true) {
                            check = 'checked="checked"';
                        }

                        return '<input class="form-check-input" type="checkbox" id="checkbox1" ' + check + ' disabled />';
                    }
                },
                {
                    data: 'buyBackDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        }

                        return '';
                    }
                },
                {
                    data: 'buyBackFileId',
                    className: 'text-center text-nowrap',
                    render: function (data, type, row) {
                        let ret = '';

                        if (data) {
                            ret = '<a href="/download/' + data + '">BuyBack File</a>';
                        }

                        return ret;
                    }
                },
                {
                    data: 'bastVendorReturnDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        if (data) {
                            return FormatDateToString(data);
                        }

                        return '';
                    }
                },
                {
                    data: 'bastVendorReturnFileId',
                    className: 'text-center text-nowrap',
                    render: function (data, type, row) {
                        let ret = '';

                        if (data) {
                            ret = '<a href="/download/' + data + '">Vendor Return File</a>';
                        }

                        return ret;
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
                            delBtn = '<a href="' + thisUrl + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Asset" data-detail="detail <b>' + row.productBrandName + ' ' + row.productTypeName + '</b> dengan serial number  <b>' + row.serialNumber + '</b>">Delete</a>';
                        }

                        return '<div>' + editBtn + delBtn + '</div>';
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
            ]
        });

        if (isAllowCreate()) {
            $('#dtBtn').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Add Asset</a>');
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

function getStatus() {
    if ($('#Id').val() > 0) {
        let data = {
            assetId: $('#Id').val(),
            isEndPeriod: DateTime.fromFormat($('#PeriodTo').val(), 'dd/MM/yyyy') < DateTime.now()
        }

        $('#AssetStatus').empty().append('<option selected="selected" value="">Please select one</option>');

        $.getJSON('/get/asset/status', data).done(function (response) {
            $.each(response, function (index, element) {
                $('#AssetStatus').append('<option value="' + element.value + '">' + element.text + '</option>');
            });
        }).fail(function (response) {
            HandleHttpRequestFail(response);
        });
    }
}

function GetIntervalMonth(periodStart, periodEnd) {
    let periodFrom = DateTime.fromString(periodStart, 'dd/LL/yyyy');
    let periodTo = DateTime.fromString(periodEnd, 'dd/LL/yyyy');

    var itv = luxon.Interval.fromDateTimes(periodFrom, periodTo);

    return Math.ceil(itv.length('months'));
}

function GetTotalPrice(rentPrice, totalPeriod) {
    return (rentPrice * totalPeriod).toString()
        .replace(/\D/g, '')
        .replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        ;
}
