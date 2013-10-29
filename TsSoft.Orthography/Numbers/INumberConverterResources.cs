namespace TsSoft.Orthography.Numbers
{
    internal interface INumberConverterResources
    {
        string[] GetPluralizeResource(string resourceName);
        string ConvertNumToWord(int num, string numberGroupName);
    }
}
