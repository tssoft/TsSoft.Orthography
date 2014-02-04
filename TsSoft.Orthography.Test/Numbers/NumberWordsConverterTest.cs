using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsSoft.Orthography.Numbers;
using TsSoft.Orthography.RussianLanguage;

namespace TsSoft.Orthography.Test.Numbers
{
    [TestClass]
    public class NumberWordsConverterTest
    {

        [TestMethod]
        [ExpectedException(typeof(System.Exception), AllowDerivedTypes = true)]
        public void TestUnsupportedLocale()
        {
            NumbersToWordsConverterFactory.CreateConverter(new CultureInfo("tr"));
        }

        [TestMethod]
        public void TestRuLocale()
        {
            NumbersToWordsConverterFactory.CreateConverter(new CultureInfo("ru"));
        }

        [TestMethod]
        public void TestEnLocale()
        {
            NumbersToWordsConverterFactory.CreateConverter(new CultureInfo("en"));
        }

        [TestMethod]
        public void TestDefaultLocale()
        {
            NumbersToWordsConverterFactory.CreateConverter();
        }


        [TestMethod]
        public void TestRussianConvert()
        {
            //Именительный (по-умолчанию)
            INumberToWordConverter numberToWordsConverter = NumbersToWordsConverterFactory.CreateRussianConverter();
            var declensionCase = new RussianDeclensionCase {Value  = RussianDeclensionCaseEnum.CaseI};
            Assert.AreEqual("десять рублей 50 копеек", numberToWordsConverter.ConvertCurrency(10.50M, declensionCase));
            Assert.AreEqual("одна тысяча рублей 00 копеек", numberToWordsConverter.ConvertCurrency(1000M, declensionCase));
            Assert.AreEqual("две тысячи рублей 00 копеек", numberToWordsConverter.ConvertCurrency(2000M, declensionCase));
            Assert.AreEqual("один миллион пять тысяч рублей 50 копеек",
                            numberToWordsConverter.ConvertCurrency(1005000.50M, declensionCase));
            Assert.AreEqual("сто рублей 05 копеек", numberToWordsConverter.ConvertCurrency(100.05M, declensionCase));
            Assert.AreEqual("десять рублей 00 копеек", numberToWordsConverter.ConvertCurrency(10M, declensionCase));
            Assert.AreEqual("четыре рубля 04 копейки", numberToWordsConverter.ConvertCurrency(4.04M, declensionCase));
            Assert.AreEqual("четырнадцать рублей 14 копеек", numberToWordsConverter.ConvertCurrency(14.14M, declensionCase));
            Assert.AreEqual("десять миллионов рублей 00 копеек",
                            numberToWordsConverter.ConvertCurrency(10000000M, declensionCase));
            Assert.AreEqual("сто двадцать два миллиона сто пятьдесят шесть тысяч девятьсот сорок два рубля 00 копеек",
                            numberToWordsConverter.ConvertCurrency(122156942M, declensionCase));
            //проверка определения падежа по-умолчанию
            Assert.AreEqual("ноль рублей 00 копеек", numberToWordsConverter.ConvertCurrency(0M));
            Assert.AreEqual("ноль рублей 01 копейка", numberToWordsConverter.ConvertCurrency(0.01M));
            Assert.AreEqual("ноль рублей 02 копейки", numberToWordsConverter.ConvertCurrency(0.02M));
            Assert.AreEqual("ноль рублей 50 копеек", numberToWordsConverter.ConvertCurrency(0.5M));
            //Родительный
            declensionCase = new RussianDeclensionCase { Value = RussianDeclensionCaseEnum.CaseR }; 
            Assert.AreEqual("ноль рублей 01 копейку", numberToWordsConverter.ConvertCurrency(0.01M, declensionCase));
            Assert.AreEqual("ноль рублей 02 копейки", numberToWordsConverter.ConvertCurrency(0.02M, declensionCase));
            Assert.AreEqual("ноль рублей 50 копеек", numberToWordsConverter.ConvertCurrency(0.5M, declensionCase));
            Assert.AreEqual("один миллион одну тысячу рублей 50 копеек",
                            numberToWordsConverter.ConvertCurrency(1001000.50M, declensionCase));
            //Дательный
            declensionCase = new RussianDeclensionCase { Value = RussianDeclensionCaseEnum.CaseD };
            Assert.AreEqual("нолю рублям 01 копейке", numberToWordsConverter.ConvertCurrency(0.01M, declensionCase));
            Assert.AreEqual("нолю рублям 02 копейкам", numberToWordsConverter.ConvertCurrency(0.02M, declensionCase));
            Assert.AreEqual("нолю рублям 50 копейкам", numberToWordsConverter.ConvertCurrency(0.5M, declensionCase));
            Assert.AreEqual("одному миллиону одной тысяче ста рублям 50 копейкам",
                            numberToWordsConverter.ConvertCurrency(1001100.50M, declensionCase));
            //Винительный
            declensionCase = new RussianDeclensionCase { Value = RussianDeclensionCaseEnum.CaseV };
            Assert.AreEqual("ноль рублей 01 копейку", numberToWordsConverter.ConvertCurrency(0.01M, declensionCase));
            Assert.AreEqual("ноль рублей 02 копейки", numberToWordsConverter.ConvertCurrency(0.02M, declensionCase));
            Assert.AreEqual("ноль рублей 50 копеек", numberToWordsConverter.ConvertCurrency(0.5M, declensionCase));
            Assert.AreEqual("один миллион одну тысячу сто рублей 50 копеек",
                            numberToWordsConverter.ConvertCurrency(1001100.50M, declensionCase));
            //Творительный
            declensionCase = new RussianDeclensionCase { Value = RussianDeclensionCaseEnum.CaseT };
            Assert.AreEqual("нолем рублями 01 копейкой", numberToWordsConverter.ConvertCurrency(0.01M, declensionCase));
            Assert.AreEqual("нолем рублями 02 копейками", numberToWordsConverter.ConvertCurrency(0.02M, declensionCase));
            Assert.AreEqual("нолем рублями 50 копейками", numberToWordsConverter.ConvertCurrency(0.5M, declensionCase));
            Assert.AreEqual("одним миллионом одной тысячей стами рублями 50 копейками",
                            numberToWordsConverter.ConvertCurrency(1001100.50M, declensionCase));
            //Предложный
            declensionCase = new RussianDeclensionCase { Value = RussianDeclensionCaseEnum.CaseP };
            Assert.AreEqual("ноле рублях 01 копейке", numberToWordsConverter.ConvertCurrency(0.01M, declensionCase));
            Assert.AreEqual("ноле рублях 02 копейках", numberToWordsConverter.ConvertCurrency(0.02M, declensionCase));
            Assert.AreEqual("ноле рублях 50 копейках", numberToWordsConverter.ConvertCurrency(0.5M, declensionCase));
            Assert.AreEqual("одном миллионе одной тысяче ста рублях 50 копейках",
                            numberToWordsConverter.ConvertCurrency(1001100.50M, declensionCase));
        }

        [TestMethod]
        public void TestEnglishConvert()
        {
            //для проверки http://www.calculatorsoup.com/calculators/conversions/numberstowords.php
            INumberToWordConverter numberToWordsConverter = NumbersToWordsConverterFactory.CreateEnglishConverter();
            Assert.AreEqual("ten dollars and 50 cents", numberToWordsConverter.ConvertCurrency(10.50M));
            Assert.AreEqual("one million, two hundred twenty-five thousand, one hundred fifty dollars and 01 cent", numberToWordsConverter.ConvertCurrency(1225150.01M));
            Assert.AreEqual("one million, one thousand dollars and 20 cents", numberToWordsConverter.ConvertCurrency(1001000.20M));
            Assert.AreEqual("one dollar and 20 cents", numberToWordsConverter.ConvertCurrency(1.20M));
            Assert.AreEqual("zero dollars and 20 cents", numberToWordsConverter.ConvertCurrency(0.20M));
        }

    }
}