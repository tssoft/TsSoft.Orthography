using System;
using System.Resources;

namespace TsSoft.Orthography.Numbers.English
{
    internal class NumberConverterEnglishResources : INumberConverterResources
    {
        private readonly ResourceManager _resourceManager;

        public NumberConverterEnglishResources(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string ConvertNumToWord(int num, string numberGroupName)
        {
            string resourceName = numberGroupName;
            var s = _resourceManager.GetString(resourceName);
            if (s != null)
            {
                string[] values = s.Split(',');
                return values[num];
            }
            throw new Exception("Resource not found " + resourceName);

        }

        public string[] GetPluralizeResource(string resourceName)
        {
            resourceName += "_Pluralize";
            var s = _resourceManager.GetString(resourceName);
            if (s != null)
            {
                return s.Split(',');
            }
            throw new Exception("Resource not found " + resourceName);
        }

    }
}
