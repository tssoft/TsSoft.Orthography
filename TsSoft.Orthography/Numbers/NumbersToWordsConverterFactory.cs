using System;
using System.Globalization;
using TsSoft.Orthography.Numbers.English;
using TsSoft.Orthography.Numbers.Russian;

namespace TsSoft.Orthography.Numbers
{
    public class NumbersToWordsConverterFactory
    {
        public static INumberToWordConverter CreateConverter(CultureInfo culture = null)
        {
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            INumberToWordConverter converter;
            switch (culture.TwoLetterISOLanguageName)
            {
                case "ru":
                    converter = new NumberToRussianWordsConverter();
                    break;
                case "en":
                    converter = new NumberToEnglishWordsConverter();
                    break;
                default:
                    throw new Exception("Not supported culture: " + culture.NativeName);
            }
            return converter;
        }

        public static INumberToWordConverter CreateRussianConverter()
        {
            return CreateConverter(CultureInfo.GetCultureInfo("ru-RU"));
        }

        public static INumberToWordConverter CreateEnglishConverter()
        {
            return CreateConverter(CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
