
$(function () {

    //$("#selectedServicesWrapper").onresize = function () {
    //    alert("resized");
    //    $("#w2").css("margin-top", "350px");
    //};

    var collapseSign = $(".collapseSign");
    var collapseMinus = true;

    collapseSign.click(function () {
        if ($(this).children("i").hasClass("fa-minus")) {
            $(this).children("i").removeClass("fa-minus").addClass("fa-plus");
        } else if ($(this).children("i").hasClass("fa-plus")) {
            $(this).children("i").removeClass("fa-plus").addClass("fa-minus");
        }
        
    });


    if (window.matchMedia('(max-width: 769px)').matches) {
        // do functionality on screens smaller than
        var selectedServBtn = $("#selectedServBtn");
        var selectedServicesWrapper = $("#selectedServicesWrapper");
        var boxMoved = false;

        selectedServBtn.click(function (e) {
            //e.preventDefault();
            if (!boxMoved) {
                selectedServicesWrapper.animate({ top: '135px' }, 500);
                boxMoved = true;
            } else {
                selectedServicesWrapper.animate({ top: '-135px' }, 500);
                boxMoved = false;
            }
            
        });
    }
})