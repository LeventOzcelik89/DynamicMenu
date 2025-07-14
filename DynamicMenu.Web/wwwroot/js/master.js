function TypeOf(obj) {
    return ({}).toString.call(obj).match(/\s([a-zA-Z]+)/)[1].toLowerCase();
}

Date.prototype.CalculateDiff_dhm = function (date) {

    if (!(TypeOf(this) == "date" || TypeOf(date) == "date")) {
        return null;
    }

    var diffMins = Math.floor((Math.abs(this - date) / 1000) / 60);

    var minute = diffMins % 60;
    var hour = ((diffMins - minute) / 60) % 24;
    var day = (diffMins - minute - (60 * hour)) / (24 * 60);

    return {
        days: day,
        hours: hour,
        minutes: minute
    };

}

Date.prototype.CalculateDiff_dhm_styled = function (date) {

    if (!(TypeOf(this) == "date" || TypeOf(date) == "date")) {
        return null;
    }

    var res = this.CalculateDiff_dhm(date);

    return (res.days != 0 ? '<b>' + res.days + '</b> Day ' : '') +
        (res.hours != 0 ? '<b>' + res.hours + '</b> Hour ' : '') +
        (res.minutes != 0 ? '<b>' + res.minutes + '</b> min ' : '');

}

function feedback(feedback) {

    if (feedback == "" || feedback == null || feedback == "null") return false;

    if (feedback == 'SERVER') {
        feedback = { action: '', status: 'error', timeout: 20, message: 'Sunucu ile bağlantı kurulamıyor. Lütfen tekrar deneyin.', title: 'Sunucu Bağlantı Problemi !' };
    }

    var feedbackObj = feedback;

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": feedbackObj.timeout * 1000,
        "extendedTimeOut": 0,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "tapToDismiss": false
    }

    if (typeof feedbackObj.action != "undefined" && feedbackObj.action != null && feedbackObj.action != "") {
        toastr.options.onHidden = function (a) {
            if (feedbackObj.isBlank != null && feedbackObj.isBlank != "undefined" && feedbackObj.isBlank == true) {
                window.open(feedbackObj.action, '_blank');
            }
            else {
                location.href = feedbackObj.action;
            }
        }
    } else {
        //bura grid refresh metodu ile güncelleneccek sayfaya refresh atılmıcak
        toastr.options.onHidden = function (a) {
            //location.reload(true);
        }
    }

    if (feedbackObj.message != "" && feedbackObj.message != null && feedbackObj.status != "" && feedbackObj.status != null) {
        toastr[feedbackObj.status](feedbackObj.message, feedbackObj.title);
    }

}

function MesajError(mesaj) {
    feedback(JSON.parse(('{"action":"","title":"Sistem Uyarısı","message":"' + mesaj + '","status":"error","timeout":8}').replace(/\n/g, '<br />')));
}

function feedbackNew(mesaj) {
    if (mesaj == null || mesaj == '') { return; }
    feedback({
        "action": "",
        "title": "Sistem Uyarısı",
        "message": mesaj.replace(/\n/g, '<br />'),
        "status": "error",
        "timeout": 8
    });
}

function ReadData(url, data, returnF, loadingObj, NoAction) {

    //  NoAction veriyi olduğu gibi dönderir. Feedback çıkart vs uğraşmaz !...
    //  Bu özelliği İstasyona veri gönder getir işlemleri için kullanıyoruz.

    //  loadingObj
    //      Text: 'Veriler yükleniyor biraz zaman alabilir. Lütfen bekleyin'
    //      Color: '#ff0f00'
    //      Elem: $('#Elem')
    //      Animation: rotatingPlane
    //          doubleBounce,rotatingPlane,wave,wanderingCubes,spinner,chasingDots,threeBounce,circle,cubeGrid,fadingCircle,foldingCube
    //

    loadingObj = loadingObj || {};

    var text = loadingObj.Text ? loadingObj.Text : 'Veriler yükleniyor.Lütfen bekleyiniz.';
    var color = loadingObj.Color ? loadingObj.Color : "#000000";
    var elem = loadingObj.Elem ? loadingObj.Elem : $('body');
    var animation = loadingObj.Animation ? loadingObj.Animation : 'foldingCube';

    $.ajax({
        dataType: 'JSON',
        type: 'POST',
        async: true,
        timeout: 6000000,
        url: url,
        data: (typeof (data) != 'undefined' ? data : null),
        beforeSend: function () {
            elem.loadingModal({ text: text, animation: animation, backgroundColor: color });
        },
        success: function (res) {

            if (NoAction == true && typeof (returnF) == 'function') {
                returnF(res);
                return;
            }

            if (typeof (res) == 'undefined' || res == null || res == '') {
                if (res == 0) { return; }
                feedback("SERVER");
            } else {

                if (res.hasOwnProperty('feedback')) {
                    feedback(res.feedback);
                }

                if (res.hasOwnProperty('success')) {
                    if (res.success == true && typeof (returnF) == 'function') {
                        returnF(res.objects);
                    }
                }

                if (!res.hasOwnProperty('success') && typeof (returnF) == 'function') {
                    returnF(res);
                }

            }

        },
        complete: function () {
            elem.loadingModal('destroy');
        },
        error: function () {
            elem.loadingModal('destroy');
            MesajError("İşlem Sırasında Hata Meydana Geldi. Lütfen Yöneticinize Başvurun.");
        }
    });

}

var cookies = {
    set: function (name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    get: function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    },
    delete: function (name) {
        cookies.set(name, "", -1);
    }
};

var TemplateEngine = {

    RenderTo: function (templateElement, data, target) {

        var template = TemplateEngine.Render(templateElement, data);
        target.append(template);

    },

    RenderAll: function (templateElement, data, target) {

        $.each(data, function (i, item) {
            var res = TemplateEngine.Render(templateElement, item);
            target.append(res);
        });

    },

    Render: function (templateElement, data) {

        var template = templateElement.html();
        var matches = template.match(/{{(.*?)}}/g);

        $.each(matches, function (m, match) {

            var replaced = eval(match);
            template = template.replace(match, replaced);

            //  template = eval(template.replace(new RegExp('#' + i + ''), item)).replace('}}', '').replace('{{', '');
        });

        template = $(template);

        return template;

    },

};



