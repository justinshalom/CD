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
String.prototype.trimstart = function(c) {
    c = c ? c : ' ';
    var i = 0;
    for (; i < this.length && this.charAt(i) == c; i++);
    return this.substring(i);
}
String.prototype.trimend = function(c) {
    c = c ? c : ' ';
    var i = this.length - 1;
    for (; i >= 0 && this.charAt(i) == c; i--);
    return this.substring(0, i + 1);
}

var setmenupositions = function(menu, t, e) {
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
var setmenuheader = function(t, classeslist) {
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
var selectize = function() {
    $('select.selectize:not(.selectized)')
        .selectize({
            delimiter: ',',
            //persist: false,
            highlight: false,
            searchField: ['value', 'text']
            //create: function (input) {
            //    debugger;
            //    return {
            //        value: input,
            //        text: input
            //    }
            //}
        });
};
