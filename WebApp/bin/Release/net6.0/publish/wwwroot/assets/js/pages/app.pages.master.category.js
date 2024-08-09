var $dTable;

var dTable = '#dTable';
var thisUrl = 'category';

const response = [
    {
        id: 1,
        code: 'C001',
        desc: 'Kubota',
        brand: [
            { name :'Massey Ferguson' },
            { name :'Kubota' },
            { name :'Canycom' },
        ],
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 1,
        code: 'C002',
        desc: 'Canycom',
        brand: [
            {name : 'Canycom'}
        ],
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 1,
        code: 'C003',
        desc: 'MF',
        brand: [
            {name : 'Kubota'}
        ],
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
];


!function () {
    getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');
        initDt(response);
        panelHideLoader('#panelDiv', '#panelLoader');


    // $.get('assets').done(function (response) {
    //     initDt(response);
    //     panelHideLoader('#panelDiv', '#panelLoader');
    // }).fail(function (response) {
    //     HandleHttpRequestFail(response);
    // })
}

function initDt(response) {
    let mdText = "test"
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
				{ data: 'code' },
				{ data: 'desc',
                    className : "text-center"
                 },
                {
                    data: 'brand',
                    render: function (data) {
                        return data.map(brand => `<span class="badge rounded-pill bg-primary">${brand.name}</span>`).join(' ');
                    }
                },
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
							editBtn = `<a href="${thisUrl}/Create?id=${data}" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>`;
						}

						if (isAllowDelete()) {
							delBtn = '<a href="' + thisUrl  + '/' + data + '" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="' + mdText + '" data-detail="name <b>' + row.name + '</b>">Delete</a>';
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
