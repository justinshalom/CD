loadscriptquee = [];
var loadscript = function (loadscriptqueei) {
    $.ajaxSetup({
        // Disable caching of AJAX responses
        cache: true
    });
    $.getScript(loadscriptquee[loadscriptqueei],
        function () {
            loadscriptqueei++;
            if (loadscriptquee[loadscriptqueei]) {
                    loadscript(loadscriptqueei);
            } else {
                $("title").text("Editor");
            }

        });
}


function loadtemplate() {
    var getData = {};
    var editorMode = window.getcache("editormode");
    if (!editorMode) {
        
    }
        getData.editormode = window.getcache("editormode");
        getData.isjquery =true;
        getData.isbootstrap = true;
   
        var e = $.fn.jquery.split(" ")[0].split(".");
        if (e[0] < 2 && e[1] < 9 || 1 == e[0] && 9 == e[1] && e[2] < 1 || e[0] > 3) {
            getData.isjquery =false;
        }
        if (!$('.collapse').collapse) {
            getData.isbootstrap = false;
        }
    
        $.get(window.cd_rooturl + "visual/menutemplate",getData,
            function (html) {
              
                var htmloobj=$(html);

                htmloobj.each(function () {
    

                    switch ($(this)[0].tagName) {
                    case "script":
                        case "SCRIPT":
                        {
                            loadscriptquee.push($(this).attr("src"));
                            if (loadscriptquee.length === 1) {
                                loadscript(0);
                            }
                                
                            break;
                        }
                            default:
                            {
                                $("body").append($(this)[0].outerHTML);            
                            }
                        }
                    
                });

                
                
            });
}

$(document).ready(function() {
    loadtemplate();
});