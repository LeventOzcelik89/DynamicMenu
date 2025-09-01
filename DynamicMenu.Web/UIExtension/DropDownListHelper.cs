using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace DynamicMenu.Web.UIExtension
{
    public static class DropDownHelper
    {
        public static IHtmlContent DropDownFor<TModel, TProperty>(
            this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string labelText,
            string remoteDataSourceUrl,
            string placeHolder = "Lütfen seçim yapın",
            string? ItemValueTemplate = "item.Id",
            string? ItemTextTemplate = "item.Name",
            Dictionary<string, object>? htmlAttributes = null)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unary = expression.Body as UnaryExpression;
                memberExpression = unary?.Operand as MemberExpression;
            }
            var propertyName = memberExpression?.Member.Name;

            string? modelValueString = "";
            var modelValue = helper.ViewData.Model != null ? expression.Compile().Invoke(helper.ViewData.Model) : default(TProperty);
            if (modelValue != null && modelValue.GetType().IsEnum)
            {
                modelValueString = Convert.ToInt32(modelValue).ToString();
            }
            else if (modelValue != null)
            {
                modelValueString = modelValue.ToString();
            }

            var baseHtmlAttributes = new Dictionary<string, object> { { "class", "form-control" } };
            var lastHtmlAttributes = htmlAttributes != null ? htmlAttributes.Concat(baseHtmlAttributes).ToDictionary(kvp => kvp.Key, kvp => kvp.Value) : baseHtmlAttributes;

            var dropdown = helper.DropDownListFor(expression, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[0], placeHolder, lastHtmlAttributes);

            var valueHtml = string.IsNullOrEmpty(ItemValueTemplate) ? "item.Id" : ItemValueTemplate;
            var optionHtml = string.IsNullOrEmpty(ItemTextTemplate) ? "item.Name" : ItemTextTemplate;

            var script = new HtmlString($@"<script type=""text/javascript"">
    ReadData('{remoteDataSourceUrl}', null, function(res){{
        $.each(res, function(i, item){{
            var optElem = $('<option>').val({valueHtml}).html({optionHtml});            
            if({valueHtml} == '{modelValueString}'){{ optElem.attr('selected', 'selected'); }} 
            $('#{propertyName}').append(optElem);
        }});
    }});
</script>");

            var label = new Microsoft.AspNetCore.Mvc.Rendering.TagBuilder("label");
            label.Attributes.Add("for", propertyName);
            label.AddCssClass("form-label");
            label.InnerHtml.Append(labelText);

            var container = new Microsoft.AspNetCore.Mvc.Rendering.TagBuilder("div");
            container.AddCssClass("mb-3");

            container.InnerHtml.AppendHtml(label);
            container.InnerHtml.AppendHtml(dropdown);
            container.InnerHtml.AppendHtml(script);

            return container;
        }


        public static IHtmlContent DropDown(
            this IHtmlHelper helper,
            string name,
            string labelText,
            string remoteDataSourceUrl,
            string? value = null,
            string? placeHolder = "Lütfen seçim yapın",
            string? ItemValueTemplate = "item.Id",
            string? ItemTextTemplate = "item.Name",
            Dictionary<string, object>? htmlAttributes = null)
        {

            var baseHtmlAttributes = new Dictionary<string, object> { { "class", "form-control" } };
            var lastHtmlAttributes = htmlAttributes != null ? htmlAttributes.Concat(baseHtmlAttributes).ToDictionary(kvp => kvp.Key, kvp => kvp.Value) : baseHtmlAttributes;

            var dropdown = helper.DropDownList(name, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[0], placeHolder, lastHtmlAttributes);

            var valueHtml = string.IsNullOrEmpty(ItemValueTemplate) ? "item.Id" : ItemValueTemplate;
            var optionHtml = string.IsNullOrEmpty(ItemTextTemplate) ? "item.Name" : ItemTextTemplate;

            var script = new HtmlString($@"<script type=""text/javascript"">
    ReadData('{remoteDataSourceUrl}', null, function(res){{
        $.each(res, function(i, item){{
            var optElem = $('<option>').val({valueHtml}).html({optionHtml});            
            if({valueHtml} == '{value}'){{ optElem.attr('selected', 'selected'); }} 
            $('#{name}').append(optElem);
        }});
    }});
</script>");

            var label = new Microsoft.AspNetCore.Mvc.Rendering.TagBuilder("label");
            label.Attributes.Add("for", name);
            label.AddCssClass("form-label");
            label.InnerHtml.Append(labelText);

            var container = new Microsoft.AspNetCore.Mvc.Rendering.TagBuilder("div");
            container.AddCssClass("mb-3");

            container.InnerHtml.AppendHtml(label);
            container.InnerHtml.AppendHtml(dropdown);
            container.InnerHtml.AppendHtml(script);

            return container;

        }

    }
}
