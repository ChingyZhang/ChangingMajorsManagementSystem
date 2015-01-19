function IsIE() {
    if ($.browser.msie)
        return true;
    else
        return false;
}

function onlyNum(target) {
    $(target).keydown(function (e) {
        return checkKeyCode(e);
    });
    $(target).keyup(function () {
        checkValue($(target));
    });
}


function checkKeyCode(e) {
    if ((e.keyCode < 48 && e.keyCode != 8) || e.keyCode > 57) {
        if (IsIE())
            window.event.returnValue = false;
        else
            return false;
    }
}

function checkValue(target) {
    $(target).val($(target).val().replace(/\D/g, ''));
}

