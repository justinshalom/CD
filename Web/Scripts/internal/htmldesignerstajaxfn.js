function Attribute(postdata) {

    $.post(cd_rooturl + "Json/Attribute", postdata,
       function (data) {
           if (!data.Data) {
               window.originalfile = data.Data;
           } else {
               window.location.reload(true);
           }
       }
   );
}


function getallcssproperties() {
    $.getJSON(window.cd_contenturl + "json/at-rules.json",
       function (data) {
           window.at_rules = data;
       });
    $.getJSON(window.cd_contenturl + "json/css-color-names.json",
      function (data) {
          window.css_color_names = data;
      });
    $.getJSON(window.cd_contenturl + "json/css-font-weight-names.json",
      function (data) {
          window.css_font_weight_names = data;
      });
    $.getJSON(window.cd_contenturl + "json/selectors.json",
      function (data) {
          window.selectors = data;
      });
    $.getJSON(window.cd_contenturl + "json/syntaxes.json",
      function (data) {
          window.syntaxes = data;
          $.each(window.syntaxes,
              function(i, v) {
                  window.syntaxes[i] = $('<textarea />').html(window.syntaxes[i]).text();
              });
      });
    $.getJSON(window.cd_contenturl + "json/types.json",
      function (data) {
          window.types = data;
      });
    $.getJSON(window.cd_contenturl + "json/units.json",
      function (data) {
          window.units = data;
      });
   
    $.getJSON(window.cd_contenturl + "json/properties.json",
        function (data) {
            console.log(data);
            window.properties = data;
            //$('body').append(hdstylemenu);
            var classes = ["panel-primary", "panel-success", "panel-warning", "panel-danger", "panel-info", "panel-default"];
            var uni = 0;
            $.each(window.properties,
                function(i, v) {
                    window.properties[i]
                        .syntax = $('<textarea />').html(window.properties[i].syntax).text();
                    //console.log(i+"="+window.properties[i].syntax);
                });
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