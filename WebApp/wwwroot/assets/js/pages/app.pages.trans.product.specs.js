var $dTable;

var dTable = '#dTable';
var thisUrl = 'productSpec';
var commonService = new webapp.CommonService();

let brandName;
let brandCode;
let Category;

$(".product-model").prop("hidden", true);
$('.form-select').select2();
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
    brandName = getQueryParameter('brandName');

   
    // getData();
    $(window).off('show.bs.modal').on('show.bs.modal', function () {
      
    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });

    // Panggil commonService untuk mendapatkan Brand berdasarkan kategori
    commonService.getDecryptedCategory(categoryValue).done(function (response) {
        debugger;
        Category = response;

        // Mengambil Brand berdasarkan Category dan menyimpan brandCode
        commonService.getBrandByCategory(Category).done(function (response) {
            debugger;
            if (response.length > 0) {
                // Ambil brandCode dari response dan simpan di hidden input
                const brandElement = response.find(element => element.name.toLowerCase() === getQueryParameter('brandName').toLowerCase());
                if (brandElement) {
                    brandCode = brandElement.code;

                    // Panggil fungsi untuk memuat Model berdasarkan brandCode
                    loadModelsForBrand(brandCode);
                }
            }
        });
    });

    $('#FilterModel').on('change', function () {
        getData()
    });

    let activeTab = $('a[data-bs-toggle="tab"].active');
    if (activeTab.length) {
        activeTab.trigger('show.bs.tab');
    }
    document.querySelectorAll('.nav-link span').forEach(function (span) {
        span.textContent = brandName; // Mengganti teks dengan brandName dari URL
    });
}();

function loadModelsForBrand(brandCode) {
    // Panggil model berdasarkan brandCode yang telah disimpan di hidden input
    commonService.getModelByBrandTN(brandCode).done(function (response) {
        debugger;
        if (response.length > 0) {
            $('#FilterModel').prop("disabled", false);
            $('#FilterModel').empty();
            $('#FilterModel').append('<option value="" disabled selected>Please select one</option>');
            $.each(response, function (index, element) {
                $('#FilterModel').append(`<option value="${element.code}">${element.model}</option>`);
            });
        }
    });
}

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

