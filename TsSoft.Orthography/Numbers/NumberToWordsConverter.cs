using System;
using System.Resources;
using TsSoft.Commons.Text;

namespace TsSoft.Orthography.Numbers
{
    internal class NumberToWordsConverter : INumberToWordConverter
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

        private ResourceManager _resourceManager;

        public NumberToWordsConverter(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        private string GetNumDescription(int num, string rank, int caseId)
        {
            string resourceName = rank + "_Case" + caseId;
            var s = _resourceManager.GetString(resourceName);
            if (s != null)
            {
                string[] values = s.Split(',');
                return values[num - 1];
            }
            throw new Exception("Не найден ресурс " + resourceName);
        }

        private static long GetThousands(decimal number)
        {
            return (long)number / Thousand;
        }

        private string ConvertThousand(int number, string[] forms, bool genderFemale = false, int caseId = 1)
        {
            string result = "";
            if (number >= Thousand * 10)
            {
                throw new Exception("ConvertThousand: переданное число больше " + (Thousand * 10));
            }
            NumberGroup rank = new NumberGroup { Value = number };
            if (rank.Hundreds > 0)
            {
                result += GetNumDescription(rank.Hundreds, "Hundreds", caseId);
                result += Space;
            }
            if (rank.Tens > 0)
            {
                if (rank.Tens == 1 && rank.Units > 0)
                {
                    result += GetNumDescription(rank.Units, "Twenties", caseId);
                }
                else
                {
                    result += GetNumDescription(rank.Tens, "Tens", caseId);
                }
                result += Space;
            }
            if (rank.Tens != 1 && rank.Units > 0)
            {
                if (genderFemale)
                {
                    result += GetNumDescription(rank.Units + 1, "UnitsFemale", caseId);
                }
                else
                {
                    result += GetNumDescription(rank.Units + 1, "Units", caseId);
                }
                result += Space;
            }
            if (result == "")
            {
                if (genderFemale)
                {
                    result += GetNumDescription(rank.Units + 1, "UnitsFemale", caseId);
                }
                else
                {
                    result += GetNumDescription(rank.Units + 1, "Units", caseId);
                }
                result += Space;
            }
            if (forms != null)
            {
                result += Pluralizer.Pluralize(rank.Value, forms);
            }
            return result;
        }

        private string[] getPluralizeResource(string resourceName, int caseId)
        {
            resourceName += "_Pluralize_Case" + caseId;
            var s = _resourceManager.GetString(resourceName);
            if (s != null)
            {
                return s.Split(',');
            }
            throw new Exception("Не найден ресурс " + resourceName);
        }

        public string ConvertCurrency(decimal number)
        {
            return Convert(number, false, 1, getPluralizeResource("CurrencyRuUnit", 1), getPluralizeResource("CurrencyRuCent", 1));
        }

        private string Convert(decimal number, bool genderFemale, int caseId, string[] unitForms, string[] fractForms = null)
        {
            string result = "";
            long trillions = GetTrillions(number);
            if (trillions > Thousand)
            {
                throw new Exception("OVER 9000!!!");
            }

            if (trillions > 0)
            {
                result += ConvertThousand((int)trillions, getPluralizeResource("Trillion", caseId));
                result += Space;
            }

            number = number - trillions * Trillion;
            long billions = GetBillions(number);
            if (billions > 0)
            {
                result += ConvertThousand((int)billions, getPluralizeResource("Billion", caseId));
                result += Space;
            }

            number = number - billions * Billion;
            long millions = GetMillions(number);
            if (millions > 0)
            {
                result += ConvertThousand((int)millions, getPluralizeResource("Million", caseId));
                result += Space;
            }

            number = number - millions * Million;
            long thousands = GetThousands(number);
            if (thousands > 0)
            {
                result += ConvertThousand((int)thousands, getPluralizeResource("Thousand", caseId), true);
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