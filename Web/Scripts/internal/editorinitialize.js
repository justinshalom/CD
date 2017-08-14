function loadtemplate() {
        var getData = {};
        getData.editormode = window.getcache("editormode");
        getData.isjquery =true;
        getData.isbootstrap =true;
        var e = $.fn.jquery.split(" ")[0].split(".");
        if (e[0] < 2 && e[1] < 9 || 1 == e[0] && 9 == e[1] && e[2] < 1 || e[0] > 3) {
            getData.isjquery =false;
        }
        if (!$('.collapse').collapse) {
            getData.isbootstrap = false;
        }
        $.get(window.cd_rooturl + "visual/menutemplate",getData,
            function(html) {
                $("body").append(html);
            });
}

$(document).ready(function() {
    loadtemplate();
});