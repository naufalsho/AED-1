var $dTable;

var dTable = '#dTable';
var thisUrl = 'specValues';

const response = [
    {
        code: 'S00000001',
        items: 'Engine',
        subItems: 'Engine Model',    
    },
    {
        code: 'S00000002',
        items: 'Engine',
        subItems: 'Power (Hp/Kw)',    
    },
    {
        code: 'S00000003',
        items: 'Engine',
        subItems: 'Rate engine Speed (rev/min)',    
    },
    {
        code: 'S00000004',
        items: 'Engine',
        subItems: 'Max Torque Nm at rpm',    
    },
    {
        code: 'S00000005',
        items: 'Transmission',
        subItems: 'Type',    
    },
    {
        code: 'S00000005',
        items: 'Transmission',
        subItems: 'Gearbox',    
    },
    {
        code: 'S00000006',
        items: 'Max. Speed (F / R ) km/h',
        subItems: 'Type',    
    },
    {
        code: 'S00000007',
        items: 'Clutch',
        subItems: 'Type',    
    },
];

$('.form-select').select2();

$('.btn-action').prop('hidden', true)

// $(dTable).attr("hidden", true);

!function () {
    // getData();

    $('#ModelFilter').on('change', function () {
        getData();
        $(dTable).attr("hidden", true);


        panelShowLoader('#panelDiv', '#panelLoader');

        $(".img-notfound").prop("hidden", true)
        
        setTimeout(() => {
            panelHideLoader('#panelDiv', '#panelLoader');
                
            $(dTable).removeAttr('hidden')

            $('.btn-action').prop('hidden', false)
            
        }, 1500);




    });
    
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
    alert("Test")

        initDt(response);
        console.log(response)
        panelHideLoader('#panelDiv', '#panelLoader');



        


    // $.get('assets').done(function (response) {
    //     initDt(response);
    //     panelHideLoader('#panelDiv', '#panelLoader');
    // }).fail(function (response) {
    //     HandleHttpRequestFail(response);
    // })
}

function initDt(response) {
    const groupedData = response.reduce((acc, item) => {
        if (!acc[item.items]) {
            acc[item.items] = [];
        }
        acc[item.items].push(item);
        return acc;
    }, {});

    const transformedData = [];

    Object.keys(groupedData).forEach(item => {
        // Add the header row
        transformedData.push({ isHeader: true, items: item });

        // Add the detail rows
        groupedData[item].forEach(detail => {
            transformedData.push(detail);
        });
    });

    console.log(transformedData);
    
    if (!$.fn.DataTable.isDataTable(dTable)) {
        $dTable = $(dTable).DataTable({
            data: transformedData,
            columns: [
                {
                    data: null,
                    className: 'text-left',
                    render: function (data, type, row, meta) {
                        if (data.isHeader) {
                            return `<strong>${data.items}</strong>`;
                        } else {
                            return `<div style="padding-left: 80px; display: flex; align-items: center;">${row.code} - ${row.subItems}</div>`;
                        }
                    }
                },
                {
                    data: 'subItems',
                    render: function (data, type, row, meta) {
                        if (row.isHeader) {
                            return ``;
                        } else {
                            return `<input class='form-control'>`;
                        }
                    }
                },
            ],
            createdRow: function(row, data, index) {
                if (data.isHeader) {
                    $(row).addClass('bg-primary text-white');
                } else {
                    $(row).css({
                        'padding-left': '80px',
                        'vertical-align': 'middle'
                    });
                }
            },
            headerCallback: function(thead, data, start, end, display) {
                $(thead).remove();
            },
            rowCallback: function(row, data, index) {
                    $(row).removeClass('odd').removeClass('even');
            },
            columnDefs: [
                { targets: '_all', visible: true }
            ],
            headerCallback: function(thead, data, start, end, display) {
                $(thead).remove();
            },
            responsive: true,
            autoWidth: false,
            scrollX: true,
            searching: false, 
            lengthChange: false,
            ordering : false,
            dom: 't', 
        });

    } else {
        $dTable.clear().search('').draw();
        $dTable.rows.add(response).draw();
    }
}

function filterOptions() {
    const input = document.getElementById('exampleDataList');
    const datalist = document.getElementById('datalistOptions');
    const options = Array.from(datalist.options);
    const filter = input.value.toLowerCase();
    const filteredOptions = options.filter(option => option.value.toLowerCase().includes(filter)).slice(0, 5);

    // Clear current options
    datalist.innerHTML = '';

    // Add filtered options
    filteredOptions.forEach(option => {
        datalist.appendChild(option);
    });
}