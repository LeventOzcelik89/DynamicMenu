using System.Linq.Expressions;

namespace DynamicMenu.Web.Helper
{
    public interface IFilterDescriptor
    {
        bool CaseSensitiveFilter { get; set; }

        //
        // Summary:
        //     Creates a predicate filter expression used for collection filtering.
        //
        // Parameters:
        //   instance:
        //     The instance expression, which will be used for filtering.
        //
        // Returns:
        //     A predicate filter expression.
        Expression CreateFilterExpression(Expression instance);
    }
}
