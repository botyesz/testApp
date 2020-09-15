$(document).ready(function () {
    $("#delete a").click(function () {
        var ans = confirm("Are you sure you want to delete this user?");
        var self = $(this);
        var id = self.attr('id');
        if (ans) {
            $.ajax({
                url: '/Home/AJAXDelete',
                data: { id: id },
                type: 'POST'
            }).done(function (result) {
                $('#tableDiv').html(result);
            });
        }
    });
});