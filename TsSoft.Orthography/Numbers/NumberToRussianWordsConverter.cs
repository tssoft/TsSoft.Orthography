using System;
using TsSoft.Orthography.Language;
using TsSoft.Orthography.RussianLanguage;

namespace TsSoft.Orthography.Numbers
{
    internal class NumberToRussianWordsConverter : NumberToWordsConverter, INumberToWordConverter
    {
        public NumberToRussianWordsConverter()
            : base(new NumberConverterRussianResources(Properties.ResourcesRu.ResourceManager))
        {
           
        }

        public string ConvertCurrency(decimal number, IDeclensionCase declensionCase)
        {
            if (declensionCase is RussianDeclensionCase)
            {
                var numberConverterRussianResources = NumberConverterResources as NumberConverterRussianResources;
                if (numberConverterRussianResources != null)
                {
                    numberConverterRussianResources.DeclensionCase = (declensionCase as RussianDeclensionCase).Value;
                    return Convert(number, false, NumberConverterResources.GetPluralizeResource("CurrencyRuUnit"), NumberConverterResources.GetPluralizeResource("CurrencyRuCent"));
                }
                throw new Exception("NumberToRussianWordsConverter: ConvertCurrency: field \"numberConverterRussianResources\" undefined.");
            }
            throw new Exception("NumberToRussianWordsConverter: ConvertCurrency: param \"declensionCase\" is not of \"RussianDeclensionCase\".");
        }

        public string ConvertCurrency(decimal number)
        {
            return ConvertCurrency(number, new RussianDeclensionCase { Value = RussianDeclensionCaseEnum.CaseI });
        }
    }
}