var ngmvinit = function () {
    $('form').attr('novalidate', true);
    $('input,select,textarea').not("[ng-mvinitialized],[type=hidden],.disableformgroup").each(function () {
        var form = $(this).closest("form");
        var formname = form.attr('name');
        $(this).not("[type=checkbox],[type=radio]").addClass("form-control");

        
        if ($(this).not("[type=checkbox],[type=radio]").length == 0 ) {
            $(this).parent().prepend($(this).next("[type=hidden]").wrapInner());
        }
        else
        if (  $(this).closest(".input-group,.form-group").length == 0) {
            if ($(this).prev("label").length == 1) {
                $(this).add($(this).prev("label")).wrapAll("<div class='form-group'></div>");
            }
            else {
                $(this).wrap("<div class='form-group'></div>");
            }

        }
        if (!$(this).attr("ng-model") && $(this).attr("name")) {
            $(this).attr("ng-model", $(this).attr("name"));
        }
        if ($(this).attr("ng-model")) {


            if ($(this).attr("data-val-required")) {
                $(this).attr("required", true);
               
                var ngif = '(submitted || ' + formname + '.' + $(this).attr("ng-model") + '.$dirty)';
                var ngshow = ngif+' && ' + formname + '.' + $(this).attr("ng-model") + '.$error.required';
                if ($(this).next(".error[ng-show='" + ngshow + "']").length == 0)
                    $(this).append('<span class="error alert alert-danger ng-hide"   ng-show="' + ngshow + '">' + $(this).attr("data-val-required") + '</span><br>');
            }
        }
        $(this).attr("ng-mvinitialized", true);
    })
}
ngmvinit();