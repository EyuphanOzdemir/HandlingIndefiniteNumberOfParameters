$(function () {
    //we need to get rid of the time-part of the datetime value
    $(".date-picker").each(function (index) {
        var onlyDate = $(this).val().substring(0, 10).trim()
        $(this).val(onlyDate);
    });
    //Here is datepicker function of JQuery-UI
    $(".date-picker").datepicker({ dateFormat: 'dd.mm.yy' });
});
