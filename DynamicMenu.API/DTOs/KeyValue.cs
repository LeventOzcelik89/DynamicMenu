namespace DynamicMenu.API.DTOs
{
    public class KeyValue
    {

        public KeyValue(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public object Value { get; set; }
    }
}
