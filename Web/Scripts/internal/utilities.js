function JSONstringify(str) {
    var obj = "";
    try {
        obj = JSON.stringify(str);
    } catch (e) {
        return "";
    }
    return obj;
}

function JSONparse(str) {
    var obj = "";
    try {
        obj = $.parseJSON(str);
    } catch (e) {
        return "";
    }
    return obj;
}
