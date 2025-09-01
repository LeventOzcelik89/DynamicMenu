using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DynamicMenu.Web.Helper
{
    public static class QueryableExtensions
    {
        private static DataSourceResult ToDataSourceResult(this DataTableWrapper enumerable, DataSourceRequest request)
        {
            List<IFilterDescriptor> list = new List<IFilterDescriptor>();
            DataTable dataTable = enumerable.Table;
            if (request.Filters != null)
            {
                list.AddRange(request.Filters);
            }

            if (list.Any())
            {
                list.SelectMemberDescriptors().Each(delegate (FilterDescriptor f)
                {
                    f.MemberType = GetFieldByTypeFromDataColumn(dataTable, f.Member);
                });
            }

            List<GroupDescriptor> list2 = new List<GroupDescriptor>();
            if (request.Groups != null)
            {
                list2.AddRange(request.Groups);
            }

            if (list2.Any())
            {
                list2.Each(delegate (GroupDescriptor g)
                {
                    g.MemberType = GetFieldByTypeFromDataColumn(dataTable, g.Member);
                });
            }

            List<AggregateDescriptor> list3 = new List<AggregateDescriptor>();
            if (request.Aggregates != null)
            {
                list3.AddRange(request.Aggregates);
            }

            if (list3.Any())
            {
                foreach (AggregateDescriptor item in list3)
                {
                    item.Aggregates.Each(delegate (AggregateFunction g)
                    {
                        g.MemberType = GetFieldByTypeFromDataColumn(dataTable, g.SourceField);
                    });
                }
            }

            DataSourceResult dataSourceResult = enumerable.AsEnumerable().ToDataSourceResult(request);
            dataSourceResult.Data = dataSourceResult.Data.SerializeToDictionary(enumerable.Table);
            return dataSourceResult;
        }

        private static Type GetFieldByTypeFromDataColumn(DataTable dataTable, string memberName)
        {
            if (!dataTable.Columns.Contains(memberName))
            {
                return null;
            }

            return dataTable.Columns[memberName].DataType;
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty resullt.
        //
        // Parameters:
        //   dataTable:
        //     An instance of System.Data.DataTable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this DataTable dataTable, DataSourceRequest request)
        {
            return dataTable.WrapAsEnumerable().ToDataSourceResult(request);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this DataTable dataTable, DataSourceRequest request)
        {
            return CreateDataSourceResultAsync(() => dataTable.ToDataSourceResult(request));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this DataTable dataTable, DataSourceRequest request, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => dataTable.ToDataSourceResult(request), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this IEnumerable enumerable, DataSourceRequest request)
        {
            return enumerable.AsQueryable().ToDataSourceResult(request);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, CancellationToken cancellation)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request), cancellation);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return enumerable.AsQueryable().ToDataSourceResult(request, modelState);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request, modelState));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState, CancellationToken cancellation)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request, modelState), cancellation);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this IQueryable queryable, DataSourceRequest request)
        {
            return queryable.ToDataSourceResult(request, null);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request), cancellationToken);
        }

        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.AsQueryable().CreateDataSourceResult(request, null, selector);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request, selector));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request, selector), cancellationToken);
        }

        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return enumerable.AsQueryable().CreateDataSourceResult(request, modelState, selector);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request, modelState, selector));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request, modelState, selector), cancellationToken);
        }

        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IQueryable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.CreateDataSourceResult(request, null, selector);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request, selector));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request, selector), cancellationToken);
        }

        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IQueryable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return enumerable.CreateDataSourceResult(request, modelState, selector);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request, modelState, selector));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request, modelState, selector), cancellationToken);
        }

        public static DataSourceResult ToDataSourceResult(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return queryable.CreateDataSourceResult<object, object>(request, modelState, null);
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request, modelState));
        }

        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request, modelState), cancellationToken);
        }

        private static DataSourceResult CreateDataSourceResult<TModel, TResult>(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            DataSourceResult dataSourceResult = new DataSourceResult();
            IQueryable queryable2 = queryable;
            List<IFilterDescriptor> list = new List<IFilterDescriptor>();
            if (request.Filters != null)
            {
                list.AddRange(request.Filters);
            }

            if (list.Any())
            {
                queryable2 = queryable2.Where(list);
            }

            List<SortDescriptor> sort = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                sort.AddRange(request.Sorts);
            }

            List<SortDescriptor> temporarySortDescriptors = new List<SortDescriptor>();
            IList<GroupDescriptor> list2 = new List<GroupDescriptor>();
            if (request.Groups != null)
            {
                list2.AddRange(request.Groups);
            }

            List<AggregateDescriptor> aggregates = new List<AggregateDescriptor>();
            if (request.Aggregates != null)
            {
                aggregates.AddRange(request.Aggregates);
            }

            if (aggregates.Any() && !request.IncludeSubGroupCount)
            {
                IQueryable queryable3 = queryable2.AsQueryable();
                IQueryable source = queryable3;
                if (list.Any())
                {
                    source = queryable3.Where(list);
                }

                dataSourceResult.AggregateResults = source.Aggregate(aggregates.SelectMany((AggregateDescriptor a) => a.Aggregates));
                if (list2.Any() && aggregates.Any())
                {
                    list2.Each(delegate (GroupDescriptor g)
                    {
                        g.AggregateFunctions.AddRange(aggregates.SelectMany((AggregateDescriptor a) => a.Aggregates));
                    });
                }
            }

            if (!request.GroupPaging || !request.Groups.Any())
            {
                dataSourceResult.Total = queryable2.Count();
            }

            if (!sort.Any() && queryable.Provider.IsEntityFrameworkProvider())
            {
                SortDescriptor item = new SortDescriptor
                {
                    Member = queryable.ElementType.FirstSortableProperty()
                };
                sort.Add(item);
                temporarySortDescriptors.Add(item);
            }

            if (list2.Any())
            {
                list2.Reverse().Each(delegate (GroupDescriptor groupDescriptor)
                {
                    SortDescriptor item2 = new SortDescriptor
                    {
                        Member = groupDescriptor.Member,
                        SortDirection = groupDescriptor.SortDirection
                    };
                    sort.Insert(0, item2);
                    temporarySortDescriptors.Add(item2);
                });
            }

            if (sort.Any())
            {
                queryable2 = queryable2.Sort(sort);
            }

            IQueryable notPagedData = queryable2;
            if (request.GroupPaging && !request.Groups.Any())
            {
                queryable2 = queryable2.Skip(request.Skip).Take(request.Take);
            }
            else if (!request.GroupPaging || !request.Groups.Any())
            {
                queryable2 = queryable2.Page(request.Page - 1, request.PageSize);
            }

            if (list2.Any())
            {
                bool includeItems = (request.IsExcelExportRequest && request.GroupPaging) || !request.GroupPaging;
                queryable2 = queryable2.GroupBy(notPagedData, list2, includeItems);
                if (request.GroupPaging)
                {
                    dataSourceResult.Total = queryable2.Count();
                    queryable2 = queryable2.Skip(request.Skip).Take(request.Take);
                }
            }

            if (!request.IncludeSubGroupCount)
            {
                dataSourceResult.Data = queryable2.Execute(selector);
            }

            if (modelState != null && !modelState.IsValid)
            {
                dataSourceResult.Errors = modelState.SerializeErrors();
            }

            temporarySortDescriptors.Each(delegate (SortDescriptor sortDescriptor)
            {
                sort.Remove(sortDescriptor);
            });
            return dataSourceResult;
        }

        private static Task<DataSourceResult> CreateDataSourceResultAsync(Func<DataSourceResult> expression)
        {
            return Task.Run(expression);
        }

        private static Task<DataSourceResult> CreateDataSourceCancellableResultAsync(Func<DataSourceResult> expression, CancellationToken cancellationToken)
        {
            return Task.Run(expression, cancellationToken);
        }

        private static IQueryable CallQueryableMethod(this IQueryable source, string methodName, LambdaExpression selector)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), methodName, new Type[2]
            {
            source.ElementType,
            selector.Body.Type
            }, source.Expression, Expression.Quote(selector)));
        }

        public static IQueryable Sort(this IQueryable source, IEnumerable<SortDescriptor> sortDescriptors)
        {
            return new SortDescriptorCollectionExpressionBuilder(source, sortDescriptors).Sort();
        }

        public static IQueryable Page(this IQueryable source, int pageIndex, int pageSize)
        {
            IQueryable source2 = source;
            source2 = source2.Skip(pageIndex * pageSize);
            if (pageSize > 0)
            {
                source2 = source2.Take(pageSize);
            }

            return source2;
        }

        public static IQueryable Select(this IQueryable source, LambdaExpression selector)
        {
            return source.CallQueryableMethod("Select", selector);
        }

        public static IQueryable GroupBy(this IQueryable source, LambdaExpression keySelector)
        {
            return source.CallQueryableMethod("GroupBy", keySelector);
        }

        public static IQueryable OrderBy(this IQueryable source, LambdaExpression keySelector)
        {
            return source.CallQueryableMethod("OrderBy", keySelector);
        }

        public static IQueryable OrderByDescending(this IQueryable source, LambdaExpression keySelector)
        {
            return source.CallQueryableMethod("OrderByDescending", keySelector);
        }

        public static IQueryable OrderBy(this IQueryable source, LambdaExpression keySelector, ListSortDirection? sortDirection)
        {
            if (sortDirection.HasValue)
            {
                if (sortDirection.Value == ListSortDirection.Ascending)
                {
                    return source.OrderBy(keySelector);
                }

                return source.OrderByDescending(keySelector);
            }

            return source;
        }

        public static IQueryable GroupBy(this IQueryable source, IEnumerable<GroupDescriptor> groupDescriptors, bool includeItems)
        {
            return source.GroupBy(source, groupDescriptors, includeItems);
        }

        public static IQueryable GroupBy(this IQueryable source, IQueryable notPagedData, IEnumerable<GroupDescriptor> groupDescriptors, bool includeItems)
        {
            GroupDescriptorCollectionExpressionBuilder groupDescriptorCollectionExpressionBuilder = new GroupDescriptorCollectionExpressionBuilder(source, groupDescriptors, notPagedData, includeItems);
            groupDescriptorCollectionExpressionBuilder.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
            return groupDescriptorCollectionExpressionBuilder.CreateQuery();
        }

        public static AggregateResultCollection Aggregate(this IQueryable source, IEnumerable<AggregateFunction> aggregateFunctions)
        {
            List<AggregateFunction> list = aggregateFunctions.ToList();
            if (list.Count > 0)
            {
                QueryableAggregatesExpressionBuilder queryableAggregatesExpressionBuilder = new QueryableAggregatesExpressionBuilder(source, list);
                queryableAggregatesExpressionBuilder.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
                {
                    IEnumerator enumerator = queryableAggregatesExpressionBuilder.CreateQuery().GetEnumerator();
                    try
                    {
                        if (enumerator.MoveNext())
                        {
                            return ((AggregateFunctionsGroup)enumerator.Current).GetAggregateResults(list);
                        }
                    }
                    finally
                    {
                        IDisposable disposable = enumerator as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
            }

            return new AggregateResultCollection();
        }

        public static IQueryable Where(this IQueryable source, Expression predicate)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new Type[1] { source.ElementType }, source.Expression, Expression.Quote(predicate)));
        }

        public static IQueryable Where(this IQueryable source, IEnumerable<IFilterDescriptor> filterDescriptors)
        {
            if (filterDescriptors.Any())
            {
                FilterDescriptorCollectionExpressionBuilder filterDescriptorCollectionExpressionBuilder = new FilterDescriptorCollectionExpressionBuilder(Expression.Parameter(source.ElementType, "item"), filterDescriptors);
                filterDescriptorCollectionExpressionBuilder.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
                LambdaExpression predicate = filterDescriptorCollectionExpressionBuilder.CreateFilterExpression();
                return source.Where(predicate);
            }

            return source;
        }

        public static IQueryable Take(this IQueryable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Take", new Type[1] { source.ElementType }, source.Expression, Expression.Constant(count)));
        }

        public static IQueryable Skip(this IQueryable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Skip", new Type[1] { source.ElementType }, source.Expression, Expression.Constant(count)));
        }

        public static int Count(this IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.Provider.Execute<int>(Expression.Call(typeof(Queryable), "Count", new Type[1] { source.ElementType }, source.Expression));
        }

        public static object ElementAt(this IQueryable source, int index)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return source.Provider.Execute(Expression.Call(typeof(Queryable), "ElementAt", new Type[1] { source.ElementType }, source.Expression, Expression.Constant(index)));
        }

        public static IQueryable Union(this IQueryable source, IQueryable second)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Union", new Type[1] { source.ElementType }, source.Expression, second.Expression));
        }

        private static IEnumerable Execute<TModel, TResult>(this IQueryable source, Func<TModel, TResult> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            Type elementType = source.ElementType;
            if (selector != null)
            {
                List<AggregateFunctionsGroup> list = new List<AggregateFunctionsGroup>();
                if (elementType == typeof(AggregateFunctionsGroup))
                {
                    foreach (AggregateFunctionsGroup item in source)
                    {
                        item.Items = item.Items.AsQueryable().Execute(selector);
                        list.Add(item);
                    }

                    return list;
                }

                List<TResult> list2 = new List<TResult>();
                {
                    foreach (TModel item2 in source)
                    {
                        list2.Add(selector(item2));
                    }

                    return list2;
                }
            }

            IList list3 = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            foreach (object item3 in source)
            {
                list3.Add(item3);
            }

            return list3;
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult(this IEnumerable enumerable, DataSourceRequest request)
        {
            return enumerable.AsQueryable().ToTreeDataSourceResult(request, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<object, object, object, object>(request, null, null, modelState, null, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, modelState));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, modelState), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, TResult>(this IQueryable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.ToTreeDataSourceResult<TModel, object, object, TResult>(request, null, null, selector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.ToTreeDataSourceResult<TModel, object, object, TResult>(request, null, null, selector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return enumerable.CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return enumerable.CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return queryable.ToTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return enumerable.CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return queryable.AsQueryable().ToTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, null);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector), cancellationToken);
        }

        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, rootSelector);
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector));
        }

        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector), cancellationToken);
        }

        private static Task<TreeDataSourceResult> CreateTreeDataSourceResultAsync(Func<TreeDataSourceResult> expression)
        {
            return Task.Run(expression);
        }

        private static Task<TreeDataSourceResult> CreateTreeDataSourceCancellableResultAsync(Func<TreeDataSourceResult> expression, CancellationToken cancellationToken)
        {
            return Task.Run(expression, cancellationToken);
        }

        private static TreeDataSourceResult CreateTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, Expression<Func<TModel, bool>> rootSelector)
        {
            TreeDataSourceResult treeDataSourceResult = new TreeDataSourceResult();
            IQueryable queryable2 = queryable;
            List<IFilterDescriptor> list = new List<IFilterDescriptor>();
            if (request.Filters != null)
            {
                list.AddRange(request.Filters);
            }

            if (list.Any())
            {
                queryable2 = queryable2.Where(list);
                queryable2 = queryable2.ParentsRecursive<TModel>(queryable, idSelector, parentIDSelector);
            }

            IQueryable allData = queryable2;
            if (rootSelector != null)
            {
                queryable2 = queryable2.Where(rootSelector);
            }

            List<SortDescriptor> list2 = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                list2.AddRange(request.Sorts);
            }

            List<AggregateDescriptor> list3 = new List<AggregateDescriptor>();
            if (request.Aggregates != null)
            {
                list3.AddRange(request.Aggregates);
            }

            if (list3.Any())
            {
                foreach (IGrouping<T2, TModel> item in queryable2.GroupBy(parentIDSelector))
                {
                    treeDataSourceResult.AggregateResults.Add(Convert.ToString(item.Key), item.AggregateForLevel(allData, list3, idSelector, parentIDSelector));
                }
            }

            treeDataSourceResult.Total = queryable2.Count();
            if (list2.Any())
            {
                queryable2 = queryable2.Sort(list2);
            }

            treeDataSourceResult.Data = queryable2.Execute(selector);
            if (modelState != null && !modelState.IsValid)
            {
                treeDataSourceResult.Errors = modelState.SerializeErrors();
            }

            return treeDataSourceResult;
        }
    }
}
