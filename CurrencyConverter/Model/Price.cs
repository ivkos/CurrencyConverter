namespace CurrencyConverter.Model
{
    public class Price
    {
        public double Value { get; private set; }
        public Currency Currency { get; private set; }

        public Price(double value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public Price Convert(Currency currency)
        {
            return new Price(ConvertToEuro().Value * currency.FromEuroRate, currency);
        }

        private Price ConvertToEuro()
        {
            return new Price(Value * Currency.ToEuroRate, Currency.EUR);
        }

        public override string ToString()
        {
            return Value.ToString() + " " + Currency.Code;
        }
    }
}
