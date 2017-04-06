////PrepareFileContent(postdata);
$(document).ready(function() {
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