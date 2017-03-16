(function (old) {
    $.fn.attr = function () {
        if (arguments.length === 0) {
            if (this.length === 0) {
                return null;
            }

            var obj = {};
            $.each(this[0].attributes, function () {
                if (this.specified) {
                    obj[this.name] = this.value;
                }
            });
            return obj;
        }

        return old.apply(this, arguments);
    };
})($.fn.attr);


var hd_rightmenu = '<style>#hd_rightmenu .toggle{ margin-left: 10px; }</style><div class="" '
    + 'style="'
    + 'position:absolute;top:40px;left:20px;width:180px;z-index:10000;font-size:12px !important;overflow:hidden;" '
    + 'id="hd_rightmenu">'
    + '<div class="col-xs-50">'
    + '<div class="panel panel-info col-xs-50" >'
    + '<div class="panel-heading col-xs-50" id="hd_rightmenu_header">'
    + '<h3 class=" panel-title">Panel info</h3>'
    + '</div>'
    + '<div class="panel-body open col-xs-50" id="hd_rightmenu_body" style="">'
    + '<div class="col-xs-50" style="">'
    + '<div ><a href="javascript:void(0)">Attributes</a><div id="hd_rightmenu_allattributes" class="hd_rightmenu_clear  col-xs-50 "></div></div>'
    + '<div><a href="javascript:void(0)">Manage Classes</a><div id="hd_rightmenu_allclasses" class="hd_rightmenu_clear  col-xs-50 "></div></div>'
    + '<div ><a href="javascript:void(0)">Add Attributes</a></div>'
    + '<div><a href="javascript:void(0)">Add Styles</a></div>'
    + '<div><a href="javascript:void(0)">Remove</a></div>'
    + '<div><a href="javascript:void(0)">Create Child</a></div>'
    + '</div>'
    + '</div>'
    +' </div>'
    +' </div>'
    +' </div>';
$(document).ready(function () {
    var hd_currentobj = $('body');
    $('body').append(hd_rightmenu);
    $('body').on('click', '*:not("#hd_rightmenu,#hd_rightmenu *")', function (e) {
        $('#hd_rightmenu').hide();
    })
    $('#hd_rightmenu').hide();
    $('body').on('contextmenu', '*', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var t = $(this);
        $('#hd_rightmenu').show();
        var position = t.offset();
        var trypositionright_left = position.left + t.width();
        var trypositionright_overlow = position.left + t.width() + $('#hd_rightmenu').width();
        var trypositionleft_left = position.left - $('#hd_rightmenu').width();
        
        $('#hd_rightmenu').css({
            top: position.top,
            left: ($(window).width() > trypositionright_overlow) ? trypositionright_left : (trypositionleft_left < 0) ? position.left:trypositionleft_left

        })


        var tagname = t.prop("tagName").toLowerCase();
        var additionals = "";
        var id = t.attr("id");
        var classeslist = (t.attr("class")) ? $(this).attr("class").split(' ') : false;
        var classes=(classeslist)?classeslist.join(','):classeslist;
        additionals += (id) ? '#' + id : "";
        additionals += (classes!="") ? '.' + classes : "";
        $('.hd_rightmenu_clear').html("")

        var attributes = t.attr();
        $.each(attributes, function (i, c) {
            if (c) {
                debugger;
                $('#hd_rightmenu_allattributes').append(''
    + '        <div class="form-group form-group-sm">'
    + '          <label class="control-label">'+i+'</label>'
    + '            <input type="text" class="form-control" value="' + c + '">' +
    + '          '
    + '        </div>');
            }
        })

            
        $.each(classeslist, function (i, c) {
            if (c) {
                $('#hd_rightmenu_allclasses').append(''
    + '        <div class="togglebutton">'
    + '          <label>'
    + '            <input type="checkbox" checked="">' + c
    + '          </label>'
    + '        </div>');
            }
        })
        
        $('#hd_rightmenu_header h3').text(tagname+additionals);
        switch (tagname) {
            case 'div': {

                break;
            }
            default: {

                break;
            }
        }
        
    })


    var autotest = 0;
    if (autotest == 0) {

        var testelements = $('div:first *');
        var testelementslength = testelements.length;
        autotest= Math.floor((Math.random() * (testelementslength-1)) + 1);
        function autotestfn() {
            setTimeout(function () {
                testelements.eq(autotest).trigger('contextmenu');
                if (autotest < testelementslength) {
                    autotestfn();
                }
                autotest++;
            },2000)
        }
        autotestfn();
        $('body').on('mouseover', '#hd_rightmenu")', function (e) {
            autotest = testelementslength;
        })
    }
    

})