// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {

    // 日本語化
    $.datepicker.regional['ja'] = {
        buttonImage: "../img/calender.png",        // カレンダーアイコン画像
        buttonText: "カレンダーから選択", // ツールチップ表示文言
        buttonImageOnly: true,           // 画像として表示
        showOn: "both",                   // カレンダー呼び出し元の定義
        closeText: '閉じる',
        prevText: '<前',
        nextText: '次>',
        currentText: '今日',
        monthNames: ['1月', '2月', '3月', '4月', '5月', '6月',
            '7月', '8月', '9月', '10月', '11月', '12月'],
        monthNamesShort: ['1月', '2月', '3月', '4月', '5月', '6月',
            '7月', '8月', '9月', '10月', '11月', '12月'],
        dayNames: ['日曜日', '月曜日', '火曜日', '水曜日', '木曜日', '金曜日', '土曜日'],
        dayNamesShort: ['日', '月', '火', '水', '木', '金', '土'],
        dayNamesMin: ['日', '月', '火', '水', '木', '金', '土'],
        weekHeader: '週',
        dateFormat: 'yy\年mm\月dd\日\(D\)',
        firstDay: 0,
        isRTL: false,
        showMonthAfterYear: true,
        yearSuffix: '年',
        showButtonPanel: true,
    };
    $.datepicker.setDefaults($.datepicker.regional['ja']);


    //年月入力
    $.ympicker.regional['ja'] = {
        buttonImage: "../img/calender.png",        // カレンダーアイコン画像
        buttonText: "カレンダーから選択", // ツールチップ表示文言
        buttonImageOnly: true,           // 画像として表示
        showOn: "both",                   // カレンダー呼び出し元の定義
        closeText: '閉じる',
        prevText: '&#x3c;前',
        nextText: '次&#x3e;',
        currentText: '今日',
        monthNames: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
        monthNamesShort: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
        dateFormat: 'yy/mm/dd',
        yearSuffix: '年'
    };
    $.ympicker.setDefaults($.ympicker.regional['ja']);

});


function pwCheckStrength(password) {
    let strength = 0 // 強さ
    if (password.length < 6) {
        $('#result').removeClass()
        $('#result').addClass('short')
        return '短すぎます!!'
    }
    // 文字数が7より大きいければ+1
    if (password.length > 7) strength += 1

    // 英字の大文字と小文字を含んでいれば+1
    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) strength += 1

    // 英字と数字を含んでいれば+1
    if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) strength += 1

    // 記号を含んでいれば+1
    if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1

    // 記号を2つ含んでいれば+1
    if (password.match(/(.*[!,%,&,@,#,$,^,*,?,_,~].*[!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1

    // 点数を元に強さを計測
    if (strength < 3) {
        $('#result').removeClass()
        $('#result').addClass('weak')
        return '弱いです〜'
    } else if (strength >= 3) {
        $('#result').removeClass()
        $('#result').addClass('strong')
        return '強いです！！'
    } else {
        $('#result').removeClass()
        $('#result').addClass('good')
        return 'いい感じ！！'
    }
}



/**
 * カンマ区切りにして返却
 * @param {any} inputAns
 */
function kanmaChange(inputAns) {
    //console.log(inputAns);
    let inputAnsValue = inputAns.value;
    //console.log(inputAnsValue);
    let numberAns = inputAnsValue.replace(/[^0-9]/g, "").replace(/^0+/, "");
    kanmaAns = numberAns.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,');
    //console.log(kanmaAns);
    /*if (kanmaAns.match(/[^0-9]/g)) {*/
    inputAns.value = kanmaAns;
    return true;
    //}
};

function filterDigitsOnly(inputElem) {
    inputElem.value = inputElem.value.replace(/[^0-9]/g, '');
}
/**
 * カンマ区切りにして返却(javascriptからの呼び出し用)
 * @param {any} inputAns
 */
function kanmaChange2(inputAns) {
    var amount = Number(inputAns);
    if (!isNaN(amount)) {
        return amount.toLocaleString();
    }
    return inputAns;
};

/**
 *  数値チェック
 * @param {any} value
 * @returns
 */
function isNumber(value) {
    return !isNaN(value) && typeof value === 'number';
}

/**
 * 
 * @param {any} inputAns
 */
function kanmaChangeRevert(inputAns) {
    if (!inputAns) {
        return Number(0);
    }
    let numberAns = replace_all(inputAns, ',', '')
    let result = Number(numberAns)
    //正しく変換されたかチェックする
    if (!isNaN(result)) {
        return result;
    } else {
        return Number(0);
    }

};

// 全角→半角(英数字)
function toHalfWidth(str) {
    let inputAnsValue = str.value;
    // 全角英数字を半角に変換
    inputAnsValue = inputAnsValue.replace(/[Ａ-Ｚａ-ｚ０-９]/g, (s) => {
        return String.fromCharCode(s.charCodeAt(0) - 0xFEE0);
    });
    str.value = inputAnsValue;
    return true;
};

function toFullWidth(str) {
    let inputAnsValue = str.value;
    // 半角英数字を全角に変換
    inputAnsValue = inputAnsValue.replace(/[A-Za-z0-9]/g, (s) => {
        return String.fromCharCode(s.charCodeAt(0) + 0xFEE0);
    });
    str.value = inputAnsValue;
    return true;
}

/**
 * 文字列を全置換する
 * @param {any} string
 * @param {any} target
 * @param {any} replacement
 */
function replace_all(string, target, replacement) {
    var result = "";
    var offset = 0;
    var target_length = target.length;
    if (target_length === 0) {
        for (var i = 0, c = string.length; i < c; i++) {
            result += string[i];
            result += replacement;
        }
        if (result.length)
            return result.substr(0, result.length - replacement.length);
        return result;
    }
    do {
        var i = string.indexOf(target, offset);
        if (i === -1) {
            result += string.substring(offset);
            return result;
        }
        result += string.substring(offset, i);
        result += replacement;
        offset = i + target_length;
    } while (true);
}


/**
 * Date型を「〇〇年〇〇月度」の文字に変換して返却
 * @param {any} dateVal
 */
function MonthToString(dateVal) {
    var disp = dateVal.getFullYear().toString() + '年' + ('00' + (dateVal.getMonth() + 1).toString()).slice(-2) + '月度';
    return disp;
}

/**
 * Date型を「〇〇年〇〇月〇〇日（曜日）」の文字に変換して返却
 * @param {any} dateVal
 */
function DayToString(dateVal) {
    const weeks = ["日", "月", "火", "水", "木", "金", "土"];
    /** 日付を文字列にフォーマットする */
    var disp = `${dateVal.getFullYear()}\年${(dateVal.getMonth() + 1).toString().padStart(2, '0')}\月${dateVal.getDate().toString().padStart(2, '0')}\日\(${weeks[dateVal.getDay().toString()]}\)`.replace(/\n|\r/g, '');
    return disp;
}

/**
 * 日付をyyyy/mm/dd hh:mm:ss形式に変換して返却
 * @param {any} date
 */
function formatDate2(date) {
    if (date == null || date.length == 0) { return null; }
    const pad = (num) => String(num).padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())} ${pad(date.getHours())}:${pad(date.getMinutes())}:${pad(date.getSeconds())}`;
}

/**
 * 日付をyyyy/mm/dd 00:00形式に変換して返却
 * @param {any} date
 */
function formatDate3(date) {
    if (date == null || date.length == 0) { return null; }
    const pad = (num) => String(num).padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())} 00:00`;
}

/**
 * 日付をyyyy/mm/dd hh:mm形式に変換して返却
 * @param {any} date
 */
function formatDate4(date) {
    if (date == null || date.length == 0) { return null; }
    const pad = (num) => String(num).padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())} ${pad(date.getHours())}:${pad(date.getMinutes())}`;
}

/**
 * 日付をhh:mm形式に変換して返却
 * @param {any} date
 */
function formatDate5(date) {
    if (date == null || date.length == 0) { return null; }
    const pad = (num) => String(num).padStart(2, '0');
    var dayHour = 0;
    if (date.getDate() > 1) {
        dayHour = (Number(date.getDate()) - 1) * 24;
    }
    return `${pad(date.getHours() + dayHour)}:${pad(date.getMinutes())}`;
}


/**
 * 日付型の引数をyyyyMM01フォーマットに変換して返却
 * セパレート文字はsepで指定
 * @param {any} date
 * @param {any} sep
 */
function formatMonth(date, sep = "") {
    if (date == null || date.length == 0) { return null; }
    const yyyy = date.getFullYear();
    const mm = ('00' + (date.getMonth() + 1)).slice(-2);
    const dd = '01';

    return `${yyyy}${sep}${mm}${sep}${dd}`;
};



/*
 * 
 * alert処理の共通化
 * SweetAlert2ライブラリは非同期処理のメッセージのため
 * async　await　を使用
 */

async function alertNoIcon(text) {
    await Swal.fire({ text: text });
};

// アラート内で改行したい場合
async function alertCustomInputError(text) {
    await Swal.fire({ title: "入力エラー", html: text, icon: "error", didOpen: () => { Swal.hideLoading(); } });
};

async function alertInputError(text) {
    await Swal.fire({ title: "入力エラー", text: text, icon: "error", didOpen: () => { Swal.hideLoading(); } });
};

async function alertError(text, title) {
    await Swal.fire({ title: title, text: text, icon: "error", didOpen: () => { Swal.hideLoading(); } });
};

async function alertError(text) {
    await Swal.fire({ text: text, icon: "error", didOpen: () => { Swal.hideLoading(); } });
};

async function alertInfo(text, title, timer) {
    await Swal.fire({ title: title, text: text, icon: "info", timer: Number(timer) * 1000, didOpen: () => { Swal.hideLoading(); } });
};

async function alertInfo(text, title) {
    await Swal.fire({ title: title, text: text, icon: "info", didOpen: () => { Swal.hideLoading(); } });
};

async function alertInfo(text) {
    await Swal.fire({ text: text, icon: "info", didOpen: () => { Swal.hideLoading(); } });
};

async function alertWarning(text, title) {
    await Swal.fire({ title: title, text: text, icon: "warning", didOpen: () => { Swal.hideLoading(); } });
};

async function alertWarning(text) {
    await Swal.fire({ text: text, icon: "warning", didOpen: () => { Swal.hideLoading(); } });
};

async function alertSuccess(text, title) {
    await Swal.fire({ title: title, text: text, icon: "success", didOpen: () => { Swal.hideLoading(); } });
};

async function alertSuccess(text) {
    await Swal.fire({ text: text, icon: "success" });
};

async function alertQuestion(text, title) {
    await Swal.fire({ title: title, text: text, icon: "question", didOpen: () => { Swal.hideLoading(); } });
};

async function alertQuestion(text) {
    await Swal.fire({ text: text, icon: "question", didOpen: () => { Swal.hideLoading(); } });
};

async function alertLoading(text, title, icon) {
    await Swal.fire({ title: title, text: text, icon: icon, didOpen: () => { Swal.Loading(); } });
};

async function alertYesNoMsg(title, text, icon) {
    return await alertYesNoMsg(title, text, icon, null, null, null);
};


async function alertYesNoMsg(title, text, icon, confirmButtonText) {
    return await alertYesNoMsg(title, text, icon, confirmButtonText, null, null);
};

async function alertYesNoMsg(title, text, icon, confirmButtonText, cancelButtonText) {
    return await alertYesNoMsg(title, text, icon, confirmButtonText, cancelButtonText, null);
};

async function alertYesNoMsg(title, text, icon, confirmButtonText, cancelButtonText, html) {

    if (confirmButtonText == null) { confirmButtonText = "OK"; }
    if (cancelButtonText == null) { cancelButtonText = "Cancel"; }

    var resultVal = false;

    await Swal.fire({
        title: title, text: text, icon: icon, html: html, showCancelButton: true, confirmButtonText: confirmButtonText, cancelButtonText: cancelButtonText, didOpen: () => { Swal.hideLoading(); }
    }).then((result) => {
        if (result.isConfirmed) {
            resultVal = true;
        } else {
            resultVal = false;
        }
    });

    return resultVal;
};

/***
 * 共通処理中ダイアログ開始
 * */
function CommonWaitStart() {
    ///処理中ダイアログ
    Swal.fire({
        title: 'データ読込中'
        , html: '表示されるまでそのままお待ちください。'
        , allowOutsideClick: false     //枠外をクリックしても画面を閉じない
        , showConfirmButton: false
        , didOpen: () => {
            Swal.showLoading();
        }
    });
}

/**
 * 共通処理中ダイアログ終了
 * */
function CommonWaitEnd() {
    //完了ダイアログ
    Swal.fire({
        title: '処理終了'
        , html: '処理を終了しました。'
        , type: 'info'
        , timer: 1
        , allowOutsideClick: true     //枠外をクリックしても画面を閉じない
        , didOpen: () => { Swal.hideLoading(); }
    });
}

async function alertThreeButtonsMsg(title, text, icon, confirmButtonText, cancelButtonText, denyButtonText) {

    if (confirmButtonText == null) { confirmButtonText = "OK"; }
    if (cancelButtonText == null) { cancelButtonText = "Cancel"; }

    var resultVal = false;

    await Swal.fire({
        title: title, text: text, icon: icon, showCancelButton: true, showDenyButton: true, confirmButtonText: confirmButtonText, cancelButtonText: cancelButtonText, denyButtonText: denyButtonText, didOpen: () => { Swal.hideLoading(); }
    }).then((result) => {
        if (result.isConfirmed) {
            resultVal = 1;
        } else if (result.isDenied) {
            resultVal = 3;
        } else {
            resultVal = 2;
        }
    });

    return resultVal;
};



function YearMonthString(d) {
    var m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m;
    return d.getFullYear() + '年' + m + '月';
}

function DisplayBlankInsteadOfZero(input_id) {
    $('#' + input_id).val("");
    $('#' + input_id).on('input', function () {
        if ($(this).val() < 1) {
            $(this).val("");
        }
    });
}


function CommonTableInsertRow(idRowKeyName, idColKeyName, idTableName) {

    var currentNo = 0;
    currentNo = $("[id^=" + idRowKeyName + "]").length;　 //現在の総行数

    var row = $("[id^=" + idRowKeyName + "]").attr('id');
    var rowId = row.substring(idRowKeyName.length, 99);

    //　ダミー行の要素をコピー
    var newRow = $("#" + row).clone(true);

    newRow.attr({
        id: idRowKeyName + (currentNo)
    });

    //alert(newRow.find("[id^=CompanyDriverSyaryoList_]").length);

    newRow.find("[id^=" + idColKeyName + "]").each(function () {
        var id = $(this).attr('id');
        var name = $(this).attr('name');
        var idRep = id.replace(rowId, currentNo);
        var nameRep = name.replace(rowId, currentNo);
        $(this).attr({
            id: idRep,
            name: nameRep,
        })
    });

    // 最終行に追加
    $("#" + idTableName).append(newRow);

    return currentNo;

};


function CommonTableInsertRow(idRowKeyName, idColKeyName, idTableName, rowCount) {

    var currentNo = 0;
    currentNo = $("[id^=" + idRowKeyName + "]").length / rowCount;　 //現在の総行数

    var row = $("[id^=" + idRowKeyName + "]").attr('id');
    var rowId = row.substring(idRowKeyName.length, row.indexOf('-', idRowKeyName.length + 1));

    row = row.substring(0, row.indexOf('-', idRowKeyName.length + 1));

    for (var i = 1; i <= rowCount; i++) {

        var rowIdEda = row + '-' + i.toString();

        //　ダミー行の要素をコピー
        var newRow = $("#" + rowIdEda).clone(true);

        newRow.attr({
            id: idRowKeyName + (currentNo) + '-' + i.toString()
        });

        //alert(newRow.find("[id^=CompanyDriverSyaryoList_]").length);

        newRow.find("[id^=" + idColKeyName + "]").each(function () {
            var id = $(this).attr('id');
            var name = $(this).attr('name');
            var idRep = id.replace(rowId, currentNo);
            var nameRep = name.replace(rowId, currentNo);
            $(this).attr({
                id: idRep,
                name: nameRep,
            })
        });

        // 最終行に追加
        $("#" + idTableName).append(newRow);

    }



    return currentNo;

};

function SelectCommonDialog(val) {

    var obj = val.parent().parent().find('td');

    var data = "";

    let result = {};

    for (var i = 1; i < obj.length; i++) {
        if (obj.eq(i).find('input').attr('name') === undefined) {
        } else {
            data += obj.eq(i).find('input').attr('name').replace('item.', '') + ';;;;' + obj.eq(i).find('input').attr('value') + ';;;;';
            var name = obj.eq(i).find('input').attr('name').replace('item.', '');
            var val = obj.eq(i).find('input').attr('value');
            /*            alert(name + ";;;;" + val);*/
            result[name] = val;
        }

    }

    return result

};

/**
 * 
 * @param {any} obj
 */
function SelectCommonDialog2(obj) {



    var data = "";

    let result = {};

    for (var i = 1; i < obj.length; i++) {
        if (obj.eq(i).find('input').attr('name') === undefined) {
        } else {
            data += obj.eq(i).find('input').attr('name').replace('item.', '') + ';;;;' + obj.eq(i).find('input').attr('value') + ';;;;';
            var name = obj.eq(i).find('input').attr('name').replace('item.', '');
            var val = obj.eq(i).find('input').attr('value');
            result[name] = val;
        }

    }

    return result

};

/**
 * JQueryのダイアログを開く
 * @param {any} id
 * @param {any} title
 * @param {any} width
 * @param {any} height
 * @param {any} closeFunction
 */
function DailogOpen(id, title, width, height, closeFunction) {
    DailogOpen(id, title, width, height, closeFunction, null);
};

/**
 * JQueryのダイアログを開く
 * @param {any} id
 * @param {any} title
 * @param {any} width
 * @param {any} height
 * @param {any} closeFunction
 * @param {any} closeFunctionForString
 */
function DailogOpen(id, title, width, height, closeFunction, closeFunctionForString) {

    var thedialogC;

    var widthB = width;
    if (widthB == 0) $("#" + id).width();

    var heightB = height;
    if (heightB == 0) $("#" + id).height();

    thedialogC = $("#" + id).dialog({
        id: 'dialog',
        autoOpen: false,
        modal: true,
        width: width,
        height: height,
        title: title,
        position: { my: "center", at: "center", of: window },
        open: function () { //Xボタン非表示

            //var aa = $(this).closest(".ui-dialog").find('.ui-dialog-titlebar-close');

            $(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).attr('id', 'btnDialogClose' + '_' + id);
            $(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).text("閉じる");
            $(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).attr('class', 'btn btn-secondary');
            //$(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).attr('style', 'float: right; width: 85px;');
            $(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).addClass('btnClose');

            $('#btnDialogClose' + '_' + id).addClass('btnClose');
            $('#btnDialogClose' + '_' + id).css('float', 'right');
            $('#btnDialogClose' + '_' + id).css('width', '85px');

            $(".ui-dialog-title", $(this).closest(".ui-dialog")).addClass('fs-4');
            $(".ui-dialog-title", $(this).closest(".ui-dialog")).css('width', '30%');
            $(".ui-dialog-title", $(this).closest(".ui-dialog")).css('text-align', 'left');
            $(".ui-dialog-titlebar").css('text-align', 'center');

            $("#btnDialogClose_" + id, $(this).closest(".ui-dialog")).on('click', function () {
                $("#" + id).html('');
                $('div.ui-dialog.ui-widget').remove();
                if (typeof closeFunction == 'function') { closeFunction(); }
                if (closeFunctionForString != null && closeFunctionForString.length > 0) { eval(closeFunctionForString + ';'); }
            });
        },
    });

    thedialogC.dialog("open");
}

function DailogOpen2(id, title, width, height, position = "center",) {

    var thedialogC;

    thedialogC = $("#" + id).dialog({
        autoOpen: false,
        modal: true,
        width: width,
        height: height,
        title: title,
        position: { my: position, at: position, of: window },
        open: function () { //Xボタン非表示
            $(".ui-dialog-titlebar-close", $(this).closest(".ui-dialog")).attr('id', 'btnDialogClose' + '_' + id);

            $(".ui-dialog-titlebar").hide();
        },

    });

    thedialogC.dialog("open");
    $("#" + id).height(height);
    $("#" + id).css("border", "#000 2px solid");
}

function CloseDailog(id, clearHTMLFlg = true) {
    $("#btnDialogClose_" + id).trigger("click");
    //$("#" + id).dialog('close');
    //$("#" + id).hide();
    //if (clearHTMLFlg) {
    //    $("#" + id).html('');
    //}

};

function CloseDailog2(id, clearHTMLFlg = true) {
    $("#" + id).dialog('close');
};