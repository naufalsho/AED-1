var DateTime = luxon.DateTime;
var commonService = new webapp.CommonService();

$('#app').on('click', '.btn-modal', function (e) {
    e.preventDefault();
    $('#modalContent').load(this.href,
        function (responseText, textStatus, response) {
            if (textStatus == "error") {
                HandleHttpRequestFail(response);
            } else {
                $('#myModal').modal('show');
            }
        }
    );
});

$('#app').on('click', '.modal-close', function (e) {
    $('#myModal').modal('hide');
    $('.modal-dialog').removeClass('modal-wide');
});

$('#app').on('keydown', '.number', function (e) {
    if ($.inArray(e.keyCode, [8, 9, 27, 13, 46, 110, 188, 190]) !== -1 ||
        (e.keyCode == 65 && e.ctrlKey === true) ||
        (e.keyCode == 67 && e.ctrlKey === true) ||
        (e.keyCode == 88 && e.ctrlKey === true) ||
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        return;
    }
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});

$('#app').on('keyup', '.currency', function (event) {
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, '')
            .replace(/\B(?=(\d{3})+(?!\d))/g, '.')
            ;
    });
});

function isAllowCreate() {
    return $('#allowCreate').val() === 'True';
}

function isAllowEdit() {
    return $('#allowEdit').val() === 'True';
}

function isAllowDelete() {
    return $('#allowDelete').val() === 'True';
}

function HandleHttpRequestFail(response) {
    var msg = response.statusText;

    if (response.responseText.length > 0) {
        msg = response.responseText;
    }

    ShowAlert(alertStatus.Error, msg, null, response);
}

function FormatDateToString(dateToFormat) {
    return DateTime.fromISO(dateToFormat).setLocale('id').toFormat('dd/LL/yyyy');
}

function ModalAction(handleSubmit) {
    $('#btnModalAct').off('click').on('click', function (e) {
        e.preventDefault();

        if ($("#FormInput")[0].checkValidity()) {
            btnShowLoader('#btnModalAct');

            commonService.formPost('#FormInput').done(function () {
                $('#myModal').modal('hide');
                ShowAlert(alertStatus.Success, 'Data has been updated!', null, null);

                if (handleSubmit) {
                    setTimeout(handleSubmit(), 5000);
                }
            }).always(function () {
                btnHideLoader('#btnModalAct');
            });
        } else {
            $("#FormInput")[0].reportValidity()
        }
    });
}

function ModalActionWithFile(handleSubmit) {
    $('#btnModalAct').off('click').on('click', function (e) {
        e.preventDefault();

        if ($("#FormInput")[0].checkValidity()) {
            btnShowLoader('#btnModalAct');

            commonService.formPostWithFile('#FormInput').done(function () {
                $('#myModal').modal('hide');
                ShowAlert(alertStatus.Success, 'Data has been updated!', null, null);

                if (handleSubmit) {
                    setTimeout(handleSubmit(), 5000);
                }
            }).always(function () {
                btnHideLoader('#btnModalAct');
            });
        } else {
            $("#FormInput")[0].reportValidity()
        }
    });
}

function BuildMasterDataFilter(targetEl, mdType) {
    $(targetEl).empty().append('<option selected="selected" value="0">All</option>');

    commonService.getMasterData(mdType).done(function (response) {
        $.each(response, function (index, element) {
            $(targetEl).append('<option value="' + element.value + '">' + element.text + '</option>');
        });
    });
}

function BuildMasterDataFilterByParent(targetEl, mdType, parentId) {
    $(targetEl).empty().append('<option selected="selected" value="0">All</option>');

    commonService.getMasterDataByParent(mdType, parentId).done(function (response) {
        $.each(response, function (index, element) {
            $(targetEl).append('<option value="' + element.value + '">' + element.text + '</option>');
        });
    });
}

function BuildMasterDataDDl(targetEl, mdType, parentId) {
    $(targetEl).empty().append('<option selected="selected" value="">Please select one</option>');

    commonService.getMasterDataByParent(mdType, parentId).done(function (response) {
        $.each(response, function (index, element) {
            $(targetEl).append('<option value="' + element.value + '">' + element.text + '</option>');
        });
    });
}

function BuildDatePicker(targetEl) {
    $(targetEl).flatpickr({
        dateFormat: "d/m/Y"
    });

    let thisId = $(targetEl).attr('id');
    $('#i' + thisId).off('click').on('click', function () {
        $(targetEl)[0]._flatpickr.open();
    });

    $('#iClear' + thisId).off('click').on('click', function () {
        $(targetEl)[0]._flatpickr.clear();
    });
}

function BuildMonthPicker(targetEl) {
    $(targetEl).flatpickr({
        plugins: [
            new monthSelectPlugin({
                dateFormat: "d/m/Y"
            })
        ]
    });

    let thisId = $(targetEl).attr('id');
    $('#i' + thisId).off('click').on('click', function () {
        $(targetEl)[0]._flatpickr.open();
    });

    $('#iClear' + thisId).off('click').on('click', function () {
        $(targetEl)[0]._flatpickr.clear();
    });
}

function FormatToCurrency(data) {
    return parseInt(data).toString()
        .replace(/\D/g, '')
        .replace(/\B(?=(\d{3})+(?!\d))/g, '.');
}

function ShowAlertDelete(type, detail, delUrl, handleSubmit) {
    let msg = 'Are you sure you want to delete this <b>' + type + '</b> with ' + detail + '?';

    let delSubmit = function () {
        $.ajax({
            url: delUrl,
            type: 'delete',
            success: function () {
                ShowAlert(alertStatus.Success, 'Data has been deleted!', null, null);
                handleSubmit();
            },
            error: function (response) {
                HandleHttpRequestFail(response);
            }
        });
    };

    ShowAlert(alertStatus.Warning, msg, delSubmit, null);
}

function ShowAlert(type, message, handleSubmit, response) {
    // REMARKS : type = success, warning, error. Check constant js.

    if (type == alertStatus.Warning) {
        Swal.fire({
            title: 'Are You Sure?',
            html: message,
            icon: type.toLowerCase(),
            showCancelButton: true,
            cancelButtonColor: '#FF3B30',
            confirmButtonColor: '#007AFF',
            confirmButtonText: 'Confirm'
        }).then(function (e) {
            if (e.value) {
                handleSubmit();
            }
        });
    } else {
        var title = type;
        var btnColor = '#4CD964';

        if (type == alertStatus.Error) {
            btnColor = '#FF3B30';

            if (response) {
                switch (response.status) {
                    case 400:
                        title = 'Bad Request';
                        break;
                    case 401:
                        title = 'Unathorized';
                        break;
                    case 404:
                        title = 'Page Not Found';
                        break;
                    case 500:
                        title = 'Internal Server Error';
                        break;
                    default:
                        title = `Status Code: ${response.status}`;
                }
            } else {
                title = 'Error';
            }
        }

        Swal.fire({
            title: title,
            text: message,
            icon: type.toLowerCase(),
            showCancelButton: false,
            confirmButtonColor: btnColor
        });
    }
};

function btnShowLoader($element) {
    $($element).prepend('<i class="fas fa-spinner fa-spin me-2"></i>');
    $($element).attr("disabled", true);
}

function btnHideLoader($element) {
    $("i", $element).remove();
    $($element).removeAttr("disabled");
}

function panelShowLoader(panelDiv, panelLoader) {
    $(panelLoader).show();
    $(panelDiv).addClass('panel-loading');
}

function panelHideLoader(panelDiv, panelLoader) {
    $(panelLoader).hide();
    $(panelDiv).removeClass('panel-loading');
}
