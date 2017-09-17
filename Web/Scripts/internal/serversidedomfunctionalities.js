var hd = {
    issolved: false,
    SolveDependency: function(postdata) {
        $.post(window.cd_rooturl + "Json/SolveDependency",
            postdata,
            function (data) {
                setcache(postdata.uniquekey, true);
                if (!data.Data) {
                    notify({ message: "Dependency Not Solved", type: "e" });
                  
                    //originalfile = data.Data;
                } else {
                   
                    notify({ message: "Dependency Solved", type: "e" });
                    window.location.href = window.location.href;
                }
            });
    },
    attribute: function (postdata) {
        
    }
    
};
var hd_postdata = {};
$(document).ready(function () {
    var uniquekey = window.hd_area + "_" + window.hd_controller + "_" + window.hd_function;
    //hd_postdata.uniquekey = uniquekey + "_layoutdependency";
    //if (!getcache(postdata.uniquekey)) {
    //    hd.SolveDependency(hd_postdata);
    //}
    //hd_postdata = {};
    hd_postdata.uniquekey = uniquekey + "_viewdependency";
  
    if (!getcache(hd_postdata.uniquekey))
    {
        hd_postdata.area = window.hd_area;
        hd_postdata.controllername = window.hd_controller;
        hd_postdata.view = window.hd_function;
        hd.SolveDependency(hd_postdata);
    }
});
