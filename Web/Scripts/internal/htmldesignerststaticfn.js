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
var hd_menuX = 0;
var hd_menuY = 0;
$(document).on("mousemove", function (event) {
    hd_menuX = event.pageX;
    hd_menuY = event.pageY;
});
var setmenupositions = function (menu, t, e) {
    //var extrawidth = 100;
    //var position = t.offset();
    //var trypositionrightLeft =
    //    position.left + t.width() + extrawidth;
    //var trypositionrightOverlow =
    //    position.left
    //        + t.width()
    //        + menu.width()
    //        + extrawidth;
    //var trypositionleftLeft =
    //    position.left
    //        - menu.width()
    //        - extrawidth;
    //if (parseInt(e.pageX - position.left) > 100
    //    && $(window).width()
    //    > (e.pageX + menu.width())) {
    //    trypositionleftLeft =
    //        e.pageX;
    //}
    ////menu.css({
    ////    top: position.top,
    ////    left: ($(window).width()
    ////            > trypositionrightOverlow)
    ////        ? trypositionrightLeft
    ////        : (trypositionleftLeft < 0)
    ////        ? position.left
    ////        : trypositionleftLeft
    ////});
    var half = (menu.width() / 2);
    var righthalf = $(window).width() - menu.width();
    var leftposition = hd_menuX - (menu.width() / 2);
    
    var topposition = hd_menuY + 50;
    //var topposition = hd_menuY - menu.height();
    if (leftposition < 0) {
        leftposition = 0;
    }
    if (leftposition > righthalf) {
        leftposition = righthalf;
    }
//    menu.css({
//        //top: topposition,
//        bottom: 0,
//        left:0
//        //left: leftposition
//});
};
var setmenuheader = function (t, classeslist) {
  
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
    window.hdMenu.find('.hdpanel-heading .hdpanel-title span').text(tagname + additionals);
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

//var selectize = function (obj, fn, destroy) {
//   // console.log(obj.attr("id"));
//  //  var html = obj.html();
//  //  console.log(html);
//    if (destroy) {
//        obj.selectize()[0].selectize.destroy();
//    }
    
////obj.html(html);

//    var options = {
//        delimiter: ',',
//        persist: false,
//        allowEmptyOption:true,
//        highlight: false,
        
//        searchField: ['value', 'text']
//    }
//    if (fn) {
//        options.create = fn;
//    }
    

//    var selector = (obj) ? obj : $('select.selectize:not(.selectized):visible');
//    selector.attr("data-selectize-dropdown-anchor-position", "top-left");
//    selector.selectize(options);
//  //  console.log(obj.html());
//  //  console.log(obj.attr("id")+"End");
    
//};
//Selectize.prototype.positionDropdown = function () {
//    var $control = this.$control;
//    var offset = this.settings.dropdownParent === 'body' ? $control.offset() : $control.position();
//    offset.top += $control.outerHeight(true);
//    var css = {
//        width: 'auto',
//        minWidth: $control.outerWidth(),
//        top: 'inherit',
//        right: 'inherit',
//        bottom: 'inherit',
//        left: 'inherit'
//    };

//    switch (this.$input.data('selectize-dropdown-anchor-position')) {
//    case 'bottom-right':
//        css.top = offset.top;
//        css.right = 0;
//        break;
//    case 'top-left':
//        css.bottom = $control.outerHeight();
//        css.left = offset.left;
//        break;
//    case 'top-right':
//        css.bottom = $control.outerHeight();
//        css.right = 0;
//        break;
//    default: // bottom-left
//        css.top = offset.top;
//        css.left = offset.left;
//    }

//    this.$dropdown.css(css);
//};