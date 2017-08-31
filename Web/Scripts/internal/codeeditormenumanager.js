function saveinputandcallback(callback, value, name) {
    debugger;
    
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
        if (window.hdbackupdata[name]) {
            $("#hd_" + name).binder(window.hdbackupdata[name]);
        }
        var keyname = $(bindmanager.htmltemplates["hd_" + name]).attr("data-key");
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
                    function (e, d) {
                        console.log(e);
                        console.log(d);
                        if ($(this).val() != "") {
                            saveinputandcallback(callback, $(this).val(), name);
                        }
                    });
            }
        }
        
        var defaultvalues = window.hdbackupdata["defaultvalues"];
       
        selectize($("#hd_" + name), claddinput,
            true);
        if (defaultvalues) {
            var defaultvalue = defaultvalues[name];
            if (defaultvalue) {
        //        $("#hd_" + name).val(defaultvalue);
                $("#hd_" + name).selectize()[0].selectize.setValue(defaultvalue);
            }
        }
        
    }
    setmenupositions(window.hdMenu);
}
function managesmenulist() {

    bindauthentications();
    bindandselectize("servernames", true, true, function (input) {
    });


}
var bindusernames = function () {
    bindandselectize("usernames", true, true, function (input) {
        bindpasswords();
    });
}
var bindpasswords = function () {
    bindandselectize("passwords", true, true, function (input) {
        binddatabses(window.hdbackupdata["defaultvalues"]["servernames"]);
        
    });
}

function createpostvalues() {
    var postdata = {};
    postdata.Encrypt = "False";
    postdata.TrustServerCertificate = "True";
    postdata.Server = window.hdbackupdata["defaultvalues"]["servernames"];
    postdata.InitialCatalog = window.hdbackupdata["defaultvalues"]["dbnames"];
    if (window.hdbackupdata["defaultvalues"]["authentications"] != "Windows Authentication") {
        postdata.Authentication = window.hdbackupdata["defaultvalues"]["authentications"];
        postdata.UID = window.hdbackupdata["defaultvalues"]["usernames"];
        postdata.Password = window.hdbackupdata["defaultvalues"]["passwords"];
    } else {
        postdata.IntegratedSecurity = "True";
    }
    return postdata;
}

var binddatabses = function () {
    var postdata = createpostvalues();
    debugger;
    requestandsavebackupfromeditorapi("DbApi/GetAllDatabases",
        function (data) {
            debugger;
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "dbnames",
        postdata,
        function () {
            bindandselectize("dbnames",
                true,
                true,
                function (input) {
                    debugger;
                    if (window.hdbackupdata["defaultvalues"]["dbnames"] && window.hdbackupdata["defaultvalues"]["dbnames"] != "") {
                        bindtables();
                    }
                });
            
        });
};function bindrenderdatatypes() {
   
    var postdata = {};
    requestandsavebackupfromeditorapi("DbApi/GetAllDataListTypes",
        function (data) {
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "datalisttypes",
        postdata,
        function (datalisttypes) {
            bindandselectize("datalisttypes",
                true,
                true,
                function(input) {

                   
                });

        });
    
}
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
                        binddatabses(window.hdbackupdata["defaultvalues"]["servernames"]);
                    } else {
                        bindusernames();
                        
                    }
                });

        });
    
}

function bindtables() {
    var postdata = createpostvalues();
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
                    function (input) {
                        if (window.hdbackupdata["defaultvalues"]["tables"] != "") {
                            bindtablecolumns();
                        }
                    });
            },true);
    
}

function bindtablecolumns() {
    var postdata = createpostvalues();
    postdata.Table = window.hdbackupdata["defaultvalues"]["tables"];
    requestandsavebackupfromeditorapi("DbApi/GetTableWithColumn",
        function (data) {
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "tablecolumns",
        postdata,
        function (tables) {
            bindandselectize("tablecolumns",
                true,
                true,
                function (input) {

                });
        }, true);
    bindrenderdatatypes();
}

$(document).ready(function () {
    
    

});