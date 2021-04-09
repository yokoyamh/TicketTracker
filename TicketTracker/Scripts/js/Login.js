(function ($) {
    var panelOne = $('.form-panel.two')[0].scrollHeight,
        panelTwo = $('.form-panel.two')[0].scrollHeight,
        loginForm = document.getElementById("LoginForm"),
        registerForm = document.getElementById("RegisterForm");
    $("#Login").click(function (e) {
        loginForm.submit();
        e.stopPropagation();
    });
    $("#Register").click(function (e) {
        registerForm.submit();
        e.stopPropagation();
    });
    $('.form-panel.two').not('.form-panel.two.active').on('click', function (e) {
        e.preventDefault();

        $('.form-toggle').addClass('visible');
        $('.form-panel.one').addClass('hidden');
        $('.form-panel.two').addClass('active');
        $('.form').animate({
            'height': panelTwo
        }, 200);
    });

    $('.form-toggle').on('click', function (e) {
        e.preventDefault();
        $(this).removeClass('visible');
        $('.form-panel.one').removeClass('hidden');
        $('.form-panel.two').removeClass('active');
        $('.form').animate({
            'height': panelOne
        }, 200);
    });
})(jQuery);
