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
        id: 2,
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
        id: 3,
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

    let loadHandler = getData;
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

    $('a[data-bs-toggle="tab"]').on('show.bs.tab', function () {
        let thisEle = $(this);
        let tabShow = thisEle.data('table');


        if (tabShow === 'tagNon') {

            getData();
            loadHandler = getData;
        } else {

            getDataTagTN();
            loadHandler = getDataTagTN;
        }


        $('#app').on('click', '.btn-delete', function (e) {
            e.preventDefault();

            ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
        });
    });
}();

function getData() {
    $.get(`${thisUrl}/GetList`).done(function (response) {
        initDt(response);
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataTagTN() {

    $.get(`${thisUrl}/GetList/TN`).done(function (response) {
        panelShowLoader('#panelDiv', '#panelLoader');
        setTimeout(() => {
            initDt(response, 'classValue');
            panelHideLoader('#panelDiv', '#panelLoader');
        }, 1500);
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
				{ data: 'code' },
				{ data: 'description',
                    className : "text-center"
                 },
                 {
                    data: 'categoryDetails',
                    render: function (data) {
                        return data.map(detail => {
                            let brand = detail.brand;
                            if (brand) {
                                return `<span class="badge rounded-pill bg-primary">${brand.name}</span>`;
                            }
                            return '';
                        }).join(' ');
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
					data: 'code',
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
