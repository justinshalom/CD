﻿function css(a) {
    var sheets = document.styleSheets, o = {};
    for (var i in sheets) {
        var rules = sheets[i].rules || sheets[i].properties;
        for (var r in rules) {
            if (a.is(rules[r].selectorText)) {
                o = $.extend(o,
                    css2json(rules[r].style),
                    css2json(a.attr('style')));
            }
        }
    }
    return o;
}
function getall(type) {
    var classes = {};
    // Extract the stylesheets
    Array.prototype.concat.apply([],
            Array.prototype.slice.call(document.styleSheets)
            .map(function (sheet) {
                // Extract the rules
                return sheet.cssRules != null
                    ? Array.prototype.concat.apply([],
                        Array.prototype.slice.call(sheet.cssRules)
                        .map(function (rule) {
                            // Grab a list of classNames from each selector
                            switch (type) {
                            case 'class':
                                return (rule.selectorText)
                                    ? rule.selectorText.match(/\.[\w\-]+/g)
                                    || []
                                    : false;
                                break;;
                            case 'id':
                            default:
                                return (rule.selectorText)
                                    ? rule.selectorText.match(/\#[\w\-]+/g)
                                    || []
                                    : false;
                                break;;
                            }
                        })
                    )
                    : false;
            })
        )
        .filter(function (name) {
            // Reduce the list of classNames to a unique list
            name = name.toString().replace('.', '');
            return !classes[name] && (classes[name] = true);
        });
    return classes;
}
function css2json(css) {
    var s = {};
    if (!css) return s;
    if (css instanceof CSSStyleDeclaration) {
        for (var i in css) {
            if ((css[i]).toLowerCase) {
                s[(css[i]).toLowerCase()] = (css[css[i]]);
            }
        }
    } else if (typeof css == "string") {
        css = css.split("; ");
        for (var i in css) {
            var l = css[i].split(": ");
            s[l[0].toLowerCase()] = (l[1]);
        }
    }
    return s;
}
var setAttribute = function (hdCurrentobj, key, value,e) {
    var genid = hdCurrentobj
        .attr('data-genid');
    if (genid ) {
        var obj = genid.split("_");
        var postdata = {};
        postdata.key = key;
        postdata.value = value;
        if (obj[obj.length - 4]) {
            postdata.area = hdCurrentobj
                .attr('data-genid');
        } else {
            postdata.area = "";
        }
        postdata.controllername = obj[obj.length - 3];
        postdata.view = obj[obj.length - 2];
        postdata.genid = genid;
        Attribute(postdata);
    }
};
var setmenubasedonattributes = function (hdmenu, t, classeslist, allclasses) {
    if (t.attr("data-genid")) {
        ////$(".hd_autogeneratedhr").remove();
        
            //t.attr("oldborder", t.css("border"));
            //t.css("border", "1px inset #21bf62");
        
        ////t.before('<hr style="left:' + t.position().left + ';top:' + t.position().top + ';width:' + t.width() + ';position:absolute;" class="hd_autogeneratedhr"/>');
    }

    $('#existingstylelist').html('');
    if (!$("#hd_classes_list").attr("data-binderinitialized")) {
        $("#hd_classes_list").binder(allclasses);
    }
    var attributes = t.attr();
    if (!attributes['class']) {
        attributes['class'] = "";
    }
    if (!attributes['style']) {
        attributes['style'] = "";
    }
    $("#hd_styleinput").val("");
    $("#hd_classes").val("");
    $('.hd_autogenerated_input').remove();
    $("#hd_newattributetext").val(t.html());

    $.each(attributes,
        function(attributename, attributevalue) {
            var options;
            if ((attributename) && (attributename != "data-genid" && attributename != "accesskeygenerated")) {
                var formelement =
                    '<input type="text"    placeholder="' +
                        attributename +
                        '" class="hdform-control input-sm hdattributevalue" value = "' +
                        attributevalue +
                        '"/>';
                switch (attributename) {
                case 'style':
                {
                    //$('#hd_rightmenu_allattributes')
                    //    .append('<div id="existingstylelist" class="hdcol-xs-50"></div>' +
                    //        '    <div class="hdcol-xs-50">    <div class="hdform-group hdform-group-sm" id="hd_rightmenu_auto_' +
                    //        attributename +
                    //        '">' +
                    //        '          <label class="control-label pull-left">' +
                    //        attributename +
                    //        '</label>'
                    //        //+ '<input type="text" list="hd_stylelist"  id="hd_styleinput"  class="hdform-control  input-sm" />'
                    //        +
                    //        options +
                    //        '        </div>  ' +
                    //        '        </div>');
                    //var text =
                    //    '    <div class="hdcol-xs-50" id="hd_styledesigner" style="position: absolute;z-index: 999999;top: 57px;left: 147px;background-color: rgba(255, 255, 255, 0.98);padding: 0px 18px;">   ';
                    //text += '        </div>';
                    //$('#hd_rightmenu_allattributes').append(text);
                    /////$("#hd_styleinput").val("border").trigger("change");
                    $.each(attributevalue.split(";"),
                       function (attri, attrv) {
                           var attrpieces = attrv.split(":");
                           if (attrpieces[0] && attrpieces[1]) {
                               setstylelabels(attrpieces[0], attrpieces[1]);
                           }

                       });
                    break;
                }
                case 'class':
                {
                    //formelement = "";
                    //options =
                    //    '<select data-attributename="' +
                    //    attributename +
                    //    '" class="hdform-control selectize  input-sm" multiple="multiple" >';
                    //options += $.bindlist({
                    //    array: classeslist,
                    //    selected: true
                    //});
                    //options += $.bindlist({
                    //    array: allclasses,
                    //    key: 'index',
                    //    value: 'index'
                    //});
                    //options += '</select>';
                    //formelement = options;


                    if (classeslist && classeslist.length > 0) {
                        $("#hd_classes").val(classeslist.join(" "));
                    } 


                    break;
                }
                default:
                }
                if (attributename != "style" && attributename != "class") {
                    $('#hd_attributes_box')
                        .before('' +
                            '    <div class="hdcol-xs-50 hd_menubox hd_autogenerated_input">     <div class="hdform-group hdform-group-sm " id="hd_rightmenu_auto_' +
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