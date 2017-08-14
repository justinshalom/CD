var hdbackupdata = {};
var requestandsavebackup = function (url, callback, variablename, postdata) {
    if (!hdbackupdata[variablename]) {
        $.post(url,
            postdata,
            function (data) {
                hdbackupdata[variablename] = callback(data);
                setcache("hdbackupdata", JSONstringify(hdbackupdata));
            });
    }
};
var StoreAllProperties = function () {
    var servers = {};
    var postdata = {};

    requestandsavebackup(window.cd_rooturl + "DbApi/GetConfigDetails",
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

    requestandsavebackup(window.cd_rooturl + "DbApi/GetAllServerNames",
        function (data) {
            if (data.Data && data.Data.length > 0) {
                return data.Data;
            }
            return false;
        },
        "servernames",
        postdata);

};
