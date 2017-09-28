$(document).ready(function () {
    $("body").on("click",
        "[data-key='DirectoryName']:not('data-opened')",
        function() {
            var fullpath = $(this).attr("data-fullpath");
            $(this).attr("data-opened", true);
            var li=$(this).closest("li");
            li.append("<ul class='hdlist-group hddirectory' data-json='" + $("#hd_rightmenu_public_variables_directory_list").attr("data-json") +"?directoryName="+fullpath+"'>" +
                bindmanager.htmltemplates.hd_rightmenu_public_variables_directory_list +
                "</ul>");
            li.find("[data-json]").binder();

        });
   
})