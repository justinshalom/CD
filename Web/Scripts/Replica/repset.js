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
(function($) {
        $.repset = function(opt) {
            var fn = $.repset.methods;
            var stn = $.extend({}, $.repset.defaults);
            if (typeof opt == 'object') {
                {
                    stn = $.extend(stn, opt);
                }
            }
            fn.add(stn);
            if (!fn.initilizedclick) {
                $(document).on('click',
                    '.' + stn.repitclass,
                    function(e) {
                        fn.post(fn.click(e, stn, $(this)));
                    });
                $(document).on('DOMNodeInserted',
                    function(event) {
                        fn.add(stn);
                    });
                fn.initilizedclick = true;
            }
        };
        $.repset.methods = {
            initilizedclick: false,
            post: function(data) {
                parent.postMessage(data, "*");
            },
            add: function(opt, addby) {
                var repclosest = $('body')
                    .find(opt.closest
                        + ":not([data-repinitialized])");
                var obj = repclosest
                    .find(opt.addrepitto);
                repclosest
                    .attr('data-repinitialized', true);
                if (obj.length > 0) {
                    if (typeof obj[opt.addrepitmethod]
                        == 'function') {
                        obj[opt
                            .addrepitmethod
                        ](this.htmldata(opt));
                    } else {
                        console
                            .log("Please enter a valid jquery function for add replica button");
                    }
                };
            },
            htmldata: function(opt) {
                return $(opt.repithtml)
                    .addClass(opt.repitclass);
            },
            click: function(e, opt, t) {
                var obj = {};
                $.each(opt.objects,
                    function(i, v) {
                        obj[i] = {};
                        obj[i].attributes = t.attr();
                        var el = t.closest(opt.closest)
                            .find(v.element);
                        if (typeof el[v.where] == 'function') {
                            obj[i].value = el[v.where]();
                        } else {
                            obj[i].value = el.attr(v.where);
                        }
                    });
                return obj;
            }
        };
        $.repset.defaults = {
            closest: ".productdiv",
            addrepitmethod: 'append',
            //append,prepend,html,after,before
            addrepitto: ".addtocart",
            ////Add selector startwith #id,.class,[name] or tag
            repithtml:
                '<button class="btn" type="button">Repit</button>',
            repitclass: "repit",
            ////Dont add dot defore class
            objects: {
                productname: {
                    element: ".div",
                    where: "data-value"
                },
                productprice: {
                    element: ".div",
                    where: "data-price"
                }
            }
        };
    })
    (jQuery);
$.repset({
    closest: "tr",
    addrepitto: "td:last",
    objects: {
        productname: {
            element: "td:first",
            where: "text"
        },
        productlink: {
            element: "a",
            where: "href"
        }
    }
})