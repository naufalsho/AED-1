var $dTable;

var dTable = '#dTable';
var thisUrl = 'comparison';


panelHideLoader('#panelDiv', '#panelLoader');


!function () {
    // Usage
    const categoryValue = getQueryParameter('category');
    $(window).off('show.bs.modal').on('show.bs.modal', function () {

    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });




    //#region  Add More
    $('#addMoreBtn').on('click', function(e) {
        e.preventDefault();


        var $lastColumn = $('#dTbComparison tr td').not(':last').last();
        var $newColumn = $lastColumn.clone();

        var lastBrand = parseInt($lastColumn.find('.FilterBrand').attr('data-brand'));
        var lastDistributor = parseInt($lastColumn.find('[data-distributor]').attr('data-distributor'));
        var lastModel = parseInt($lastColumn.find('.filter-model').attr('data-model'));

        var number = lastBrand + 1;

        $newColumn.find('.FilterBrand')
                  .attr('data-brand', number)
                  .val("");

        $newColumn.find('.FilterDistributor')
                  .attr('data-distributor', number)
                  .val("");

        $newColumn.find('.FilterModel')
                  .attr('data-model', number)
                  .val("");


        $lastColumn.after($newColumn);

    });
    //#endregion

    //#region  Filter Class
    
    $('#FilterClass').off('change').on('change', function (e) {
        const code = $(this).val()
        // $('.form-select').select2();
        commonService.getBrandByClass($(this).val()).done(function (response) {
            console.log(response)
            if(response.length > 0 ) $('.FilterBrand').prop("disabled", false)
            $('.FilterBrand').empty()
            $('.FilterBrand').append('<option value="" disabled selected>Please select one</option>');
            $.each(response, function (index, element) {
                $('.FilterBrand').append(`<option value="${element.code}">${element.name}</option>`);
            })
        });
    }); 
    //#endregion

    $(document).ready(function() {
        // #region Filter Brand
        $(document).on('change', '.FilterBrand', function(e) {
            const brandNumber = $(this).data('brand');
            var $distributorSelect = $(`[data-distributor="${brandNumber}"]`);
    
            commonService.getDistributorByBrand($(this).val()).done(function (response) {
                console.log(response);
                if (response.length > 0) $distributorSelect.prop("disabled", false);
                $distributorSelect.empty();
                $distributorSelect.append('<option value="" disabled selected>Please select one</option>');
                $.each(response, function (index, element) {
                    $distributorSelect.append(`<option value="${element}">${element}</option>`);
                });
            });
        });
        // #endregion
    
        // #region Filter Distributor
        $(document).on('change', '.FilterDistributor', function(e) {
            const distNumber = $(this).data('distributor');
            const brandCode = $(`.FilterBrand[data-brand="${distNumber}"]`).val();
            const distributor = $(this).val();
            const classCode = $("#FilterClass").val();
    
            var $modelSelect = $(`[data-model="${distNumber}"]`);
    
            commonService.getModelByParam(brandCode, distributor, classCode).done(function (response) {
                console.log(response);
                if (response.length > 0) $modelSelect.prop("disabled", false);
                $modelSelect.empty();
                $modelSelect.append('<option value="" disabled selected>Please select one</option>');
                $.each(response, function (index, element) {
                    $modelSelect.append(`<option value="${element.code}">${element.model}</option>`);
                });
            });
        });
        // #endregion
        $(document).on('click', '#compareNowBtn', function() {
            getData();
        });
    });
        //#endregion
    
}();

var columnIndex = 1;

function getData() {
    const modelSelectors = $('.FilterModel'); 
    let models = [];

    modelSelectors.each(function() {
        let model = $(this).val();
        if (model) {
            models.push(model);
        }
    });

    let modelCode = models.join(',');

    var data = {
        modelCode : modelCode,
        categoryCode : $('.nav-link.active[data-table]').data('table')
    }

    panelShowLoader('#panelDiv', '#panelLoader');

    $.get(`${thisUrl}/GetList`, data).done(function (response) {
        $('.img-notfound').prop('hidden', true)
        $(dTable).html(response)
        panelHideLoader('#panelDiv', '#panelLoader');
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    })
}


function getQueryParameter(name) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}

