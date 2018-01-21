$(document).ready(function () {
    $('.login').on('click', function (event) {
        event.preventDefault();
        /* Act on the event */
        $('.popuplogin').css('display', 'block');
        console.log("đa add được class");
    });
});