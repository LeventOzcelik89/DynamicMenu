$(document)

    .on('validate:submit', '[data-formType="ajax"]', function (e) {

        $(this).trigger("before:submit");
        if (location.href.indexOf('localhost') > -1) { console.log(this); console.log('before:submit'); }

        e.preventDefault();

        var _this = $(this);
        var button = _this.find('[type="submit"]');
        var action = button.attr("formaction") || _this.attr("action");
        var method = button.attr("formmethod") || _this.attr("method");
        var target = button.attr("formtarget") || _this.attr("target");
        var title = _this.attr("data-loadingtitle") ? _this.attr("data-loadingtitle") : "Lütfen Bekleyiniz...";
        var formData = new FormData();
        var stringData = _this.find('input:not([type="file"]),select,textarea').serializeArray();
        var indexFile = 0;

        if (target == "_blank") {
            _this.removeAttr('data-formType');
            _this.attr("action", action);
            _this.attr("target", "_blank");
            _this.trigger("submit");
            _this.attr("data-formType", "ajax");
            _this.removeAttr("target");
            return;
        }

        _this.find('[type="submit"]').each(function (c) {
            if ($(this).attr("value") !== undefined && $(this).attr("name") !== undefined && $(this).attr("value") != "" && $(this).attr("name") != "") {
                stringData.push({ name: $(this).attr("name"), value: $(this).attr("value") });
            }
        });

        $.each(stringData, function (key, input) {
            formData.append(input.name, input.value);
        });

        _this.find(".fileupload-container").each(function (e) {
            var table = $(this).data("table");
            var id = $(this).data("id");
            var group = $(this).data("group");
            $(this).find('li.file-item').each(function (c) {
                var file = $(this).data("file");
                if (file) {
                    formData.append(table + '|' + group + '|' + id, file, file.name);
                    indexFile++;
                }
            });
        });

        var settings = {
            url: action,
            type: method,
            data: stringData,
            beforeSend: function () {
                $('body').loadingModal({ text: title, animation: 'rotatingPlane', backgroundColor: 'black' });
            },
            success: function (response) {
                feedback(response.feedback);
                if (response.Result) {
                    var modal = _this.parents(".modal");
                    if (modal.length > 0 && typeof _this.attr('data-modal-close') == "undefined") {
                        modal.modal("hide");
                    }
                    $(".k-pager-refresh.k-link").each(function (i, e) {
                        $(this).trigger("click");
                    });
                }
                _this.trigger("success", response);
                if (location.href.indexOf('localhost') > -1) { console.log(response); console.log('success'); }
            },
            error: function () {
                feedback("SERVER");
            },
            complete: function (response) {
                $('body').loadingModal('destroy');
                _this.parents('.modal').find('.bootbox-close-button').trigger('click');
                _this.trigger("complete", response.responseJSON, response.responseText);
                if (location.href.indexOf('localhost') > -1) { console.log(response.responseJSON); console.log('complete'); }
            }
        };

        if (indexFile > 0) {
            settings["contentType"] = false;
            settings["processData"] = false;
            settings["data"] = formData;
        }

        $.ajax(settings);

    })

    .ready(function () {

        fn_RunFormValidators();

    })

    .on('click', '[data-modal]', function (e) {

        e.preventDefault();
        var _this = $(this);
        if (_this.attr("disabled") == "disabled") { return false; }

        if (_this.attr('data-href') === undefined) { return false; }

        var _blank = _this.attr("data-blank") != undefined;
        var _data = {};
        var _grid = _this.parents('.k-grid').eq(0).data('kendoGrid');
        var _multiple = _grid && _grid.options.selectable.indexOf('Multiple') > -1;
        var _postArray = _multiple == undefined || _multiple == false ? false : (_this.attr('data-show') == 'single' ? false : true);

        var _modal = _this.attr("data-modal") != 'false';
        var _id = [];
        if (_this.attr("data-show") != 'always') {
            _id = _grid ? _grid.select().map(function (i, elem) { return _grid.dataItem(elem)[_grid.wrapper.attr('data-selection')]; }).toArray() :     //  grid varsa gridden al
                _this.attr('data-id') ? [_this.attr('data-id')]                                                                                           //  yoksa elementten
                    : '';
        }
        _data.id = _postArray == false ? _id[0] : _id;
        var _modalType = typeof (_this.attr('data-modalType')) != 'undefined' ? ('type-' + _this.attr('data-modalType')) : ('type-info');
        var _ask = typeof (_this.attr('data-ask')) != 'undefined';

        var _extraData = _this.attr('data-extraData');
        if (typeof (_extraData) != 'undefined') {
            $.each(_extraData.split(','), function (i, item) {
                _data[item] = (typeof ($('#' + item).val()) != 'undefined' ? $('#' + item).val() : '');
            });
        }

        if (_ask) {


            Swal.fire({
                title: "Devam Et ?",
                text: (_this.attr('data-ask') == '' || _this.attr('data-ask') == undefined)
                    ? "İşlemi gerçekleştirmek için onay vermeniz gereklidir. Devam etmek istediğinize emin misiniz !"
                    : _this.attr('data-ask'),
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Evet",
                cancelButtonText: "Hayır",
                closeOnConfirm: false,
                closeOnCancel: false
            }).then((result) => {

                if (result.isConfirmed) {

                    if (_modal) {
                        Kendo_GetRequest(_this.attr('data-href'), _data, _this, _modalType);
                    } else {

                        var __data = '';
                        $.each(_data, function (i, item) { if (item != '' && item != null) { __data += '&' + i + '=' + item; } });

                        var tUrl = window.encodeURI(_this.attr('data-href') + (__data.length > 0 ? (_this.attr('data-href').indexOf('?') > -1 ? '&' : '?') + __data.substring(1) : ''));
                        if (_blank) {
                            window.open(tUrl, '_blank');
                        } else {
                            window.location.href = tUrl;
                        }

                    }
                }

                Swal.close()

            });

        } else {

            if (_modal) {
                Kendo_GetRequest(_this.attr('data-href'), _data, _this, _modalType);
            } else {
                var __data = '';
                $.each(_data, function (i, item) { if (item != '' && item != null) { __data += '&' + i + '=' + item; } });
                var tUrl = window.encodeURI(_this.attr('data-href') + (__data.length > 0 ? (_this.attr('data-href').indexOf('?') > -1 ? '&' : '?') + __data.substring(1) : ''));
                if (_blank) {
                    window.open(tUrl, '_blank');
                } else {
                    window.location.href = tUrl;
                }
            }

        }

        return false;

    })

    .on('click', '.bootbox.modal [data-dismiss="modal"]', function (e) {

        e.preventDefault();

        $(this).parents('.bootbox.modal').modal('hide');

        return false;

    })

    ;

var formSubmitFunc = function (e) {

    var form = this;

    form.classList.add('was-validated');

    if (!form.checkValidity()) {
        e.preventDefault();
        e.stopPropagation();
        return false;
    }

    var formResult = true;
    var inputs = [];
    var $form = $(form);

    var inputTypes = ['input', 'textarea'];
    $.each(inputTypes, function (_i, _item) {
        var elems = $form.find(_item + '[data-validateurl]');
        $.each(elems, function (i, item) {
            inputs.push($(item)[0]);
        });
    });

    $.each(inputs, function (i, item) {

        var vUrl = $(item).attr('data-validateurl');
        var vData = $(item).attr('name') + '=' + $(item).val();

        //  URL Oluştu
        if (typeof ($(item).attr('data-validatefields')) != 'undefined') {
            var newData = [];
            $.each($(item).attr('data-validatefields').split(','), function (i2, item2) {
                newData.push(item2.trim() + '=' + $form.find('#' + item2.trim()).val());
            });
            vData = newData.join('&');
        }

        var JsonResult = $.ajax({
            dataType: 'JSON',
            type: 'POST',
            async: false,
            url: vUrl,
            data: vData,
        }).responseJSON;

        if (!JsonResult) {
            return;
        }
        if (JsonResult.result == false) {

            if ($(item).parents('.form-group').find('.help-block').length < 1) {
                $(item).parents('div').eq(0).append('<span class="help-block with-errors"></span>');
            }
            //  Burayı düzeltebiliriz. Yeni taglara göre düzenlenecek.
            $(item).parents('.form-group').find('.help-block').html('<ul class="list-unstyled with-errors"><li>' + JsonResult.message + '</li></ul>');
            $(item).parents('.form-group').addClass('has-error');

            formResult = false;

        }
        else {

            var elem = $(item).parents('.form-group');
            elem.find('.help-block').html(null);
            elem.removeClass('has-error');

        }

    });

    $form.find('.k-grid[data-required="true"]').each(function (e) {

        if ($(this).data('kendoGrid').dataSource.total() == 0) {
            $(this).css("border", "1px red solid");
            $(this).before('<div class="card-panel red" style="color:red">Lütfen Verileri Eksiksiz Giriniz</div>');
            formResult = false;
        }
    });

    if (formResult) {
        if (typeof ($(this).attr('data-before')) != 'undefined') {
            var func = $(this).attr('data-before');
            if (typeof (window[func]) === 'function') {
                formResult = window[func]();
            }
        }
    }

    if ($form.attr('data-formType') == "ajax") {
        if (formResult) {
            if (location.href.indexOf('localhost') > -1) { console.log($form); console.log('validate:submit'); }
            e.preventDefault();
            e.stopPropagation();
            $form.trigger("validate:submit");
        }
        return false;
    } else {
        return formResult;
    }

};

function fn_RunFormValidators() {

    // Loop over them and prevent submission
    $.each($('.needs-validation'), function (f, form) {

        form.removeEventListener('submit', formSubmitFunc);
        form.addEventListener('submit', formSubmitFunc, { once: true });

    });

}

function Kendo_GetRequest(_url, _data, _button, _modalType, title) {
    var _title = '';
    var $message = $('<div class="clearfix"></div>');
    var $isJSON = false;

    if (_button == undefined) {
        _button = $('<button data-method="GET"></button>');
    }

    $.ajax({
        timeout: 6000000,
        url: _url,
        type: _button.attr('data-method') ?? 'POST',
        data: _data,
        beforeSend: function () {
            $('body').loadingModal({ text: 'Lütfen Bekleyin...', animation: 'rotatingPlane', backgroundColor: 'black' });
            _button.attr("disabled", "disabled");
        },
        success: function (response) {

            //  AJAX Result Control
            if (typeof (response.feedback) != 'undefined' && typeof (response.success) != 'undefined' && typeof (response.objects) != 'undefined') {
                feedback(response.feedback);
                $isJSON = true;
                if (_button.parents('form') != undefined) {
                    _button.parents("form").trigger("success", response);
                }
                if (_button.parents('table') != undefined) {
                    _button.parents('table').trigger("success", response);
                }
                return;
            }

            var tHtml = $(response).filter(function (i, e) { return $(e).attr("data-selector") == "modalContainer" });

            tHtml = tHtml.length > 0 ? tHtml : $(response).find('[data-selector="modalContainer"]');

            $message.append($(response).find('[data-selector="modalContainer"]'));
            var tHtml = $(response).each(function (i, e) {
                if ($(e).attr("data-selector") == "modalContainer") { $message.append($(e)); }
                if ($(this)[0]?.outerHTML?.startsWith('<title>') && $(this)[0]?.outerHTML?.endsWith('</title>')) {
                    _title = $(this)[0].innerHTML;
                }
            });

            if (title != undefined) { _title = title; }

            if (tHtml.children() == 0) { $message.html("Seçici Bulunamadı"); }

        },
        error: function (response) {
            $message.html('<div class="text-center">İç sunucu hatası ' + response.statusText + '</div>');
        },
        complete: function () {

            _button.removeAttr("disabled");
            $('body').loadingModal('destroy');

            if (_button.data("nothing")) //  alttaki işlemlerin yapılmaması için tanımlanmıştır.
                return;

            if (!$isJSON) {

                Dialog($message, {
                    type: _modalType ? _modalType : 'type-info',
                    title: (_title == '' || typeof (_title) == 'undefined') ? "Kayıt Detayı" : _title,
                    onShown: function (dialogRef) {
                        _button.trigger("open:modal");
                        //  $.each(haritalar, function (i, item) { item.map.updateSize(); });
                        fn_RunFormValidators();
                    }
                });

            } else {
                $(".k-pager-refresh.k-link").each(function (i, e) { $(e).trigger('click'); });
            }

        }
    })

}

function Dialog(message, opts) {

    var opts = $.extend(opts, {
        message: message,
        size: 'large',
        locale: 'tr',
        closeButton: true
    });

    bootbox.alert(opts);

}





