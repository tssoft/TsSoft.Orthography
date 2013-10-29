using TsSoft.Orthography.Language;

namespace TsSoft.Orthography.Numbers
{
    public interface INumberToWordConverter
    {
        /// <summary>
        /// Преобразовать число, содержащее сумму в запись прописью
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        string ConvertCurrency(decimal number);
        string ConvertCurrency(decimal number, IDeclensionCase declensionCaseCase);
    }
}
