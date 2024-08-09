var $dTable;

var dTable = '#dTable';
var thisUrl = 'classes';


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

    $(window).off('show.bs.modal').on('show.bs.modal', function () {


        ModalAction(loadHandler);
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


        $('#app').on('click', '.btn-delete', function (e) {
            e.preventDefault();

            ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), loadHandler);
        });
    });
}();

function getDataClass() {
    $.get(`${thisUrl}/GetList/Class`).done(function (response) {
        initDt(response, 'class');
        panelShowLoader('#panelDiv', '#panelLoader');
        setTimeout(() => {
            panelHideLoader('#panelDiv', '#panelLoader');
        }, 1500);
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}

function getDataClassValue() {

    $.get(`${thisUrl}/GetList/ClassValue`).done(function (response) {
        panelShowLoader('#panelDiv', '#panelLoader');
        setTimeout(() => {
            initDt(response, 'classValue');
            panelHideLoader('#panelDiv', '#panelLoader');
        }, 1500);
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
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
					data: 'code',
					className: 'text-center',
					render: function (data, type, row) {
						let editBtn = '';
						let delBtn = '';

						if (isAllowEdit()) {
							editBtn = `<a href="${thisUrl}/Edit/${mdType}/?id=${data}" class="btn btn-outline-warning btn-sm me-2 btn-modal">Edit</a>`;
						}

						if (isAllowDelete()) {
							delBtn = `<a href="${thisUrl}/${mdType}/${data}" type="button" class="btn btn-outline-danger btn-sm btn-delete" data-type="${mdText}" data-detail="name <b>' ${row.name} '</b>">Delete</a>`;
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
