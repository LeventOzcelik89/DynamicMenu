using System.Text;

namespace DynamicMenu.Web.Helper
{
    public class AggregateDescriptor : IDescriptor
    {
        private readonly IDictionary<string, Func<AggregateFunction>> aggregateFactories;

        public ICollection<AggregateFunction> Aggregates { get; private set; }

        public string Member { get; set; }

        public AggregateDescriptor()
        {
            Aggregates = new List<AggregateFunction>();
            aggregateFactories = new Dictionary<string, Func<AggregateFunction>>
        {
            {
                "sum",
                () => new SumFunction
                {
                    SourceField = Member
                }
            },
            {
                "count",
                () => new CountFunction
                {
                    SourceField = Member
                }
            },
            {
                "average",
                () => new AverageFunction
                {
                    SourceField = Member
                }
            },
            {
                "min",
                () => new MinFunction
                {
                    SourceField = Member
                }
            },
            {
                "max",
                () => new MaxFunction
                {
                    SourceField = Member
                }
            }
        };
        }

        public void Deserialize(string source)
        {
            string[] array = source.Split('-');
            if (array.Any())
            {
                Member = array[0];
                for (int i = 1; i < array.Length; i++)
                {
                    DeserializeAggregate(array[i]);
                }
            }
        }

        private void DeserializeAggregate(string aggregate)
        {
            if (aggregateFactories.TryGetValue(aggregate, out var value))
            {
                Aggregates.Add(value());
            }
        }

        public string Serialize()
        {
            StringBuilder stringBuilder = new StringBuilder(Member);
            foreach (string item in Aggregates.Select((AggregateFunction aggregate) => aggregate.FunctionName.Split('_')[0].ToLowerInvariant()))
            {
                stringBuilder.Append("-");
                stringBuilder.Append(item);
            }

            return stringBuilder.ToString();
        }
    }
}
