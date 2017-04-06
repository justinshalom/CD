(function ($) {
    $.bindlist = function (opt, value) {

        var html = "";
        var array = this;
        var stn = $.extend({}, $.bindlist.defaults);

        if (typeof opt == 'object') {
            {
                stn = $.extend(stn, opt);
            }
        }

        if (value) {
            stn.value = value;
        }
        var selectedarray;
        if (stn.selected && typeof stn.selected == 'string') {
            selectedarray = stn.selected.split(",");
        }
        $.each(stn.array,
            function (boi, bov) {

                html += '<' + stn.tag;

                if (stn.selected === true) {
                    html += ' selected';
                } else if (stn.selected) {
                    $.each(selectedarray,
                        function () {
                            if (
                                $
                                    .inArray(bov[stn.value],
                                        selectedarray)
                                    !== -1
                                    || $
                                    .inArray(boi,
                                        selectedarray)
                                    !== -1
                                    || $
                                    .inArray(bov,
                                        selectedarray)
                                    !== -1) {
                                html += ' selected';
                            }
                        });
                }
                html += ' value="';
                html += stn
                    .keyvalue(stn.value, stn.key, boi, bov);
                html += '"';
                if (stn.keys) {
                    $.each(stn.keys.split(','),
                        function (keyi, keyv) {
                            html += ' data-'
                                + keyv
                                + '="'
                                + stn.array[keyv]
                                + '"';

                        });

                }
                html += '>';
                html += stn
                    .keyvalue(stn.key, stn.value, boi, bov);

                html += '</' + stn.tag + '>';
            });

        return html;
    };
    $.bindlist.defaults = {
        array: [],
        key: false,
        value: false,
        customkey: false,
        placeholder: "Select",
        placeholdervalue: "0",
        tag: 'option',
        selected: false,
        keyvalue: function (param1, param2, i, v) {
            if (typeof v == 'object') {
                if (v[param1]
                    && (v[param1]
                        || v[param1] == false)) {
                    return v[param1];
                } else if (v[param2]
                    && (v[param2]
                        || v[param2] == false)) {
                    return v[param2];
                }else if (param1 == 'index') {
                    return i;
                }

            } else {
                if (param1 == 'index') {
                    return i;
                }
                if (typeof v == 'string'
                    || typeof v == 'number'
                    || typeof v == 'boolean') {
                    return v;
                }

            }
            return v;
        }
    }
})
(jQuery);

/*$.bindlist({
                                array: classeslist,
                                selected: true
                            });
                            $.bindlist({
                                array: allclasses,
                                key: 'index',
                                value: 'index'
                            });
                            
                            */