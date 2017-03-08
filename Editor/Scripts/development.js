var developmentmode = true;
$(document).ready(function ()
{
    /*development mode*/
    $("#logout").click(function () {

        remcache('UserName');
        remcache('Password');
    })
    if ($('#loginForm form').length == 1 && window.location.href.split("localhost").length > 1)
        {
        $("#login").click(function () {

                setcache('UserName', $('#UserName').val());
                setcache('Password', $('#Password').val());
            })


            setTimeout( function ()
            {
                if (getcache('UserName') != false && getcache('Password') != false) {
                    $('#UserName').val(getcache('UserName'));
                    $('#Password').val(getcache('Password'));
                    $.post($('#loginpage').val(), $('#LoginForm').serialize(), function (data) {
                        if (data == 'invalid') {
                            $('body').removeClass("modal-open");
                            alert("Login failed");
                            if (defined(document.getElementById('txtLoginEmailId')))
                                document.getElementById('txtLoginEmailId').value = "";
                            if (defined(document.getElementById('txtLoginPassword')))
                                document.getElementById('txtLoginPassword').value = "";
                        }
                        else if (data == 'success') {
                            var url = window.location.hostname;
                            if (getcache('url') != false) {
                                window.location = getcache('url');
                            }
                            else {
                                window.location = "/AdminWeb/Home";
                            }
                        }
                    });
                    //$('#LoginForm').submit();
                    // $('#inputLogon').trigger('submit');

                }
            }, 100 );
        }
        else   if ( window.location.href.toLowerCase().split("/search/").length==1)
        {
            /*part of page reload after login*/
            setcache('url', window.location.href); /*Searching part*/
        }
        /*development mode ends*/
 

})