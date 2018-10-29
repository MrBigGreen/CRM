jQuery.fn.placeholder = function () {
    //var i = document.createElement('input'),
	//	placeholdersupport = 'placeholder' in i;
    //if (!placeholdersupport) {
        var inputs = jQuery(this);
        inputs.each(function () {
            var input = jQuery(this),
				text = input.attr('placeholder');
           // input.val(text);
         
            if (input.val() != text && input.val() != "") {
                input.val(input.val());

                // input.removeClass("colorccc");
            } else {
                input.val(text);
                // input.addClass("colorccc");
            }
            //   placeholder.insertAfter(input);
            input.blur(function (e) {

                if (input.val() != text && input.val()!="") {
                    input.val(input.val());
                    //   input.removeClass("colorccc");
                } else {
                    input.val(text);
                    //  input.addClass("colorccc");
                }
            });
            input.click(function (e) {
                if (input.val() == text) {
                    input.val("");
                }
            });
        });
    //}
    return this;
};