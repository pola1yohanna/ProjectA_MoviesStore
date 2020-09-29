// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.btn-block').click(function () {
        var url = $('#mymodal').data('url');
        $.get(url, function (data) {
            $("mymodal").html(data);
            $("mymodal").modal('show');
        });
    });
});