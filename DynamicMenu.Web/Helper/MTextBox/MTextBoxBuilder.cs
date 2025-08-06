namespace DynamicMenu.Web.Helper
{
    public class MTextBoxBuilder<T> : MvvmWidgetBuilderBase<MTextBox<T>, MTextBoxBuilder<T>> where T : struct
    {
        public MTextBoxBuilder(MTextBox<T> component)
            : base(component)
        {
        }

        //
        // Summary:
        //     If this property is enabled and you have configured min and/or max values, and
        //     the user enters a value that falls out of that range, the value will automatically
        //     be set to either the minimum or maximum allowed value.
        //
        // Parameters:
        //   value:
        //     The value for AutoAdjust
        public MTextBoxBuilder<T> AutoAdjust(bool value)
        {
            base.Container.AutoAdjust = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the culture info used by the widget.
        //
        // Parameters:
        //   value:
        //     The value for Culture
        public MTextBoxBuilder<T> Culture(string value)
        {
            base.Container.Culture = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the number precision applied to the widget value and when the MTextBox
        //     is focused. If not set, the precision defined by the current culture is used.
        //     If the user enters a number with a greater precision than is currently configured,
        //     the widget value will be rounded. For example, if decimals is 2 and the user
        //     inputs 12.346, the value will become 12.35. If decimals is 1 the user inputs
        //     12.99, the value will become 13.00.Compare with the format property.
        //
        // Parameters:
        //   value:
        //     The value for Decimals
        public MTextBoxBuilder<T> Decimals(int value)
        {
            base.Container.Decimals = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the text of the tooltip on the down arrow.
        //
        // Parameters:
        //   value:
        //     The value for DownArrowText
        public MTextBoxBuilder<T> DownArrowText(string value)
        {
            base.Container.DownArrowText = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the factor by which the value is multiplied. The obtained result is
        //     used as edit value. So, if 15 as string is entered in the MTextBox and
        //     the factor value is set to 100 the visual value will be 1500. On blur the visual
        //     value will be divided by 100 thus scaling the widget value to the original proportion.
        //
        //
        // Parameters:
        //   value:
        //     The value for Factor
        public MTextBoxBuilder<T> Factor(double value)
        {
            base.Container.Factor = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the number format used when the widget is not focused. Check this page
        //     for all valid number formats.Compare with the decimals property.
        //
        // Parameters:
        //   value:
        //     The value for Format
        public MTextBoxBuilder<T> Format(string value)
        {
            base.Container.Format = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the inputmode attribute of the inner <input /> element. It is used
        //     to specify the type of on-screen keyboard that should be displayed when the user
        //     focuses the input.
        //
        // Parameters:
        //   value:
        //     The value for InputMode
        public MTextBoxBuilder<T> InputMode(string value)
        {
            base.Container.InputMode = value;
            return this;
        }

        //
        // Summary:
        //     Adds a label before the input. If the input has no id attribute, a generated
        //     id will be assigned. The string and the function parameters are setting the inner
        //     HTML of the label.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the label setting.
        public MTextBoxBuilder<T> Label(Action<MTextBoxLabelSettingsBuilder<T>> configurator)
        {
            base.Container.Label.MTextBox = base.Container;
            configurator(new MTextBoxLabelSettingsBuilder<T>(base.Container.Label));
            return this;
        }

        //
        // Summary:
        //     Specifies the largest value the user can enter.
        //
        // Parameters:
        //   value:
        //     The value for Max
        public MTextBoxBuilder<T> Max(T? value)
        {
            base.Container.Max = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the smallest value the user can enter.
        //
        // Parameters:
        //   value:
        //     The value for Min
        public MTextBoxBuilder<T> Min(T? value)
        {
            base.Container.Min = value;
            return this;
        }

        //
        // Summary:
        //     The hint displayed by the widget when it is empty. Not set by default.
        //
        // Parameters:
        //   value:
        //     The value for Placeholder
        public MTextBoxBuilder<T> Placeholder(string value)
        {
            base.Container.Placeholder = value;
            return this;
        }

        //
        // Summary:
        //     The configuration for the prefix adornment of the component.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the prefixoptions setting.
        public MTextBoxBuilder<T> PrefixOptions(Action<MTextBoxPrefixOptionsSettingsBuilder<T>> configurator)
        {
            base.Container.PrefixOptions.MTextBox = base.Container;
            configurator(new MTextBoxPrefixOptionsSettingsBuilder<T>(base.Container.PrefixOptions));
            return this;
        }

        //
        // Summary:
        //     Specifies whether the decimals length should be restricted during typing. The
        //     length of the fraction is defined by the decimals value.
        //
        // Parameters:
        //   value:
        //     The value for RestrictDecimals
        public MTextBoxBuilder<T> RestrictDecimals(bool value)
        {
            base.Container.RestrictDecimals = value;
            return this;
        }

        //
        // Summary:
        //     Specifies whether the decimals length should be restricted during typing. The
        //     length of the fraction is defined by the decimals value.
        public MTextBoxBuilder<T> RestrictDecimals()
        {
            base.Container.RestrictDecimals = true;
            return this;
        }

        //
        // Summary:
        //     Specifies whether the value should be rounded or truncated. The length of the
        //     fraction is defined by the decimals value.
        //
        // Parameters:
        //   value:
        //     The value for Round
        public MTextBoxBuilder<T> Round(bool value)
        {
            base.Container.Round = value;
            return this;
        }

        //
        // Summary:
        //     When set to true, the text of the input will be selected after the widget is
        //     focused.
        //
        // Parameters:
        //   value:
        //     The value for SelectOnFocus
        public MTextBoxBuilder<T> SelectOnFocus(bool value)
        {
            base.Container.SelectOnFocus = value;
            return this;
        }

        //
        // Summary:
        //     When set to true, the text of the input will be selected after the widget is
        //     focused.
        public MTextBoxBuilder<T> SelectOnFocus()
        {
            base.Container.SelectOnFocus = true;
            return this;
        }

        //
        // Summary:
        //     Specifies whether the up and down spin buttons should be rendered
        //
        // Parameters:
        //   value:
        //     The value for Spinners
        public MTextBoxBuilder<T> Spinners(bool value)
        {
            base.Container.Spinners = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the value used to increment or decrement widget value.
        //
        // Parameters:
        //   value:
        //     The value for Step
        public MTextBoxBuilder<T> Step(T? value)
        {
            base.Container.Step = value;
            return this;
        }

        //
        // Summary:
        //     The configuration for the suffix adornment of the component.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the suffixoptions setting.
        public MTextBoxBuilder<T> SuffixOptions(Action<MTextBoxSuffixOptionsSettingsBuilder<T>> configurator)
        {
            base.Container.SuffixOptions.MTextBox = base.Container;
            configurator(new MTextBoxSuffixOptionsSettingsBuilder<T>(base.Container.SuffixOptions));
            return this;
        }

        //
        // Summary:
        //     Specifies the text of the tooltip on the up arrow.
        //
        // Parameters:
        //   value:
        //     The value for UpArrowText
        public MTextBoxBuilder<T> UpArrowText(string value)
        {
            base.Container.UpArrowText = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the value of the MTextBox widget.
        //
        // Parameters:
        //   value:
        //     The value for Value
        public MTextBoxBuilder<T> Value(T? value)
        {
            base.Container.Value = value;
            return this;
        }

        //
        // Summary:
        //     Enables or disables the textbox
        //
        // Parameters:
        //   value:
        //     The value for Enable
        public MTextBoxBuilder<T> Enable(bool value)
        {
            base.Container.Enable = value;
            return this;
        }

        //
        // Summary:
        //     Sets the size of the component.
        //
        // Parameters:
        //   value:
        //     The value for Size
        public MTextBoxBuilder<T> Size(ComponentSize value)
        {
            base.Container.Size = value;
            return this;
        }

        //
        // Summary:
        //     Sets a value controlling the border radius.
        //
        // Parameters:
        //   value:
        //     The value for Rounded
        public MTextBoxBuilder<T> Rounded(Rounded value)
        {
            base.Container.Rounded = value;
            return this;
        }

        //
        // Summary:
        //     Sets a value controlling how the color is applied.
        //
        // Parameters:
        //   value:
        //     The value for FillMode
        public MTextBoxBuilder<T> FillMode(FillMode value)
        {
            base.Container.FillMode = value;
            return this;
        }

        //
        // Summary:
        //     Configures the client-side events.
        //
        // Parameters:
        //   configurator:
        //     The client events action.
        public MTextBoxBuilder<T> Events(Action<MTextBoxEventBuilder> configurator)
        {
            configurator(new MTextBoxEventBuilder(base.Container.Events));
            return this;
        }
    }
}
