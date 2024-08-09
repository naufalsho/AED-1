var $dTable;

var dTable = '#dTable';
var thisUrl = 'Implement';

$('.form-select').select2();

$('.btn-action').prop('hidden', true)

// $(dTable).attr("hidden", true);

!function () {
    getData();
    
    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

       
        ModalAction(getData);
        
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });
}();

function getData() {
    $.get(`${thisUrl}/GetList`).done(function (response) {
        console.log(response)
        initDt(response);
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}



function initDt(response) {
    let mdText = "test"
    if (!$.fn.DataTable.isDataTable(dTable)) {
        $dTable = $(dTable).DataTable({
            data: response,
            columns: [
				{
					data: 'code',
					className: 'text-center',
					render: function (data, type, row, meta) {
						return meta.row + meta.settings._iDisplayStart + 1;
					}
				},
				{ data: 'modelAttach.model' },
				{ data: 'modelProduct.model' },
				{ data: 'classValues.name' },
				{ data: 'createdBy' },
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
						var check = data ? 'checked="checked"' : '';
						return '<input class="form-check-input" type="checkbox" ' + check + ' disabled />';
					}
				},
				{
					data: 'id',
					className: 'text-center',
					render: function (data, type, row) {
						let editBtn = '';
						let delBtn = '';

						if (isAllowEdit()) {
							editBtn = `<a href="${thisUrl}/Edit?id=${data}" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>`;
						}

						if (isAllowDelete()) {
							delBtn = '<a href="' + thisUrl  + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="Master Category" data-detail="name <b>' + row.description + '</b>">Delete</a>';
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
            $('#dtBtn').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Add Category</a>');
        }

        // $('#dtBtn')
        //     .append(
        //         $('<span class="btn btn-secondary ms-1">Excel</span>')
        //             .on('click', function () {
        //                 $('.buttons-excel').click();
        //             }));
    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}
