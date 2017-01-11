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
function button_autocomplete() {
    button_autocomplete.count = ++button_autocomplete.count || 0;
    var value = button_autocomplete.count;
    var text_node = document.createElement("input");
    text_node.setAttribute("class", "form-control");
    text_node.setAttribute("name", "Actors[" + value + "].Name");
    text_node.setAttribute("id", "Actors[" + value + "].Name");
    text_node.setAttribute("type", "text");
    document.getElementById("actorsList").appendChild(text_node);
    $(text_node).autocomplete({
        source: '/Movie/GetActors'
    });
};