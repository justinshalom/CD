////PrepareFileContent(postdata);
var hdMenu;
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
    //$('body').append(hd_rightmenu);
    
        getallcssproperties();
        hdMenu = $('#hd_rightmenu');
        hdMenu.hide();
        $('body').on('contextmenu',
            '*',
            function(e) {
                window.hdCurrentobj = $(this);
                e.preventDefault();
                e.stopPropagation();
                var t = $(this);
                hdMenu.show();
                setmenupositions(hdMenu, t, e);
                var classeslist =
                    (t.attr('class'))
                        ? t.attr('class').split(' ')
                        : false;
                setmenubasedonattributes(hdMenu, t, classeslist, allclasses);
               // $('#hd_rightmenu_allattributes').append(newattributehtml);
                selectize();
                setmenuheader(t, classeslist);
                $('#hd_styledesigner').hide();
            });
        $.material.init();
    
    //    autotest();
});