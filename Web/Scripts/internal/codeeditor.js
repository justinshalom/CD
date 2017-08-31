
var hdCurrentobj = $("body");
function hdhandleclick(th) {
    var t = th;
    var classeslist =
        (t.attr('class'))
            ? t.attr('class').split(' ')
            : false;

    setmenuheader(t, classeslist);
    window.hdMenu.find(".hdpanel-body").show();
}
$(document).ready(function () {
   
    var backupdata = getcache("hdbackupdata");
    if (backupdata != false) {
        window.hdbackupdata = JSONparse(backupdata);
    }
    StoreAllProperties();
    window.hdMenu = $('#hd_rightmenu');
    window.hdMenu.hide();
    $(".hd_menubox").hide();
    $(".hd_rightmenu").show();
    $(".hd_rightmenu .hdpanel-body").hide();
   
    $('body').on('click',
        '.hdpanel-heading',
        function (e) {
            $(".hdpanel-body").hide();
            window.hdMenu = $(this).closest(".hd_rightmenu");
            $(this).closest(".hd_rightmenu").find(".hdpanel-body").toggle();
        });
    

    managesmenulist();
    $(".hd_menubox").show();
    
    $('body').on('contextmenu click',
        '*:not(html,body,.hd_rightmenu)',
        function (e) {
            //e.preventDefault();
            //e.stopPropagation();
            var t = $(this);
            hdhandleclick(t);
        });
    $.material.init();
    
});