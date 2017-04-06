//(function ($) {
//    $.notify = function (opt) {
     
//        var stn = $.extend({}, $.notify.defaults);

//        if (typeof opt == 'object') {
//            {
//                stn = $.extend(stn, opt);
//            }
//        }
//        var notification_options = {
//            body: stn.message,
//            icon: stn.icon
//        }
       
//       return true
//    };
//        $.notify.defaults = {
//            message: "",
//            type: "s",//default is success [w,e,s]
//            time: Infinity,
//            icon:"",
//            successicon: "https://cdn0.iconfinder.com/data/icons/round-ui-icons/512/tick_green.png",//it will not work if icon is not empty
//            erroricon: "",//it will not work if icon is not empty
//            warningicon: "",//it will not work if icon is not empty
//            url: "",
//            errorheader: "Error Message",
//            successheader: "Error Message",
//            warningheader: "Error Message",
//            notify: function (type,icon) {
                
//                switch (type) {
//                    //Error case
//                    case "e":
//                        {
//                            options.icon = erroricon;
//                            return new Notification(errorheader, options);
//                            break;
//                        }
//                        //Warning case
//                    case "w":
//                        {
//                            options.icon = warningicon;
//                            return new Notification(successheader, options);
//                            break;
//                        }
//                        //Success Case
//                    default:
//                        {
//                            options.icon = successicon;
//                            return new Notification(warningheader, options);
//                            break;
//                        }
//                }
//            },
//            accesspermission: function (message, time, type) {
//                if (!("Notification" in window)) {
//                    return false;
//                } else if (Notification.permission === "granted") {
//                    var notification = notifyit(message, type);
//                } else if (Notification.permission !== 'denied') {
//                    Notification.requestPermission(function (permission) {
//                        if (permission === "granted") {
//                            var notification = new Notification(notify);
//                        }
//                    });
//                } else {
//                    return false;
//                }
//                if (notification) {
//                    setTimeout(notification.close.bind(notification), time);
//                }
//            }
            
//        };

//    })
//(jQuery);


//function notifyit(message, alertfor) {
   
//}
//var text = "Allow Notification";

//if (notify(text, time, alertfor) != false) {

//}