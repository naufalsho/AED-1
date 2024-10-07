var $dTable;

var dTable = '#dTable';
var thisUrl = 'comparison';


panelHideLoader('#panelDiv', '#panelLoader');

//NO Print fungsional
var autoBlur = true;
var noPrint = true;
var noCopy = true;
var noScreenshot = true;
var noSelectText = true;

!function () {
    // Usage
    const categoryValue = getQueryParameter('category');
    $(window).off('show.bs.modal').on('show.bs.modal', function () {

    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });




    //#region Add More
    $('#addMoreBtn').on('click', function (e) {
        e.preventDefault();

        // Pilih kolom terakhir dari Competitor Product (td ke-2)
        var $lastCompetitorColumn = $('#dTbComparison tr').find('td:nth-child(2)').last();
        var $newColumn = $lastCompetitorColumn.clone();

        // Debugging: Log kolom terakhir

        // Ambil nilai dari kolom terakhir berdasarkan competitor
        var lastBrand = parseInt($lastCompetitorColumn.find('.FilterBrand[data-brand="2"]').attr('data-brand'));
        var lastDistributor = parseInt($lastCompetitorColumn.find('.FilterDistributor[data-distributor]').attr('data-distributor'));
        var lastModel = parseInt($lastCompetitorColumn.find('.FilterModel[data-model="2"]').attr('data-model'));

        var number = lastBrand + 1;

        // Ubah untuk memastikan kita menggunakan competitor
        $newColumn.find('.FilterBrand[data-brand="2"]')
            .attr('data-brand', number)
            .val("");

        $newColumn.find('.FilterDistributor[data-distributor]')
            .attr('data-distributor', number)
            .val("");

        $newColumn.find('.FilterModel[data-model="2"]')
            .attr('data-model', number)
            .val("");

        $lastCompetitorColumn.after($newColumn);
        // Pastikan kontainer tabel menggulir ke kolom baru
        $('.table-responsive').scrollLeft($('.table-responsive')[0].scrollWidth);
    });
    //#endregion


    //#region Remove Last Column
    $('#removeBtn').on('click', function (e) {
        e.preventDefault();

        var $columns = $('#dTbComparison tr td').not(':last');

        if ($columns.length > 2) {
            // Remove the last column if there is more than one column
            $columns.last().remove();
        } else {
            alert("At least two comparison column is required.");
        }
    });
    //#endregion


    


    //$(document).ready(function() {
    //    // #region Filter Brand
    //    $(document).on('change', '.FilterBrand', function(e) {
    //        const brandNumber = $(this).data('brand');
    //        var $distributorSelect = $(`[data-distributor="${brandNumber}"]`);

    //        commonService.getDistributorByBrand($(this).val()).done(function (response) {
    //            console.log(response);
    //            if (response.length > 0) $distributorSelect.prop("disabled", false);
    //            $distributorSelect.empty();
    //            $distributorSelect.append('<option value="" disabled selected>Please select one</option>');
    //            $.each(response, function (index, element) {
    //                $distributorSelect.append(`<option value="${element}">${element}</option>`);
    //            });
    //        });
    //    });
    //    // #endregion

    //    // #region Filter Distributor
    //    $(document).on('change', '.FilterDistributor', function(e) {
    //        const distNumber = $(this).data('distributor');
    //        const brandCode = $(`.FilterBrand[data-brand="${distNumber}"]`).val();
    //        const distributor = $(this).val();
    //        const classCode = $("#FilterClass").val();

    //        var $modelSelect = $(`[data-model="${distNumber}"]`);

    //        commonService.getModelByParam(brandCode, distributor, classCode).done(function (response) {
    //            console.log(response);
    //            if (response.length > 0) $modelSelect.prop("disabled", false);
    //            $modelSelect.empty();
    //            $modelSelect.append('<option value="" disabled selected>Please select one</option>');
    //            $.each(response, function (index, element) {
    //                $modelSelect.append(`<option value="${element.code}">${element.model}</option>`);
    //            });
    //        });
    //    });
    //    // #endregion
    //    $(document).on('click', '#compareNowBtn', function() {
    //        getData();
    //    });
    //});
    $(document).ready(function () {
        // Inisialisasi FilterClass dan panggil API untuk mengisi FilterBrand
        const initialClassCode = $('#FilterClass').val();
        if (initialClassCode) {
            loadBrands(initialClassCode);
        }

        // #region Filter Class
        $('#FilterClass').off('change').on('change', function (e) {
            const code = $(this).val();
            loadBrands(code);
        });
        // #endregion

        // Fungsi untuk memuat brand berdasarkan class yang dipilih
        function loadBrands(classCode) {
            commonService.getBrandByClass(classCode, distributor.ProductTN).done(function (response) {
                $('.FilterBrand[data-brand="1"]').empty().prop("disabled", true);
                if (response.length > 0) $('.FilterBrand[data-brand="1"]').prop("disabled", false);
                $('.FilterBrand[data-brand="1"]').append('<option value="" disabled selected>Please select one</option>');
                $.each(response, function (index, element) {
                    $('.FilterBrand[data-brand="1"]').append(`<option value="${element.code}">${element.name}</option>`);
                });
            });

            commonService.getBrandByClass(classCode, distributor.ProductCompetitor).done(function (response) {
                $('.FilterBrand[data-brand="2"]').empty().prop("disabled", true);
                if (response.length > 0) $('.FilterBrand[data-brand="2"]').prop("disabled", false);
                $('.FilterBrand[data-brand="2"]').append('<option value="" disabled selected>Please select one</option>');
                $.each(response, function (index, element) {
                    $('.FilterBrand[data-brand="2"]').append(`<option value="${element.code}">${element.name}</option>`);
                });
            });
        }

        // #region Filter Brand
        $(document).on('change', '.FilterBrand', function (e) {
            const brandNumber = $(this).data('brand');
            const type = brandNumber === 1 ? distributor.ProductTN : ' '; // Tentukan type berdasarkan data-brand
            var $modelSelect = $(`[data-model="${brandNumber}"]`);

            // Panggil API untuk mendapatkan model berdasarkan Brand yang dipilih
            const brandCode = $(this).val();
            const classCode = $("#FilterClass").val();

            // Panggil API dengan parameter type (distributor)
            commonService.getModelByParam(brandCode, type, classCode).done(function (response) {
                if (response.length > 0) $modelSelect.prop("disabled", false);
                $modelSelect.empty();
                $modelSelect.append('<option value="" disabled selected>Please select one</option>');
                $.each(response, function (index, element) {
                    $modelSelect.append(`<option value="${element.code}">${element.model}</option>`);
                });
            });
        });
        // #endregion

        $(document).on('click', '#compareNowBtn', function () {
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

