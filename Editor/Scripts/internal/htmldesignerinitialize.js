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
    require("internal", "htmldesignerobj");
    require("internal", "htmldesignerfunctions");
    require("internal", "htmldesignerstdynfn");
    require("internal", "htmldesignerhtml");
    require("internal", "htmldesignerststaticfn");
    require("internal", "htmldesignerevents");
    require("internal", "htmldesignerstajaxfn");
    require("internal", "htmldesigner");
});