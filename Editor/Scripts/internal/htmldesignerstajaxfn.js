function Attribute(postdata) {

    $.post(rooturl + "Json/Attribute", postdata,
       function (data) {
           if (!data.IsTrue) {
               originalfile = data.Data;
           } else {
               window.location.reload(true);
           }
       }
   );
}


function getallcssproperties() {
    $.get(hd_contenturl + "json/at-rules.json",
       function (data) {
           at_rules = data;
       });
    $.get(hd_contenturl + "json/css-color-names.json",
      function (data) {
          css_color_names = data;
      });
    $.get(hd_contenturl + "json/css-font-weight-names.json",
      function (data) {
          css_font_weight_names = data;
      });
    $.get(hd_contenturl + "json/selectors.json",
      function (data) {
          selectors = data;
      });
    $.get(hd_contenturl + "json/syntaxes.json",
      function (data) {
          syntaxes = data;
          $.each(syntaxes, function (i, v) {
              syntaxes[i] = $('<textarea />').html(syntaxes[i]).text();
          })
      });
    $.get(hd_contenturl + "json/types.json",
      function (data) {
          types = data;
      });
    $.get(hd_contenturl + "json/units.json",
      function (data) {
          units = data;
      });
   
    $.get(hd_contenturl + "json/properties.json",
        function (data) {
            console.log(data);
            properties = data;
            $('body').append(hdstylemenu);
            var classes = ["panel-primary", "panel-success", "panel-warning", "panel-danger", "panel-info", "panel-default"];
            var uni = 0;
            $.each(properties, function (i,v) {
                properties[i].syntax = $('<textarea />').html(properties[i].syntax).text();
                console.log(properties[i].syntax);
            })

            

  //          $.each(properties,
  //              function (i, v) {
  //                  $.each(v.groups,
  //                      function (i2, v2) {

  //                          var id = v2.replace(/ /ig, "_");
  //                          if ($("#hdstylemenu")
  //                              .find("#" + id)
  //                                  .length
  //                                  == 0) {
  //                              uni++;
  //                              $('#hdstylemenu .header').append('<div style="width:10%;float:left"><div class="panel ' + classes[uni % 6] + '">' +
  //  '<div class="panel-heading" style="padding: 4px;" role="tab" id="heading' + id + '">' +
  //  '  <h4 class="panel-title">' +
  //   '   <a role="button" style="font-size: 12px !important;" data-parent="#hdstylemenu" href="#collapse' + id + '"  aria-controls="collapse' + id + '">' +
  //   v2.replace('CSS ', "") +
  //     ' </a>' +
  //    '</h4>' +
  //  '</div>' +

  //  '</div></div>' +
  //'</div>');
  //                              $('#hdstylemenu .body').append('<div id="collapse' + id + '" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading' + id + '">' +
  //   ' <div class="panel-body" id="' + id + '">' +
  //    '</div>');

  //                          }
  //                          $('#hdstylemenu #' + id).append('<label>' + i + ":</label>");
  //                      });

  //              });
  //          $('.collapse').collapse();

    });

}