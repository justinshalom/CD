var hdbackupdata = {};

function savehdbackupdata() {
    setcache("hdbackupdata", JSONstringify(hdbackupdata));
}

function ifexistinobjectarray(objectarray,keyname,value) {
    var exist = false;
    $.each(objectarray,
        function (i, v) {

            if (v[keyname] == value) {
                exist = true;
            }
        });
    return exist;
}

function pushandsave(variablename, keyname, value) {
    
    if (!hdbackupdata[variablename]) {
        hdbackupdata[variablename] = [];
    }
    var data={};
    data[keyname] = value;
    var exist = ifexistinobjectarray(hdbackupdata[variablename], keyname, value);
    if (!exist) {
        hdbackupdata[variablename].push(data);
        savehdbackupdata();
    }
}
function pusharrayandsave(variablename, arraylist, clearandreplace) {
    
    if (!hdbackupdata[variablename] || clearandreplace) {
        hdbackupdata[variablename] = [];
    }
    $.each(arraylist,
        function(i, v) {
            hdbackupdata[variablename].push(v);
        });
    
    savehdbackupdata();
}
var requestandsavebackup = function (url, callback, variablename, postdata, aftercallback, clearandreplace) {
    
        $.post(url,
            postdata,
            function (data) {
                var cl = callback(data);
                if (cl != false) {
                    pusharrayandsave(variablename, cl, clearandreplace);
                }
                if (aftercallback) {
                    aftercallback(cl);
                }


            });
    
};

function requestandsavebackupfromeditorapi(url, callback, variablename, postdata, aftercallback,clearandreplace) {
    if (!$("#hd_" + name).attr("data-editorinitialized") || clearandreplace) {
        requestandsavebackup(window.cd_rooturl + url, callback, variablename, postdata, aftercallback, clearandreplace);
        if (!clearandreplace) {
            $("#hd_" + name).attr("data-editorinitialized", true);
        }
    }
}
    
var StoreAllProperties = function () {
   
    var postdata = {};

    requestandsavebackupfromeditorapi("DbApi/GetConfigDetails",
        function (data) {
            if ($(data).find("connectionStrings add[connectionString]").length > 0) {
                var connectionstrings = [];
                $(data).find("connectionStrings add[connectionString]").each(function (coni, conn) {
                    var connobj = {};
                    connobj.connectionstring = $(conn).attr("connectionString");
                    connobj.name = $(conn).attr("name");
                    connectionstrings.push(connobj);
                });

                return connectionstrings;
            }
            return false;
        },
        "connectionstring",
        postdata);

    //requestandsavebackupfromeditorapi("DbApi/GetAllServerNames",
    //    function (data) {
    //        if (data.Data && data.Data.length > 0) {
    //            return data.Data;
    //        }
    //        return false;
    //    },
    //    "servernames",
    //    postdata, bindandselectize("servernames",
    //        true,
    //        true,
    //        function(input) {
    //        }));

};
