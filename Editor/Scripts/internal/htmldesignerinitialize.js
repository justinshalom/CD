/// <reference path="../bootstrap.min.js" />
var activerequests = 0;
var queueerequests = [];
var queueerequestscallbacks = [];
var queueerequestsi = 0;
var queueerequestsmethods = [];
var requesthandler = function (toappend, reqi,method,callbackfn) {
  
    if (!reqi) {
        reqi = -1;
    }
    if ($.inArray(toappend, queueerequests) == -1) {
        queueerequests.push(toappend);
        queueerequestscallbacks.push(callbackfn);
        queueerequestsmethods.push(method);
    }
   
    if (activerequests === 0) {
        activerequests++;
        if (queueerequests[queueerequestsi]) {
           
            var setajax = false;
           
            if ($.ajaxSettings.cache == false) {
                $.ajaxSetup({
                    cache: true
                });
                setajax = true;
            }
            $[queueerequestsmethods[queueerequestsi]](queueerequests[queueerequestsi], function (response) {
                if (setajax) {
                    $.ajaxSetup({
                        cache: false
                    });
                }
                if (queueerequestscallbacks[queueerequestsi]) {
                    queueerequestscallbacks[queueerequestsi](response);
                }
                console.log(queueerequests.length - 1);
                console.log(queueerequestsi);
               
                if (queueerequests.length - 1 === queueerequestsi) {
                    setTimeout(function() {
                            $("#resultLoading").css("visibility", "visible");
                        },
                        20000);
                } else {
                        $("#resultLoading").css("visibility", "hidden");
                    }
                
                queueerequestsi++;
                activerequests--;
              
            }).always(function (e, response) {
                
                
            });
        }
    } else {
        setTimeout(function () {
            reqi++;
            requesthandler(toappend, reqi, method);
        },
            100);
    }
}
function require(category, name) {
    if (name) {
        category = category + "/" + name;
    }
    requesthandler( hd_scripturl + "/" + category + '.js',false,"getScript");
}
function include(category, name) {
    if (name) {
        category = category + "/" + name;
    }
    $('head').prepend('<link href="' + hd_styleurl + "/" + category + '.css" rel="stylesheet" />');
}
$(document).ready(function () {
    include('awe', 'css/font-awesome.min');
   //include('bootstrap','bootstrap.min');
    include('material', 'bootstrap-material-design.min');
    include('material', 'ripples.min');
    include('material-datepicker', 'css/bootstrap-material-datetimepicker');
    //include('chosen', 'chosen.min');
    include('selectize', 'css/selectize');
    include('hd_styles');


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
    //require("chosen", "chosen.jquery.min");
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