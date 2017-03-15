
var hd_rightmenu = '<style>#hd_rightmenu *:not(span){position:relative;float:left;width:100%}#hd_rightmenu .toggle{ margin-left: 10px; }</style><div class="" '
    + 'style="'
    + 'position:absolute;top:40px;left:20px;width:180px;z-index:10000;font-size:12px !important;overflow:hidden;" '
    + 'id="hd_rightmenu">'
    + '<div class="col-xs-50">'
    + '<div class="panel panel-info col-xs-50" >'
    + '<div class="panel-heading col-xs-50" id="hd_rightmenu_header">'
    + '<h3 class="panel-title">Panel info</h3>'
    + '</div>'
    + '<div class="panel-body open col-xs-50" id="hd_rightmenu_body" style="padding:5px;overflow:hidden">'
    + '<ul class="dropdown-menu col-xs-50" style="position: relative;max-width:100%;margin-left: -12px;padding-left: 0px !important;margin-left: 0px !important;">'
    + '<li ><a href="javascript:void(0)">Add and Id</a></li>'
    + '<li ><a href="javascript:void(0)">Manage Classes</a><ul id="hd_rightmenu_allclasses" class="list-unstyled col-xs-50"></ul></li>'
    + '<li ><a href="javascript:void(0)">Add Attributes</a></li>'
    + '<li><a href="javascript:void(0)">Add Styles</a></li>'
    + '<li><a href="javascript:void(0)">Remove</a></li>'
    + '<li><a href="javascript:void(0)">Create Child</a></li>'
    + '</ul>'
    + '</div>'
    +' </div>'
    +' </div>'
    +' </div>';
$(document).ready(function () {
    var hd_currentobj = $('body');
    $('body').append(hd_rightmenu);
    $('body').on('contextmenu', '*', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var position = $(this).position();
        $('#hd_rightmenu').css({
            top: position.top,
            left:position.left
        })


        var tagname = $(this).prop("tagName").toLowerCase();
        var additionals = "";
        var id = $(this).attr("id");
        var classeslist = $(this).attr("class").split(' ');
        var classes=classeslist.join(',');
        additionals += (id) ? '#' + id : "";
        additionals += (classes) ? '.' + classes : "";
        $('#hd_rightmenu_allclasses').html("")
        $.each(classeslist, function (i, c) {
            if (c) {
                $('#hd_rightmenu_allclasses').append('<li>'
    + '        <div class="togglebutton">'
    + '          <label>'
    + '            <input type="checkbox" checked="">' + c
    + '          </label>'
    + '        </div></li>');
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


})