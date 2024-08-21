var dTable = '#dTable';

var thisUrl = 'comparisonType';

!function () {
    getData();

    let loadHandler = getData;

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.modal-dialog').addClass('modal-md');
        $(".code-area").prop('disabled', false)

        const isEdit = $(".id").val() != 0


        if(isEdit) $(".code-area").prop('readonly', true);

        ModalAction(loadHandler);
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
    });
}();

function getData() {
    $.get({
        url: thisUrl + '/GetList',
        beforeSend: function() {
            // Langkah-langkah persiapan sebelum mengirim request
            console.log('Request is about to be sent');
            panelShowLoader('#panelDiv', '#panelLoader'); // Asumsikan Anda memiliki fungsi untuk menampilkan loader
        }
    }).done(function(response) {
        // Tambahkan delay 1 menit sebelum memproses respons
        setTimeout(() => {
            console.log(response);
            panelHideLoader('#panelDiv', '#panelLoader'); // Sembunyikan loader setelah 1 menit
            initDataTable(response); // Inisialisasi tabel atau elemen lain dengan data respons
        }, 1500);
    }).fail(function(response) {
        // Tangani kesalahan permintaan HTTP
        HandleHttpRequestFail(response);
    });    
}

function initDataTable(response) {
    $(dTable).DataTable({
        destroy: true,
        autoWidth: false,
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
                data: 'title',
                className : "text-center"
            },
            {
                data: 'createdBy',
                render: function (data,type, row) {
                    return `<span class="btn btn-outline-primary">${row.createdBy} ${FormatDateTimeToString(row.createdDate)}</span>`;
                }
            },
            {
                data: 'updatedBy',
                render: function (data,type, row) {
                    if(data != null)
                    {
                        return `<span class="btn btn-outline-warning">${data} ${FormatDateTimeToString(row.updatedDate)}</span>`;
                    }else{
                        return ``;
                    }
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
                    let downloadBtn ='';

                    if (isAllowEdit()) {
                        editBtn = `<a href="${thisUrl}/Create/${data}" class="btn btn-outline-warning btn-sm me-2 btn-modal"><span class="fas fa-edit"></span></a>`;
                    }

                    if (isAllowDelete()) {
                        delBtn = `<a href="${thisUrl}/Delete/${data}" type="button" class="btn btn-outline-danger me-2 btn-sm btn-delete" data-type="Yard Area" data-detail="name <b>${row.name}</b>"><span class="fas fa-trash"></span</a>`;
                    }
                    downloadBtn = `<a href="${thisUrl}/DownloadQR/${row.yardQRCode}" type="button" ${row.isDelete? "disabled" : ""} class="btn btn-outline-info btn-sm btn-download me-2"><span class="fas fa-download"></span</a>`;
                    return `<div>${editBtn} ${delBtn} ${downloadBtn}</div>`;
                }
            }
        ],
        responsive: true,
        paging: true,
        lengthChange: true,
        autoWidth: false,
        dom: '<"row"<"col text-start"<"#dtBtnAddGeneral">><"col justify-content-end"f>>' +
            '<"row"<"col-sm-12"tr>>' +
            '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>'
    });

    if (isAllowCreate()) {
        $('#dtBtnAddGeneral').append('<a href="' + thisUrl + '/Create" class="btn btn-primary btn-modal"><span class="fa fa-plus"></span> New Yard Area</a>');
    }
}
