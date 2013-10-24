using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsSoft.Orthography.Numbers;

namespace TsSoft.Orthography.Test.Numbers
{
    [TestClass]
    public class NumberWordsConverterTest
    {

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void TestEnLocale()
        {
            NumbersToWordsConverterFactory.CreateConverter(new CultureInfo("en"));
        }

        [TestMethod]
        public void TestRuLocale()
        {
            NumbersToWordsConverterFactory.CreateConverter(new CultureInfo("ru"));
        }

        [TestMethod]
        public void TestDefaultLocale()
        {
            NumbersToWordsConverterFactory.CreateConverter();
        }

        
        [TestMethod]
        public void TestConvert()
        {
            INumberToWordConverter numberToWordsConverter = NumbersToWordsConverterFactory.CreateRussianConverter();
            Assert.AreEqual("десять рублей 50 копеек", numberToWordsConverter.ConvertCurrency(10.50M));
            Assert.AreEqual("одна тысяча рублей 00 копеек", numberToWordsConverter.ConvertCurrency(1000M));
            Assert.AreEqual("две тысячи рублей 00 копеек", numberToWordsConverter.ConvertCurrency(2000M));
            Assert.AreEqual("один миллион пять тысяч рублей 50 копеек", numberToWordsConverter.ConvertCurrency(1005000.50M));
            Assert.AreEqual("сто рублей 05 копеек", numberToWordsConverter.ConvertCurrency(100.05M));
            Assert.AreEqual("десять рублей 00 копеек", numberToWordsConverter.ConvertCurrency(10M));
            Assert.AreEqual("десять миллионов рублей 00 копеек", numberToWordsConverter.ConvertCurrency(10000000M));
            Assert.AreEqual("сто двадцать два миллиона сто пятьдесят шесть тысяч девятьсот сорок два рубля 00 копеек",
                            numberToWordsConverter.ConvertCurrency(122156942M));
            //Assert.AreEqual("десять рублей",
            //                NumberToWordsConverter.Convert(10.50M, NumberMeasuringForms.CurrencyRuUnit));
            Assert.AreEqual("ноль рублей 00 копеек", numberToWordsConverter.ConvertCurrency(0M));
            Assert.AreEqual("ноль рублей 50 копеек", numberToWordsConverter.ConvertCurrency(0.5M));
            //Assert.AreEqual("ноль рублей",
            //                NumberToWordsConverter.Convert(0M, NumberMeasuringForms.CurrencyRuUnit));
            //Assert.AreEqual("одна штука",
            //                NumberToWordsConverter.Convert(1M, true, NumberMeasuringForms.AmountUnit));
            //Assert.AreEqual("одна тысяча штук",
            //                NumberToWordsConverter.Convert(1000M, true, NumberMeasuringForms.AmountUnit));
        }
    }
}