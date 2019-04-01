
$(function () {
    /*=============== variables ========================*/
    var searchInput = $("#searchInput");
    var searchForm = $("#searchForm");
    var serchBtn = $("#serchBtn");
    var resultsDisplay = $("#resultsDisplay");
    var pageLink = $(".page-link");
    var selectSortOption = $("#selectSortOption");
    var compHref = $(".compHref");

    /*** jquery UI autocomplete *******/
    $.widget("custom.catcomplete", $.ui.autocomplete, {
        _create: function () {
            this._super();
            this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
        },
        _renderMenu: function (ul, items) {
            var that = this,
                currentCategory = "";
            $.each(items, function (index, item) {
                var li;
                if (item.category !== currentCategory) {
                    ul.append("<li class='ui-autocomplete-category'>" + item.category + "</li>");
                    currentCategory = item.category;
                }
                li = that._renderItemData(ul, item);
                if (item.category) {
                    li.attr("aria-label", item.category + " : " + item.label);
                }
            });
        }
    });

    //to include category for autocomplete results
    $("#searchInput").catcomplete({
        delay: 300,
        source: function (request, response) {
            $.ajax({
                url: "GetSearchAutocompResults",
                type: "GET",
                contentType: "application/json; charset=utf-8;",
                dataType: "json",
                data: {
                    reqData: request.term
                },
                success: function (respData) {
                    response($.map(respData, function (item) {
                        return {
                            label: item.companyName,
                            category: item.name
                        };
                    }));
                },
                error: function () {
                    alert("error in search!");
                }
            });
        },
        select: function (event, ui) {
            searchInput.val(ui.item.label);
            goSearch(searchInput.val(), getSelServCatChkBoxVal(), getSelServChkBoxVal(), 0, getSortOption());
        }
        //autoFocus: true
        //source: data
    })
        //to close autocomplete results box when "enter" is pressed.
        .keyup(function (e) {
        e.preventDefault();
        if (e.which === 13) {
            $(this).catcomplete("close");
        }
    });
    /*********** end *************/

    serchBtn.click(function (e) {
        e.preventDefault();
        goSearch(searchInput.val(), getSelServCatChkBoxVal(), getSelServChkBoxVal(), 0, getSortOption());
    });

    //function to display next set of result when "Previous" or "Next" is clicked.
    resultsDisplay.on("click", ".page-link", function (e) {
        e.preventDefault();
        var subseqPageId = $(this).attr("data-subseqPageId");
        if (subseqPageId > 0 && subseqPageId <= $("#totalPages").attr("data-totalPages") + 1) {
            goSearch(searchInput.val(), getSelServCatChkBoxVal(), getSelServChkBoxVal(), subseqPageId, getSortOption());
        } 
    });

    selectSortOption.change(function (e) {
        e.preventDefault();
        goSearch(searchInput.val(), getSelServCatChkBoxVal(), getSelServChkBoxVal(), 0, getSortOption());
    });

    $("input[type='checkbox']").change(function (e) {
        e.preventDefault();
        goSearch(searchInput.val(), getSelServCatChkBoxVal(), getSelServChkBoxVal(), 0, getSortOption());
    });


    /*======================= search function ====================*/

    //RequestVerificationToken is important to protect against CSRF. Controller with AutoValidateAntiforgeryToken attribute
    //will check if token is generated. Else will get Error 400
    function goSearch(input, servCat, serv, subseqPageIdx, sortOption) {
        $.ajax({
            url: "SearchCompanies",
            type: "POST",
            contentType: "application/json; charset=utf-8;",
            //datatype is what type Jquery ajax call expects in return.
            dataType: "html",
            data: JSON.stringify( {
                searchIn: input,
                servCatIn: servCat,
                servIn: serv,
                subseqPageIdxIn: subseqPageIdx,
                sortOptionIn: sortOption
            }),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (respData) {
                //displaySearchResults(respData);
                resultsDisplay.html(respData);
            }
        });
    }

    //get SelServCat checkbox values
    function getSelServCatChkBoxVal() {
        var chkBoxArr = [];
        $("input[name='selServCat']:checked").each(function () {
            chkBoxArr.push($(this).val());
        });
        return chkBoxArr;
    }

    //get checkbox values
    function getSelServChkBoxVal() {
        var chkBoxArr = [];
        $("input[name='selServ']:checked").each(function () {
            chkBoxArr.push($(this).val());
        });
        return chkBoxArr;
    }


    //to print company services list
    function getCompServList(obj) {
        var tempStr = '';
        $.each(obj, function (idx2, obj2) {
            tempStr += '<li>' + obj2.title + '</li>';
        });
        return tempStr;
    }

    //get sort option value
    function getSortOption() {
        return selectSortOption.val();
    }

    /*================= end ===============================*/

});