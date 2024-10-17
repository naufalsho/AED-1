var $dTable;

var dTable = '#dTable';
var thisUrl = 'comparison';


panelHideLoader('#panelDiv', '#panelLoader');

//NO Print fungsional
//var autoBlur = true;
//var noPrint = true;
//var noCopy = true;
//var noScreenshot = true;
//var noSelectText = true;

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
        var lastModel = parseInt($lastCompetitorColumn.find('.FilterModel[data-model="2"]').attr('data-model'));

        var number = lastBrand + 1;

        // Ubah untuk memastikan kita menggunakan competitor
        $newColumn.find('.FilterBrand[data-brand="2"]')
            .attr('data-brand', number)
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
    
    $(document).ready(function () {
        // Get the brand name from the URL
        const brandName = getQueryParameter('brandName');

        // Toggle the visibility of MHD and non-MHD sections based on the brand name
        if (brandName && brandName.toLowerCase() === 'toyota') {
            $('.MHD').show(); // Show MHD section for Toyota
            $('.non-MHD').hide(); // Hide non-MHD section
        } else {
            $('.MHD').hide(); // Hide MHD section
            $('.non-MHD').show(); // Show non-MHD section for other brands
        }


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

            if (brandName.toLowerCase() == 'toyota') {
                var $capSelect = $(`[data-cap="${brandNumber}"]`);
                commonService.getCapByBrand(brandCode, classCode, type).done(function (response) {
                    if (response.length > 0) $capSelect.prop("disabled", false);
                    $capSelect.empty();
                    $capSelect.append('<option value="" disabled selected>Please select one</option>');
                    $.each(response, function (index, element) {
                        $capSelect.append(`<option value="${element.code}">${element.name}</option>`);
                    });
                });
            } else {
                // Panggil API dengan parameter type (distributor)
                commonService.getModelByParam(brandCode, type, classCode).done(function (response) {
                    if (response.length > 0) $modelSelect.prop("disabled", false);
                    $modelSelect.empty();
                    $modelSelect.append('<option value="" disabled selected>Please select one</option>');
                    $.each(response, function (index, element) {
                        $modelSelect.append(`<option value="${element.code}">${element.model}</option>`);
                    });
                });
            } 
        });
        // #endregion

        // #region Filter cap
        $(document).on('change', '.Cap', function (e) {
            if (brandName.toLowerCase() == 'toyota') {
                const capNumber = $(this).data('cap');
                const type = capNumber === 1 ? distributor.ProductTN : ' '; // Tentukan type berdasarkan data-brand
                var $modelSelect = $(`[data-model="${capNumber}"]`);

                // Panggil API untuk mendapatkan model berdasarkan Brand yang dipilih
                const brandCode = $(`.MHD .FilterBrand[data-brand="${capNumber}"]`).val();

                const capCode = $(this).val();
                const classCode = $("#FilterClass").val();
                commonService.getModelByParam(brandCode, type, classCode, capCode).done(function (response) {
                    if (response.length > 0) $modelSelect.prop("disabled", false);
                    $modelSelect.empty();
                    $modelSelect.append('<option value="" disabled selected>Please select one</option>');
                    $.each(response, function (index, element) {
                        $modelSelect.append(`<option value="${element.code}">${element.model}</option>`);
                    });
                });
            } 
        });

        // #region Filter model mhb
        $(document).on('change', '.FilterModel', function (e) {
            if (brandName.toLowerCase() == 'toyota') {
                const modNumber = $(this).data('model');
                const type = modNumber === 1 ? distributor.ProductTN : ' '; // Tentukan type berdasarkan data-brand
                var $mastTypeSelect = $(`[data-mastType="${modNumber}"]`);

                // Panggil API untuk mendapatkan model berdasarkan Brand yang dipilih
                const brandCode = $(`.MHD .FilterBrand[data-brand="${modNumber}"]`).val();

                const capCode = $(`.MHD .Cap[data-cap="${modNumber}"]`).val();
                const classCode = $("#FilterClass").val();
                commonService.getMastTypeByCap(brandCode, classCode, type, capCode).done(function (response) {
                    console.log(response);
                    if (response.length > 0) $mastTypeSelect.prop("disabled", false);
                    $mastTypeSelect.empty();
                    $mastTypeSelect.append('<option value="" disabled selected>Please select one</option>');
                    $.each(response, function (index, element) {
                        $mastTypeSelect.append(`<option value="${element.code}">${element.name}</option>`);
                    });
                });
            }
        });

        // #region Filter masttype mhb
        $(document).on('change', '.MastType', function (e) {
            if (brandName.toLowerCase() == 'toyota') {
                const masNumber = $(this).data('masttype');
                const type = masNumber === 1 ? distributor.ProductTN : ' '; // Tentukan type berdasarkan data-brand
                var $liftingHeightSelect = $(`[data-liftingheight="${masNumber}"]`);

                // Panggil API untuk mendapatkan model berdasarkan Brand yang dipilih
                const brandCode = $(`.MHD .FilterBrand[data-brand="${masNumber}"]`).val();

                const capCode = $(`.MHD .Cap[data-cap="${masNumber}"]`).val();
                const mastTypeCode = $(this).val();
                const classCode = $("#FilterClass").val();
                commonService.getLiftingHeightByMastType(brandCode, classCode, type, capCode, mastTypeCode).done(function (response) {
                    console.log(response);
                    if (response.length > 0) $liftingHeightSelect.prop("disabled", false);
                    $liftingHeightSelect.empty();
                    $liftingHeightSelect.append('<option value="" disabled selected>Please select one</option>');
                    $.each(response, function (index, element) {
                        $liftingHeightSelect.append(`<option value="${element.code}">${element.name}</option>`);
                    });
                });
            }
        });


        // #region Filter liftingheight mhb
        $(document).on('change', '.LiftingHeight', function (e) {
            if (brandName.toLowerCase() == 'toyota') {
                const lifNumber = $(this).data('liftingheight');
                const type = lifNumber === 1 ? distributor.ProductTN : ' '; // Tentukan type berdasarkan data-brand
                var $tireSelect = $(`[data-tire="${lifNumber}"]`);

                // Panggil API untuk mendapatkan model berdasarkan Brand yang dipilih
                const brandCode = $(`.MHD .FilterBrand[data-brand="${lifNumber}"]`).val();
                const capCode = $(`.MHD .Cap[data-cap="${lifNumber}"]`).val();
                const mastTypeCode = $(`.MHD .MastType[data-masttype="${lifNumber}"]`).val();
                const liftingHeightCode = $(this).val();
                const classCode = $("#FilterClass").val();
                commonService.getTireByLiftingHeight(brandCode, classCode, type, capCode, mastTypeCode, liftingHeightCode).done(function (response) {
                    console.log(response);
                    if (response.length > 0) $tireSelect.prop("disabled", false);
                    $tireSelect.empty();
                    $tireSelect.append('<option value="" disabled selected>Please select one</option>');
                    var typeDistributor = $(`.TypeDistributor[data-distributor="${lifNumber}"]`).val(type);
                    $.each(response, function (index, element) {
                        $tireSelect.append(`<option value="${element.code}">${element.name}</option>`);
                    });
                });
            }
        });

        $(document).on('change', '.Tire', function (e) {
            debugger;
            if (brandName.toLowerCase() == 'toyota') {
                const $class = $(`#FilterClass`).val();
                const tireNumber = $(this).data('tire');

                const $brand = $(`.MHD .FilterBrand[data-brand="${tireNumber}"]`).val();
                const $distributor = $(`.MHD .TypeDistributor[data-distributor="${tireNumber}"]`).val();
                const $capSelect = $(`.MHD .Cap[data-cap="${tireNumber}"]`).val();
                const $mastTypeSelect = $(`.MHD .MastType[data-masttype="${tireNumber}"]`).val();
                const $liftingHeightSelect = $(`.MHD .LiftingHeight[data-liftingheight="${tireNumber}"]`).val();
                const $tireSelect = $(this).val();


                $.get(`get/getModelByParam/${$brand}/${$distributor}/${$class}?capCode=${$capSelect}&mastTypeCode=${$mastTypeSelect}&liftingHeightCode=${$liftingHeightSelect}&tireCode=${$tireSelect}`).done(function (response) {
                    var model = response.map(item => item.code);
                    $(`.MHD FilterModel[data-model="${tireNumber}"]`).val(model);
                    console.log("here = " +  model);
                }).fail(function (response) {
                    HandleHttpRequestFail(response);
                })
            }
        });

        $(document).on('click', '#compareNowBtn', function () {
            if (brandName.toLowerCase() == 'toyota') {
                const $capSelect = $(`.MHD .Cap`).val();
                const $mastTypeSelect = $(`.MHD .MastType`).val();
                const $liftingHeightSelect = $(`.MHD .LiftingHeight`).val();
                const $tireSelect = $(`.MHD .Tire`).val();
                if ($capSelect == '' || $mastTypeSelect == '' || $liftingHeightSelect == '' || $tireSelect == '') {
                    console.log("fail");
                } else {
                    getData();
                }
            } else {
                getData();
            }
        });
    });
        //#endregion
    
}();

var columnIndex = 1;

function getData() {
    const modelSelectors = $('.FilterModel');
    let models = [];

    modelSelectors.each(function () {
        let model = $(this).val();
        if (model) {
            models.push(model);
        }
    });

    let modelCode = models.join(',');

    var data = {
        modelCode: modelCode,
        categoryCode: $('.nav-link.active[data-table]').data('table')
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

