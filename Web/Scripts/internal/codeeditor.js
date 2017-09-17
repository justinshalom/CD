
var hdCurrentobj = $("body");
function hdhandleclick(th) {
    hdCurrentobj = th;
    var t = th;
    var classeslist =
        (t.attr('class'))
            ? t.attr('class').split(' ')
            : false;

    setmenuheader(t, classeslist);
    window.hdMenu.find(".hdpanel-body").show();


}
$(document).on('beforerequest',
    'select.selectized[data-json]',
    function (e) {
        $(this).selectize()[0].selectize.destroy();
    });

$(document).on('change',
    'input[list]',
    function (e, data) {
        if ($(this).val() != "") {
        if($(this).attr("id")){
            var selectname = $(this).attr("id").replace("hd_", "");
            var keyname = $(bindmanager.htmltemplates["hd_" + selectname + "_list"]).attr("data-key");
            pushandsave(selectname, keyname, $(this).val());
            saveinputandcallback(selectname, $(this).val());
        }
        }
    });
$(document).on('afterappendcomplete',
    'datalist[data-json]',
    function (e, data) {
        if (!$(this).attr("data-disabledefaultvalues")) {
            var selectname = $("[list=" + $(this).attr("id") + "]").attr("id").replace("hd_", "");

            var defaultvalues = window.hdbackupdata["defaultvalues"];
            if (defaultvalues) {
                var defaultvalue = defaultvalues[selectname];
                if (defaultvalue) {
                    $("#hd_" + selectname).val(defaultvalue);
                    if ($("#hd_" + selectname).val() == null) {
                        $("#hd_" + selectname).append("<option>" + defaultvalue + "</option>");
                        $("#hd_" + selectname).val(defaultvalue);
                    }
                    // console.log(selectname + "valueis" + $("#hd_" + selectname).val());
                    //$("#hd_" + name).selectize()[0].selectize.setValue(defaultvalue);
                }
            }
            if ($("#hd_" + selectname).val() != "") {
                $("#hd_" + selectname).trigger("change");
            }
        }


        //selectize($("#hd_" + selectname),
                //    function(input) {
                //        pushandsave(selectname, keyname, input);
                //        return {
                //            value: input,
                //            text: input
                //        }
                //    },
                //    false);
        
        
    });
//$('select[data-bindersuccess="true"]').trigger("beforerequest");
//$('select[data-bindersuccess="true"]:not(.selectized)').trigger("afterappendcomplete");
$(document).ready(function () {
    
    var backupdata = getcache("hdbackupdata");
    if (backupdata != false) {
        window.hdbackupdata = JSONparse(backupdata);
    }
    $(".hd_menubox").hide();
    $(".hd_rightmenu").show();
    $(".hd_rightmenu .hdpanel-body").hide();

    if (getdefaultvalue("defaulttab")) {
        window.hdMenu = $('#' + getdefaultvalue("defaulttab"));
        window.hdMenu.find(".hdpanel-body").show();
    } else {
        window.hdMenu = $('#hd_rightmenu');
    }
    
    
    
    $('body').on('click',
        '.hdpanel-heading',
        function (e) {
            var th = $(this).closest(".hd_rightmenu").find(".hdpanel-body");
            $(".hdpanel-body").not(th).hide();
            window.hdMenu = $(this).closest(".hd_rightmenu");
            th.toggle();
            saveinputandcallback("defaulttab", window.hdMenu.attr("id"));
        });
   
  

    $("#hd_integratedsecurity_box").show();
    $(".hd_menubox").show();
    var allclasses = getall('class');

    
    setallcssproperties();


    $('body').on('contextmenu',
        '[data-genid]:not(html,body,.hd_rightmenu,.hd_rightmenu *)',
        function (e) {
            $("#hd_styledesigner").hide();
            e.preventDefault();
            e.stopPropagation();
            var t = $(this);
            var classeslist =
                (t.attr('class'))
                    ? t.attr('class').split(' ')
                    : false;
            hdhandleclick(t);

            
            setmenubasedonattributes(hdMenu, t, classeslist, allclasses);
        });
    $.material.init();


    
});