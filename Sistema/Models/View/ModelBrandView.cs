namespace Sistema.Models.View
{
    public class ModelBrandView
    {
        public string srcImage { get; internal set; } = "logo.png";
        public string srcImageXS { get; internal set; } = "logo_mini.png";
        public string name { get; internal set; } = "Sistema";
        public string alt { get; internal set; } = "Sistema Logo";
        public ModelBrandController controller { get; internal set; } = new ModelBrandController();
    }

    public class ModelBrandController
    {
        public string controller { get; internal set; } = "Home";
        public string rute { get; internal set; } = "Index";
        public string? action { get; internal set; } = "Index";
    }
}
