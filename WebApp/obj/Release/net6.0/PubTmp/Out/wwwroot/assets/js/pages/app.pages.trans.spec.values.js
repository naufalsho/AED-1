var $dTable;

var dTable = '#dTable';
var thisUrl = 'specValues';

var commonService = new webapp.CommonService();

$('.form-select').select2();

$('.btn-action').prop('hidden', true)
$('#ModelFilter').prop("disabled", true)
panelHideLoader('#panelDiv', '#panelLoader');


!function () {

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


    });

    $("#btnModalAct").on("click", function (event) {
        event.preventDefault();
        handleFormSubmit();
    });



    // Change Category 
    $('#CategoryFilter').off('change').on('change', function (e) {
        $('.form-select').select2();
        commonService.getModelByCategory($('#CategoryFilter').val()).done(function (response) {
            if(response.length > 0 ) $('#ModelFilter').prop("disabled", false)
            $('#ModelFilter').empty()
            $('#ModelFilter').append('<option value="" disabled selected>Please select one</option>');
            $.each(response, function (index, element) {
                $('#ModelFilter').append(`<option value="${element.code}">${element.model}</option>`);
            })
        });
    }); 

    // Btn Delete Click
    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });
}();

function getData() {

    let data = {
        CategoryCode : $('#CategoryFilter').val(),
        ModelCode : $('#ModelFilter').val()
    }

    $.get(`${thisUrl}/GetList`, data).done(function (response) {
        $(dTable).html(response)
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}


function handleFormSubmit() {


    if ($("#FormInput")[0].checkValidity()) {
        btnShowLoader('#btnModalAct');

        commonService.formPost('#FormInput').done(function () {
            $('#myModal').modal('hide');
            ShowAlert(alertStatus.Success, 'Data has been updated!', null, null);

            let handleFormSubmit = getData()

            if (handleSubmit) {
                setTimeout(handleSubmit(), 5000);
            }
        }).always(function () {
            btnHideLoader('#btnModalAct');
        });
    } else {
        $("#FormInput")[0].reportValidity()
    }
}


