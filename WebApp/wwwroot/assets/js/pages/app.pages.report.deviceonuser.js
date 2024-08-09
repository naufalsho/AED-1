var $dTable;

var dTable = '#dTable';

!function () {
    BuildMasterDataFilter('#CompFilter', masterDataType.Company);
    BuildMasterDataFilter('#BranchFilter', masterDataType.Branch);

    $('#CompFilter').on('change', function () {
        if ($('#CompFilter').val() === '0') {
            BuildMasterDataFilter('#BranchFilter', masterDataType.Branch);
        } else {
            BuildMasterDataFilterByParent('#BranchFilter', masterDataType.Branch, $('#CompFilter').val());
        }

        getData();
    });

    $('#BranchFilter').on('change', function () {
        getData();
    });

    getData();
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    let data = {
        'companyId': $('#CompFilter').val(),
        'branchId': $('#BranchFilter').val()
    };

    $.post('deviceuser/list', data).done(function (response) {
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
                    data: 'nrp',
                    className: 'text-center text-nowrap'
                },
                {
                    data: 'empName',
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
                    data: 'empStatus',
                    className: 'text-nowrap'
                },
                {
                    data: 'bastCompDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        let ret = '';

                        if (data) {
                            return FormatDateToString(data);
                        }

                        return ret;
                    }
                },
                {
                    data: 'periodTo',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        let ret = '';

                        if (data) {
                            return FormatDateToString(data);
                        }

                        return ret;
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            scrollX: true,
            dom: '<"row"<"col-sm-5"B><"col-sm-7"fr>>t<"row"<"col-sm-5"i><"col-sm-7"p>>',
            buttons: [
                { extend: 'excel', className: 'btn-sm' }
            ]
        });
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}
