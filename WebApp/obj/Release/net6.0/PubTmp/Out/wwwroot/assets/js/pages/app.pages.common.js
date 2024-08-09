!function () {
    var commonService = new webapp.CommonService();

    $('#FormInput').on('submit', function (e) {
        e.preventDefault();

        btnShowLoader('#btnSubmit');

        commonService.formPost('#FormInput').done(function (response) {
            window.location.href = response;
        }).always(function () {
            btnHideLoader('#btnSubmit');
        });
    });
}();
