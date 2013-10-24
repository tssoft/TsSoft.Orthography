namespace TsSoft.Orthography.Numbers
{
    internal class NumberToRussianWordsConverter : NumberToWordsConverter
    {
        public NumberToRussianWordsConverter()
            : base(Properties.ResourcesRu.ResourceManager)
        {
            
        }
    }
}