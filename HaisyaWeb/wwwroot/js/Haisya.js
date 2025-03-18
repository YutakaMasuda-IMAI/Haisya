
var theDialog2;

/**
 *
 * @@param date
 * @@param driverID
 */
function AttendanceModalEx(url, date, driverID, employeeNumber, closeFunction) {

    $.post(url,
        {
            date: date,
            driverId: driverID,
            employeeNumber: employeeNumber,
        })
        .done(function (response) {
            if (response.message != null) {
                alertError(`【エラー】${response.message}`);
                return false;
            } else {
                //alert("【done】" + response.partialView);
                //$('#ModalHere').find('.modal').modal('hide');
                $("#ModalHere2").html(response.partialView);
                //$("#ModalHere2").find('.modal').modal('show');

                theDialog2 = $("#ModalHere2").dialog({
                    autoOpen: false,
                    modal: true,
                    width: 520,
                    position: { my: "center top", at: "center top", of: window },
                    open: function () { //Xボタン非表示
                        $(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).hide();
                        $(".ui-dialog-titlebar").hide();
                        $("#btnClose2").on('click', function () {
                            closeDialog2();
                            if (typeof closeFunction == 'function') { closeFunction(); }
                        });
                    },
                });

                theDialog2.dialog("open");

                return false;
            }
        })
        .fail(function (xhr) {
            console.log(xhr);
        })
        .always(function (xhr, msg) {
            //console.log(xhr, msg);
            //alert(msg);
        });
};


function closeDialog2() {
    theDialog2.dialog("close");
};



var thedialog;

/**
 * ドライバー月間カレンダー（モーダル）を開く
 * @@param kubun
 */
function MonthScheduleCalendarModalEx(url, kubun, driverID, selectedDate, closeFunction) {

    $.post(url,
        {
            kubun,
            driverID,
            selectedDate,
        })
        .done(function (response) {
            if (response.message != null) {
                alertError(`【エラー】${response.message}`);
                return false;
            } else {
                //alert("【done】" + response.partialView);
                $("#ModalHere").html(response.partialView);
                //$("#ModalHere").find('.modal').modal('show');

                thedialog = $("#ModalHere").dialog({
                    autoOpen: false,
                    modal: true,
                    width: 700,
                    position: { my: "center top", at: "center top", of: window },
                    title: "個別月次配車表",
                    open: function () { //Xボタン非表示
                        $(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).hide();
                        $(".ui-dialog-titlebar").hide();
                        $("#btnClose").on('click', function () {
                            //$("#ModalHere").dialog("close");
                            closeDialog();
                            if (typeof closeFunction == 'function') { closeFunction(); }
                        });
                    },
                });

                thedialog.dialog("open");

                return false;
            }
        })
        .fail(function (xhr) {
            console.log(xhr);
        })
        .always(function (xhr, msg) {
            //console.log(xhr, msg);
            //alert(msg);
        });



};


function closeDialog() {
    thedialog.dialog("close");
};


