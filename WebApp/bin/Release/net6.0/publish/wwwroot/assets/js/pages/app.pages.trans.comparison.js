var $dTable;

var dTable = '#dTable';
var thisUrl = 'comparison';


var sampleData = {
    "tableData": {
        "headers": [
            { "label": "Header 1" },
            { "label": "Header 2" },
            { "label": "Header 3" },
            { "label": "Header 4" }
        ],
        "rows": [
            {
                "type": "Engine",
                "models": [
                    { "id": "S00000001", "name": "Engine Model" },
                    { "id": "S00000002", "name": "Engine Model 2" },
                    { "id": "S00000003", "name": "Engine Model 3" },
                    { "id": "S00000004", "name": "Engine Model 4" },
                    { "id": "S00000005", "name": "Engine Model 5" },
                    { "id": "S00000006", "name": "Engine Model 6" },
                    { "id": "S00000007", "name": "Engine Model 7" },
                    { "id": "S00000008", "name": "Engine Model 8" },
                    { "id": "S00000009", "name": "Engine Model 9" },
                    { "id": "S00000010", "name": "Engine Model 10" }
                ]
            },
            {
                "type": "Transmission",
                "models": [
                    { "id": "T00000001", "name": "Transmission Model" },
                    { "id": "T00000002", "name": "Transmission Model 2" },
                    { "id": "T00000003", "name": "Transmission Model 3" },
                    { "id": "T00000004", "name": "Transmission Model 4" },
                    { "id": "T00000005", "name": "Transmission Model 5" },
                    { "id": "T00000006", "name": "Transmission Model 6" },
                    { "id": "T00000007", "name": "Transmission Model 7" },
                    { "id": "T00000008", "name": "Transmission Model 8" },
                    { "id": "T00000009", "name": "Transmission Model 9" },
                    { "id": "T00000010", "name": "Transmission Model 10" }
                ]
            }
        ]
    }
};

panelHideLoader('#panelDiv', '#panelLoader');


!function () {
    // getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
        // $('.form-select').select2({
        //     dropdownParent: $("#FormInput")
        // });

    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });



    //#region  Add More
    $('#addMoreBtn').on('click', function(e) {
        e.preventDefault();

        var $thead = $('#dTbComparison thead tr');

        if ($thead.length === 0) {
            console.error('Table header row not found.');
            return;
        }

        var $newTh = $('<th>', { class: 'col-auto' });

        $newTh.html(createColumnContent(columnIndex));
        $thead.children().last().before($newTh);

        columnIndex++;
    });
    //#endregion



    //#region Process Compare

    $('#compareNowBtn').on('click', function(e) {
        e.preventDefault();

        alert(columnIndex);
        processComparison(sampleData);
    });
    //#endregion
    
}();

var columnIndex = 1;

// function getData() {
//     panelShowLoader('#panelDiv', '#panelLoader');
//         initDt(response);
//         panelHideLoader('#panelDiv', '#panelLoader');


//     // $.get('assets').done(function (response) {
//     //     initDt(response);
//     //     panelHideLoader('#panelDiv', '#panelLoader');
//     // }).fail(function (response) {
//     //     HandleHttpRequestFail(response);
//     // })
// }

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
				{ data: 'name' },
				{ data: 'country' },
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
            $('#dtBtn').append('<a href="' + thisUrl + '/create" class="btn btn-outline-primary btn-modal">Add Brand</a>');
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

// #reguin Add More
function createColumnContent(index) {
    return `
        <div class="row">
            <div class="mb-2">
                <label for="brand${index}" class="form-label">Brand</label>
                <select class="form-select select2" id="brand${index}">
                    <option value="">Please select one</option>
                    <option value="MF2605-4WD">Massey Ferguson</option>
                    <option value="MF2605-4WD">John Deere</option>
                </select>
            </div>
            <div class="mb-2">
                <label for="distributor${index}" class="form-label">Distributor</label>
                <select class="form-select" id="distributor${index}">
                    <option value="">Please select one</option>
                    <option value="PT Traktor Nusantara">PT Traktor Nusantara</option>
                    <option value="PT Wahana Inti Selaras">PT Wahana Inti Selaras</option>
                </select>
            </div>
            <div class="mb-2">
                <label for="modelFilter${index}" class="form-label">Product Model</label>
                <select class="form-select" id="modelFilter${index}">
                    <option value="">Please select one</option>
                    <option value="MF2605-4WD">MF2605-4WD</option>
                </select>
            </div>
        </div>
    `;
}
//#endregion

//#region  Process Compare

function processComparison(data) {
    var $tbody = $('#dTbComparison tbody');
    var $thead = $('#dTbComparison thead tr');
    var $thElements = $thead.children('th'); 
    

    if ($tbody.length === 0) {
        console.error('Table body not found.');
        return;
    }

    if ($thead.length === 0 || $thElements.length === 0) {
        console.error('Table header row or th elements not found.');
        return;
    }

    $tbody.empty(); // Clear existing rows

    var thCount = $thElements.length - 2; // Number of columns without the first and last columns

    data.tableData.rows.forEach(function(row) {
        var newRow = '';

        newRow += `<tr class="bg-primary">`;
        newRow += `<td style="padding-left: 40px;" colspan="${thCount + 2}"><b>${row.type}</b></td>`;
        newRow += '</tr>';

        row.models.forEach(function(model) {
            newRow += `<tr>`;
            newRow += `<td style="padding-left: 80px;">${model.id} - ${model.name}</td>`;

            for (var i = 0; i < thCount; i++) {
                newRow += `<td>Simpson S325.3 Tier II ${i + 1} - ${i + 1}</td>`;
            }

            newRow += '</tr>';
        });

        $tbody.append(newRow);
    });
}
//#endregion