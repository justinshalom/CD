var seteachinnerstyles = function(objbox, sbv, objects, parent) {
    $.each(objects,
        function(syi, syv) {
            if (syv != "") {
                setinnerstyles(objbox, syv, parent);
            }
        });
};
var setlengthbox = function(objbox, uniquesid, valuefor) {
    var obj = setstylebox(objbox, uniquesid + "_" + valuefor, valuefor);
    var input = setinputbox(obj,
        'number',
        uniquesid,
        valuefor);
}
var setpixelbox = function(objbox, uniquesid, valuefor) {
    var groupobj = setstylebox(objbox,
        uniquesid + "_" + valuefor + "_valueby",
        valuefor + " value by");
    var selectobj = setselectbox(groupobj,
        'hd_stylevalueinput_' + valuefor + "_valueby",
        valuefor);
    setoptionbox(selectobj, "px", "Pixel");
    setoptionbox(selectobj, "%", "Percentage");
}
var setcolorbox = function(objbox, uniquesid, valuefor) {
    ////var obj = setstylebox(objbox, uniquesid + "_" + valuefor, valuefor);
    var input = setinputbox(objbox,
        'text',
        uniquesid,
        valuefor);

    input.colorpicker({
        horizontal: true
    });
    return input;
}
var setinnerstyles = function(objbox, syv, parent) {
    //if (syntaxes[syv]) {
    //    seteachinnerstyles(objbox, syv, syntaxes[syv].split("|"));
    //}
    //else {
    //    setoptionbox(selectbaseobj, syv, syv);
    //}
    if (syv.split("<").length > 1 && syv.split(">").length > 1) {
        var valuefor = syv.replace("<", "")
            .replace(">", "")
            .replace("#", "")
            .replace(/ /g, "")
            .trim();
        var uniquesid = valuefor.replace(/ /g, "");
        switch (valuefor) {

        case "shadow":
        {
            //setlengthbox(objbox, uniquesid + "_x", valuefor + "-x");
            //setlengthbox(objbox, uniquesid + "_y", valuefor + "-y");
            //setlengthbox(objbox, uniquesid + "_g", valuefor + "-grow");
            //setlengthbox(objbox, uniquesid + "_s", valuefor + "-spread");
            //setpixelbox(objbox, uniquesid, valuefor);
            //setcolorbox(objbox, uniquesid, valuefor);
            break;
        }
        case "color":
        case "hex-color":
        {
            //setcolorbox(objbox, uniquesid, valuefor);
            break;
        }
        case "percentage":
        case "length":
        {
            //setlengthbox(objbox, uniquesid, valuefor);
            //setpixelbox(objbox, uniquesid, valuefor);
            break;
        }
        default:
        {
            if (syntaxes[valuefor]) {
                var synatx = syntaxes[valuefor].replace(/\[/g, "|")
                    .replace(/\]/g, "|")
                    .replace(/\?/g, "|")
                    .replace(/\&&/g, "|")
                    .replace(/ /g, "")
                    .replace(/\|\|/g, "|")
                    .replace(/\|\|/g, "|");
                var pieces = synatx
                    .split("|");
                //var basegroup = setgroupbox(objbox, valuefor);
                seteachinnerstyles(objbox, syv, pieces, uniquesid);
            }
            break;
        }
        }
    } else {
        ////var groupobj = setstylebox(objbox, uniquesid + "_" + syv, syv);
        ////var selectobj = setselectbox(groupobj, 'hd_stylevalueinput_' + syv + "_options", syv);
        syv = syv.trim();
        //var baseoptionsobj = setstylebox(objbox, "_basevalues" + parent, parent);
        //var selectbaseobj = setselectbox(baseoptionsobj,
        //    'hd_stylevalueinput__basevalues' + parent,
        //    parent);
        setoptionbox(objbox, syv, syv);
        //styeinputs += '<option >' + syv + '</option>';
    }
};
var setstylebox = function(objbox, idpostfix, label) {
    var id = 'hd_rightmenu_auto_style_' + idpostfix;
    var obj = $("#" + id);
    if ($("#" + id).length == 0) {
        objbox.append('<div class="hdform-group hdform-group-sm styleinputs" id="' +
            id +
            '">' +
            '          <label class="control-label pull-left">' +
            label.replace(/-/g, " ") +
            '</label>' +
            '        </div> ');
        return $("#" + id);
    } else {
        return obj;
    }
}
var setgroupbox = function(objbox, label) {
    //' + label + '
    objbox
        .append('<div class="hdgroup"><div class="hdgrouplabel hidden"></div><div class="hdgroupcontent" style="display:nonee"></div></div> ');
    return objbox.find(".hdgroupcontent:last");
}
var setinputbox = function(objbox, type, id, name) {
    if (id == "") {

        id = "Id" + new Date().getTime();

    }
    var obj = $("#" + id);
    if ($("#" + id).length == 0) {
        objbox.append('<input type="' +
            type +
            '" id="' +
            id +
            '" name="' +
            name +
            '"  class="hdform-control  hdinput-sm ' + name + '"  />');
        return $("#" + id);
    } else {
        return obj;
    }

}
var setselectbox = function(objbox, id, name) {
    var obj = $("#" + id);
    if ($("#" + id).length == 0) {
        objbox.append('<select id="' +
            id +
            '" name="' +
            name +
            '"  class="hdform-control  hdinput-sm selectize ' + name +
            '"   ><option value="" selected></option></select>');
        return $("#" + id);
    } else {
        return obj;
    }
}
var setoptionbox = function(objbox, key, value) {
    if (objbox.find('[value="' + key + '"]').length == 0) {
        objbox.append('<option data-text="' + value + '" data-value="' + key + '" value="' + key + '"  >' + value + '</option>');
    } else {
       // objbox.attr("multiple", "multiple");
    }
    return objbox;
}
var setstylelabels= function(key,value) {
    $('#existingstylelist')
                               .append('<div class="hdcol-xs-50"><div class="hdcol-xs-25"><label class="hdstylename control-label pull-left">' +
                                   key +
                                   '</label></div><div class="hdcol-xs-25"><label class="hdstylevalue control-label pull-left">' +
                                   value +
                                   '</label></div></div>');

}
