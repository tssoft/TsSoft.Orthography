using System;
using System.Globalization;
using System.Text;
using TsSoft.Commons.Text;
using TsSoft.Orthography.Language;

namespace TsSoft.Orthography.Numbers.English
{
    internal class NumberToEnglishWordsConverter : INumberToWordConverter
    {
        public readonly INumberConverterResources NumberConverterResources;

        public NumberToEnglishWordsConverter()
        {
            NumberConverterResources = new NumberConverterEnglishResources(Properties.ResourcesEn.ResourceManager);
        }

        public string ConvertCurrency(decimal number, IDeclensionCase declensionCase)
        {
            throw new Exception("NumberToEnglishWordsConverter: ConvertCurrency: param \"declensionCase\" is not of \"EnglishDeclensionCase\".");
        }

        public string ConvertCurrency(decimal number)
        {
            return Convert(number, NumberConverterResources.GetPluralizeResource("CurrencyEnUnit"),
                           NumberConverterResources.GetPluralizeResource("CurrencyEnCent"));
        }

        private string Convert(decimal number, string[] unitForms, string[] fractForms)
        {
            //Источник: http://www.blackbeltcoder.com/Articles/strings/converting-numbers-to-words
            bool allZeros = true;
            var units = (int)number;
            // Use StringBuilder to build result
            var builder = new StringBuilder();
            // Convert integer portion of value to string
            string digits = ((long)number).ToString(CultureInfo.InvariantCulture);
            // Traverse characters in reverse order
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int ndigit = (digits[i] - '0');
                int column = (digits.Length - (i + 1));
                // Determine if ones, tens, or hundreds column
                string temp;
                switch (column % 3)
                {
                    case 0:        // Ones position
                        bool showThousands = true;
                        if (i == 0)
                        {
                            // First digit in number (last in loop)
                            temp = String.Format("{0} ", NumberConverterResources.ConvertNumToWord(ndigit, "Ones"));
                        }
                        else if (digits[i - 1] == '1')
                        {
                            // This digit is part of "teen" value
                            temp = String.Format("{0} ", NumberConverterResources.ConvertNumToWord(ndigit, "Teens"));
                            // Skip tens position
                            i--;
                        }
                        else
                        {
                            if (ndigit != 0)
                            {
                                // Any non-zero digit
                                temp = String.Format("{0} ", NumberConverterResources.ConvertNumToWord(ndigit, "Ones"));
                            }
                            else
                            {
                                // This digit is zero. If digit in tens and hundreds
                                // column are also zero, don't show "thousands"
                                temp = String.Empty;
                                // Test for non-zero digit in this grouping
                                showThousands = (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'));
                            }
                        }
                        // Show "thousands" if non-zero in grouping
                        if (showThousands)
                        {
                            if (column > 0)
                            {
                                temp = String.Format("{0}{1}{2}", temp, NumberConverterResources.ConvertNumToWord(column / 3, "Thousands"), allZeros ? " " : ", ");
                            }
                            // Indicate non-zero digit encountered
                            allZeros = false;
                        }
                        builder.Insert(0, temp);
                        break;
                    case 1:        // Tens column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0}{1}",
                                NumberConverterResources.ConvertNumToWord(ndigit, "Tens"),
                                (digits[i + 1] != '0') ? "-" : " ");
                            builder.Insert(0, temp);
                        }
                        break;
                    case 2:        // Hundreds column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0} hundred ", NumberConverterResources.ConvertNumToWord(ndigit, "Ones"));
                            builder.Insert(0, temp);
                        }
                        break;
                }
            }
            if (builder.ToString() != "")
            {
                builder.AppendFormat("{0}", Pluralizer.Pluralize(units, unitForms));
            }
            // Append fractional portion/cents
            var cents = (int) ((number - (long) number) * 100);
            builder.AppendFormat(" and {0:00} ", cents);
            builder.AppendFormat("{0}", Pluralizer.Pluralize(cents, fractForms));
            return builder.ToString();
        }
    }
}