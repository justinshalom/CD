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


        $('body').
          on('keyup change blur',
              '#hd_stylevalueinput,#hd_styleinput',
              function (e) {
                  var hd_styleinputval=$('#hd_styleinput').val();
                  if (e.keyCode == 13) {
                      $('#existingstylelist').append('<div class="hdcol-xs-25"><label class="control-label pull-left">'
                                + $('#hd_styleinput').val()
                                + '</label></div><div class="hdcol-xs-25"><label class="control-label pull-left">'
                                + $('#hd_stylevalueinput').val()
                                + '</label></div>')
                  }
                  $('#hd_styledesigner').hide();
                  var obj = properties[hd_styleinputval];
                  if (obj) {
                      debugger;

                      if (obj.percentages == "no") {

                      }
                      else {

                      }
                      var syntax =obj.syntax;
                      var stylepieces = syntax.split("||");
                      $('.styleinputs').hide();
                      $('#hd_styledesigner').html("");
                      $('#hd_styledesigner').show();
                      $.each(stylepieces, function (si, sv) {
                         
                          var styeinputs = "";
                          var additionalinputs = "";
                          var htmlinputscount = 0;
                          if (sv.split("<").length > 1 &&
                              sv.split(">").length > 1) {
                              sv = sv.replace("<", "").replace(">", "").trim();

                              var boxstartsformgroup = '<div class="hdform-group hdform-group-sm styleinputs" id="hd_rightmenu_auto_style'
                                   + '_'
                                   + sv
                                   + '">'
                                   + '          <label class="control-label pull-left">'
                                   + sv.replace(/-/, " ")
                                   + '</label>';
                              var boxstartsinputgroup = '<div class="hdinput-group hdinput-group-sm styleinputs" id="hd_rightmenu_auto_style'
                                  + '_'
                                  + sv
                                  + '">'
                                  + '          <label class="control-label pull-left">'
                                  + sv.replace(/-/, " ")
                                  + '</label>';
                              boxends = '        </div> ';
                              styeinputs += boxstartsformgroup
                                   + '<select name="' + sv + '" id="hd_stylevalueinput_'
                                   + sv
                                   + '" class="hdform-control  selectize  hdinput-sm"  ><option value=""></option>';
                               $.each(syntaxes[sv].split("|"), function (syi,syv) {
                                   if (syv.split("<").length > 1 &&
                             syv.split(">").length > 1) {
                                       var valuefor = syv.replace("<", "").replace(">", "").trim();
                                       switch(valuefor)
                                       {
                                           case "length": {
                                               additionalinputs += boxstartsformgroup;
                                               additionalinputs += '<input type="number" id="hd_stylevalueinput_'+valuefor+'_'
                                   + sv
                                   + '" name="' + sv + '"  class="hdform-control  hdinput-sm"  />';
                                               additionalinputs += boxends;
                                               additionalinputs += boxstartsformgroup;
                                               additionalinputs += '<input type="checkbox" id="hd_stylevalueinput_' + valuefor + '_valueby_'
                                  + sv
                                  + '" name="' + sv + '" class="hdform-control hdinput-sm"  />';
                                               additionalinputs += boxends;
                                               break;
                                           }
                                           default: {
                                               break;
                                           }
                                   }
                                   }
                                   else {
                                       htmlinputscount++;
                                       syv = syv.trim();
                                       styeinputs += '<option >' + syv + '</option>';
                                   }

                               })
                               styeinputs += '</select>'+boxends;
                               
                           }
                          if (htmlinputscount > 0) {
                              $('#hd_styledesigner').append(styeinputs + additionalinputs);
                              styeinputs = "";
                          }
                          else {
                          }


                          //$('#hd_rightmenu_auto_style_' + orv).show();




                      });
                      
                     
                      selectize();
                      $('#hd_styledesigner').show();


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