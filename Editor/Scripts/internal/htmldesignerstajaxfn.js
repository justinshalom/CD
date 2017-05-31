function Attribute(postdata) {

    $.post(rooturl + "Json/Attribute", postdata,
       function (data) {
           if (!data.IsTrue) {
               window.originalfile = data.Data;
           } else {
               window.location.reload(true);
           }
       }
   );
}


function getallcssproperties() {
    requesthandler(window.hd_contenturl + "json/at-rules.json", false, "getJSON",
       function (data) {
           window.at_rules = data;
       });
    requesthandler(window.hd_contenturl + "json/css-color-names.json", false, "getJSON",
      function (data) {
          window.css_color_names = data;
      });
    requesthandler(window.hd_contenturl + "json/css-font-weight-names.json", false, "getJSON",
      function (data) {
          window.css_font_weight_names = data;
      });
    requesthandler(window.hd_contenturl + "json/selectors.json", false, "getJSON",
      function (data) {
          window.selectors = data;
      });
    requesthandler(window.hd_contenturl + "json/syntaxes.json", false, "getJSON",
      function (data) {
          window.syntaxes = data;
          $.each(window.syntaxes,
              function(i, v) {
                  window.syntaxes[i] = $('<textarea />').html(window.syntaxes[i]).text();
              });
      });
    requesthandler(window.hd_contenturl + "json/types.json", false, "getJSON",
      function (data) {
          window.types = data;
      });
    requesthandler(window.hd_contenturl + "json/units.json", false, "getJSON",
      function (data) {
          window.units = data;
      });
   
    requesthandler(window.hd_contenturl + "json/properties.json", false, "getJSON",
        function (data) {
            console.log(data);
            window.properties = data;
            $('body').append(hdstylemenu);
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