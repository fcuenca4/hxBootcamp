$(".datepicker").datepicker({
    format: 'dd-mm-yy',
    startDate: '01-01-2012',
    minDate: Date.parse("01-01-1960"),
    maxDate: Date.parse("01-01-2020")
});

$(function () {
    $.validator.addMethod(
        "date",
        function (value, element) {
            var minDate = Date.parse("1960-01-01");
            var maxDate = Date.parse("2100-01-01");
            var valueEntered = Date.parse(value);
            if (valueEntered < minDate || valueEntered > maxDate) {
                return false;
            }
            return !/Invalid|NaN/.test(new Date(minDate));
        },
        "Please enter a valid date!"
    );
});

$("#ImageInput").change(function (event) {
    $("#Image").attr('src', URL.createObjectURL(event.target.files[0]));
});