var notify;
(function($) {
    notify = function (opt) {
        var stn = $.extend({}, this.notify.defaults);
        if (typeof opt == 'object') {
            {
                stn = $.extend(stn, opt);
            }
        }
        this.notify.accesspermission(stn);
        return true;
    };
        notify.manage = function(stn) {
            var options = {
                body: stn.message
            }
            switch (stn.type) {
                //Error case
            case "e":
            {
                options.icon = stn.erroricon;
                return new Notification(stn.errorheader, options);
                break;
            }
            //Warning case
            case "w":
            {
                options.icon = stn.warningicon;
                return new Notification(stn.successheader, options);
                break;
            }
            //Success Case
            default:
            {
                options.icon = stn.successicon;
                return new Notification(stn.warningheader, options);
                break;
            }
            }

        };
        notify.accesspermission = function (stn) {
            var notification = false;
            if (!("Notification" in window)) {
                return false;
            } else if (Notification.permission === "granted") {
                notification = notify.manage(stn);
            } else if (Notification.permission !== 'denied') {
                Notification.requestPermission(function(permission) {
                    if (permission === "granted") {
                        notification = notify.manage(stn);
                    }
                });
            } else {
                return false;
            }
            if (notification) {
                setTimeout(notification.close.bind(notification),stn.time);
            }
            return false;
        };

        notify.defaults = {
            message: "",
            type: "s", //default is success [w,e,s]
            time: 1000,
            successicon:
                "https://cdn0.iconfinder.com/data/icons/round-ui-icons/512/tick_green.png", //it will not work if icon is not empty
            erroricon: "", //it will not work if icon is not empty
            warningicon: "", //it will not work if icon is not empty
            url: "",
            errorheader: "Error Message",
            successheader: "Success Message",
            warningheader: "Warning Message"
        };
})(jQuery)
