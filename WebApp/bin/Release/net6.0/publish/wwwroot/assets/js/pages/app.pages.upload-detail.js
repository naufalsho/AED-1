var $dTable;

var dTable = '#dTable';
var thisUrl = 'upload';

!function () {
    $dTable = $(dTable).DataTable({
        responsive: true,
        autoWidth: false,
        scrollX: true,
        stripeClasses: []
    });

    $('.btnSubmit').on('click', function () {
        let btnAct = $(this).data('action');

        let msg = 'Are you sure you want to <b>' + btnAct + '</b> this upload?';

        let handleSubmit = function () {
            var dataToSend = {
                dataId: $('#uploadId').val(),
                action: btnAct
            }
            console.log(dataToSend);

            $.post('/upload/submit', dataToSend).done(function (result) {
                ShowAlert(alertStatus.Success, 'Data has been submitted!', null, null);

                window.location.href = "/upload";
            }).fail(function (response) {
                HandleHttpRequestFail(response);
            });
        };

        ShowAlert(alertStatus.Warning, msg, handleSubmit, null);
    });
}();
