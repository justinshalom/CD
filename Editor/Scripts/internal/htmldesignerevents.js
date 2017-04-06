////PrepareFileContent(postdata);
$(document).
    ready(function() {
        $('body').
            on('keyup change blur',
                '#hd_rightmenu select[data-attributename],#hd_rightmenu input[data-attributename]',
                function(e) {
                    var attributename = $(this).attr('data-attributename');
                    switch (attributename) {
                    case 'class':
                        hdCurrentobj.removeAttr("tempclass");
                        var classname = $(this).val().join(' ');
                        hdCurrentobj.attr(attributename,
                            classname);
                        setAttribute(hdCurrentobj,
                            "class",
                            classname);
                        break;
                    default:
                    {
                        var value = $(this).val();
                        hdCurrentobj.attr(attributename, value);
                        setAttribute(hdCurrentobj,
                            attributename,
                            value);
                    }
                    }
                });
        $('body').
            on('focus',
                '#hd_rightmenu input[data-attributename]',
                function(e) {
                });
        $('body').
            on('keyup',
                '#hd_rightmenu_allattributes #hd_rightmenu_attr_name,#hd_rightmenu_allattributes #hd_rightmenu_attr_value',
                function(e) {
                    if (e.keyCode == 13) {
                        var value = $('#hd_rightmenu_attr_value').val();
                        var key = $('#hd_rightmenu_attr_name').val();
                        hdCurrentobj.attr($('#hd_rightmenu_attr_name').val(),
                            $('#hd_rightmenu_attr_value').val());
                        $('#hd_rightmenu_attr_name').val("");
                        $('#hd_rightmenu_attr_value').val("");
                        hdCurrentobj.trigger("contextmenu");
                        $('#hd_rightmenu_auto_' + key + " input").trigger("keyup");
                    }
                });
        $(document).
            bind('keydown keyup',
                function(e) {
                    var obj =
                        $('#hd_rightmenu_allattributes #hd_rightmenu_auto_class .selectize-dropdown-content .option.active');
                    if (
                        obj.length > 0) {
                        hdCurrentobj.removeClass(hdCurrentobj.attr('tempclass'));
                        hdCurrentobj.attr("tempclass",
                            obj.attr('data-value'));
                        hdCurrentobj.addClass(obj.attr('data-value'));
                    }
                });
        $('body').
            on('hidden.bs.collapse show.bs.collapse',
                '#absolutestyleeditor .panel-group',
                function() {
                    setTimeout(function() {
                            $('body').
                                css("margin-bottom",
                                    $('#absolutestyleeditor').height());
                        },
                        1000);
                });
        $('body').
            on('click',
                '*:not("#hd_rightmenu,#hd_rightmenu *,#absolutestyleeditor,#absolutestyleeditor *")',
                function(e) {
                    $('#hd_rightmenu').hide();
                    hdCurrentobj.removeClass(hdCurrentobj.attr('tempclass'));
                    hdCurrentobj.removeAttr("tempclass");
                });
    });