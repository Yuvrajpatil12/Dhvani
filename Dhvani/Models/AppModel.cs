namespace Core.Models
{
    public class AppModel
    {
        public class AppUpdate
        {
            public string Version { get; set; }
            public string Build { get; set; }
            public string AppType { get; set; }
        }
    }
}