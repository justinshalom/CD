function saveinputandcallback(name,value, callback) {
    if (!window.hdbackupdata["defaultvalues"]) {
        window.hdbackupdata["defaultvalues"] = {};
    }
    window.hdbackupdata["defaultvalues"][name] = value;
    savehdbackupdata();
    if (callback) {
        callback(value);
    }
}
function getdefaultvalue(name) {
    if (!window.hdbackupdata["defaultvalues"]) {
        window.hdbackupdata["defaultvalues"] = {};
        return false;
    }
    return window.hdbackupdata["defaultvalues"][name];
}

function bindandselectize(name, ispushandsave, iscladdinput, callback) {

    $("#hd_" + name + "_box").show();
    if (window.hdbackupdata) {
        if (window.hdbackupdata[name]) {
          //  $("#hd_" + name).binder(window.hdbackupdata[name]);
        }
    }
    var keyname = $(bindmanager.htmltemplates["hd_" + name]).attr("data-key");
        var claddinput = false;
        if (iscladdinput) {
            claddinput = function(input) {
                if (ispushandsave) {
                    pushandsave(name, keyname, input);
                }
                //if (callback) {
                //    saveinputandcallback(callback, input, name);
                //}
                return {
                    value: input,
                    text: input
                }
            };
        }

        var defaultvalues = window.hdbackupdata["defaultvalues"];
        selectize($("#hd_" + name),
            claddinput,
            true);
        if (defaultvalues) {
            var defaultvalue = defaultvalues[name];
            if (defaultvalue) {
                //        $("#hd_" + name).val(defaultvalue);
              //  $("#hd_" + name).selectize()[0].selectize.setValue(defaultvalue);
            }
        }
    
    //setmenupositions(window.hdMenu);
}

function managesmenulist() {
   
    //bindauthentications();
   
    //bindrenderdatatypes();
}

var bindusernames = function() {
    bindandselectize("usernames",
        true,
        true,
        function(input) {
        });
}
var bindpasswords = function() {
    bindandselectize("passwords",
        true,
        true,
        function(input) {
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

var binddatabses = function() {
    var postdata = createpostvalues();
    requestandsavebackupfromeditorapi("DbApi/GetAllDatabases",
        function(data) {
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "dbnames",
        postdata,
        function() {
            bindandselectize("dbnames",
                true,
                true,
                function(input) {
                });
        });
};

function bindrenderdatatypes() {
    var postdata = {};
    requestandsavebackupfromeditorapi("DbApi/GetAllDataListTypes",
        function(data) {
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "datalisttypes",
        postdata,
        function(datalisttypes) {
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
        function(data) {
            if (data && data.Data) {
                return data.Data;
            }
            return false;
        },
        "authentications",
        postdata,
        function(authentications) {
            bindandselectize("authentications",
                true,
                true,
                function(input) {
                   
                });
        });
}

function bindtables() {
    if (window.hdbackupdata["defaultvalues"]["dbnames"] && window.hdbackupdata["defaultvalues"]["dbnames"] != "") {
        var postdata = createpostvalues();
        requestandsavebackupfromeditorapi("DbApi/GetAllTables",
            function(data) {
                if (data && data.Data) {
                    return data.Data;
                }
                return false;
            },
            "tables",
            postdata,
            function(tables) {
                bindandselectize("tables",
                    true,
                    true,
                    function(input) {
                    });
            },
            true);
    }
}

function bindtablecolumns() {
    if (window.hdbackupdata["defaultvalues"]["tables"] != "") {
        var postdata = createpostvalues();
        postdata.Table = window.hdbackupdata["defaultvalues"]["tables"];
        requestandsavebackupfromeditorapi("DbApi/GetTableWithColumn",
            function(data) {
                if (data && data.Data) {
                    return data.Data;
                }
                return false;
            },
            "tablecolumns",
            postdata,
            function(tables) {
                bindandselectize("tablecolumns",
                    true,
                    true,
                    function(input) {
                    });
            },
            true);
      
    }
}

var bodyeventwithvalueforselect = function(id, notvalue, callback) {
    $(document).on("change",
        id,
        function(e) {
            if ($(this).val() != notvalue) {
                callback($(this).val());
            }
        });
}

$(document).ready(function () {
    $("body").on("change",
        ".TrueFalse",
        function () {
            if ($(this).prop("checked")) {
                $("#" + $(this).attr("id").replace("_check", "")).val("True").trigger("change");
            } else {
                $("#" + $(this).attr("id").replace("_check", "")).val("False").trigger("change");
            }

        });


    bodyeventwithvalueforselect("#hd_authentications",
        "",
        function (value) {
           
            if (value == "Windows Authentication") {
                window.hdbackupdata["defaultvalues"]["usernames"] = "";
                window.hdbackupdata["defaultvalues"]["passwords"] = "";
                $("#hd_passwords,#hd_usernames,#hd_authentications").val("");
               

                $("#hd_integratedsecurity_check").prop("checked", true).trigger("change");
                $("#hd_trustservercertificate_check,#hd_multipleactiveresultsets_check").prop("checked", false).trigger("change");
                $("#hd_dbnames_list,#hd_tables_list,#hd_tablecolumns_list").each(function () {
                    if ($(this).attr("data-filterby-required")) {
                        $(this).attr("data-filterby-required",
                            $(this).attr("data-filterby-required").replace(",#hd_authentications,#hd_usernames,#hd_passwords", ""));
                    }
                });
                $("#hd_servernames").trigger("change");
            } else {
                $("#hd_dbnames_list,#hd_tables_list,#hd_tablecolumns_list").each(function () {
                    if ($(this).attr("data-filterby-required")&&$(this).attr("data-filterby-required").split("hd_usernames").length == 1) {
                        $(this).attr("data-filterby-required",
                            $(this).attr("data-filterby-required") + ",#hd_authentications,#hd_usernames,#hd_passwords");
                    }
                });
            }

        });
    bodyeventwithvalueforselect("#hd_servernames",
        "",
        function(value) {
        });
    bodyeventwithvalueforselect("#hd_dbnames",
        "",
        function(value) {
           ///// bindtables();
        });
    bodyeventwithvalueforselect("#hd_tablecolumns",
        "",
        function(value) {
           /// bindtablecolumns();
        });
    bodyeventwithvalueforselect("#hd_usernames",
        "",
        function(value) {
        });
    bodyeventwithvalueforselect("#hd_datalisttypes",
        "",
        function(value) {
        });
    bodyeventwithvalueforselect("#hd_passwords",
        "",
        function(value) {
        });





});