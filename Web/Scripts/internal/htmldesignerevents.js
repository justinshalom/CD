////PrepareFileContent(postdata);
$(document).
    ready(function () {


        $('body').on('click',
            '#hd_dom_left',
            function (e) {
                e.preventDefault();
                if (window.hdCurrentobj.prev().length > 0) {
                    window.hdCurrentobj.prev().trigger("contextmenu");
                }
            });
        $('body').on('click',
            '#hd_dom_right',
            function (e) {
                e.preventDefault();
                if (window.hdCurrentobj.next().length > 0) {
                    window.hdCurrentobj.next().trigger("contextmenu");
                }
            });
        $('body').on('click',
            '#hd_dom_up',
            function (e) {
                e.preventDefault();
                if (window.hdCurrentobj.parent().length > 0) {
                    window.hdCurrentobj.parent().trigger("contextmenu");
                }
            });
        $('body').on('click',
            '#hd_dom_down',
            function (e) {
                e.preventDefault();
                if (window.hdCurrentobj.children().length > 0) {
                    window.hdCurrentobj.children().first().trigger("contextmenu");
                }
            });

        $('body').
            on('keyup change blur',
                '#hd_rightmenu select[data-attributename],#hd_rightmenu input[data-attributename]',
                function (e) {
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
                function (e) {
                });
        $('body').
            on('keyup',
                '#hd_rightmenu_allattributes #hd_rightmenu_attr_name,#hd_rightmenu_allattributes #hd_rightmenu_attr_value',
                function (e) {
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

        (function ($) {
            $.fn.getCursorPosition = function (setstart, setend) {
                var input = this.get(0);
                if (!input) return; // No (input) element found
                if ('selectionStart' in input) {
                    // Standard-compliant browsers
                    if (setstart) {
                        input.selectionStart = setstart;  
                    }
                    if (setend) {
                        input.selectionEnd = setend;
                    }
                    return input.selectionStart;
                } else if (document.selection) {
                    // IE
                    input.focus();
                    var sel = document.selection.createRange();
                    var selLen = document.selection.createRange().text.length;
                    sel.moveStart('character', -input.value.length);
                    return sel.text.length - selLen;
                }
            }
        })(jQuery);
        var dynamicposition = 0;
        //After enter eny value for style it will remove it from screen
        $('body').on('change',
           '.subdynamicinput',
           function (e) {
              debugger;
               if ($(".colorpicker:visible").length == 0) {
                   setTimeout(function() {
                           //$("#subdynamicinput").colorpicker('hide');
                          
                           if ($("#subdynamicinput").val()) {
                               $(".dynamicinput").val($(".dynamicinput").val().substring(0, dynamicposition) + " " +
                                   $("#subdynamicinput").val() + " " +
                                   $(".dynamicinput").val().substring(dynamicposition) + " ");
                           } else {
                               $(".dynamicinput").val($(".dynamicinput").val());
                           }
                           $(".dynamicinput").val($(".dynamicinput").val().replace(/  /g, " ").replace(/ px/g, "px"));
                           $(".dynamicinput").focus();
                           if ($("#subdynamicinput").val()) {
                               dynamicposition = $(this)
                                   .getCursorPosition(dynamicposition + parseInt($("#subdynamicinput").val().length),
                                       dynamicposition + $("#subdynamicinput").val().length);
                           }
                           $("#subdynamicinput").closest(".hdform-group").remove();
                           $("#hd_styledesigner").css("width", (($(".dynamicinput").val().length) * 5) + "%");
                       },
                       100);
                   var cssstyle = {};
                   cssstyle[$("#hd_styleinput").val()] = $(this).val();
                   window.hdCurrentobj.removeAttr("style");
                   hdCurrentobj.css(cssstyle);
               } else {
                   //$("#subdynamicinput").colorpicker('destroy');
               }
           });
        //After enter color removing from screen
        $('body').on('mouseover',
            '.subdynamicinput',
            function (e) {
                if ($(".colorpicker:visible").length == 1) {
                    if ($("#subdynamicinput").val() != "" && $("#subdynamicinput").val() != "#") {
                        $("#subdynamicinput").colorpicker('destroy');
                        $("#subdynamicinput").trigger("change");
                    }
                } else {
                    //
                }
            });
        $('body').on('keydown',
            '.dynamicinput',
            function(e) {
                if (e.keyCode == 38 || e.keyCode == 40) {
                    e.preventDefault();
                    var currenpos = $(this).getCursorPosition();
                    $(this).trigger("keyup", e);
                }

            });
        $('body').on('click',
            '#existingstylelist > div',
            function(e, inherit) {
                var key = $(this).find(".hdstylename").text();
                var value = $(this).find(".hdstylevalue").text();
                $(this).remove();
                $("#hd_styleinput").val(key).trigger("change");
                $(".dynamicinput").val(value);
                $(".dynamicinput").focus();

            });

        var clickedenter = false;
        $('body').on('keyup click',
            '.dynamicinput',
            function (e, inherit) {
               
                if (inherit) {
                    //e = inherit;
                }
                $("#subdynamicinput").closest(".hdform-group").remove();
                if (e.originalEvent) {

                    var currenpos = $(this).getCursorPosition();
                    if (e.keyCode == 8 ) {
                        dynamicposition = currenpos;
                    }else
                    if (e.keyCode == 16 || e.keyCode == 35 || e.keyCode == 36 || e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 46) {
                        dynamicposition = currenpos;
                    }else
                    if (e.type == "click" ) {
                        dynamicposition = currenpos;
                    } else {
                        $("#hd_styledesigner").css("min-width", 100+"%");
                        $("#hd_styledesigner").css("width", (($(".dynamicinput").val().length) * 5) + "%");
                        var value = $(this).val();
                        var hdStyleinputval = $('#hd_styleinput').val();
                        if (e.keyCode == 13 && value && clickedenter) {
                          
                            setstylelabels($('#hd_styleinput').val(), value);
                            $('#hd_styledesigner').hide();
                        }
                        if (e.keyCode == 13) {
                            clickedenter = true;
                        } else {
                            clickedenter = false;

                            var obj = properties[hdStyleinputval];
                            var stylepieces = {};
                            if (obj) {
                                if (obj.percentages == "no") {

                                } else {

                                }
                                var syntax = obj.syntax;
                                stylepieces = syntax.split("||");
                            }


                            if (currenpos == 0 && value.length > 0) {
                                currenpos = 1;
                            }
                            var lastchar = value[currenpos - 1];
                            var uptochar = value.substring(0, currenpos).replace(/ /g, "");
                            currenpos = uptochar.length;
                            var pieces = value.match(/rgb\(?.*\)+|#[0-9A-Za-z]+|#|[0-9]+|[A-Za-z]+/g);
                            var objbox = $("#hd_styledesigner");
                            var stringlength = 0;
                            var currChar = "";
                            var currindex = "";
                            if(pieces){
                            $.each(pieces,
                                function(pi, pv) {
                                    stringlength += pv.length;
                                    if (stringlength >= currenpos && currChar == "") {
                                        currChar = pv;
                                        currindex = pi;
                                    }
                                });
                            }
                            //value.substring(0, index);


                            //$.each(pieces,
                            //    function(i, v) {
                            if (e.keyCode == 38 && !isNaN(currChar)) {
                                pieces[currindex] = parseInt(currChar) + 1;
                                $(this).val(pieces.join(" ").replace(/ px/g, "px"));
                                $(this).getCursorPosition(currenpos, currenpos);
                            } else if (e.keyCode == 40 && !isNaN(currChar)) {
                                pieces[currindex] = parseInt(currChar) - 1;
                                $(this).val(pieces.join(" ").replace(/ px/g, "px"));
                                $(this).getCursorPosition(currenpos, currenpos);
                            } else if (dynamicposition < value.length && lastchar != " ") {
                                var baseobj;

                                if (currChar.startsWith("rgb") || currChar.startsWith("#")) {
                                    debugger;
                                    pieces[currindex] = "";
                                    $(this).val(pieces.join(" ").replace(/ px/g, "px"));

                                    baseobj = setstylebox(objbox, "subdynamicinput", "");
                                    var inputbox = setcolorbox(baseobj, "subdynamicinput", "subdynamicinput");
                                    inputbox.val(currChar);
                                    inputbox.focus();
                                } else if (isNaN(currChar)) {
                                    
                                    pieces[currindex] = "";
                                    $(this).val(pieces.join(" ").replace(/ px/g, "px"));

                                    baseobj = setstylebox(objbox, "subdynamicinput", "");
                                    var input = setselectbox(baseobj, "subdynamicinput", "subdynamicinput");
                                    setoptionbox(input, "px", "Pixel");
                                    setoptionbox(input, "%", "Percentage");

                                    $.each(stylepieces,
                                        function(si, sv) {
                                            console.log("sv:" + sv);
                                            var stylesubpieces = sv.split("|");
                                            $.each(stylesubpieces,
                                                function(sbi, sbv) {
                                                    setinnerstyles(input, sbv, "options");
                                                });

                                        });
                                    //selectize();

                                    //input.val(v);
                                    //input.focus();

                                    var selectizeinput = $("#subdynamicinput");
                                    selectizeinput.val(currChar);
                                    selectizeinput.focus();


                                }
                            }
                        }
                        dynamicposition = currenpos;
                    }
                    
                }
                //});
                
                var cssstyle = {};
               
                window.hdCurrentobj.removeAttr("style");
                $('#existingstylelist > div').each(
           function (ei,ev) {
               var key = $(this).find(".hdstylename").text();
               var value = $(this).find(".hdstylevalue").text();
               
               cssstyle[key] = value;

           });
                cssstyle[$("#hd_styleinput").val()] = $(this).val();
                    hdCurrentobj.css(cssstyle);
                

            });
        $('body').
          on('keyup change blur',
              '#hd_stylevalueinput,#hd_styleinput',
              function (e) {
                 
                  dynamicposition = 0;
                  var hdStyleinputval = $('#hd_styleinput').val();
                  //if (e.keyCode == 13) {
                  //    $('#existingstylelist')
                  //        .append('<div class="hdcol-xs-25"><label class="control-label pull-left">'
                  //            + $('#hd_styleinput').val()
                  //            + '</label></div><div class="hdcol-xs-25"><label class="control-label pull-left">'
                  //            + $('#hd_stylevalueinput').val()
                  //            + '</label></div>');
                  //}
                  $('#hd_styledesigner').hide();
                  var obj = properties[hdStyleinputval];
                  if (obj) {


                      if (obj.percentages == "no") {

                      }
                      else {

                      }
                      var syntax = obj.syntax;
                      var stylepieces = syntax.split("||");
                      $('.styleinputs').hide();
                      $('#hd_styledesigner').html("");
                      $('#hd_styledesigner').show();
                     
                      var objbox = $('#hd_styledesigner');

                      var baseobj = setstylebox(objbox, "dynamicinput","");
                      debugger;
                      var input = setinputbox(baseobj, "text", "", "dynamicinput");
                      input.val($("#hd_stylevalueinput").val());
                      $("#hd_stylevalueinput").val("");
                      input.focus();

                      ////$.each(stylepieces, function (si, sv) {


                      ////    console.log("sv:" + sv);
                      ////    var stylesubpieces = sv.split("|");


                      ////    $.each(stylesubpieces, function (sbi, sbv) {

                      ////        var basegroup = setgroupbox(objbox, "");
                      ////        setinnerstyles(basegroup, sbv, "options");
                      ////    });

                      ////});
                      ////selectize();
                      ////$('#hd_styledesigner').show();
                      ////$(".hdgroup")
                      ////    .each(function () {
                      ////        if ($(this).text() == "") {
                      ////            $(this).remove();
                      ////        }
                      ////    });
                  }


              });
        
        $(document).
            on('click', '.hdgrouplabel',
                function (e) {
                    $(this).closest(".hdgroup").find(".hdgroupcontent").toggle();
                });
        $(document).
            bind('keydown keyup',
                function (e) {
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
                function () {
                    setTimeout(function () {
                        $('body').
                            css("margin-bottom",
                                $('#absolutestyleeditor').height());
                    },
                        1000);
                });
        $('body').
            on('click',
                '*:not(".colorpicker,.colorpicker *,#hd_rightmenu,#hd_rightmenu *,#absolutestyleeditor,#absolutestyleeditor *")',
                function (e) {
                    $('#hd_rightmenu').hide();
                    hdCurrentobj.removeClass(hdCurrentobj.attr('tempclass'));
                    hdCurrentobj.removeAttr("tempclass");
                });

        $('body').on('afterappendcomplete',
            '#hd_Styles_list',
            function (e, data) {

                window.allhtmlelements = data.rows;

            });
        $('body').on('afterappendcomplete',
            '#hd_styleinput_list',
            function (e, data) {

                window.properties = data.rows;
                $.each(window.properties,
                            function(i, v) {
                                window.properties[i]
                                    .syntax = $('<textarea />').html(window.properties[i].syntax).text();
                            });
            });
    });