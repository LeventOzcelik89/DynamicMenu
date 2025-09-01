using System.ComponentModel;

namespace DynamicMenu.Web.Helper
{
    public class GroupDescriptor : SortDescriptor
    {
        private object displayContent;

        private AggregateFunctionCollection aggregateFunctions;

        //
        // Summary:
        //     Gets or sets the type of the member that is used for grouping. Set this property
        //     if the member type cannot be resolved automatically. Such cases are: items with
        //     ICustomTypeDescriptor, XmlNode or DataRow.
        //
        // Value:
        //     The type of the member used for grouping.
        public Type MemberType { get; set; }

        //
        // Summary:
        //     Gets or sets the content which will be used from UI.
        public object DisplayContent
        {
            get
            {
                if (displayContent == null)
                {
                    return base.Member;
                }

                return displayContent;
            }
            set
            {
                displayContent = value;
            }
        }

        //
        // Summary:
        //     Gets or sets the aggregate functions used when grouping is executed.
        //
        // Value:
        //     The aggregate functions that will be used in grouping.
        public AggregateFunctionCollection AggregateFunctions
        {
            get
            {
                AggregateFunctionCollection obj = aggregateFunctions ?? new AggregateFunctionCollection();
                AggregateFunctionCollection result = obj;
                aggregateFunctions = obj;
                return result;
            }
        }

        //
        // Summary:
        //     Changes the Kendo.Mvc.SortDescriptor to the next logical value.
        public void CycleSortDirection()
        {
            base.SortDirection = GetNextSortDirection(base.SortDirection);
        }

        private static ListSortDirection GetNextSortDirection(ListSortDirection? sortDirection)
        {
            if (sortDirection.HasValue && sortDirection.GetValueOrDefault() == ListSortDirection.Ascending)
            {
                return ListSortDirection.Descending;
            }

            return ListSortDirection.Ascending;
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            base.Serialize(json);
            if (AggregateFunctions.Any())
            {
                json["aggregates"] = AggregateFunctions.ToJson();
            }
        }
    }
}
