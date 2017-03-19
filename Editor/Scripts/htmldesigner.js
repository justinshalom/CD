(function($) {
    $.bindlist = function(opt, value) {

        var html = "";
        var array = this;
        var stn = $.extend({}, $.bindlist.defaults);
        
            if (typeof opt == 'object') {
                {
                    stn = $.extend(stn,opt );
                }
            }
        
        if (value) {
            stn.value = value;
        }
        var selectedarray;
        if (stn.selected && typeof stn.selected=='string') {
            selectedarray = stn.selected.split(",");
        }
        $.each(stn.array,
            function(boi, bov) {

                html += '<' + stn.tag;
                
                if (stn.selected === true) {
                    html += ' selected';
                } else if (stn.selected) {
                    $.each(selectedarray,
                        function() {
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
                        function(keyi, keyv) {
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
    keyvalue: function(param1, param2, i, v) {
        if (typeof v == 'object') {
            if (v[param1]
                && (v[param1]
                    || v[param1] == false)) {
                return v[param1];
            } else if (v[param2]
                && (v[param2]
                    || v[param2] == false)) {
                return v[param2];
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

(function(old) {
    $.fn.attr = function() {
        if (arguments.length === 0) {
            if (this.length === 0) {
                return null;
            }

            var obj = {};
            $.each(this[0].attributes,
                function() {
                    if (this.specified) {
                        obj[this.name] = this.value;
                    }
                });
            return obj;
        }

        return old.apply(this, arguments);
    };
})($.fn.attr);

function css(a) {
    var sheets = document.styleSheets, o = {};
    for (var i in sheets) {
        var rules = sheets[i].rules || sheets[i].cssRules;
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
        .map(function(sheet) {
            // Extract the rules
            return sheet.cssRules != null
                ? Array.prototype.concat.apply([],
                    Array.prototype.slice
                    .call(sheet.cssRules)
                    .map(function(rule) {
                        // Grab a list of classNames from each selector
                        switch (type) {
                        case 'class':
                            return (rule.selectorText)
                                ? rule.selectorText
                                .match(/\.[\w\-]+/g)
                                || []
                                : false;
                            break;;
                        case 'id':
                        default:
                            return (rule.selectorText)
                                ? rule.selectorText
                                .match(/\#[\w\-]+/g)
                                || []
                                : false;
                            break;;
                        }

                    })
                )
                : false;
        })
    ).filter(function(name) {
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


var hd_rightmenu =
    '<style>.hd_borderselected{ 1px dashed rgba(0, 0, 0, 0.22);} #hd_rightmenu .selectize-input{border-radius: 0px !important;} #hd_rightmenu .selectize-dropdown .active {background-color: #252424 !important; color: #ffffff;} #hd_rightmenu .selectize-dropdown .option  { padding: 3px 6px !important;} #hd_rightmenu .selectize-control.form-control .item{ line-height:20px; margin: 0 1px 1px 0 !important;background-color: #0f03f4;padding: 0px 3px 0px 5px;color: #FFFFFF;}  #hd_rightmenu .selectize-control.form-control{ height:auto;} #hd_rightmenu{ font-size:11px !important; } #hd_rightmenu .selectize-control.multi .selectize-input.has-items { font-size:11px !important;padding: 0px 0px 0px;} #hd_rightmenu .form-group{ margin-top:0px; }#hd_rightmenu .toggle{ margin-left: 10px; }</style><div class="" ' + 'style="' + 'position:absolute;top:40px;left:20px;width:180px;z-index:10000;font-size:12px !important;overflow:hidden;" ' + 'id="hd_rightmenu">' + '<div class="col-xs-50">' + '<div class="panel panel-info col-xs-50" >' + '<div class="panel-heading col-xs-50" id="hd_rightmenu_header">' + '<h3 class=" panel-title">Panel info</h3>' + '</div>' + '<div class="panel-body open col-xs-50" id="hd_rightmenu_body" style="">' + '<div class="col-xs-50" style="">' + '<div ><a href="javascript:void(0)">Attributes</a><div id="hd_rightmenu_allattributes" class="hd_rightmenu_clear  col-xs-50 "></div></div>' + '<div><a href="javascript:void(0)">Manage Classes</a><div id="hd_rightmenu_allclasses" class="hd_rightmenu_clear  col-xs-50 "></div></div>' + '<div ><a href="javascript:void(0)">Add Attributes</a></div>' + '<div><a href="javascript:void(0)">Add ' + 'Styles</a></div>' + '<div><a href="javascript:void(0)">Remove</a></div>' + '<div><a href="javascript:void(0)">Create Child</a></div>' + '</div>' + '</div>' + ' </div>' + ' </div>' + ' </div>';
$(document).ready(function() {
    var allclasses = getall('class');
    var allid = getall('id');
    var hdCurrentobj = $('body');
    $('body').append(hd_rightmenu);
    $('body').on('click',
        '*:not("#hd_rightmenu,#hd_rightmenu *")',
        function(e) {
            $('#hd_rightmenu').hide();
            $('.hd_borderselected')
                .removeClass('hd_borderselected');
        });
    $('#hd_rightmenu').hide();
    $('body').on('contextmenu',
        '*',
        function(e) {
            hdCurrentobj = $(this);
            $('.hd_borderselected')
                .removeClass('hd_borderselected');
            hdCurrentobj.addClass('hd_borderselected');
            e.preventDefault();
            e.stopPropagation();
            var t = $(this);
            $('#hd_rightmenu').show();
            var extrawidth = 100;
            var position = t.offset();
           

            var trypositionrightLeft =
                position.left + t.width()+extrawidth;
            
            var trypositionrightOverlow =
                position.left
                    + t.width()
                    + $('#hd_rightmenu').width() + extrawidth;
            var trypositionleftLeft =
                position.left - $('#hd_rightmenu').width() - extrawidth;
            if (parseInt(e.pageX - position.left) > 100 && $(window).width()
                        > (e.pageX + $('#hd_rightmenu').width())) {
                trypositionleftLeft =
                e.pageX;

            }

            $('#hd_rightmenu').css({
                top: position.top,
                left: ($(window).width()
                        > trypositionrightOverlow)
                    ? trypositionrightLeft
                    : (trypositionleftLeft < 0)
                    ? position.left
                    : trypositionleftLeft

            });

            var tagname = t.prop('tagName').toLowerCase();
            var additionals = '';
            var id = t.attr('id');
            var classeslist =
                (t.attr('class'))
                    ? $(this).attr('class').split(' ')
                    : false;
            var classes = (classeslist)
                ? classeslist.join(',')
                : classeslist;
            additionals += (id) ? '#' + id : '';
            additionals += (classes != '')
                ? '.' + classes
                : '';
            $('.hd_rightmenu_clear').html('');

            var attributes = t.attr();
            $.each(attributes,
                function(i, c) {
                    if ((c) && (i)) {

                        var formelement =
                            '<input type="text"  data-attributename="'
                                + i
                                + '" class="form-control input-sm" value = "'
                                + c
                                + '"/>';

                        switch (i) {
                        case 'class':
                            formelement = "";
                            var
                                options =
                                    '<select data-attributename="' + i + '" class="form-control chosen  input-sm" multiple="multiple" >';
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

                        $('#hd_rightmenu_allattributes')
                            .append(''
                                + '        <div class="form-group form-group-sm">'
                                + '          <label class="control-label">'
                                + i
                                + '</label>'
                                + formelement
                                + '          '
                                + '        </div>');
                    }
                });

            //$(".chosen")
            //    .chosen({ no_results_text: 'Add New ' });
            $('.chosen').selectize({
                delimiter: ',',
                persist: false,
                highlight:false,
                create: function(input) {
                    return {
                        value: input,
                        text: input
                    }
                }
            });
            $('#hd_rightmenu_header h3')
                .text(tagname + additionals);
            switch (tagname) {
            case 'div':
            {

                break;
            }
            default:
            {

                break;
            }
            }

        });

    var autotest = 1;
    if (autotest == 0) {

        var testelements = $('div:first *');
        var testelementslength = testelements.length;
        autotest = Math
            .floor((Math
                    .random()
                    * (testelementslength - 1))
                + 1);

        function autotestfn() {
            setTimeout(function() {
                    testelements.eq(autotest)
                        .trigger('contextmenu');
                    if (autotest < testelementslength) {
                        autotestfn();
                    }
                    autotest++;
                },
                2000);
        }

        autotestfn();
        $('body').on('click',
            '#hd_rightmenu',
            function(e) {
                autotest = testelementslength;
            });
    }

    // Bind the keyup event to the search box input
    //$('body').on('keyup',
    //    '.chosen-container input',
    //    function(e) {
    //        // If we hit Enter and the results list is empty (no matches) add the option
    //        var dropdown = $(this)
    //            .closest('.chosen-container');
    //        var select = dropdown.prev('select');
    //        if (e.which == 13
    //            && dropdown.find('li.active-result').length
    //            == 0) {
    //            var option = $("<option>").val(this.value)
    //                .text(this.value);

    //            // Add the new option
    //            select.prepend(option);
    //            // Automatically select it
    //            select.find(option).prop('selected', true);
    //            // Trigger the update
    //            select.trigger("chosen:updated");
    //            select.trigger("change");
    //        }
    //    });

    $('body').on('keyup change blur',
        '#hd_rightmenu select[data-attributename],#hd_rightmenu input[data-attributename]',
        function(e) {

            var attributename = $(this)
                .attr('data-attributename');

            switch (attributename) {
            case 'class':

                hdCurrentobj
                    .attr(attributename,
                        $(this).val().join(' '));
                break;
            default:
            {
                hdCurrentobj
                    .attr(attributename, $(this).val());
            }
            }

        });

})