////PrepareFileContent(postdata);

$(document).ready(function () {
   
    //$('body').
    //  css("overflow", "hidden");
    $(window).
        resize(function () {
           // $('#editoriframe').height(window.innerHeight);
        });
    $('#editoriframe').on("load", function () {
        //$('#editoriframe').height(window.innerHeight);
    });
    $('#editoriframe').height("1000");
    var allclasses = getall('class');
    var allid = getall('id');
    $('body').append(hd_rightmenu);
    getallcssproperties();

    
    var hd_menu = $('#hd_rightmenu');
    hd_menu.hide();

    $('body').
            on('contextmenu',
                '*',
                function (e) {

                    hdCurrentobj = $(this);
                    e.preventDefault();
                    e.stopPropagation();
                    var t = $(this);
                    hd_menu.show();
                    setmenupositions(hd_menu, t, e);
                    var classeslist =
                        (t.attr('class'))
                            ? t.attr('class').split(' ')
                            : false;
                    setmenubasedonattributes(hd_menu, t, classeslist, allclasses);
                    $('#hd_rightmenu_allattributes').append(newattributehtml);
                    selectize();
                    setmenuheader(t, classeslist);
                    $('#hd_styledesigner').hide();


                });

    $.material.init();
    autotest();

});