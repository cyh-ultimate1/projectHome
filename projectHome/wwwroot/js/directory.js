
$(function () {

    /*======== variables ============*/
    var alphaStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var pageLink = $(".page-link");
    var selectedPageId = 0;
    var prevSelectedPageId = -1;
    var selectedPageChar = "A";
    var dirWrapper = $("#dirWrapper");
    var activeLetter = null;

    /*========== implmentation ==========*/
    displayResults();
    pageLink.click(function (e) {
        e.preventDefault();
        prevSelectedPageId = selectedPageId;
        //remove active from previous pagination.
        $("[data-pageId=" + selectedPageId + "]").parent().removeClass("active");
        //record current pagination.
        if (selectedPageId > 0 && $(this).attr("data-pageId") == "prev") {
            selectedPageId--;
            $("[data-pageId=" + selectedPageId + "]").parent().addClass("active");
        } else if (selectedPageId < 25 && $(this).attr("data-pageId") == "next") {
            selectedPageId++;
            $("[data-pageId=" + selectedPageId + "]").parent().addClass("active");
        } else if ($(this).attr("data-pageId") != "next" && $(this).attr("data-pageId") != "prev") {
            selectedPageId = $(this).attr("data-pageId");
            //add active to current pagination.
            $(this).parent().addClass("active");
        } else {
            $("[data-pageId=" + selectedPageId + "]").parent().addClass("active");
        }

        selectedPageChar = alphaStr.charAt(selectedPageId);


        //alert($(this).attr("data-pageId"));
        if (selectedPageId != prevSelectedPageId) {
            //remove directory elements from page.
            displayResults();
        }
    });

    /*====== functions ===============*/
    function displayResults() {
        dirWrapper.children().remove();
        $.ajax({
            url: "Directory/GetDirListByInitial",
            type: "GET",
            contentType: "application/json; charset=utf-8;",
            dataType: "json",
            data: {
                reqData: selectedPageChar
            },
            success: function (respData) {
                //var jsonObj = $.parseJSON(respData);
                $.each(respData, function (idx, obj) {
                    dirWrapper.append(
                        '<div class="card myBoxShadow trans_300">' +
                            '<a href="#" data-companyId="' + obj.id + '">' +
                                '<div class="compLogo" style="background-image: url(\'' + '/images/myCompanyLogo.png' + '\')"></div>' +
                                '<div class="myCardText"><div>' + obj.companyName + '</div></div>' +
                            '</a>' +
                        '</div >');
                });
            }
        });
    } 


});