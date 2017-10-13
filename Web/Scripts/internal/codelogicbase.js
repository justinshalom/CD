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
                    $("#hd_rightmenu_public_variables_header").trigger("click");
                    
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


})