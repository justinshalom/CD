$.hd = {
    issolved: false,
    SolveDependency: function(postdata) {
        if (postdata.issolved) {
            this.issolved = true;
            return true;
        }
        $.post(rooturl + "Json/SolveDependency",
            postdata,
            function(data) {
                if (!data.IsError) {
                    //originalfile = data.Data;
                } else {
                    $('#editoriframe').attr('src', $('#editoriframe').attr('src'));
                }
            });
    },
    setglobalvariables: function(data) {
        hd_postdata.area = data.area;
        hd_postdata.controllername = data.controller;
        hd_postdata.view = data.action;
    }
};
window.addEventListener('message',
    function(e) {
        var key = e.message ? 'message' : 'data';
        var data = e[key];
        if (typeof $.hd[data.method] == 'function') {
            $.hd[data.method](data);
        }
    },
    false);
var hd_postdata = {};
$(document).ready(function() {
    $('#editoriframe').on("load",
        function() {
            setTimeout(function() {
                    $.hd.SolveDependency(hd_postdata);
                },
                1000);
        });
});