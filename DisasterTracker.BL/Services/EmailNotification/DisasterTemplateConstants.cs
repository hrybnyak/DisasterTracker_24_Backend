namespace DisasterTracker.BL.Services.EmailNotification
{
    public static class DisasterTemplateConstants
    {
        public const string DisasterTemplateName = "DisasterNotification";

        public const string DisasterNameKey = "[DisasterName]";
        public const string LocationLabelKey = "[LocationLabel]";
        public const string DisasterMapUrlKey = "[DisasterMapUrl]";
        public const string DisasterDescriptionKey = "[DisasterDescription]";
        public const string DisasterUrlKey = "[DisasterURL]";

        public const string DisasterUrlTemplate = "https://disaster-tracker-24.herokuapp.com/details/{id}";
    }
}
