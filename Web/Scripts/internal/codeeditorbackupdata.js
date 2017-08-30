var hdbackupdata = {};

function savehdbackupdata() {
    setcache("hdbackupdata", JSONstringify(hdbackupdata));
}

function pushandsave(variablename, keyname, value) {
    
    if (!hdbackupdata[variablename]) {
        hdbackupdata[variablename] = [];
    }
    var data={};
    data[keyname]= value;
    hdbackupdata[variablename].push(data);
    savehdbackupdata();
}
var requestandsavebackup = function (url, callback, variablename, postdata, aftercallback) {
    
        $.post(url,
            postdata,
            function (data) {
                var cl = callback(data);
                if (cl != false) {
                    hdbackupdata[variablename] = cl;
                }
               
                savehdbackupdata();
                if (aftercallback) {
                    aftercallback(cl);
                }
            });
    
};

function requestandsavebackupfromeditorapi(url, callback, variablename, postdata, aftercallback) {
    requestandsavebackup(window.cd_rooturl + url, callback, variablename, postdata,aftercallback);
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

    requestandsavebackupfromeditorapi("DbApi/GetAllServerNames",
        function (data) {
            if (data.Data && data.Data.length > 0) {
                return data.Data;
            }
            return false;
        },
        "servernames",
        postdata);

};
