var $dTable;

var dTable = '#dTable';
var thisUrl = 'productSpec';

$(".product-model").prop("hidden", true);
$('.form-select').select2();
panelHideLoader('#panelDiv', '#panelLoader');


!function () {
    // getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
      
    });

    $('a[data-bs-toggle="tab"]').on('show.bs.tab', function () {
        let thisEle = $(this);
        let tabShow = thisEle.data('table');
        $(".img-notfound").prop("hidden", false)


        // $(dTable).empty()
        $('.form-select').val(null).select2().trigger('change.select2');

        // if (tabShow === 'implement') {
        //     $(".implement").prop("hidden", false);
        //     $(".product-model").prop("hidden", true);
        // }else{
        //     $(".implement").prop("hidden", true);
        //     $(".product-model").prop("hidden", false);
        // }

    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });



    $('#FilterClass').on('change', function () {
        alert("test")
        getData()
    });

    
    
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(`${thisUrl}/GetList`)
        .done(function (response) {
            setTimeout(() => {
                $(dTable).html(response);  // Menghapus tanda kutip agar respons HTML dimuat dengan benar
                console.log(response);  // Menampilkan respon di konsol untuk debugging
    
                // Menyembunyikan loader pada panel
                $(".img-notfound").prop("hidden", true)
                panelHideLoader('#panelDiv', '#panelLoader');
            }, 1500);

        })
        .fail(function (response) {
            // Menangani kesalahan permintaan HTTP
            HandleHttpRequestFail(response);

            // Menyembunyikan loader pada panel jika terjadi kesalahan
            panelHideLoader('#panelDiv', '#panelLoader');
        });
}


// function initDt(response) {
//     let mdText = "test"
//     if (!$.fn.DataTable.isDataTable(dTable)) {
//         $dTable = $(dTable).DataTable({
//             data: response,
//             columns: [
// 				{
// 					data: 'id',
// 					className: 'text-center',
// 					render: function (data, type, row, meta) {
// 						return meta.row + meta.settings._iDisplayStart + 1;
// 					}
// 				},
// 				{ data: 'code' },
// 				{ data: 'name' },
// 				{ data: 'country' },
// 				{ data: 'createdBy' },
// 				{
// 					data: 'createdDate',
// 					className: 'text-center',
// 					render: function (data) {
// 						return FormatDateToString(data);
// 					}
// 				},
// 				{
// 					data: 'isActive',
// 					className: 'text-center',
// 					render: function (data) {
// 						var check = data ? 'checked="checked"' : '';
// 						return '<input class="form-check-input" type="checkbox" ' + check + ' disabled />';
// 					}
// 				},
// 				{
// 					data: 'id',
// 					className: 'text-center',
// 					render: function (data, type, row) {
// 						let editBtn = '';
// 						let delBtn = '';

// 						if (isAllowEdit()) {
// 							editBtn = `<a href="${thisUrl}/Create?id=${data}" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>`;
// 						}

// 						if (isAllowDelete()) {
// 							delBtn = '<a href="' + thisUrl  + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="' + mdText + '" data-detail="name <b>' + row.name + '</b>">Delete</a>';
// 						}

// 						return '<div>' + editBtn + delBtn + '</div>';
// 					}
// 				}
// 			],
			
//             responsive: true,
//             autoWidth: false,
//             scrollX: true,
//             dom: '<"row"<"col text-start"<"#dtBtn"B>><"col justify-content-end"f>>' +
//                 '<"row"<"col-sm-12"tr>>' +
//                 '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
//             buttons: [
//                 {
//                     extend: 'excel',
//                     className: 'd-none',
//                     exportOptions: {
//                         columns: ':not(:last-child)'
//                     }
//                 }
//             ]
//         });

//         if (isAllowCreate()) {
//             $('#dtBtn').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Add Brand</a>');
//         }

//         // $('#dtBtn')
//         //     .append(
//         //         $('<span class="btn btn-secondary ms-1">Excel</span>')
//         //             .on('click', function () {
//         //                 $('.buttons-excel').click();
//         //             }));
//     } else {
//         $dTable.clear().search('').draw();
//         $dTable.rows.add(response).draw();
//     }
// }

// #reguin Add More

