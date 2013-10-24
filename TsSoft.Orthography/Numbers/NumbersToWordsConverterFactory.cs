using System;
using System.Globalization;

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
                default:
                    throw new Exception("Not supported culture: " + culture.NativeName);
            }
            return converter;
        }

        public static INumberToWordConverter CreateRussianConverter()
        {
            return CreateConverter(CultureInfo.GetCultureInfo("ru-RU"));
        }
    }
}
