using System.Linq.Expressions;

namespace DynamicMenu.Web.Helper
{
    public abstract class ExpressionBuilderBase
    {
        private readonly ExpressionBuilderOptions options;

        private readonly Type itemType;

        private ParameterExpression parameterExpression;

        public ExpressionBuilderOptions Options => options;

        protected internal Type ItemType => itemType;

        public ParameterExpression ParameterExpression
        {
            get
            {
                if (parameterExpression == null)
                {
                    parameterExpression = Expression.Parameter(ItemType, "item");
                }

                return parameterExpression;
            }
            set
            {
                parameterExpression = value;
            }
        }

        protected ExpressionBuilderBase(Type itemType)
        {
            this.itemType = itemType;
            options = new ExpressionBuilderOptions();
        }
    }
}
