using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DynamicMenu.Web.Helper
{
    public class ModelDescriptor : JsonObject
    {
        public IList<ModelFieldDescriptor> Fields { get; }

        public IDataKey Id { get; set; }

        public DataSource ChildrenDataSource { get; set; }

        public string ChildrenMember { get; set; }

        public string HasChildrenMember { get; set; }

        public string IsDirectoryMember { get; set; }

        public ModelDescriptor(Type modelType, IModelMetadataProvider modelMetadataProvider)
        {
            ModelMetadata metadataForType = modelMetadataProvider.GetMetadataForType(modelType);
            Fields = Translate(metadataForType);
        }

        public ModelFieldDescriptor AddDescriptor(string member)
        {
            ModelFieldDescriptor modelFieldDescriptor = Fields.FirstOrDefault((ModelFieldDescriptor f) => f.Member == member);
            if (modelFieldDescriptor != null)
            {
                return modelFieldDescriptor;
            }

            modelFieldDescriptor = new ModelFieldDescriptor
            {
                Member = member
            };
            Fields.Add(modelFieldDescriptor);
            return modelFieldDescriptor;
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (Id != null)
            {
                json["id"] = Id.Name;
            }

            json.Add("hasChildren", HasChildrenMember, HasChildrenMember.HasValue);
            if (ChildrenDataSource != null)
            {
                json["children"] = ChildrenDataSource.ToJson();
            }
            else if (ChildrenMember.HasValue())
            {
                json["children"] = ChildrenMember;
            }

            if (Fields.Count <= 0)
            {
                return;
            }

            Dictionary<string, object> fields = new Dictionary<string, object>();
            json["fields"] = fields;
            Fields.Each(delegate (ModelFieldDescriptor prop)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                fields[prop.Member] = dictionary;
                if (!prop.IsEditable)
                {
                    dictionary["editable"] = false;
                }

                dictionary["type"] = prop.MemberType.ToJavaScriptType().ToLowerInvariant();
                if (prop.MemberType.IsNullableType() || prop.DefaultValue != null)
                {
                    object obj = prop.DefaultValue;
                    if (prop.MemberType.IsEnumType() && obj is Enum)
                    {
                        Type underlyingType = Enum.GetUnderlyingType(prop.MemberType.GetNonNullableType());
                        obj = Convert.ChangeType(obj, underlyingType);
                    }

                    dictionary["defaultValue"] = obj;
                }

                if (!string.IsNullOrEmpty(prop.From))
                {
                    dictionary["from"] = prop.From;
                }

                if (prop.IsNullable.HasValue && prop.IsNullable.Value)
                {
                    dictionary["nullable"] = true;
                }

                if (prop.Parse.HasValue())
                {
                    dictionary["parse"] = prop.Parse;
                }
            });
        }

        private IList<ModelFieldDescriptor> Translate(ModelMetadata metadata)
        {
            return metadata.Properties.Select((ModelMetadata p) => new ModelFieldDescriptor
            {
                Member = p.PropertyName,
                MemberType = p.ModelType,
                IsEditable = !p.IsReadOnly
            }).ToList();
        }

        private object CreateDataItem(Type modelType)
        {
            return Activator.CreateInstance(modelType);
        }
    }
}
