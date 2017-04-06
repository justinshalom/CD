function Attribute(postdata) {

    $.post(rooturl + "Json/Attribute", postdata,
       function (data) {
           if (!data.IsError) {
               originalfile = data.Data;
           } else {
               window.location.reload(true);
           }
       }
   );
}
function getallcssrules() {
    $.get(contenturl + "json/properties.json",
        function (data) {
            console.log(data);
            cssrules = data;
            $('body').append(hdstylemenu);
            var classes = ["panel-primary", "panel-success", "panel-warning", "panel-danger", "panel-info", "panel-default"];
            var uni = 0;
            $.each(cssrules,
                function (i, v) {
                    $.each(v.groups,
                        function (i2, v2) {

                            var id = v2.replace(/ /ig, "_");
                            if ($("#hdstylemenu")
                                .find("#" + id)
                                    .length
                                    == 0) {
                                uni++;
                                $('#hdstylemenu .header').append('<div style="width:10%;float:left"><div class="panel ' + classes[uni % 6] + '">' +
    '<div class="panel-heading" style="padding: 4px;" role="tab" id="heading' + id + '">' +
    '  <h4 class="panel-title">' +
     '   <a role="button" style="font-size: 12px !important;" data-parent="#hdstylemenu" href="#collapse' + id + '"  aria-controls="collapse' + id + '">' +
     v2.replace('CSS ', "") +
       ' </a>' +
      '</h4>' +
    '</div>' +

    '</div></div>' +
  '</div>');
                                $('#hdstylemenu .body').append('<div id="collapse' + id + '" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading' + id + '">' +
     ' <div class="panel-body" id="' + id + '">' +
      '</div>');

                            }
                            $('#hdstylemenu #' + id).append('<label>' + i + ":</label>");
                        });

                });
            $('.collapse').collapse();

        });

}