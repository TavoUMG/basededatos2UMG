namespace Sistema.Models.View
{
    public class ModelMenuView
    {
        public List<MENUITEM> items { get; internal set; } = new List<MENUITEM>();
    }

    public class ItemSelect
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class MENUITEM
    {
        public string name { get; internal set; }
        public string icon { get; internal set; }
        public string controller { get; internal set; }
        public string action { get; internal set; }
    }
}
