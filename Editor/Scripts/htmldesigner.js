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
var hdstylemenu = '<style>#hdstylemenu a,#hdstylemenu a:focus,#hdstylemenu a:hover {color: inherit !important;text-decoration: none;}</style><div id="absolutestyleeditor" style="position: fixed;bottom: 0px;width: 100%;" ><div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true" style="margin-bottom: 0px;"><div class="panel panel-success"><div class="panel-heading" style="padding: 4px;"><h6 class="panel-title">   <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseStyle" style="font-size: 12px !important;" aria-expanded="true"  aria-controls="collapseStyle">Style Editor</a></h6></div> <div id="collapseStyle" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne"><div class="panel-body" ><div class="panel-group" id="hdstylemenu" role="tablist" aria-multiselectable="false"></div></div></div></div></div></div>';

var hd_rightmenu =
    '<style>.hd_borderselected{ 1px dashed rgba(0, 0, 0, 0.22);} #hd_rightmenu .selectize-input{border-radius: 0px !important;} #hd_rightmenu .selectize-dropdown .active {background-color: #252424 !important; color: #ffffff;} #hd_rightmenu .selectize-dropdown .option  { padding: 3px 6px !important;} #hd_rightmenu .selectize-control.form-control .item{ line-height:20px; margin: 0 1px 1px 0 !important;background-color: #0f03f4;padding: 0px 3px 0px 5px;color: #FFFFFF;}  #hd_rightmenu .selectize-control.form-control{ height:auto;} #hd_rightmenu{ font-size:11px !important; }#hd_rightmenu .selectize-control.single .item{ background-color: #ffffff;padding: 1px;color: #656464;} #hd_rightmenu .selectize-control .selectize-input.has-items, #hd_rightmenu input { font-size:11px !important;padding: 0px 0px 0px;} #hd_rightmenu .form-group{ margin-top:0px; }#hd_rightmenu .toggle{ margin-left: 10px; }</style><div class="" ' + 'style="' + 'position:absolute;top:40px;left:20px;width:180px;z-index:10000;font-size:12px !important;" ' + 'id="hd_rightmenu">' + '<div class="col-xs-50">' + '<div class="panel panel-info col-xs-50" style="background-color: rgba(255, 255, 255, 0.83);padding: 0px" >' + '<div class="panel-heading col-xs-50" id="hd_rightmenu_header">' + '<h3 class=" panel-title">Panel info</h3>' + '</div>' + '<div class="panel-body open col-xs-50" id="hd_rightmenu_body" style="">' + '<div class="col-xs-50" style="">' + '<div ><div id="hd_rightmenu_allattributes" class="hd_rightmenu_clear  col-xs-50 "></div></div>' + '<div class="hidden"><a href="javascript:void(0)">Style</a><div id="hd_rightmenu_styles" class="hd_rightmenu_clear  col-xs-50 "></div></div>' + '</div>' + '</div>' + ' </div>' + ' </div>' + ' </div>';
var originalfile = "";
function PrepareFileContent(postdata) {
    $.post(rooturl + "Json/PrepareHtmlContent", postdata,
        function (data) {
            if (!data.IsError) {
                originalfile = data.Data;
            } else {
                window.location.reload(true);
            }
        }
    );
   
}
function Attribute(postdata) {
    
    $.post(rooturl + "Json/Attribute", postdata,
       function (data) {
           if (!data.IsError) {
               originalfile = data.Data;
           } else {
               window.location.reload(true);
           }
       }
   );
}
var postdata = {};
postdata.area = area;
postdata.controllername = controller;
postdata.view = action;

////PrepareFileContent(postdata);
$(document).ready(function () {
    var cssrules = {};
    $.get(contenturl + "json/properties.json",
        function (data) {
            console.log(data);
            cssrules = data;
            $('body').append(hdstylemenu);
            var classes = ["panel-primary", "panel-success", "panel-warning", "panel-danger", "panel-info", "panel-default"];
            var uni = 0;
            $.each(cssrules,
                function (i, v) {
                    $.each(v.groups,
                        function (i2, v2) {
                            
                            var id =  v2.replace(/ /ig, "_");
                            if ($("#hdstylemenu")
                                .find("#"+id)
                                    .length
                                    == 0) {
                                uni++;
                                $('#hdstylemenu .header').append('<div style="width:10%;float:left"><div class="panel ' + classes[uni%6] + '">' +
    '<div class="panel-heading" style="padding: 4px;" role="tab" id="heading' + id + '">' +
    '  <h4 class="panel-title">'+
     '   <a role="button" style="font-size: 12px !important;" data-parent="#hdstylemenu" href="#collapse' + id + '"  aria-controls="collapse' + id + '">' +
     v2.replace('CSS ',"") +
       ' </a>'+
      '</h4>'+
    '</div>'+
    
    '</div></div>' +
  '</div>');
                                $('#hdstylemenu .body').append('<div id="collapse' + id + '" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading' + id + '">' +
     ' <div class="panel-body" id="' + id + '">' +
      '</div>');

                            }
                            $('#hdstylemenu #'+id).append('<label>'+i+":</label>");
                        });
                    
                });
            $('.collapse').collapse();

        });

    

    var allclasses = getall('class');
    var allid = getall('id');
    var hdCurrentobj = $('body');
    $('body').append(hd_rightmenu);
    $('body').on('hidden.bs.collapse show.bs.collapse', '#absolutestyleeditor .panel-group',
        function() {
            setTimeout(function() {
                    $('body').css("margin-bottom",
                        $('#absolutestyleeditor').height());
                },
                1000);
        });
    $('body').on('click',
        '*:not("#hd_rightmenu,#hd_rightmenu *,#absolutestyleeditor,#absolutestyleeditor *")',
        function(e) {
            $('#hd_rightmenu').hide();
            $('.hd_borderselected')
                .removeClass('hd_borderselected');
            hdCurrentobj.removeClass(hdCurrentobj.attr('tempclass'));
            hdCurrentobj.removeAttr("tempclass");

        });
    $('#hd_rightmenu').hide();
    $('body').on('contextmenu',
        '*',
        function(e) {
            hdCurrentobj = $(this);
            $('.hd_borderselected')
                .removeClass('hd_borderselected');
            //hdCurrentobj.addClass('hd_borderselected');
            e.preventDefault();
            e.stopPropagation();
            var t = $(this);
            $('#hd_rightmenu').show();
            var extrawidth = 100;
            var position = t.offset();

            var trypositionrightLeft =
                position.left + t.width() + extrawidth;

            var trypositionrightOverlow =
                position.left
                    + t.width()
                    + $('#hd_rightmenu').width()
                    + extrawidth;
            var trypositionleftLeft =
                position.left
                    - $('#hd_rightmenu').width()
                    - extrawidth;
            if (parseInt(e.pageX - position.left) > 100
                && $(window).width()
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
            if (!attributes['class']) {
                attributes['class'] = "";
            }
            if (!attributes['style']) {
                attributes['style'] = "";
            }
            $.each(attributes,
                function (i, c) {
                    var options;
                   
                    
                    if ((i) && (i != "data-genid" && i != "accesskeygenerated")) {

                        var formelement =
                            '<input type="text"  data-attributename="'
                                + i
                                + '" class="form-control input-sm" value = "'
                                + c
                                + '"/>';

                        switch (i) {
                        case 'class':
                            formelement = "";
                            options = '<select data-attributename="' + i + '" class="form-control chosen  input-sm" multiple="multiple" >';
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
                                + '        <div class="form-group form-group-sm" id="hd_rightmenu_auto_' + i + '">'
                                + '          <label class="control-label">'
                                + i
                                + '</label>'
                                + formelement
                                + '          '
                                + '        </div>');
                    }
                    
                });
            $('#hd_rightmenu_allattributes')
                            .append(''
                                + '        <div class="form-group form-group-sm">'
                                + '          <label class="control-label">'
                                + 'New Attribute'
                                + '</label>'
                                + '<input type="text" class="form-control input-sm " placeholder="Key" id="hd_rightmenu_attr_name" />'
                                + '<input type="text" class="form-control input-sm " placeholder="Value" id="hd_rightmenu_attr_value"/>'
                                + '          '
                                + '        </div>');
            //$(".chosen")
            //    .chosen({ no_results_text: 'Add New ' });
            $('.chosen').selectize({
                delimiter: ',',
                persist: false,
                highlight: false,
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
    $(document).bind('keydown keyup',
        function (e) {
            var obj =
                $('#hd_rightmenu_allattributes #hd_rightmenu_auto_class .selectize-dropdown-content .option.active');
            if (
                obj.length > 0) {
                hdCurrentobj.removeClass(hdCurrentobj.attr('tempclass'));
                hdCurrentobj.attr("tempclass", obj.attr('data-value'));
                hdCurrentobj.addClass(obj.attr('data-value'));
            }
        });
   
    $('body').on('keyup',
        '#hd_rightmenu_allattributes #hd_rightmenu_attr_name,#hd_rightmenu_allattributes #hd_rightmenu_attr_value',
    function (e) {
        if (e.keyCode == 13) {
            var value = $('#hd_rightmenu_attr_value')
                .val();
            var key = $('#hd_rightmenu_attr_name')
                .val();
            hdCurrentobj.attr($('#hd_rightmenu_attr_name')
                .val(), $('#hd_rightmenu_attr_value').val());
            $('#hd_rightmenu_attr_name').val("");
            $('#hd_rightmenu_attr_value').val("");
            hdCurrentobj.trigger("contextmenu");
            $('#hd_rightmenu_auto_' + key+" input").trigger("keyup");

        }
    });
    /**
      if (i == "style") {

                        options = '<select data-stylename="' + i + '" class="form-control chosen  input-sm"  ><option value="">Select Style</option>';
                       
                        options += $.bindlist({
                            array: cssrules,
                            key: 'index',
                            value: 'index'
                        });

                        options += '</select>';
                        $('#hd_rightmenu_styles')
                          .append(''
                              + '        <div class="form-group form-group-sm" >'
                              + '          <label class="control-label">'
                              + i
                              + '</label>'
                              + options
                              + ' <div class="absolutegroup stylevalue"><input type="range" min="0"  max="100"></div> '
                              + '        </div>');


                    }
     */
    $('body').on('focus',
        '#hd_rightmenu input[data-attributename]',
        function (e) {

        });
    $('body').on('keyup change blur',
        '#hd_rightmenu select[data-attributename],#hd_rightmenu input[data-attributename]',
        function(e) {

            var attributename = $(this)
                .attr('data-attributename');

            switch (attributename) {
                case 'class':
                    hdCurrentobj.removeAttr("tempclass");
                var classname = $(this).val().join(' ');
                hdCurrentobj
                    .attr(attributename,
                        classname);
                setAttribute(hdCurrentobj,"class",classname);
                break;
            default:
            {
                var value = $(this).val();
                hdCurrentobj
                    .attr(attributename, value);
                setAttribute(hdCurrentobj, attributename, value);
            }
            }

        });

});
var setAttribute = function (hdCurrentobj,key,value) {
    var genid = hdCurrentobj
            .attr('data-genid');
    if (genid) {
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