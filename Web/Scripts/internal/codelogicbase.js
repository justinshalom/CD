$(document).ready(function () {

    ////var postData = {};
    ////postData.Language = "CSharp";
    ////postData.Extension = "cs";
    ////postData.FileName = "Sample";
    ////postData.ClassName = "Sample";
    ////postData.NameSpaceName = "Samples";
    ////postData.Path = "SampleDirective";
    //// postData.Imports=[];
    ////postData.Imports[0] = "System";
    ////postData.MethodName = "ToString";
    ////postData.ReturnType="System.string";

    ////$.post(window.cd_rooturl + "GenerateCodeLogicApi/CreateClass", postData,
    ////    function (data) {
           
    ////    }
    ////);




    ////var postData = {};
    ////postData.Language = "CSharp";
    ////postData.Extension = "cs";
    ////postData.FileName = "Sample";
    ////postData.ClassName = "Sample";
    ////postData.NameSpaceName = "Samples";
    ////postData.Path = "SampleDirective";
    ////postData.Imports = [];
    ////postData.Imports[0] = "System";
    ////postData.MethodName = "ToString";
    ////postData.ReturnType = "System.string";


    ////$.post(window.cd_rooturl + "GenerateCodeLogicApi/AddMethod", postData,
    ////    function (data) {
            
    ////    }
    ////);
    var hdidentifiersList = [];
    $("body").on("click",
        "[data-key='FileName']",
        function() {
            var fullpath = $(this).attr("data-fullpath");
            var extension = $(this).attr("data-ext");
            var postData = {};
            postData.filePath = fullpath;
            $.post(window.cd_rooturl + "FileApi/GetJson",
                postData,
                function (data) {
                    var code = data.Data;

                    $("#hd_publiceditor").hide();
                    //$("#hd_publiceditor").text(code);                   
                    $("#hd_publiceditortextarea").val(code);
                    code = $("#hd_publiceditor").text();
                   
                    
                    
                    

                    ////$.each(hdidentifiersList.rows,
                    ////    function (id, identifierslist) {
                    ////        $.each(identifierslist,
                    ////            function(i, v) {
                    ////                if (v.identifiername) {
                    ////                    var replacetext = "";
                    ////                    if (typeof v.prefix=='undefined') {
                    ////                        v.prefix=" ";
                    ////                    }
                    ////                    replacetext += v.prefix;
                    ////                    replacetext +=  v.identifiername;
                                        
                    ////                        if (typeof v.postfix=='undefined') {
                    ////                        v.postfix = " ";
                    ////                    }
                    ////                    replacetext += v.postfix;
                                       
                    ////                    var replace = new RegExp(replacetext, "ig");
                                      
                    ////                    code = code.replace(replace,
                    ////                        v.prefix + "<span class='hd" + v.identifiername + " hd" + id + "'>" + v.identifiername + "</span>" + v.postfix);
                    ////                }
                    ////            });
                    ////    });
                     
                    ////$("#hd_publiceditor").html("<code class='" + extension + "'>" + code + "</code>");
                    $("#hd_rightmenu_public_variables_header").trigger("click");
                    
                    window.editor = CodeMirror.fromTextArea($("#hd_publiceditortextarea")[0], {
                        lineNumbers: true,
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

    

})