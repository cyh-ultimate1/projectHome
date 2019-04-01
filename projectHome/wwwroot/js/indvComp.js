
$(function () {
    /*==== variables ==========*/
    var photoIdx = 0;

    $('.slider').slick({
        infinite: false,
        dots: true,
        arrows: true,
        slidesToShow: 5,
        slidesToScroll: 5,
        //asNavFor: '.modalSlider',
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });


    $(".slide img").click(function () {
        openModal();
        photoIdx = $(this).attr("data-imgNum");
    });

    //need to initialize slick after modal is loaded, else slick will calculate its position wrongly.
    $('#imgModal').on('shown.bs.modal', function () {
        if ($('.modalSlider').hasClass('slick-initialized')) {
            $('.modalSlider').slick('destroy');
        } 
        $('.modalSlider').slick({
            infinite: false,
            dots: true,
            arrows: true,
            slidesToShow: 1,
            slidesToScroll: 1,
            //initialSlide indexing important. starts from zero.
            initialSlide: photoIdx-1+1

        });
    });    

    ////for debugging
    //$("input[type=radio]").change(function () {
    //    alert("value is " + $(this).attr('id') + ":" + $(this).val());
    //});

    /*========= functions ==================*/
    function openModal() {
        $('#imgModal').modal();
    }

    function closeModal() {
        $('#imgModal').hide();
    }
});