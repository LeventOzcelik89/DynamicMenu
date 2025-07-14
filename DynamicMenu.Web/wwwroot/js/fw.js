$(document)

    .on('validate:submit', '[data-formType="ajax"]', function (e) {

        $(this).trigger("before:submit");
        if (location.href.indexOf('localhost') > -1) { console.log(this); console.log('before:submit'); }

        e.preventDefault();

        var _this = $(this);
        var button = _this.find('.active[type="submit"]');
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
            _this.attr("data-formType", "Ajax");
            _this.removeAttr("target");
            return;
        }


        _this.find('.active[type="submit"]').each(function (c) {
            if ($(this).attr("value") != "" && $(this).attr("name") != "") {
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
                feedback(response.FeedBack);
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
  
    ;




function fn_RunFormValidators() {

    //Bütün ek validate işlemleri burda yapılacak
    $('form').livequery(function (e) {

        var _this = $(this);

        _this.validator().submit(function (e) {

            var formResult = true;

            if (e.isDefaultPrevented()) {

                formResult = false;

            }
            else {



                var inputs = [];
                var $form = $(this);

                var inputTypes = ['input', 'textarea'];
                $.each(inputTypes, function (_i, _item) {

                    var elems = $($form).find(_item + '[data-validateurl]');

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

                            newData.push(item2.trim() + '=' + $($form).find('#' + item2.trim()).val());

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

                _this.find('.k-grid[data-required="true"]').each(function (e) {

                    if ($(this).data('kendoGrid').dataSource.total() == 0) {
                        $(this).css("border", "1px red solid");
                        $(this).before('<div class="card-panel red" style="color:red">Lütfen Verileri Eksiksiz Giriniz</div>');
                        formResult = false;
                    }
                });

            }

            if (formResult) {

                if (typeof ($(this).attr('data-before')) != 'undefined') {

                    var func = $(this).attr('data-before');

                    if (typeof (window[func]) === 'function') {
                        formResult = window[func]();
                    }

                }

            }

            if (_this.attr('data-formType') == "Ajax") {
                if (formResult) {
                    if (location.href.indexOf('localhost') > -1) { console.log(_this); console.log('validate:submit'); }
                    _this.trigger("validate:submit");
                }
                return false;
            } else {
                return formResult;
            }

        });

    });

}
