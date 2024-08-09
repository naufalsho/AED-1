var $dTable;

var dTable = '#dTable';

!function () {
    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.modal-dialog').addClass('modal-lg');
    });

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

    $.post('history/list', data).done(function (response) {
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
                    data: 'assetId',
                    className: 'text-center',
                    render: function (data, type, row) {
                        return '<div><a href="history/detail/' + data + '" class="btn btn-outline-gray-dark btn-sm me-2 btn-modal">View</a></div>';
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            scrollX: true,
            dom: '<"row"<"col-sm-5"><"col-sm-7"fr>>t<"row"<"col-sm-5"i><"col-sm-7"p>>'
        });
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}
