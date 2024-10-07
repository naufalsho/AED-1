if (typeof webapp === 'undefined')
    var webapp = {};

+function () {
    var commonService = function () {

    };

    commonService.prototype.formPost = function (formName) {
        var dfd = $.Deferred();

        $.post($(formName).attr('action'), $(formName).serialize()).done(function (result) {
            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.formPostWithFile = function (formName) {
        var dfd = $.Deferred();

        var formData = new FormData($(formName)[0]);

        $.ajax({
            url: $(formName).attr('action'),
            type: 'post',
            processData: false,
            contentType: false,
            data: formData,
            success: function (result) {
                dfd.resolve(result);
            },
            error: function (response) {
                HandleHttpRequestFail(response);
                dfd.reject(response);
            }
        });

        return dfd.promise();
    }

    commonService.prototype.getEmployeeByNrp = function (nrp) {
        var dfd = $.Deferred();

        $.getJSON('/get/employee/' + nrp).done(function (result) {
            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getMasterData = function (mdType) {
        var dfd = $.Deferred();

        $.getJSON('/get/master/' + mdType).done(function (result) {
            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getMasterDataByParent = function (mdType, parentId) {
        var dfd = $.Deferred();

        $.getJSON('/get/master/' + mdType + '/' + parentId).done(function (result) {
            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getModelByCategory = function (CategoryId) {
        var dfd = $.Deferred();

        $.getJSON(`/get/modelByCategory/${CategoryId}` ).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getModelByBrand = function (ModelId) {
        var dfd = $.Deferred();

        $.getJSON(`/get/modelByBrand/${ModelId}` ).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getModelByBrandTN = function (ModelId) {
        var dfd = $.Deferred();

        $.getJSON(`/get/modelByBrandTN/${ModelId}`).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }


    commonService.prototype.getBrandByCategory = function (CategoryId) {
        var dfd = $.Deferred();

        $.getJSON(`/get/brandByCategory/${CategoryId}` ).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getBrandByClass = function (ClassId, Distributor) {
        var dfd = $.Deferred();

        $.getJSON(`/get/brandByClass/${ClassId}/${Distributor}`).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getDistributorByBrand = function (BrandCode) {
        var dfd = $.Deferred();

        $.getJSON(`/get/getDistributorByBrand/${BrandCode}` ).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getModelByParam = function (BrandCode, Distributor, ClassCode) {
        var dfd = $.Deferred();

        $.getJSON(`/get/getModelByParam/${BrandCode}/${Distributor}/${ClassCode}` ).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }

    commonService.prototype.getDecryptedCategory = function (encrypt) {
        var dfd = $.Deferred();
        $.getJSON(`/get/getDecryptedCategory/${encrypt}`).done(function (result) {

            dfd.resolve(result);
        }).fail(function (response) {
            HandleHttpRequestFail(response);
            dfd.reject(response);
        });

        return dfd.promise();
    }




    webapp.CommonService = commonService;
}();