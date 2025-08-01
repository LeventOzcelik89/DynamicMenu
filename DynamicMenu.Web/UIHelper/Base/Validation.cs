namespace DynamicMenu.Web.UIHelper
{
    public class Validation
    {
        public string validFeedback { get; set; }
        public string invalidFeedback { get; set; }
        public bool required { get; set; } = false;


        public Validation(bool required, string invalidFeedback, string validFeedback)
        {
            this.required = required;
            this.validFeedback = validFeedback;
            this.invalidFeedback = invalidFeedback;
        }

        public Validation(bool required, string invalidFeedback)
        {
            this.required = required;
            this.invalidFeedback = invalidFeedback;
        }

    }
}
