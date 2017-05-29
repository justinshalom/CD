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
            if ((attributename)
                && (attributename != "data-genid"
                    && attributename != "accesskeygenerated")) {
                var formelement =
                    '<input type="text"   data-attributename="'
                        + attributename
                        + '" class="hdform-control input-sm" value = "'
                        + attributevalue
                        + '"/>';
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
                        .append('<div id="existingstylelist" class="hdcol-xs-50"></div>'
                            + '    <div class="hdcol-xs-50">    <div class="hdform-group hdform-group-sm" id="hd_rightmenu_auto_'
                            + attributename
                            + '">'
                            + '          <label class="control-label pull-left">'
                            + attributename
                            + '</label>'
                            //+ '<input type="text" list="hd_stylelist"  id="hd_styleinput"  class="hdform-control  input-sm" />'
                            + options
                            + '        </div>  '
                            + '        </div>');
                    var text =
                        '    <div class="hdcol-xs-50" id="hd_styledesigner" style="position: absolute;z-index: 999999;top: 57px;left: 147px;background-color: rgba(255, 255, 255, 0.98);padding: 0px 18px;">   ';
                   
                    text += '        </div>';
                    $('#hd_rightmenu_allattributes').append(text);
                    break;
                }
                case 'class':
                {
                    formelement = "";
                    options =
                        '<select data-attributename="'
                        + attributename
                        + '" class="hdform-control selectize  input-sm" multiple="multiple" >';
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
                        .append(''
                            + '    <div class="hdcol-xs-50 ">     <div class="hdform-group hdform-group-sm" id="hd_rightmenu_auto_'
                            + attributename
                            + '">'
                            + '          <label class="control-label pull-left">'
                            + attributename
                            + '</label>'
                            + formelement
                            + '          '
                            + '      </div>   </div>');
                }
            }
        });
};