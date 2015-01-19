using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;
using ChangingMajorsManagementSystem.Models;

namespace ChangingMajorsManagementSystem.Helper
{
    /// <summary>
    /// HtmlHelper扩展
    /// </summary>
    public static class MvcHtmlHelper
    {
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="helper">表示支持在视图中呈现AJAx方案中的HTMl</param>
        /// <param name="pageInfo">要显示的信息</param>
        /// <param name="pageUrl"></param>
        /// <param name="ajaxOptions">表示用于在MVC应用程序中运行AJAx脚本的选项设置</param>
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this AjaxHelper helper,
            PageInfo pageInfo, Func<Int32, String> pageUrl, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return PageLinksHelper(helper, pageInfo, pageUrl, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }


        private static MvcHtmlString PageLinksHelper(this AjaxHelper helper,
            PageInfo pageInfo, Func<Int32, String> pageUrl, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            pageInfo.TotalItems = pageInfo.TotalItems == 0 ? 1 : pageInfo.TotalItems;
            //String ajaxClick = "Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), " +
            //    "{ insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId: '" + ajaxOptions.UpdateTargetId + "', onComplete: Function.createDelegate(this, " + ajaxOptions.OnComplete + ") });";
            //var asd = pageUrl(pageInfo.CurrentPage - 1);


            var asd = pageUrl(pageInfo.CurrentPage);

            String ajaxKeyPress = "$.ajax({type: '" + (ajaxOptions.HttpMethod == "" ? "GET" : ajaxOptions.HttpMethod) + "'" +
                ",url: '" + pageUrl(-1).Replace("-1", "") + "',data: {'page':$('#pageSkip').val()},success: function (data) {$('#" + ajaxOptions.UpdateTargetId + "').html(data)}});";

            StringBuilder result = new StringBuilder();

            result.AppendLine("共" + pageInfo.TotalPages + "页，当前第" + pageInfo.CurrentPage + "页");

            var firstPage = new TagBuilder("a");
            firstPage.MergeAttribute("style", "cursor:pointer;");
            firstPage.InnerHtml = "首页";
            string a = pageUrl(0);
            if (pageInfo.CurrentPage != 1)
            {
                firstPage.MergeAttribute("href", pageUrl(1));
                firstPage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                firstPage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                firstPage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                firstPage.MergeAttribute("data-ajax", "true");

            }
            else
            {
                firstPage.MergeAttribute("style", "color:Gray");
            }
            result.AppendLine(firstPage.ToString());

            var prePage = new TagBuilder("a");
            prePage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.CurrentPage > 1)
            {
                prePage.MergeAttribute("href", pageUrl((pageInfo.CurrentPage - 1)));
                prePage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                prePage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                prePage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                prePage.MergeAttribute("data-ajax", "true");
            }
            else
            {
                prePage.MergeAttribute("style", "color:Gray");
            }
            prePage.InnerHtml = "上一页";
            result.AppendLine(prePage.ToString());

            result.AppendLine("&nbsp;第");
            var inputPage = new TagBuilder("input");
            inputPage.MergeAttribute("id", "pageSkip");
            inputPage.MergeAttribute("type", "text");
            inputPage.MergeAttribute("value", pageInfo.CurrentPage.ToString());
            inputPage.MergeAttribute("style", "width:30px");
            inputPage.MergeAttribute("onkeyup", @"if($('#pageSkip').val().match(/^\d+$/)==null){this.value=this.value.replace(/\D/g,'')}" +
                "else{if(parseInt(this.value)>parseInt($('#hiddenTotalPages').val())){$('#pageSkip').css({ color: '#ff0011'}); }else{$('#pageSkip').css({ color: '#000000'});" + ajaxKeyPress + "}}");
            result.AppendLine(inputPage.ToString());
            result.AppendLine("页&nbsp;");

            var hideInput = new TagBuilder("input");
            hideInput.MergeAttribute("id", "hiddenTotalPages");
            hideInput.MergeAttribute("type", "hidden");
            hideInput.MergeAttribute("value", pageInfo.TotalPages.ToString());
            result.AppendLine(hideInput.ToString());

            var nextPage = new TagBuilder("a");
            nextPage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.CurrentPage < pageInfo.TotalPages)
            {
                nextPage.MergeAttribute("href", pageUrl((pageInfo.CurrentPage + 1)));
                nextPage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                nextPage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                nextPage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                nextPage.MergeAttribute("data-ajax", "true");
            }
            else
            {
                nextPage.MergeAttribute("style", "color:Gray");
            }
            nextPage.InnerHtml = "下一页";
            result.AppendLine(nextPage.ToString());

            var lastPage = new TagBuilder("a");
            lastPage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.TotalPages != 1)
            {
                lastPage.MergeAttribute("href", pageUrl(pageInfo.TotalPages));
                lastPage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                lastPage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                lastPage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                lastPage.MergeAttribute("data-ajax", "true");
            }
            else
            {
                lastPage.MergeAttribute("style", "color:Gray");
            }
            lastPage.InnerHtml = "末页";
            result.AppendLine(lastPage.ToString());

            TagBuilder div = new TagBuilder("div");
            div.MergeAttributes(htmlAttributes);
            div.InnerHtml = result.ToString();

            return MvcHtmlString.Create(div.ToString());
        }
    }


}