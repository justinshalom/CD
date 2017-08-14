var hdCurrentobj = $("body");
$(document).ready(function () {
    
    var backupdata = getcache("hdbackupdata");
    if (backupdata != false) {
        window.hdbackupdata = JSONparse(backupdata);
    }
    StoreAllProperties();
    var hdMenu = $('#hd_rightmenu');
    hdMenu.hide();
        $(".hd_menubox").hide();
    $('body').on('contextmenu',
        '*',
        function(e) {
            window.hdCurrentobj = $(this);
            e.preventDefault();
            e.stopPropagation();
            var t = $(this);
            setmenupositions(hdMenu, t, e);
            managesmenulist(hdMenu, t);
            var classeslist =
                (t.attr('class'))
                    ? t.attr('class').split(' ')
                    : false;
            selectize();
            setmenuheader(t,classeslist);
            hdMenu.show();
        });
    $.material.init();
    
});