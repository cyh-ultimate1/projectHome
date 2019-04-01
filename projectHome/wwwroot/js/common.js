$(function () {
     //alert("hahaha");

    /*************variables************ */
    //var myNav = $('.header');
    //var hambActive = false;

    /*****************execution**************/
    //$(window).on('resize', function () {
    //    setNavScroll();
    //});

    //$(document).on('scroll', function () {
    //    setNavScroll();
    //});

    $('[data-toggle="tooltip"]').tooltip();

    //trigger Modal to show when delete button is clicked
    //$("#loginClick").click(function (e) {
    //    e.preventDefault();
    //    $("#delConfirmModal").modal("show");
    //});

    /*====== modal js  ======*/
    $('.formInput').blur(function () {
        var $this = $(this);
        if ($this.val())
            $this.addClass('used');
        else
            $this.removeClass('used');
    });



    /*************functions******************/
    //function setBlogTxtColorOnHover() {

    //}

    //function setNavScroll() {
    //    if ($(window).scrollTop() > 150) {
    //        myNav.addClass('scrolled');
    //    } else {
    //        myNav.removeClass('scrolled');
    //    }
    //}

});

