$(document).ready(function () {
    $('body').
        css("overflow", "hidden");
    $(window).
        resize(function () {
            $('#editoriframe').height(window.innerHeight);
        });
    $('#editoriframe').on("load", function () {

        $('#editoriframe').height(window.innerHeight);
    });

});