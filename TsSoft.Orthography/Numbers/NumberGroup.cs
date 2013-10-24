namespace TsSoft.Orthography.Numbers
{
    internal class NumberGroup
    {
        public int Value { get; set; }

        public int Hundreds
        {
            get { return Value / 100; }
        }

        public int Tens
        {
            get { return (Value - Hundreds * 100) / 10; }
        }

        public int Units
        {
            get { return (Value - Hundreds * 100) - Tens * 10; }
        }
    }
}