var $dTable;

var dTable = '#dTable';
var thisUrl = 'productSpec';
var commonService = new webapp.CommonService();

let Category;


$(".product-model").prop("hidden", true);
$('.form-select').select2();
panelHideLoader('#panelDiv', '#panelLoader');



!function () {

    // Usage
    const categoryValue = getQueryParameter('category');
   
    // getData();

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
      
    });

    $('a[data-bs-toggle="tab"]').on('show.bs.tab', function () {
        let thisEle = $(this);
        /*Category = thisEle.data('table');*/
        $(".img-notfound").prop("hidden", false)
        $(dTable).empty()

        $('.form-select').val(null).select2().trigger('change.select2');

        commonService.getDecryptedCategory(categoryValue).done(function (response) {
            Category = response;

            commonService.getBrandByCategory(Category).done(function (response) {
                if (response.length > 0) $('#FilterBrand').prop("disabled", false)
                $('#FilterBrand').empty()
                $('#FilterBrand').append('<option value="" disabled selected>Please select one</option>');
                $.each(response, function (index, element) {
                    $('#FilterBrand').append(`<option value="${element.code}">${element.name}</option>`);
                })
            });
        });
        

    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });

    $('#FilterBrand').on('change', function () {
        let brandCode = $('#FilterBrand').val()

        

        commonService.getModelByBrand(brandCode).done(function (response) {
            console.log(response)
            if(response.length > 0 ) $('#FilterModel').prop("disabled", false)
            $('#FilterModel').empty()
            $('#FilterModel').append('<option value="" disabled selected>Please select one</option>');
            $.each(response, function (index, element) {
                $('#FilterModel').append(`<option value="${element.code}">${element.model}</option>`);
            })
        });

    });



    $('#FilterModel').on('change', function () {
        getData()
    });

    let activeTab = $('a[data-bs-toggle="tab"].active');
    if (activeTab.length) {
        activeTab.trigger('show.bs.tab');
    }

    
    
}();

function getData() {
    panelShowLoader('#panelDiv', '#panelLoader');

    var data = {
        CategoryCode : Category,
        ModelCode : $("#FilterModel").val()
        }


    $.get(`${thisUrl}/GetList`, data)
        .done(function (response) {
            setTimeout(() => {
                $(dTable).html(response);  
                console.log(response);      
                $(".img-notfound").prop("hidden", true)
                panelHideLoader('#panelDiv', '#panelLoader');
            }, 1500);

        })
        .fail(function (response) {
            HandleHttpRequestFail(response);
            panelHideLoader('#panelDiv', '#panelLoader');
        });
}

function getQueryParameter(name) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}

