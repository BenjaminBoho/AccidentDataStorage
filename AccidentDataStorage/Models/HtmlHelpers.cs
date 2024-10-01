using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccidentDataStorage.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent RenderSortForm(this IHtmlHelper htmlHelper, string sortOrderValue, HttpRequest request)
        {
            TagBuilder formTag = new TagBuilder("form");
            formTag.MergeAttribute("method", "get");
            formTag.MergeAttribute("style", "display: inline;");

            foreach (var key in request.Query.Keys)
            {
                if (key != "sortOrder" && key != "success")
                {
                    TagBuilder inputTag = new TagBuilder("input");
                    inputTag.MergeAttribute("type", "hidden");
                    inputTag.MergeAttribute("name", key);
                    inputTag.MergeAttribute("value", request.Query[key]);
                    formTag.InnerHtml.AppendHtml(inputTag);
                }
            }

            TagBuilder sortOrderInputTag = new TagBuilder("input");
            sortOrderInputTag.MergeAttribute("type", "hidden");
            sortOrderInputTag.MergeAttribute("name", "sortOrder");
            sortOrderInputTag.MergeAttribute("value", sortOrderValue);
            formTag.InnerHtml.AppendHtml(sortOrderInputTag);

            TagBuilder buttonTag = new TagBuilder("button");
            buttonTag.MergeAttribute("type", "submit");
            buttonTag.AddCssClass("btn btn-link btn-sm");

            TagBuilder iconTag = new TagBuilder("i");
            iconTag.AddCssClass("fa fa-sort");
            buttonTag.InnerHtml.AppendHtml(iconTag);

            formTag.InnerHtml.AppendHtml(buttonTag);

            return formTag;
        }
    }
}