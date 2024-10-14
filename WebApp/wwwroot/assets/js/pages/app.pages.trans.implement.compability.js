var $dTable;

var dTable = '#dTable';
var thisUrl = 'implementCompability';

$(".product-model").prop("hidden", true);
$('.form-select').select2();
panelHideLoader('#panelDiv', '#panelLoader');

getDataImplement($("#FilterImplement").val())

//NO Print fungsional
//var autoBlur = true;
//var noPrint = true;
//var noCopy = true;
//var noScreenshot = true;
//var noSelectText = true;

!function () {

    $(window).off('show.bs.modal').on('show.bs.modal', function () {
      
    });

    $('a[data-bs-toggle="tab"]').on('show.bs.tab', function () {
        let thisEle = $(this);
        let tabShow = thisEle.data('table');
        $(".img-notfound").prop("hidden", false)


        $(dTable).empty()
        $('.form-select').val(null).select2().trigger('change.select2');

        if (tabShow === 'implement') {
            getDataImplement($("#FilterImplement").val())
            $(".implement").prop("hidden", false);
            $(".product-model").prop("hidden", true);
        }else{
            
            getDataProductMode($("#FilterProductModel").val())
            $(".implement").prop("hidden", true);
            $(".product-model").prop("hidden", false);
        }

    });

    $('#app').on('click', '.btn-delete', function (e) {
        e.preventDefault();

        ShowAlertDelete($(this).data('type'), $(this).data('detail'), $(this).attr('href'), getData);
    });


    $('#FilterImplement').on('change', function () {
        getDataImplement($("#FilterImplement").val())
    });

    $('#FilterProductModel').on('change', function () {
        getDataProductMode($("#FilterProductModel").val())
    });

    
    
}();

function getDataImplement(id = null) {
    panelShowLoader('#panelDiv', '#panelLoader');
    var data = {
        ModelCode : id
    }



    $.get(`${thisUrl}/GetImplement`, id != '' ?  data : null)
        .done(function (response) {
            setTimeout(() => {
                $(dTable).html(response);  // Menghapus tanda kutip agar respons HTML dimuat dengan benar
                console.log(response);  // Menampilkan respon di konsol untuk debugging
    
                // Menyembunyikan loader pada panel
                panelHideLoader('#panelDiv', '#panelLoader');
                $(".img-notfound").prop("hidden", true)
            }, 1500);

        })
        .fail(function (response) {
            // Menangani kesalahan permintaan HTTP
            HandleHttpRequestFail(response);

            // Menyembunyikan loader pada panel jika terjadi kesalahan
            panelHideLoader('#panelDiv', '#panelLoader');
        });
}

function getDataProductMode(id = null) {
    panelShowLoader('#panelDiv', '#panelLoader');

    var data = {
        ModelCode : id
    }

    $.get(`${thisUrl}/GetProductModel`, id != '' ?  data : null)
        .done(function (response) {
            setTimeout(() => {
                $(dTable).html(response);  
                console.log(response);  

                panelHideLoader('#panelDiv', '#panelLoader');
                $(".img-notfound").prop("hidden", true)
            }, 1500);

        })
        .fail(function (response) {
            // Menangani kesalahan permintaan HTTP
            HandleHttpRequestFail(response);

            // Menyembunyikan loader pada panel jika terjadi kesalahan
            panelHideLoader('#panelDiv', '#panelLoader');
        });
}
