namespace DynamicMenu.Web.Helper
{
    public class DataSourceSchema : JsonObject
    {
        public string Data { get; set; }

        public ClientHandlerDescriptor FunctionData { get; }

        public string Total { get; set; }

        public ClientHandlerDescriptor FunctionTotal { get; }

        public string Errors { get; set; }

        public ClientHandlerDescriptor FunctionErrors { get; }

        public string Aggregates { get; set; }

        public ClientHandlerDescriptor FunctionAggregates { get; }

        public string Groups { get; set; }

        public ClientHandlerDescriptor FunctionGroups { get; }

        public string Type { get; set; }

        public ClientHandlerDescriptor Parse { get; }

        public object FunctionModel { get; set; }

        public ModelDescriptor Model { get; set; }

        public DataSourceSchema()
        {
            Data = "Data";
            Total = "Total";
            Errors = "Errors";
            FunctionData = new ClientHandlerDescriptor();
            FunctionTotal = new ClientHandlerDescriptor();
            FunctionErrors = new ClientHandlerDescriptor();
            FunctionAggregates = new ClientHandlerDescriptor();
            FunctionGroups = new ClientHandlerDescriptor();
            Parse = new ClientHandlerDescriptor();
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (FunctionData.HasValue())
            {
                json.Add("data", FunctionData);
            }
            else
            {
                json.Add<string>("data", Data, string.Empty);
            }

            if (FunctionTotal.HasValue())
            {
                json.Add("total", FunctionTotal);
            }
            else
            {
                json.Add<string>("total", Total, string.Empty);
            }

            if (FunctionErrors.HasValue())
            {
                json.Add("errors", FunctionErrors);
            }
            else
            {
                json.Add<string>("errors", Errors, string.Empty);
            }

            if (FunctionAggregates.HasValue())
            {
                json.Add("aggregates", FunctionAggregates);
            }
            else
            {
                json.Add<string>("aggregates", Aggregates, string.Empty);
            }

            if (FunctionGroups.HasValue())
            {
                json.Add("groups", FunctionGroups);
            }
            else
            {
                json.Add<string>("groups", Groups, string.Empty);
            }

            if (FunctionModel != null)
            {
                json.Add("model", FunctionModel);
            }
            else if (Model != null)
            {
                json.Add("model", Model.ToJson());
            }

            if (!string.IsNullOrEmpty(Type))
            {
                json.Add("type", Type);
            }

            if (Parse.HasValue())
            {
                json.Add("parse", Parse);
            }
        }
    }
}
