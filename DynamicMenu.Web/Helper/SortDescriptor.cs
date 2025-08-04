using System.ComponentModel;

namespace DynamicMenu.Web.Helper
{
    public class SortDescriptor : JsonObject, IDescriptor
    {
        //
        // Summary:
        //     Gets or sets the member name which will be used for sorting.
        public string Member { get; set; }

        //
        // Summary:
        //     Gets or sets the sort direction for this sort descriptor. If the value is null
        //     no sorting will be applied.
        //
        // Value:
        //     The sort direction. The default value is null.
        public ListSortDirection SortDirection { get; set; }

        //
        // Summary:
        //     Gets or sets the sort compare for this sort descriptor.
        public ClientHandlerDescriptor SortCompare { get; set; }

        public SortDescriptor()
            : this(null, ListSortDirection.Ascending)
        {
        }

        public SortDescriptor(string member, ListSortDirection order)
        {
            Member = member;
            SortDirection = order;
        }

        public void Deserialize(string source)
        {
            string[] array = source.Split(new char[1] { '-' });
            if (array.Length > 1)
            {
                Member = array[0];
            }

            string text = array.Last();
            SortDirection = ((text == "desc") ? ListSortDirection.Descending : ListSortDirection.Ascending);
        }

        public string Serialize()
        {
            return "{0}-{1}".FormatWith(Member, (SortDirection == ListSortDirection.Ascending) ? "asc" : "desc");
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            json["field"] = Member;
            json["dir"] = ((SortDirection == ListSortDirection.Ascending) ? "asc" : "desc");
            if (SortCompare != null && SortCompare.HasValue())
            {
                json["compare"] = SortCompare;
            }
        }
    }
}
