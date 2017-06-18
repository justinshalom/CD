﻿var seteachinnerstyles = function(objbox, sbv, objects, parent) {
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
        objbox.append('<option value="' + key + '"  >' + value + '</option>');
    } else {
       // objbox.attr("multiple", "multiple");
    }
    return objbox;
}
var setmenubasedonattributes = function(hdmenu, t, classeslist, allclasses) {
    $('.hd_rightmenu_clear').html('');
    var attributes = t.attr();
    if (!attributes['class']) {
        attributes['class'] = "";
    }
    if (!attributes['style']) {
        attributes['style'] = "";
    }
    $.each(attributes,
        function(attributename, attributevalue) {
            var options;
            if ((attributename) && (attributename != "data-genid" && attributename != "accesskeygenerated")) {
                var formelement =
                    '<input type="text"   data-attributename="' +
                        attributename +
                        '" class="hdform-control input-sm" value = "' +
                        attributevalue +
                        '"/>';
                switch (attributename) {
                case 'style':
                {
                    options =
                        '<select id="hd_styleinput" class="hdform-control selectize  input-sm"  ><option value=""></option>';
                    options += $.bindlist({
                        array: properties,
                        key: 'index',
                        value: 'index'
                    });
                    options += '</select>';
                    $('#hd_rightmenu_allattributes')
                        .append('<div id="existingstylelist" class="hdcol-xs-50"></div>' +
                            '    <div class="hdcol-xs-50">    <div class="hdform-group hdform-group-sm" id="hd_rightmenu_auto_' +
                            attributename +
                            '">' +
                            '          <label class="control-label pull-left">' +
                            attributename +
                            '</label>'
                            //+ '<input type="text" list="hd_stylelist"  id="hd_styleinput"  class="hdform-control  input-sm" />'
                            +
                            options +
                            '        </div>  ' +
                            '        </div>');
                    var text =
                        '    <div class="hdcol-xs-50" id="hd_styledesigner" style="position: absolute;z-index: 999999;top: 57px;left: 147px;background-color: rgba(255, 255, 255, 0.98);padding: 0px 18px;">   ';
                    text += '        </div>';
                    $('#hd_rightmenu_allattributes').append(text);
                    $("#hd_styleinput").val("border").trigger("change");
                    break;
                }
                case 'class':
                {
                    formelement = "";
                    options =
                        '<select data-attributename="' +
                        attributename +
                        '" class="hdform-control selectize  input-sm" multiple="multiple" >';
                    options += $.bindlist({
                        array: classeslist,
                        selected: true
                    });
                    options += $.bindlist({
                        array: allclasses,
                        key: 'index',
                        value: 'index'
                    });
                    options += '</select>';
                    formelement = options;
                    break;
                }
                default:
                }
                if (attributename != "style") {
                    $('#hd_rightmenu_allattributes')
                        .append('' +
                            '    <div class="hdcol-xs-50 ">     <div class="hdform-group hdform-group-sm" id="hd_rightmenu_auto_' +
                            attributename +
                            '">' +
                            '          <label class="control-label pull-left">' +
                            attributename +
                            '</label>' +
                            formelement +
                            '          ' +
                            '      </div>   </div>');
                }
            }
        });
};