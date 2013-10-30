using System;
using TsSoft.Commons.Text;

namespace TsSoft.Orthography.Numbers.Russian
{
    internal class NumberToWordsConverter
    {
        private const long Trillion = (long)Million * Million;
        private const int Billion = Million * Thousand;
        private const int Million = Thousand * Thousand;
        private const int Thousand = 1000;
        private const string Space = " ";

        private static long GetTrillions(decimal number)
        {
            return (long)number / Trillion;
        }

        private static long GetBillions(decimal number)
        {
            return (long)number / Billion;
        }

        private static long GetMillions(decimal number)
        {
            return (long)number / Million;
        }

        public readonly INumberConverterResources NumberConverterResources;

        public NumberToWordsConverter(INumberConverterResources numberConverterResources)
        {
            NumberConverterResources = numberConverterResources;
        }

        private static long GetThousands(decimal number)
        {
            return (long)number / Thousand;
        }

        private string ConvertThousand(int number, string[] forms, bool genderFemale)
        {
            string result = "";
            if (number >= Thousand * 10)
            {
                throw new Exception("ConvertThousand: переданное число больше " + (Thousand * 10));
            }
            var numberGroup = new NumberGroup { Value = number };
            if (numberGroup.Hundreds > 0)
            {
                result += NumberConverterResources.ConvertNumToWord(numberGroup.Hundreds, "Hundreds");
                result += Space;
            }
            if (numberGroup.Tens > 0)
            {
                if (numberGroup.Tens == 1 && numberGroup.Units > 0)
                {
                    result += NumberConverterResources.ConvertNumToWord(numberGroup.Units, "Twenties");
                }
                else
                {
                    result += NumberConverterResources.ConvertNumToWord(numberGroup.Tens, "Tens");
                }
                result += Space;
            }
            if (numberGroup.Tens != 1 && numberGroup.Units > 0)
            {
                if (genderFemale)
                {
                    result += NumberConverterResources.ConvertNumToWord(numberGroup.Units + 1, "UnitsFemale");
                }
                else
                {
                    result += NumberConverterResources.ConvertNumToWord(numberGroup.Units + 1, "Units");
                }
                result += Space;
            }
            if (result == "")
            {
                if (genderFemale)
                {
                    result += NumberConverterResources.ConvertNumToWord(numberGroup.Units + 1, "UnitsFemale");
                }
                else
                {
                    result += NumberConverterResources.ConvertNumToWord(numberGroup.Units + 1, "Units");
                }
                result += Space;
            }
            if (forms != null)
            {
                result += Pluralizer.Pluralize(numberGroup.Value, forms);
            }
            return result;
        }

        protected string Convert(decimal number, bool genderFemale, string[] unitForms, string[] fractForms = null)
        {
            string result = "";
            long trillions = GetTrillions(number);
            if (trillions > Thousand)
            {
                throw new Exception("OVER 9000!!!");
            }

            if (trillions > 0)
            {
                result += ConvertThousand((int)trillions, NumberConverterResources.GetPluralizeResource("Trillion"), false);
                result += Space;
            }

            number = number - trillions * Trillion;
            long billions = GetBillions(number);
            if (billions > 0)
            {
                result += ConvertThousand((int)billions, NumberConverterResources.GetPluralizeResource("Billion"), false);
                result += Space;
            }

            number = number - billions * Billion;
            long millions = GetMillions(number);
            if (millions > 0)
            {
                result += ConvertThousand((int)millions, NumberConverterResources.GetPluralizeResource("Million"), false);
                result += Space;
            }

            number = number - millions * Million;
            long thousands = GetThousands(number);
            if (thousands > 0)
            {
                result += ConvertThousand((int)thousands, NumberConverterResources.GetPluralizeResource("Thousand"), true);
                result += Space;
            }

            number = number - thousands * Thousand;
            var units = (int)number;
            if ((units > 0) || (result == ""))
            {
                result += ConvertThousand(units, null, genderFemale);
            }
            result += Pluralizer.Pluralize(units, unitForms);

            number = number - units;

            if (fractForms != null)
            {
                result += Space;
                int cents = (int)(number * 100);
                if (cents >= 0)
                {
                    result += cents.ToString().PadLeft(2, '0');
                    result += Space;
                    result += Pluralizer.Pluralize(cents, fractForms);
                }
            }
            return result;
        }

    }
}