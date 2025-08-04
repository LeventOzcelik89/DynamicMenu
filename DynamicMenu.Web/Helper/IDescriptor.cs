namespace DynamicMenu.Web.Helper
{
    public interface IDescriptor
    {
        void Deserialize(string source);

        string Serialize();
    }
}
