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
var autotest = function () {
    var autotest = 0;
    var testelements = $('div:first *');
    var testelementslength = testelements.length;
    autotest = Math
        .floor((Math
                .random()
                * (testelementslength - 1))
            + 1);
    function autotestfn() {
        setTimeout(function () {
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
    $('body')
        .on('click',
            '#hd_rightmenu',
            function (e) {
                autotest = testelementslength;
            });
};
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
            Array.prototype.slice.call(document.styleSheets).
            map(function(sheet) {
                // Extract the rules
                return sheet.cssRules != null
                    ? Array.prototype.concat.apply([],
                        Array.prototype.slice.call(sheet.cssRules).
                        map(function(rule) {
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
        ).
        filter(function(name) {
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
var setmenupositions = function(menu,t,e) {
    var extrawidth = 100;
    var position = t.offset();
    var trypositionrightLeft =
        position.left + t.width() + extrawidth;
    var trypositionrightOverlow =
        position.left
            + t.width()
            + menu.width()
            + extrawidth;
    var trypositionleftLeft =
        position.left
            - menu.width()
            - extrawidth;
    if (parseInt(e.pageX - position.left) > 100
        && $(window).width()
        > (e.pageX + menu.width())) {
        trypositionleftLeft =
            e.pageX;
    }
    menu.css({
        top: position.top,
        left: ($(window).width()
                > trypositionrightOverlow)
            ? trypositionrightLeft
            : (trypositionleftLeft < 0)
            ? position.left
            : trypositionleftLeft
    });
};
var setmenuheader = function(t,classeslist) {
    var tagname = t.prop('tagName').toLowerCase();
    var additionals = '';
    var id = t.attr('id');
    var classes = (classeslist)
        ? classeslist.join(',')
        : classeslist;
    additionals += (id) ? '#' + id : '';
    additionals += (classes != '')
        ? '.' + classes
        : '';
    $('#hd_rightmenu_header h3').text(tagname + additionals);
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
};
var selectise = function() {
    $('.chosen').
        selectize({
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
};
var setAttribute = function (hdCurrentobj, key, value) {
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