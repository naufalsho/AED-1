var $dTable;

var dTable = '#dTable';
var thisUrl = 'classes';

const response = [
    {
        id: 1,
        code: 'CL00001',
        name: 'U30',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 2,
        code: 'CL00002',
        name: 'U50',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 3,
        code: 'CL00003',
        name: 'HP 30 -40',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 4,
        code: 'CL00004',
        name: 'HP 40-50',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
];


const responseValue = [
    {
        id: 1,
        code: 'CV00001',
        name: '2 X 22"',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 2,
        code: 'CV00002',
        name: '2 X 24"',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 3,
        code: 'CV00003',
        name: '2 X 26"',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
    {
        id: 4,
        code: 'CV00004',
        name: '2 X 28"',
        createdBy: 'Admin',
        createdDate: '2023-01-01',
        isActive: true
    },
];


!function () {
    getDataClass();

    let loadHandler = getDataClass;

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        $('.form-select').select2({
            dropdownParent: $("#FormInput")
        });

        
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
    });

    $('a[data-bs-toggle="tab"]').on('show.bs.tab', function () {
        let thisEle = $(this);
        let tabShow = thisEle.data('table');


        if (tabShow === 'class') {
            getDataClass();
            loadHandler = getDataClass;
        } else {
            getDataClassValue();
            loadHandler = getDataClassValue;
        }

        $(window).off('show.bs.modal').on('show.bs.modal', function () {
            $('.form-select').select2({
                dropdownParent: $("#FormInput")
            });

            // $('#lblParent').text(lblParent);
            ModalAction(loadHandler);
        });

        $('#app').on('click', '.btn-delete', function (e) {
            e.preventDefault();

            ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
        });
    });
}();

function getDataClass() {
    panelShowLoader('#panelDiv', '#panelLoader');
        initDt(response, "class");
        panelHideLoader('#panelDiv', '#panelLoader');


    // $.get('assets').done(function (response) {
    //     initDt(response);
    //     panelHideLoader('#panelDiv', '#panelLoader');
    // }).fail(function (response) {
    //     HandleHttpRequestFail(response);
    // })
}

function getDataClassValue() {
    panelShowLoader('#panelDiv', '#panelLoader');
        initDt(responseValue, "classValue");
        panelHideLoader('#panelDiv', '#panelLoader');


    // $.get('assets').done(function (response) {
    //     initDt(response);
    //     panelHideLoader('#panelDiv', '#panelLoader');
    // }).fail(function (response) {
    //     HandleHttpRequestFail(response);
    // })
}

function initDt(response, type) {
    let mdType = '';
    let mdText = '';
    let mdBtn = '';


    switch(type)
    {
        case "class":
            mdType = 'class';
            mdText = 'Class'
            mdBtn = '#dtBtnAddClass';
            break
        case "classValue" : 
            mdType = 'classValue';
            mdText = 'Class Value'
            mdBtn = '#dtBtnAddClassValue';
            break
        default :
            mdType = '';
            mdText = ''
            mdBtn = '';
        
        
    }

    $(".panel-title").html(`Master ${mdText}`)



        $dTable = $(dTable).DataTable({
            destroy : true,
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
				{ data: 'name' },
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
							editBtn = `<a href="${thisUrl}/Create/${mdType}/?id=${data}" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>`;
						}

						if (isAllowDelete()) {
							delBtn = `<a href="${thisUrl}/${data}" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="${mdText}" data-detail="name <b>' ${row.name} '</b>">Delete</a>`;
						}

						return '<div>' + editBtn + delBtn + '</div>';
					}
				}
			],
			
            responsive: true,
            autoWidth: false,
            scrollX: true,
            dom: '<"row"<"col text-start"<"'+ mdBtn + '">><"col justify-content-end"f>>' +
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
            $(mdBtn).html(`<a href="${thisUrl}/create/${mdType}" class="btn btn-outline-primary btn-modal">Add ${mdText}</a>`);
        }

        // $('#dtBtn')
        //     .append(
        //         $('<span class="btn btn-secondary ms-1">Excel</span>')
        //             .on('click', function () {
        //                 $('.buttons-excel').click();
        //             }));
   
}
