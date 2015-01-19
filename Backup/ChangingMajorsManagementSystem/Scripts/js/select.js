
function init(id) {
    selects = document.getElementById(id).getElementsByTagName('select');
}

var selects;



var isIE = (document.all && window.ActiveXObject && !window.opera) ? true : false;

//function $(id) {
//    return document.getElementById(id);
//}

function stopBubbling(ev) {
    ev.stopPropagation();
}

function rSelects() {
    for (i = 0; i < selects.length; i++) {
        //alert(selects[i].name);
        if ($("#" + selects[i].name).parent().children().length == 1) {
            // $(selects).css("display", "none");
            selects[i].style.display = 'none';
            //select_tag = $("div");
            select_tag = document.createElement('div');
            //$(selects).attr("id", "select_" + selects[i].name);
            select_tag.id = 'select_' + selects[i].name;
            //alert($(select_tag).attr("id"));
            //        $(select_tag).attr("aa", $(selects[i]).val())
            //select_tag.name = selects[i].value;
            //        alert($(select_tag).attr("aa"));
            //$(selects).attr("class", "select_box");
            select_tag.className = 'select_box';
            //$(selects[i]).parent().append(selects[i]);
            selects[i].parentNode.insertBefore(select_tag, selects[i]);


            select_info = document.createElement('div');
            select_info.id = 'select_info_' + selects[i].name;



            $(select_info).attr("value", $(selects[i]).val())
            //alert($(select_info).attr("value"));



            select_info.className = 'tag_select';
            select_info.style.cursor = 'pointer';
            select_tag.appendChild(select_info);

            var inputt = $("<input type='hidden' value='' id='' />");
            $(inputt).attr("id", selects[i].name);
            $(inputt).attr("name", selects[i].name);
            $(inputt).attr("value", $(selects[i]).val())
            $(select_tag).append(inputt);

            //我添加
            boxul = document.createElement('div');
            boxul.id = 'boxul_' + selects[i].name;
            boxul.className = 'boxul_options';
            boxul.style.position = 'absolute';
            boxul.style.cursor = 'pointer';
            boxul.style.display = 'none';
            boxul.style.zIndex = '9999';
            select_tag.appendChild(boxul);
            //我添加

            select_ul = document.createElement('ul');
            select_ul.id = 'options_' + selects[i].name;
            select_ul.className = 'tag_options';
            boxul.appendChild(select_ul);




            rOptions(i, selects[i].name);

            mouseSelects(selects[i].name);

            if (isIE) {
                selects[i].onclick = new Function("clickLabels3('" + selects[i].name + "');window.event.cancelBubble = true;");
            }
            else if (!isIE) {
                selects[i].onclick = new Function("clickLabels3('" + selects[i].name + "')");
                selects[i].addEventListener("click", stopBubbling, false);
            }
        }



        //$(selects[i]).remove();

    }
}


function rOptions(i, name) {
    var options = selects[i].getElementsByTagName('option');
    var options_ul = 'options_' + name;
    for (n = 0; n < selects[i].options.length; n++) {
        option_li = document.createElement('li');
        option_li.style.cursor = 'pointer';
        option_li.className = 'open';


        //alert($(selects[i].options[n]).attr("value"));
        $(option_li).attr("fuckme", $(selects[i].options[n]).attr("value"));

        //alert($(option_li).attr("value"));


        document.getElementById(options_ul).appendChild(option_li);
        //$(options_ul).appendChild(option_li);

        //alert(selects[i].options[n].value);
        option_text = document.createTextNode(selects[i].options[n].text);
        option_li.appendChild(option_text);

        option_selected = selects[i].options[n].selected;

        if (option_selected) {
            option_li.className = 'open_selected';
            option_li.id = 'selected_' + name;
            document.getElementById('select_info_' + name).appendChild(document.createTextNode(option_li.innerHTML));
            //$('select_info_' + name).appendChild(document.createTextNode(option_li.innerHTML));
        }

        option_li.onmouseover = function () { this.className = 'open_hover'; }
        option_li.onmouseout = function () {
            if (this.id == 'selected_' + name) {
                this.className = 'open_selected';
            }
            else {
                this.className = 'open';
            }
        }

        option_li.onclick = new Function("clickOptions(" + i + "," + n + ",'" + selects[i].name + "')");
    }
}

function mouseSelects(name) {
    //alert('a');
    var sincn = 'select_info_' + name;
    document.getElementById(sincn).onmouseover = function () { if (this.className == 'tag_select') this.className = 'tag_select_hover'; }
    document.getElementById(sincn).onmouseout = function () { if (this.className == 'tag_select_hover') this.className = 'tag_select'; }
    //$(sincn).onmouseover = function () { if (this.className == 'tag_select') this.className = 'tag_select_hover'; }
    //$(sincn).onmouseout = function () { if (this.className == 'tag_select_hover') this.className = 'tag_select'; }

    if (isIE) {
        document.getElementById(sincn).onclick = new Function("clickSelects('" + name + "');window.event.cancelBubble = true;");
        //$(sincn).onclick = new Function("clickSelects('" + name + "');window.event.cancelBubble = true;");
    }
    else if (!isIE) {
        document.getElementById(sincn).onclick = new Function("clickSelects('" + name + "');");
        //$(sincn).onclick = new Function("clickSelects('" + name + "');");
        document.getElementById('select_info_' + name).addEventListener("click", stopBubbling, false);
        //$('select_info_' + name).addEventListener("click", stopBubbling, false);
    }

}

function clickSelects(name) {
    var sincn = 'select_info_' + name;
    var sinul = 'boxul_' + name;
    //alert(name);
    for (i = 0; i < selects.length; i++) {
        //alert(selects[i].name);
        if (selects[i].name == name) {
            if (document.getElementById(sincn).className == 'tag_select_hover') {
                document.getElementById(sincn).className = 'tag_select_open';
                document.getElementById(sinul).style.display = '';
            }
            else if (document.getElementById(sincn).className == 'tag_select_open') {
                document.getElementById(sincn).className = 'tag_select_hover';
                document.getElementById(sinul).style.display = 'none';
            }
        }
        else {
            document.getElementById('select_info_' + selects[i].name).className = 'tag_select';
            //$('select_info_' + selects[i].name).className = 'tag_select';
            document.getElementById('boxul_' + selects[i].name).style.display = 'none';
            //$('options_' + selects[i].name).style.display = 'none';
        }
    }
}

function clickOptions(i, n, name) {

    var li = document.getElementById('options_' + name).getElementsByTagName('li');
    document.getElementById('selected_' + name).className = 'open';
    document.getElementById('selected_' + name).id = '';
    //$('selected_' + name).className = 'open';
    //$('selected_' + name).id = '';
    li[n].id = 'selected_' + name;
    li[n].className = 'open_hover';
    document.getElementById('select_' + name).removeChild(document.getElementById('select_info_' + name));
    //$('select_' + name).removeChild($('select_info_' + name));

    select_info = document.createElement('div');
    select_info.id = 'select_info_' + name;
    select_info.className = 'tag_select';
    select_info.style.cursor = 'pointer';
    document.getElementById('boxul_' + name).parentNode.insertBefore(select_info, document.getElementById('boxul_' + name));
    //$('options_' + name).parentNode.insertBefore(select_info, $('options_' + name));

    mouseSelects(name);

    $("#select_info_" + name).attr("value", $(li[n]).attr("fuckme"));
    $("#" + name).val($(li[n]).attr("fuckme"));

    document.getElementById('select_info_' + name).appendChild(document.createTextNode(li[n].innerHTML));
    //$('select_info_' + name).appendChild(document.createTextNode(li[n].innerHTML));
    document.getElementById('boxul_' + name).style.display = 'none';
    //$('options_' + name).style.display = 'none';
    document.getElementById('select_info_' + name).className = 'tag_select';
    //$('select_info_' + name).className = 'tag_select';
    selects[i].options[n].selected = 'selected';

}

//$(function () {


//    $("select").live("click", function () { 
//        
//    });

//    bodyclick = $(document.getElementsByTagName('body').item(0));

//    selects = document.getElementsByTagName('select');

//    rSelects();

//    $(bodyclick).click(function () {
//        for (i = 0; i < selects.length; i++) {
//            $('#select_info_' + selects[i].name).attr("class", "tag_select");
//            $('#options_' + selects[i].name).css("display", "none");
//        }
//    });
//});


//window.onload = function (e) {
//    selects = document.getElementsByTagName('body').item(0).getElementsByTagName('select');
//    bodyclick = document.getElementsByTagName('body').item(0);

//    rSelects();
//    bodyclick.onclick = function () {
//        for (i = 0; i < selects.length; i++) {
//            $('select_info_' + selects[i].name).className = 'tag_select';
//            $('options_' + selects[i].name).style.display = 'none';
//        }
//    }
//}