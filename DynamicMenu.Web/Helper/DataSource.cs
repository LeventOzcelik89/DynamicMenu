using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DynamicMenu.Web.Helper
{
    public class DataSource : JsonObject
    {
        //
        // Summary:
        //     The number of available pages.
        public int TotalPages { get; set; }

        //
        // Summary:
        //     The page of data which the data source will return when the view method is invoked.
        public int Page { get; set; }

        //
        // Summary:
        //     The grouping configuration of the data source. If set, the data items will be
        //     grouped when the data source is populated. By default, grouping is not applied.
        public bool GroupPaging { get; set; }

        //
        // Summary:
        //     It allows the filtering operation to be performed considering the diacritic characters
        //     for specific language.
        public string AccentFoldingFiltering { get; set; }

        public string Culture { get; set; }

        //
        // Summary:
        //     The total number of data items.
        public int Total { get; set; }

        public string OfflineStorageKey { get; set; }

        public IDictionary<string, object> OfflineStorage { get; }

        //
        // Summary:
        //     The configuration used to parse the remote service response.
        public DataSourceSchema Schema { get; }

        //
        // Summary:
        //     The events configuration.
        public IDictionary<string, object> Events { get; }

        public bool IsClientOperationMode
        {
            get
            {
                if (IsClientBinding)
                {
                    if (ServerPaging && ServerSorting && ServerGrouping && ServerFiltering)
                    {
                        return !ServerAggregates;
                    }

                    return true;
                }

                return false;
            }
        }

        public bool IsClientBinding
        {
            get
            {
                if (Type != DataSourceType.Ajax && Type.GetValueOrDefault() != DataSourceType.WebApi)
                {
                    return Type.GetValueOrDefault() == DataSourceType.Custom;
                }

                return true;
            }
        }

        public IModelMetadataProvider ModelMetaDataProvider { get; protected set; }

        //
        // Summary:
        //     If set to true, the data source will batch CRUD operation requests. For example,
        //     updating two data items would cause one HTTP request instead of two. By default,
        //     the data source makes an HTTP request for every CRUD operation.
        public bool Batch { get; set; }

        //
        // Summary:
        //     If set, the data source will use a predefined transport and/or schema. The supported
        //     values are: "odata" which supports the OData v.2 protocol "odata-v4" which partially
        //     supports odata version 4 "signalr"
        public DataSourceType? Type { get; set; }

        public string CustomType { get; set; }

        //
        // Summary:
        //     The filters which are applied over the data items. By default, no filter is applied.
        public IList<IFilterDescriptor> Filters { get; }

        public IList<SortDescriptor> OrderBy { get; }

        //
        // Summary:
        //     The grouping configuration of the data source. If set, the data items will be
        //     grouped when the data source is populated. By default, grouping is not applied.
        public IList<GroupDescriptor> Groups { get; }

        //
        // Summary:
        //     The aggregates which are calculated when the data source populates with data.
        //     The supported aggregates are: "average" - Only for Number. "count" - String,
        //     Number and Date. "max" - Number and Date. "min" - Number and Date. "sum" - Only
        //     for Number.
        public IList<AggregateDescriptor> Aggregates { get; }

        //
        // Summary:
        //     The number of data items per page. The property has no default value. Therefore,
        //     to use paging, make sure some pageSize value is set.
        public int PageSize { get; set; }

        //
        // Summary:
        //     If set to true, the data source will leave the data item paging implementation
        //     to the remote service. By default, the data source performs paging client-side.
        public bool ServerPaging { get; set; }

        //
        // Summary:
        //     If set to true, the data source will leave the data item sorting implementation
        //     to the remote service. By default, the data source performs sorting client-side.
        public bool ServerSorting { get; set; }

        //
        // Summary:
        //     If set to true, the data source will leave the filtering implementation to the
        //     remote service. By default, the data source performs filtering client-side.
        public bool ServerFiltering { get; set; }

        //
        // Summary:
        //     If set to true, the data source will leave the grouping implementation to the
        //     remote service. By default, the data source performs grouping client-side.
        public bool ServerGrouping { get; set; }

        //
        // Summary:
        //     If set to true, the data source will leave the aggregate calculation to the remote
        //     service. By default, the data source calculates aggregates client-side.
        public bool ServerAggregates { get; set; }

        //
        // Summary:
        //     The configuration used to load and save the data items. A data source is remote
        //     or local based on the way it retrieves data items. Remote data sources load and
        //     save data items from and to a remote end-point (also known as remote service
        //     or server). The transport option describes the remote service configuration -
        //     URL, HTTP verb, HTTP headers, and others. The transport option can also be used
        //     to implement custom data loading and saving. Local data sources are bound to
        //     a JavaScript array via the data option.
        public Transport Transport { get; }

        public IDictionary<string, object> CustomTransport { get; set; }

        public IEnumerable Data { get; set; }

        public bool AutoSync { get; set; }

        public IEnumerable RawData { get; set; }

        public IEnumerable<AggregateResult> AggregateResults { get; set; }

        public DataSource(IModelMetadataProvider modelMetaDataProvider)
            : this()
        {
            ModelMetaDataProvider = modelMetaDataProvider;
        }

        protected DataSource()
        {
            Transport = new Transport();
            Filters = new List<IFilterDescriptor>();
            OrderBy = new List<SortDescriptor>();
            Groups = new List<GroupDescriptor>();
            Aggregates = new List<AggregateDescriptor>();
            Events = new Dictionary<string, object>();
            Schema = new DataSourceSchema();
            OfflineStorage = new Dictionary<string, object>();
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (Transport.Read.Url == null && Type.GetValueOrDefault() != DataSourceType.Custom)
            {
                Transport.Read.Url = "";
            }

            if (Type.HasValue)
            {
                if (Type == DataSourceType.Ajax || Type.GetValueOrDefault() == DataSourceType.Server)
                {
                    json["type"] = new ClientHandlerDescriptor
                    {
                        HandlerName = GenerateTypeFunction(isAspNetMvc: true)
                    };
                }
                else if (Type.GetValueOrDefault() == DataSourceType.Custom)
                {
                    if (!string.IsNullOrEmpty(CustomType))
                    {
                        json["type"] = CustomType;
                    }
                }
                else
                {
                    json["type"] = new ClientHandlerDescriptor
                    {
                        HandlerName = GenerateTypeFunction(isAspNetMvc: false)
                    };
                    if (Type.GetValueOrDefault() == DataSourceType.WebApi)
                    {
                        if (Schema.Model.Id != null)
                        {
                            Transport.IdField = Schema.Model.Id.Name;
                        }

                        if (!string.IsNullOrEmpty(Culture) && Transport != null)
                        {
                            Transport.Culture = Culture;
                        }
                    }
                }
            }

            if (CustomTransport != null)
            {
                json["transport"] = CustomTransport;
            }
            else
            {
                IDictionary<string, object> dictionary = Transport.ToJson();
                if (dictionary.Keys.Any())
                {
                    json["transport"] = dictionary;
                }
            }

            if (!string.IsNullOrEmpty(OfflineStorageKey))
            {
                json["offlineStorage"] = OfflineStorageKey;
            }

            if (OfflineStorage.Any())
            {
                json["offlineStorage"] = OfflineStorage;
            }

            if (!string.IsNullOrEmpty(AccentFoldingFiltering))
            {
                json["accentFoldingFiltering"] = AccentFoldingFiltering;
            }

            if (PageSize > 0)
            {
                json["pageSize"] = PageSize;
                json["page"] = Page;
                json["groupPaging"] = GroupPaging;
                json["total"] = Total;
            }

            if (ServerPaging)
            {
                json["serverPaging"] = ServerPaging;
            }

            if (ServerSorting)
            {
                json["serverSorting"] = ServerSorting;
            }

            if (ServerFiltering)
            {
                json["serverFiltering"] = ServerFiltering;
            }

            if (ServerGrouping)
            {
                json["serverGrouping"] = ServerGrouping;
            }

            if (ServerAggregates)
            {
                json["serverAggregates"] = ServerAggregates;
            }

            if (OrderBy.Any())
            {
                json["sort"] = OrderBy.ToJson();
            }

            if (Groups.Any())
            {
                if (Aggregates.Any())
                {
                    Groups.Where((GroupDescriptor g) => g.AggregateFunctions.Count == 0).Each(delegate (GroupDescriptor g)
                    {
                        g.AggregateFunctions.AddRange(Aggregates.SelectMany((AggregateDescriptor a) => a.Aggregates));
                    });
                }

                json["group"] = Groups.ToJson();
            }

            if (Aggregates.Any())
            {
                json["aggregate"] = Aggregates.SelectMany((AggregateDescriptor agg) => agg.Aggregates.ToJson());
            }

            if (Filters.Any() || ServerFiltering)
            {
                json["filter"] = Filters.OfType<FilterDescriptorBase>().ToJson();
            }

            if (Events.Keys.Any())
            {
                json.Merge(Events);
            }

            IDictionary<string, object> dictionary2 = Schema.ToJson();
            if (dictionary2.Keys.Any())
            {
                json["schema"] = dictionary2;
            }

            if (Batch)
            {
                json["batch"] = Batch;
            }

            if (AutoSync)
            {
                json["autoSync"] = AutoSync;
            }

            if (IsClientOperationMode && Type.GetValueOrDefault() == DataSourceType.Custom && CustomType != "aspnetmvc-ajax")
            {
                RawData = Data;
            }

            if (IsClientOperationMode && RawData != null)
            {
                SerializeData(json, RawData);
            }
            else if (IsClientBinding && !IsClientOperationMode && Data != null)
            {
                SerializeData(json, Data);
            }
        }

        private string GenerateTypeFunction(bool isAspNetMvc)
        {
            string format = "(function(){{if(kendo.data.transports['{0}{1}']){{return '{0}{1}';}} else{{throw new Error('The kendo.aspnetmvc.min.js script is not included.');}}}})()";
            if (isAspNetMvc)
            {
                return string.Format(format, "aspnetmvc-", Type.ToString().ToLower());
            }

            return string.Format(format, "", Type.ToString().ToLower());
        }

        private void SerializeData(IDictionary<string, object> json, IEnumerable data)
        {
            if (string.IsNullOrEmpty(Schema.Data))
            {
                json["data"] = SerializeDataSource(data);
                return;
            }

            json["data"] = new Dictionary<string, object>
        {
            {
                Schema.Data,
                SerializeDataSource(data)
            },
            { Schema.Total, Total }
        };
        }

        private object SerializeDataSource(IEnumerable data)
        {
            if (data is IEnumerable<AggregateFunctionsGroup> && !ServerGrouping)
            {
                return data.Cast<IGroup>().Leaves();
            }

            return data;
        }

        public void ModelType(Type modelType)
        {
            Schema.Model = new ModelDescriptor(modelType, ModelMetaDataProvider);
        }

        public void Process(DataSourceRequest request, bool processData)
        {
            RawData = Data;
            if (request.Sorts == null)
            {
                request.Sorts = OrderBy;
            }
            else if (request.Sorts.Any())
            {
                OrderBy.Clear();
                OrderBy.AddRange(request.Sorts);
            }
            else
            {
                OrderBy.Clear();
            }

            if (request.PageSize == 0)
            {
                request.PageSize = PageSize;
            }

            PageSize = request.PageSize;
            if (request.Groups == null)
            {
                request.Groups = Groups;
            }
            else if (request.Groups.Any())
            {
                Groups.Clear();
                Groups.AddRange(request.Groups);
            }
            else
            {
                Groups.Clear();
            }

            if (request.Filters == null)
            {
                request.Filters = Filters;
            }
            else if (request.Filters.Any())
            {
                Filters.Clear();
                Filters.AddRange(request.Filters);
            }
            else
            {
                Filters.Clear();
            }

            if (!request.Aggregates.Any())
            {
                request.Aggregates = Aggregates;
            }
            else if (request.Aggregates.Any())
            {
                MergeAggregateTypes(request);
                Aggregates.Clear();
                Aggregates.AddRange(request.Aggregates);
            }
            else
            {
                Aggregates.Clear();
            }

            if (Groups.Any() && Aggregates.Any() && Data == null)
            {
                Groups.Each(delegate (GroupDescriptor g)
                {
                    g.AggregateFunctions.AddRange(Aggregates.SelectMany((AggregateDescriptor a) => a.Aggregates));
                });
            }

            if (Data != null)
            {
                if (processData)
                {
                    DataSourceResult dataSourceResult = Data.AsQueryable().ToDataSourceResult(request);
                    Data = dataSourceResult.Data;
                    Total = dataSourceResult.Total;
                    AggregateResults = dataSourceResult.AggregateResults;
                }
                else if (Data is IGridCustomGroupingWrapper gridCustomGroupingWrapper)
                {
                    IEnumerable rawData = (Data = gridCustomGroupingWrapper.GroupedEnumerable.AsGenericEnumerable());
                    RawData = rawData;
                }
            }

            Page = request.Page;
            if (Total == 0 || PageSize == 0)
            {
                TotalPages = 1;
            }
            else
            {
                TotalPages = (Total + PageSize - 1) / PageSize;
            }
        }

        private void MergeAggregateTypes(DataSourceRequest request)
        {
            if (!Aggregates.Any())
            {
                return;
            }

            foreach (AggregateDescriptor requestAggregate in request.Aggregates)
            {
                AggregateDescriptor match = Aggregates.SingleOrDefault((AggregateDescriptor agg) => agg.Member.Equals(requestAggregate.Member, StringComparison.OrdinalIgnoreCase));
                if (match == null)
                {
                    continue;
                }

                requestAggregate.Aggregates.Each(delegate (AggregateFunction function)
                {
                    AggregateFunction aggregateFunction = match.Aggregates.SingleOrDefault((AggregateFunction matchFunction) => matchFunction.AggregateMethodName == function.AggregateMethodName);
                    if (aggregateFunction != null && aggregateFunction.MemberType != null)
                    {
                        function.MemberType = aggregateFunction.MemberType;
                    }
                });
            }
        }
    }
}
