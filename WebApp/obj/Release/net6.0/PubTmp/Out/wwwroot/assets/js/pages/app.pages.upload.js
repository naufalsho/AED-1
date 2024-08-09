var $dTable;

var dTable = '#dTable';
var thisUrl = 'upload';

!function () {
    getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        ModalActionWithFile(getData);
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get('/upload/list').done(function (response) {
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
                    data: 'status',
                    className: 'text-center text-nowrap'
                },
                {
                    data: 'id',
                    className: 'text-nowrap',
                    render: function (data, type, row) {
                        let ret = '';

                        if (data) {
                            ret = '<a href="/downloadupload/' + data + '">' + row.uploadName + '</a>';
                        }

                        return ret;
                    }
                },
                {
                    data: 'uploadDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        return FormatDateToString(data);
                    }
                },
                {
                    data: 'uploadBy',
                    className: 'text-nowrap'
                },
                {
                    data: 'submitDate',
                    className: 'text-center text-nowrap',
                    render: function (data) {
                        let ret = '';

                        if (data) {
                            ret = FormatDateToString(data);
                        }

                        return ret;
                    }
                },
                {
                    data: 'submitBy',
                    className: 'text-nowrap'
                },
                {
                    data: 'id',
                    className: 'text-center text-nowrap',
                    render: function (data, type, row) {
                        let viewBtn = '<a href="' + thisUrl + '/detail/' + data + '" class="btn btn-outline-info btn-sm me-2">Detail</a>';
                        let delBtn = '';

                        if (isAllowDelete()) {
                            if (row.status === 'DRAFT') {
                                delBtn = '<a href="' + thisUrl + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Upload" data-detail="name <b>' + row.uploadName + '</b>">Delete</a>';
                            }
                        }

                        return '<div>' + viewBtn + delBtn + '</div>';
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            scrollX: true,
            dom: '<"row"<"col text-start"<"#dtBtn">><"col justify-content-end"f>>' +
                '<"row"<"col-sm-12"tr>>' +
                '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
        });

        if (isAllowCreate()) {
            $('#dtBtn').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Upload</a>');
        }
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}
