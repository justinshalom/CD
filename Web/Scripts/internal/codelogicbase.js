var hdeditor;


$(document).ready(function () {
    var hdcategorylevelshtml=$(".hdcategorylevels:first").closest(".hdform-group")[0].outerHTML

    $("body").on("change",
        ".hdcategorylevels",
        function() {

            if ($(this).closest(".hdform-group").next(".hdform-group").length == 0) {
                $(this).closest(".hdform-group").after(hdcategorylevelshtml);
            }

        });
   
    var hdidentifiersList = [];
    var hdfillpathvalueobject;
    $("body").on("click",
        "[data-key='DirectoryName']",
        function() {
            var fullpath = $(this).attr("data-fullpath");
            if (hdfillpathvalueobject) {
                hdfillpathvalueobject.val(fullpath).trigger("change");
                
            }


        });

    $("body").on("click",
        "[data-key='FileName']",
        function() {
            var fullpath = $(this).attr("data-fullpath");
            if (hdfillpathvalueobject) {
                hdfillpathvalueobject.attr("data-isfile", true);
                hdfillpathvalueobject.val(fullpath).trigger("change");
            }
            var extension = $(this).attr("data-ext");
            var postData = {};
            postData.filePath = fullpath;
            $.post(window.cd_rooturl + "FileApi/GetJson",
                postData,
                function (data) {
                    var code = data.Data;

                    //$("#hd_publiceditor").hide();
                    //$("#hd_publiceditor").text(code);                   
                    $("#hd_publiceditortextarea").val(code);
                    //$("#hd_rightmenu_public_variables_header").trigger("click");
                    
                    hdeditor = window.CodeMirror.fromTextArea($("#hd_publiceditortextarea")[0], {
                      lineNumbers: true,
                      styleActiveLine: true,
                        lineWrapping: true,
                        matchBrackets: true,
                        mode: "text/x-csharp",
                        gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                    });
                  
                }
            );

        });
    $("body").on("afterappendcomplete",
        "#hd_classidentifiers_list",
        function (e,data) {
            hdidentifiersList = data;
        });

  $("body").on("change",
    "#hd_theme",
    function (e) {
if(hdeditor){
    var theme = $(this).find("option:selected").text();

      hdeditor.setOption("theme", theme);
}
    });
    
    $("body").on("click",
        ".hdremovethislevel",
        function (e) {
            if ($(".hd_codestructurebox").length > 1) {
                $(this).closest(".hd_codestructurebox").remove();
               
            }
        });
    $("body").on("focus",
        ".hdcodepathinput",
        function (e) {
            hdfillpathvalueobject = $(this);
        });
    $("body").on("focus",
        ".hdcodepathinput",
        function (e) {
            hdfillpathvalueobject = $(this);
            var th = hdfillpathvalueobject;
            
            if (th.attr("data-isfile") == "true") {
                var keywords = getallkeywords();
                generatecodefromkeywords(keywords, hdfillpathvalueobject.val());

                $.post(window.cd_rooturl + "FileApi/GetJson",
                    postdata,
                    function(data) {
                        var code = data.Data;
                        if (code) {
                            var edit = th.closest(".hd_codestructurebox").find(".hdcodestructureinput");
                            edit.val(code);
                            //hdeditor = window.CodeMirror.fromTextArea(edit[0],
                            //    {
                            //        lineNumbers: true,
                            //        styleActiveLine: true,
                            //        lineWrapping: true,
                            //        matchBrackets: true,
                            //        mode: "text/x-csharp",
                            //        gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                            //    });
                        } else {

                        }

                    });
            }
        });
    $("body").on("focus change blur",
        "textarea:not(.hdcodepathinput),input:not(.hdcodepathinput),select:not(.hdcodepathinput)",
        function (e) {
            hdfillpathvalueobject = false;

        });
    
    $("body").on("change blur",
        "#hd_rightmenu_code_structure input,#hd_rightmenu_code_structure textarea,#hd_rightmenu_code_structure input,#hd_rightmenu_code_structure select",
        function (e) {
            var jsonobj = {};
            jsonobj.categories = [];
            jsonobj.structure = [];
            $(".hdcategorylevels").each(function () {
                jsonobj.categories.push($(this).val());
            });

            $("#hd_codestructurelist .hd_codestructurebox").each(function () {
                var obj = {};
                $(this).find("input,select,textarea").each(function() {
                    var classname = $(this).attr("class").replace("hdform-control", "").replace(/ /ig, "");
                    obj[classname] = $(this).val();

                });
                jsonobj.structure.push(obj);
            });

            var postdata = {};
            postdata.json = JSON.stringify(jsonobj);
            postdata.filename = $("#hdprofilename").val();
            $.post(window.cd_rooturl + "FileApi/SaveProfile",
                postdata,
                function () {
                    
                });
        });
   
    $(".hd_codestructurebox").find(".hdform-control:not(.hdcoderoute,.hdcodepathinput)").hide();
    $("body").on("change",
        ".hdcoderoute",
        function (e) {
            var value = $(this).val();
            $(this).closest(".hd_codestructurebox").find(".hdform-control:not(.hdcoderoute,.hdcodepathinput)").hide();

            switch (value) {
                case "newfolder":
                    {
                    
                    break;
                    }
                    
                case "extclass":
                {
                  
                  $(this).closest(".hd_codestructurebox").find(".hdcoderegionname,.hdcodecustomnamespace,.hdcodestructureinput,.hdcodestructurepreview,.hdcoderegionname").show();
                    break;
                }
                case "newclass":
                {
                  $(this).closest(".hd_codestructurebox").find(".hdcoderegionname,.hdcodecustomnamespace,.hdcodestructureinput,.hdcodestructurepreview,.hdcoderegionname").show();
                    break;
                }

                case "extmethod":
                {
                    $(this).closest(".hd_codestructurebox").find(".hdcodelinenumber").show();
                    $(this).closest(".hd_codestructurebox").find(".hdcodeinsidecondition").show();
                    break;
                }
            default:
                {
                    
                    
                   
                $(this).closest(".hd_codestructurebox").find(".hdcodestructureinput").show();
                break;
            }
            }
        });
    
    ////function cachehdRightmenuCodeStructure() {
    ////    setcache("cachehdRightmenuCodeStructure", $("#hd_rightmenu_code_structure").html());






    ////}
    ////if (getcache("cachehdRightmenuCodeStructure")) {
    ////    $("#hd_rightmenu_code_structure").html(getcache("cachehdRightmenuCodeStructure"));
    ////}
    ////function cachehdRightmenuPublicVariablesDirectoryAllattributes() {
    ////    setcache("cachehdRightmenuPublicVariablesDirectoryAllattributes", $("#hd_rightmenu_public_variables_directory_allattributes").html());
    ////}
    ////if (getcache("cachehdRightmenuPublicVariablesDirectoryAllattributes")) {
    ////    $("#hd_rightmenu_public_variables_directory_allattributes").html(getcache("cachehdRightmenuPublicVariablesDirectoryAllattributes"));
    ////}
    $("body").on("change blur keyup",
        "#hd_rightmenu_code_structure input,#hd_rightmenu_code_structure textarea",
        function (e) {
            var value = $(this).val();
            $(this).attr("value", value);
            ////cachehdRightmenuCodeStructure();
        });

    $("body").on("change blur keyup",
        "#hd_rightmenu_code_structure select",
        function (e) {
          $(this).find("option").not($(this).find("option:selected")).removeAttr("selected");
            $(this).find("option:selected").attr("selected", true);
            ////cachehdRightmenuCodeStructure();
        });

    function continuegenerate(eq) {
      eq++;
        if ($(".hd_codestructurebox").eq(eq).length > 0) {
            generatecode(eq);
        }
    }

    function getallkeywords() {
        var keywords = [];
        $(".hdcategorylevels").each(function () {
            var value = $(this).val();
            keywords.push(value);
        });
        return keywords;
    }

    function generatecodefromkeywords(keywords,stringvalue) {
      if (!stringvalue) {
        stringvalue = "";
      }
        if (!keywords) {
                keywords = getallkeywords();
        }
        $.each(keywords,
            function(i, v) {
                var reg = new RegExp("{KeyWord" + (i+1) + "}", "ig");

                stringvalue = stringvalue.replace(reg, v);

            });
        return stringvalue;
    }

    function generatecode(eq) {
        debugger;
        var postdata = {};
        postdata.profilename = $("#hdprofilename").val();
        postdata.keywords = getallkeywords();
        var th = $(".hd_codestructurebox").eq(eq);
        var hdcoderoutevalue = generatecodefromkeywords(postdata.keywords,th.find(".hdcoderoute").val());
        var hdcodelinenumber = generatecodefromkeywords(postdata.keywords,th.find(".hdcodelinenumber").val());
        var hdcodeinsidecondition = generatecodefromkeywords(postdata.keywords,th.find(".hdcodeinsidecondition").val());
        var hdcodestructureinput = generatecodefromkeywords(postdata.keywords,th.find(".hdcodestructureinput").val());
        var hdcodepathinput = generatecodefromkeywords(postdata.keywords,th.find(".hdcodepathinput").val());

            switch (hdcoderoutevalue) {
            case "newfolder":
            {
                postdata.folderPath = hdcodepathinput;
                $.post(window.cd_rooturl + "FileApi/CreateDirectory",
                    postdata,
                    function () {
                        continuegenerate(eq);
                    });
                break;
            }   
            case "extclass":
                {
                postdata.filePath = hdcodepathinput;
                $.post(window.cd_rooturl + "FileApi/GetJson",
                    postdata,
                    function(data) {
                        var code = data.Data;
                        if (code) {
                          

                        }

                    });
                break;
            } case "newclass":
            {
                postdata.filePath = hdcodepathinput;
                postdata.fileContent =hdcodestructureinput;
                $.post(window.cd_rooturl + "FileApi/SaveCode",
                    postdata,
                    function (data) {
                      var success = data.Data;
                      continuegenerate(eq);
                    });
                break;
            } case "newmethod":
            {
                postdata.filePath = hdcodepathinput;
                $.post(window.cd_rooturl + "FileApi/GetJson",
                    postdata,
                    function (data) {
                        var code = data.Data;
                        if (code) {

                        }


                    });
                break;
            } case "extmethod":
            {
                postdata.filePath = hdcodepathinput;
                $.post(window.cd_rooturl + "FileApi/GetJson",
                    postdata,
                    function (data) {
                        var code = data.Data;
                        if (code) {

                        }


                    });
                break;
            } 
            default:
            {

                break;
            }
            }
        
    }

    $("body").on("click",
        "#hdcodegeneratebtn",
        function (e) {
            generatecode(0);

        });


})