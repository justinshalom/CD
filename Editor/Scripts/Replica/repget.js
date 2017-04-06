window.addEventListener('message',
    function(e) {
        var key = e.message ? 'message' : 'data';
        var data = e[key];
        receiveMessage(event, data);
    },
    false);
$(window).on('message',
    function(e,data) {
        
    });
function receiveMessage(e,data) {
    
    var origin = e.origin || e.originalEvent.origin; // For Chrome, the origin property is in the event.originalEvent object.
    //if (origin !== $('#projecturl').val())
    //    return;
    
    if ($('.repitlist').length == 0) {
        $('body')
            .append('<div class="repdiv" style="position: absolute;left: 0px;top: 0px;word-wrap: break-word;background-color: #FFFFFF;"><table class="table table-striped table-hover table-condensed"><thead class="repithead"><tr></tr></thead><tbody class="repitlist" ></tbody></table></div>');
        var repithead = $('.repithead tr');
        $.each(data,
        function (i, v) {
            repithead.append('<th>'+i+'</th>');
        });

    }
    $('.repitlist').append('<tr></tr>');
    var repitlist = $('.repitlist tr:last');
    
    $.each(data,
        function (i, v) {
            
            repitlist.append('<td>' + v.value + '</td>');
        });

}