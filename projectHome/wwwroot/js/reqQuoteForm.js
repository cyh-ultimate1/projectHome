
$(function () {
    var termCondChkBox = $("#termCondChkBox");
    var submitBtn = $("#submitBtn");
    var nextBtn = $(".nextBtn");
    var prevBtn = $(".prevBtn");
    var progressBarList = $("#progressbar li");
    var currentForm;
    var left, opacity, scale;
    var animating;

    //$("form").removeData('validator');
    //$("form").removeData('unobtrusiveValidation');
    //$.validator.unobtrusive.parse($("form"));

    termCondChkBox.change(function () {
        if ($(this).is(':checked')) {
            submitBtn.removeAttr("disabled");
        } else {
            submitBtn.attr("disabled", true);
        }
    });

    nextBtn.click(function (e) {
        //e.preventDefault();

        //if validation fail, stop the function.
        if (!$("form").valid()) {
            return false;
        }

        currentForm = $(this).parent().parent();
        
        //update progressbar, add active to next step number
        progressBarList.eq($("#cardForms .card").index(currentForm.next())).addClass("active");
        //if (animating) return false;
        //animating = true;
        $('html, body').stop().animate({ scrollTop: 150 }, 500);

        //show next form
        currentForm.next().show();

        currentForm.animate(
            { opacity: 0 },
            {
                step: function (now, mx) {
                    //scale = 1 - (1 - now) * 0.2;
                    left = (now * 50) + "%";
                    opacity = 1 - now;
                    currentForm.css({
                        //'transform': 'scale(' + scale + ')',
                        //'position': 'absolute'
                    });

                    //hide current form.
                    currentForm.hide();

                    //get the next form.
                    currentForm.next().css({ 'left': left, 'opacity': opacity });
                },
                duration: 1000,
                complete: function () {
                    //animating = false;
                }
            }
        );
    });

    prevBtn.click(function (e) {
        e.preventDefault();
        currentForm = $(this).parent().parent();
        
        //update progressbar, remove active from current step number
        progressBarList.eq($("#cardForms .card").index(currentForm)).removeClass("active");

        //if (animating) return false;
        //animating = true;

        $('html, body').stop().animate({ scrollTop: 200 }, 500);

        //show previous form
        currentForm.prev().show();

        currentForm.animate(
            { opacity: 0 },
            { step: function (now, mx) {
                //as the opacity of current_fs reduces to 0 - stored in "now"
                //1. scale previous_fs from 80% to 100%
                scale = 0.8 + (1 - now) * 0.2;
                //2. take current_fs to the right(50%) - from 0%
                left = ((1 - now) * 50) + "%";
                //3. increase opacity of previous_fs to 1 as it moves in
                opacity = 1 - now;
                //currentForm.css({ 'left': left });
                currentForm.hide();
                currentForm.prev().css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
                },
                duration: 1000,
                complete: function () {
                //animating = false;
                }
            }
        );
    });
})