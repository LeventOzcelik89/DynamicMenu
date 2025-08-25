using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            string? ItemTemplate = null,
            Dictionary<string, object>? htmlAttributes = null) 
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unary = expression.Body as UnaryExpression;
                memberExpression = unary?.Operand as MemberExpression;
            }
            var propertyName = memberExpression?.Member.Name;

            // Modeldeki ilgili property'sinin değerini al
            var modelValue = helper.ViewData.Model != null ? expression.Compile().Invoke(helper.ViewData.Model) : default(TProperty);

            var baseHtmlAttributes = new Dictionary<string, object> { { "class", "form-control" } };
            var lastHtmlAttributes = htmlAttributes != null ? htmlAttributes.Concat(baseHtmlAttributes).ToDictionary(kvp => kvp.Key, kvp => kvp.Value) : baseHtmlAttributes;

            var dropdown = helper.DropDownListFor(expression, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[0], placeHolder, lastHtmlAttributes);

            string optionHtml = string.IsNullOrEmpty(ItemTemplate) ? "item.Name" : ItemTemplate; 

            var script = new HtmlString($@"<script type=""text/javascript"">
    ReadData('{remoteDataSourceUrl}', null, function(res){{
        $.each(res, function(i, item){{
            var optElem = $('<option>').val(item.Id).html({optionHtml});            
            if(item.Id == '{modelValue}'){{ optElem.attr('selected', 'selected'); }} 
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


        public static IHtmlContent DropDown(this IHtmlHelper helper) { 
            return new HtmlString("<div>Test</div>");
        }

    }
}
