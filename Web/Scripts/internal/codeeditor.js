
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

$(document).on('change blur',
    'input[id]',
    function (e, data) {
        if ($(this).val() != "") {
        if($(this).attr("id")){
            var selectname = $(this).attr("id").replace("hd_", "");
            var keyname = window.bindmanager && bindmanager.htmltemplates? $(bindmanager.htmltemplates["hd_" + selectname + "_list"]).attr("data-key"):false;
            if (!keyname) {
                selectname = $(this).attr("id");
                keyname = selectname;
            }
            pushandsave(selectname, keyname, $(this).val());
            saveinputandcallback(selectname, $(this).val());
        }
        }
    });
$(document).on('afterappendcomplete',
    'datalist[data-json]',
    function (e, data) {
        if (!$(this).attr("data-disabledefaultvalues")&&$("[list=" + $(this).attr("id") + "]").attr("id")) {
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
var hdCodestructurelist = $("#hd_codestructurelist").html();

$("body").on("click",
    ".hdaddnewlevel",
    function (e) {
        if (hdCodestructurelist) {
            $(this).closest(".hd_codestructurebox").after(hdCodestructurelist);

        }
    });
$(document).on("change",
    "#hdprofilename",
    function (e) {
        var postdata = {};
        postdata.filename = $("#hdprofilename").val();
        $.post(window.cd_rooturl + "FileApi/GetProfile",
            postdata,
            function (result) {
                if (result && result.Data) {
                    var jsondata=JSON.parse(result.Data)
                    var jsonobj = jsondata.structure;
                    $.each(jsonobj,
                        function (ji, jv) {
                            var obj = $("#hd_codestructurelist .hd_codestructurebox").eq(ji);
                            if (obj.length === 0) {
                                $(".hdaddnewlevel:last").trigger("click");
                            }
                            obj = $("#hd_codestructurelist .hd_codestructurebox").eq(ji);
                            $.each(jv,
                                function (ki, kv) {
                                    obj.find("." + ki).val(kv).trigger("change");
                                });
                        });
                    var jsonobjcat = jsondata.categories;
                    $.each(jsonobjcat,
                        function (ci, cv) {
                            var obj = $(".hdcategorylevels").eq(ci);
                            obj.val(cv).trigger("change");
                        });
                }
            });
    });
$(document).ready(function () {
    
    var backupdata = getcache("hdbackupdata");
    if (backupdata != false) {
        window.hdbackupdata = JSONparse(backupdata);
    }


    $.each(window.hdbackupdata["defaultvalues"],
        function (di, dv) {
            
            if ($("#" + di).length > 0) {
               
                $("#" + di).val(dv).trigger("change");
            }

        });
   


    $(".hd_menubox").hide();
    $(".hd_rightmenu").show();
    $(".hd_rightmenu .hdpanel-body").hide();

    if (getdefaultvalue("defaulttab")) {
        window.hdMenu = $('#' + getdefaultvalue("defaulttab"));
        window.hdMenu.find(".hdpanel-body").show();
    } else {
        window.hdMenu = $('#hd_rightmenu');
    }

    $(".hd_rightmenu").css("width", "auto");
    window.hdMenu.css("width", window.hdMenu.attr("max-width") + "px");
    var currentleft = 0;
    $(".hd_rightmenu").each(function () {
        $(this).css("left", currentleft + "px");
        currentleft += parseInt($(this).width());
    });
    
    
    $('body').on('click',
        '.hdpanel-heading',
        function (e) {
        
            var th = $(this).closest(".hd_rightmenu").find(".hdpanel-body");
            $(".hdpanel-body").not(th).hide();
            window.hdMenu = $(this).closest(".hd_rightmenu");
            th.toggle();
            saveinputandcallback("defaulttab", window.hdMenu.attr("id"));
            var currentleft = 0;
            $(".hd_rightmenu").css("width", "auto");
            window.hdMenu.css("width", window.hdMenu.attr("max-width") + "px");
            $(".hd_rightmenu").each(function() {

              $(this).css("left", currentleft + "px");
              currentleft += parseInt($(this).width());
            });

          
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