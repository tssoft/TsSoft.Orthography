using System;
using System.Resources;
using TsSoft.Orthography.RussianLanguage;

namespace TsSoft.Orthography.Numbers.Russian
{
    internal class NumberConverterRussianResources : INumberConverterResources
    {
        private readonly ResourceManager _resourceManager;

        public RussianDeclensionCaseEnum DeclensionCase { get; set; }

        public NumberConverterRussianResources(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string[] GetPluralizeResource(string resourceName)
        {
            resourceName += "_Pluralize_Case" + (int)DeclensionCase;
            var s = _resourceManager.GetString(resourceName);
            if (s != null)
            {
                return s.Split(',');
            }
            throw new Exception("Не найден ресурс " + resourceName);
        }

        public string ConvertNumToWord(int num, string numberGroupName)
        {
            string resourceName = numberGroupName + "_Case" + (int)DeclensionCase;
            var s = _resourceManager.GetString(resourceName);
            if (s != null)
            {
                string[] values = s.Split(',');
                return values[num - 1];
            }
            throw new Exception("Не найден ресурс " + resourceName);

        }
    }
}
