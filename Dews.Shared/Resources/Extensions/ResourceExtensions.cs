using Dews.Shared.Resources.Enums;
using System.Resources;

namespace Dews.Shared.Resources.Extensions
{
    public static class ResourceExtensions
    {
        private static ResourceManager resourceManager = new ResourceManager(typeof(Resource));
        private const string EmptyResourceFormat = "[{0}]";
        public static string Translate(this ResourcesEnum value)
        {
            string translatedResource = resourceManager.GetString(value.ToString());
            return translatedResource.CheckIsNull() ? string.Format(EmptyResourceFormat,value) : translatedResource;
        }
    }
}
