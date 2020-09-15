$(document).ready(function () {
    $("#Search").keyup(function () {
        var SearchValue = $("#Search").val();
        DisplayTable(SearchValue);
    });
    DisplayTable("");
});

function DisplayTable(SearchValue) {
    $.ajax({
        url: '/Home/BuildUsersTableWithSearch',
        data: { SearchValue: SearchValue },
        type: 'POST'
    }).done(function (result) {
        $('#tableDiv').html(result);
    });
}