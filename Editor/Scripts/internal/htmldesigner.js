////PrepareFileContent(postdata);
function require(category, name) {
    if (name) {
        category = category + "/" + name;
    }
    $('head').append('<script src="' + hd_scripturl + "/" + category + '.js" type="text/javascript"></script>');
}
function include(category, name) {
    if (name) {
        category = category + "/" + name;
    }
    $('head').append('<link href="' + hd_styleurl + "/" + category + '.css" type="text/stylesheet" />');
}
$(document).ready(function () {

    require("internal", "htmldesignerhtml");
    require("internal", "htmldesignerinitilize");
    require("internal", "htmldesignerobj");
    require("internal", "htmldesignerfunctions");
    require("internal", "htmldesignerstdynfn");
    require("internal", "htmldesignerhtml");
    require("internal", "htmldesignerststaticfn");
    require("internal", "htmldesignerevents");
    require("internal", "htmldesignerstajaxfn");
    require("internal", "htmldesigner");
    
    getallcssrules();
        var allclasses = getall('class');
        var allid = getall('id');
        $('body').append(hd_rightmenu);
        var hd_menu = $('#hd_rightmenu');
        hd_menu.hide();
    
    $('body').
            on('contextmenu',
                '*',
                function(e) {
                    hdCurrentobj = $(this);
                    e.preventDefault();
                    e.stopPropagation();
                    var t = $(this);
                    hd_menu.show();
                    setmenupositions(hd_menu);
                    var classeslist =
                        (t.attr('class'))
                            ? t.attr('class').split(' ')
                            : false;
                    setmenubasedonattributes(hdmenu, t, classeslist, allclasses);
                    $('#hd_rightmenu_allattributes').append(newattributehtml);
                    selectise();
                    setmenuheader(t,classeslist);
                });
        //autotest();
    });