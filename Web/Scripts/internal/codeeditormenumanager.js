function saveinputandcallback(callback, value, name) {
    
    
    if (!window.hdbackupdata["defaultvalues"]) {
        window.hdbackupdata["defaultvalues"] = {};
    }
    window.hdbackupdata["defaultvalues"][name] = value;

    savehdbackupdata();
    callback(value);
}
function bindandselectize(name, ispushandsave, iscladdinput, callback) {

    $("#hd_" + name + "_box").show();
    if (window.hdbackupdata) {
        var selectinput = $("#hd_" + name);

        var keyname = selectinput.find("option[data-key]:first").attr("data-key");

        if (window.hdbackupdata[name]) {
            $("#hd_" + name).binder(window.hdbackupdata[name]);
            
        }
        var claddinput=false;
        if (iscladdinput) {
            claddinput = function(input) {
                if (ispushandsave) {
                    pushandsave(name,keyname, input);
                }
                if (callback) {
                    saveinputandcallback(callback, input, name);
                }
                return {
                    value: input,
                    text: input
                }
            };
        }
        if (callback) {
            if (!$("#hd_" + name).attr("changeeventinitialized")) {
                $("#hd_" + name).attr("changeeventinitialized", true);
                $("body").on("change",
                    "#hd_" + name,
                    function () {
                        if ($(this).val() != "") {
                            saveinputandcallback(callback, $(this).val(), name);
                        }
                    });
            }
        }
        
        var defaultvalues = window.hdbackupdata["defaultvalues"];
        if (defaultvalues) {
            var defaultvalue = defaultvalues[name];
            if (defaultvalue && defaultvalue.value) {
                $("#hd_" + name).val(defaultvalue.value);
            }
        }
        selectize($("#hd_" + name), claddinput,
            true);
        $("#hd_" + name).trigger("change");
    }
}
function managesmenulist(hdMenu, t) {

    //var formelement =
    //    '<input type="text"   data-attributename="' +
    //        attributename +
    //        '" class="hdform-control input-sm" value = "' +
    //        attributevalue +
    //        '"/>';


   
    bindandselectize("servernames", true, true, function (input) {
      
        bindauthentications();
        binddatabses(input);
    });


}
var bindusernames = function () {
    bindandselectize("usernames", true, true, function (input) {
        bindpasswords();
    });
}
var bindpasswords = function () {
    bindandselectize("passwords", true, true, function (input) {
        bindtables();
    });
}
var binddatabses = function (servername) {
    var postdata = {};
    postdata.serverName = servername;
    requestandsavebackupfromeditorapi("DbApi/GetAllDatabases",
        function (data) {
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "dbnames",
        postdata,
        function (dbnames) {
            bindusernames();
        });
};
function bindauthentications() {
   
    var postdata = {};
    postdata.serverType = "mssql";
    requestandsavebackupfromeditorapi("DbApi/GetAllAuthenticationTypes",
        function (data) {
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "authentications",
        postdata,
        function (authentications) {
            bindandselectize("authentications",
                true,
                true,
                function(input) {

                    if (window.hdbackupdata["defaultvalues"]["authentications"] == "Windows Authentication") {
                        bindtables();
                    }
                });

        });
    
}

function bindtables() {
    var postdata = {};

    postdata.IntegratedSecurity = "True";

        postdata.Server = window.hdbackupdata["defaultvalues"]["servernames"];
        postdata.InitialCatalog = window.hdbackupdata["defaultvalues"]["dbnames"];
        if (window.hdbackupdata["defaultvalues"]["authentications"] != "Windows Authentication") {
            postdata.Authentication = window.hdbackupdata["defaultvalues"]["authentications"];
        }

        requestandsavebackupfromeditorapi("DbApi/GetAllTables",
            function (data) {
                if (data && data.Data) {
                    return data.Data;
                }
                return false;
            },
            "tables",
            postdata,
            function (tables) {
                bindandselectize("tables",
                    true,
                    true,
                    function(input) {

                    });
            });
    
}

$(document).ready(function () {
    
    

});