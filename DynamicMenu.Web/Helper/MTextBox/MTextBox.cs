using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DynamicMenu.Web.Helper
{
    public class MTextBox<T> : WidgetBase, IFormEditor where T : struct
    {
        //
        // Summary:
        //     If this property is enabled and you have configured min and/or max values, and
        //     the user enters a value that falls out of that range, the value will automatically
        //     be set to either the minimum or maximum allowed value.
        public bool? AutoAdjust { get; set; }

        //
        // Summary:
        //     Specifies the culture info used by the widget.
        public string Culture { get; set; }

        //
        // Summary:
        //     Specifies the number precision applied to the widget value and when the MTextBox
        //     is focused. If not set, the precision defined by the current culture is used.
        //     If the user enters a number with a greater precision than is currently configured,
        //     the widget value will be rounded. For example, if decimals is 2 and the user
        //     inputs 12.346, the value will become 12.35. If decimals is 1 the user inputs
        //     12.99, the value will become 13.00.Compare with the format property.
        public int? Decimals { get; set; }

        //
        // Summary:
        //     Specifies the text of the tooltip on the down arrow.
        public string DownArrowText { get; set; }

        //
        // Summary:
        //     Specifies the factor by which the value is multiplied. The obtained result is
        //     used as edit value. So, if 15 as string is entered in the MTextBox and
        //     the factor value is set to 100 the visual value will be 1500. On blur the visual
        //     value will be divided by 100 thus scaling the widget value to the original proportion.
        public double? Factor { get; set; }

        //
        // Summary:
        //     Specifies the number format used when the widget is not focused. Check this page
        //     for all valid number formats.Compare with the decimals property.
        public string Format { get; set; }

        //
        // Summary:
        //     Specifies the inputmode attribute of the inner <input /> element. It is used
        //     to specify the type of on-screen keyboard that should be displayed when the user
        //     focuses the input.
        public string InputMode { get; set; }

        //
        // Summary:
        //     Adds a label before the input. If the input has no id attribute, a generated
        //     id will be assigned. The string and the function parameters are setting the inner
        //     HTML of the label.
        public MTextBoxLabelSettings<T> Label { get; } = new MTextBoxLabelSettings<T>();


        //
        // Summary:
        //     Specifies the largest value the user can enter.
        public T? Max { get; set; }

        //
        // Summary:
        //     Specifies the smallest value the user can enter.
        public T? Min { get; set; }

        //
        // Summary:
        //     The hint displayed by the widget when it is empty. Not set by default.
        public string Placeholder { get; set; }

        //
        // Summary:
        //     The configuration for the prefix adornment of the component.
        public MTextBoxPrefixOptionsSettings<T> PrefixOptions { get; } = new MTextBoxPrefixOptionsSettings<T>();


        //
        // Summary:
        //     Specifies whether the decimals length should be restricted during typing. The
        //     length of the fraction is defined by the decimals value.
        public bool? RestrictDecimals { get; set; }

        //
        // Summary:
        //     Specifies whether the value should be rounded or truncated. The length of the
        //     fraction is defined by the decimals value.
        public bool? Round { get; set; }

        //
        // Summary:
        //     When set to true, the text of the input will be selected after the widget is
        //     focused.
        public bool? SelectOnFocus { get; set; }

        //
        // Summary:
        //     Specifies whether the up and down spin buttons should be rendered
        public bool? Spinners { get; set; }

        //
        // Summary:
        //     Specifies the value used to increment or decrement widget value.
        public T? Step { get; set; }

        //
        // Summary:
        //     The configuration for the suffix adornment of the component.
        public MTextBoxSuffixOptionsSettings<T> SuffixOptions { get; } = new MTextBoxSuffixOptionsSettings<T>();


        //
        // Summary:
        //     Specifies the text of the tooltip on the up arrow.
        public string UpArrowText { get; set; }

        //
        // Summary:
        //     Specifies the value of the MTextBox widget.
        public T? Value { get; set; }

        //
        // Summary:
        //     Enables or disables the textbox
        public bool? Enable { get; set; }

        //
        // Summary:
        //     Sets the size of the component.
        public ComponentSize? Size { get; set; }

        //
        // Summary:
        //     Sets a value controlling the border radius.
        public Rounded? Rounded { get; set; }

        //
        // Summary:
        //     Sets a value controlling how the color is applied.
        public FillMode? FillMode { get; set; }

        public MTextBox(ViewContext viewContext)
            : base(viewContext)
        {
        }

        protected override TagBuilder GetElement()
        {
            var expressionProvider = new ModelExpressionProvider(base.HtmlHelper.MetadataProvider);
            var modelExplorer = expressionProvider.CreateModelExpression(base.HtmlHelper.ViewData, base.Name);


            //  ModelExplorer modelExplorer = base.Explorer ?? Microsoft.AspNetCore.Mvc.ViewFeatures.ExpressionMetadataProvider.FromStringExpression(base.Name, base.HtmlHelper.ViewData, base.HtmlHelper.MetadataProvider);
            TagBuilder tagBuilder = base.Generator.GenerateNumericInput(base.ViewContext, modelExplorer, Id, base.Name, Value, string.Empty, base.HtmlAttributes);
            if (Value.HasValue)
            {
                tagBuilder.MergeAttribute("value", "{0}".FormatWith(Value));
            }

            tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;
            return tagBuilder;
        }

        protected override void WriteHtml(TextWriter writer)
        {
            GetElement().WriteTo(writer, base.HtmlEncoder);
            base.WriteHtml(writer);
        }

        public override IDictionary<string, object> Serialize()
        {
            return SerializeSettings();
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            writer.Write(base.Initializer.Initialize(base.Selector, "MTextBox", Serialize()));
        }

        //
        // Summary:
        //     Serialize current instance to Dictionary
        protected override Dictionary<string, object> SerializeSettings()
        {
            Dictionary<string, object> dictionary = base.SerializeSettings();
            if (AutoAdjust.HasValue)
            {
                dictionary["autoAdjust"] = AutoAdjust;
            }

            string culture = Culture;
            if (culture != null && culture.HasValue())
            {
                dictionary["culture"] = Culture;
            }

            if (Decimals.HasValue)
            {
                dictionary["decimals"] = Decimals;
            }

            string downArrowText = DownArrowText;
            if (downArrowText != null && downArrowText.HasValue())
            {
                dictionary["downArrowText"] = DownArrowText;
            }

            if (Factor.HasValue)
            {
                dictionary["factor"] = Factor;
            }

            string format = Format;
            if (format != null && format.HasValue())
            {
                dictionary["format"] = Format;
            }

            string inputMode = InputMode;
            if (inputMode != null && inputMode.HasValue())
            {
                dictionary["inputMode"] = InputMode;
            }

            Dictionary<string, object> dictionary2 = Label.Serialize();
            if (dictionary2.Any())
            {
                dictionary["label"] = dictionary2;
            }

            if (Max.HasValue)
            {
                dictionary["max"] = Max;
            }

            if (Min.HasValue)
            {
                dictionary["min"] = Min;
            }

            string placeholder = Placeholder;
            if (placeholder != null && placeholder.HasValue())
            {
                dictionary["placeholder"] = Placeholder;
            }

            Dictionary<string, object> dictionary3 = PrefixOptions.Serialize();
            if (dictionary3.Any())
            {
                dictionary["prefixOptions"] = dictionary3;
            }

            if (RestrictDecimals.HasValue)
            {
                dictionary["restrictDecimals"] = RestrictDecimals;
            }

            if (Round.HasValue)
            {
                dictionary["round"] = Round;
            }

            if (SelectOnFocus.HasValue)
            {
                dictionary["selectOnFocus"] = SelectOnFocus;
            }

            if (Spinners.HasValue)
            {
                dictionary["spinners"] = Spinners;
            }

            if (Step.HasValue)
            {
                dictionary["step"] = Step;
            }

            Dictionary<string, object> dictionary4 = SuffixOptions.Serialize();
            if (dictionary4.Any())
            {
                dictionary["suffixOptions"] = dictionary4;
            }

            string upArrowText = UpArrowText;
            if (upArrowText != null && upArrowText.HasValue())
            {
                dictionary["upArrowText"] = UpArrowText;
            }

            if (Enable.HasValue)
            {
                dictionary["enable"] = Enable;
            }

            if (Size.HasValue)
            {
                dictionary["size"] = Size?.Serialize();
            }

            if (Rounded.HasValue)
            {
                dictionary["rounded"] = Rounded?.Serialize();
            }

            if (FillMode.HasValue)
            {
                dictionary["fillMode"] = FillMode?.Serialize();
            }

            return dictionary;
        }
    }
}
