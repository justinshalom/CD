var setmenubasedonattributes = function (hdmenu, t, classeslist, allclasses) {
            $('.hd_rightmenu_clear').html('');
            var attributes = t.attr();
            if (!attributes['class']) {
                attributes['class'] = "";
            }
            if (!attributes['style']) {
                attributes['style'] = "";
            }
            $.each(attributes,
                function (attributename, attributevalue) {
                    var options;
                    if ((attributename)
                        && (attributename != "data-genid"
                            && attributename != "accesskeygenerated")) {
                        var formelement =
                            '<input type="text"  data-attributename="'
                                + attributename
                                + '" class="form-control input-sm" value = "'
                                + attributevalue
                                + '"/>';
                        switch (attributename) {
                            case 'class':
                                formelement = "";
                                options =
                                    '<select data-attributename="'
                                    + attributename
                                    + '" class="form-control chosen  input-sm" multiple="multiple" >';
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
                            default:
                        }
                        $('#hd_rightmenu_allattributes').
                            append(''
                                + '        <div class="form-group form-group-sm" id="hd_rightmenu_auto_'
                                + attributename
                                + '">'
                                + '          <label class="control-label">'
                                + attributename
                                + '</label>'
                                + formelement
                                + '          '
                                + '        </div>');
                    }
                });
        };