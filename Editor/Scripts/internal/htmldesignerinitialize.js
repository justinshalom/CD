/// <reference path="../bootstrap.min.js" />
var activerequests = 0;
var queueerequests = [];
var queueerequestsi = 0;
var requesthandler = function (toappend, reqi) {
    if (!reqi) {
        reqi = -1;
    }
    if ($.inArray(toappend, queueerequests) == -1) {
        queueerequests.push(toappend);
        
    }
    if (activerequests === 0) {
        activerequests++;
        if (queueerequests[queueerequestsi]) {
            $.getScript(queueerequests[queueerequestsi], function () {
                queueerequestsi++;
                activerequests--;
            });
        }
    } else {
        setTimeout(function () {
            reqi++;
            requesthandler(toappend, reqi);
        },
            100);
    }
}
function require(category, name) {
    if (name) {
        category = category + "/" + name;
    }
    requesthandler( hd_scripturl + "/" + category + '.js');
}
function include(category, name) {
    if (name) {
        category = category + "/" + name;
    }
    $('head').prepend('<link href="' + hd_styleurl + "/" + category + '.css" rel="stylesheet" />');
}
$(document).ready(function () {
    include('bootstrap','bootstrap');
    include('material', 'bootstrap-material-design');
    include('material', 'ripples.min');
    include('material-datepicker', 'css/bootstrap-material-datetimepicker');
    include('chosen', 'chosen.min');
    include('selectize', 'css/selectize');


    var e =$.fn.jquery.split(" ")[0].split(".");
    if (e[0] < 2 && e[1] < 9 || 1 == e[0] && 9 == e[1] && e[2] < 1 || e[0] > 3) {
        require("jquery-3.1.1.min");
    }
    if (!$('.collapse').collapse) {
        require("bootstrap.min");
    }
    require("moment","moment.min");
    require("material", "arrive.min");
    require("material", "material.min");
    require("material", "ripples.min");
    require("material-datepicker", "js/bootstrap-material-datetimepicker");
    require("chosen", "chosen.jquery.min");
    require("selectize", "standalone/selectize.min");
    require("internal", "bindlist");
    require("internal", "htmldesignerhtml");
    require("internal", "htmldesignerobj");
    require("internal", "htmldesignerhtml");
    require("internal", "htmldesignerfunctions");
    require("internal", "htmldesignerstdynfn");
    require("internal", "htmldesignerststaticfn");
    require("internal", "htmldesignerstajaxfn");
    require("internal", "htmldesignerevents");
    require("internal", "htmldesigner");
});