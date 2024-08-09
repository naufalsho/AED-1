var $dTable;

var dTable = '#dTable';

!function () {
    getData();
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get('readystock/list').done(function (response) {
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
                }
            ],
            responsive: true,
            autoWidth: false,
            scrollX: true,
            dom: '<"row"<"col-sm-5"B><"col-sm-7"fr>>t<"row"<"col-sm-5"i><"col-sm-7"p>>',
            buttons: [
                { extend: 'excel', className: 'btn-sm' },
                {
                    extend: 'pdf',
                    className: 'btn-sm',
                    customize: function (doc) {
                        console.log(doc);
                        doc.defaultStyle.fontSize = 10;
                        doc.styles.tableHeader.fontSize = 10;
                        doc.content.splice(0, 1, {
                            text: [
                                {
                                    text: 'Seat Management \n',
                                    bold: true,
                                    fontSize: 12
                                },
                                {
                                    text: 'Report Ready Stock',
                                    bold: true,
                                    fontSize: 14
                                }
                            ],
                            margin: [0, 0, 0, 10],
                            alignment: 'center'
                        });
                        doc.footer = (function (page, pages) {
                            return {
                                columns: [
                                    'Generated on: ' + FormatDateToString(DateTime.now()),
                                    {
                                        alignment: 'right',
                                        text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                    }
                                ],
                                margin: [10, 10]
                            }
                        });
                    }
                }
            ]
        });
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}
