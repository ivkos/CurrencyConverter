using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace CurrencyConverter.Model
{
    public class Currency
    {
        public static readonly Currency EUR = new Currency("EUR", 1);

        private static List<Currency> rates;
        public static List<Currency> Rates
        {
            get { return rates ?? new[] { EUR }.ToList(); }
            private set { rates = value; }
        }

        public string Code { get; private set; }
        public double FromEuroRate { get; private set; }

        public double ToEuroRate => 1 / FromEuroRate;

        public Currency(string code, double fromEuroRate)
        {
            Code = code;
            FromEuroRate = fromEuroRate;
        }

        public static void UpdateRates()
        {
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString("https://api.fixer.io/latest");
                var response = JsonConvert.DeserializeObject<ApiResponse>(json);

                Rates = new[] { EUR }.ToList();
                Rates.AddRange(response.rates.Select(rate => new Currency(rate.Key, rate.Value)));
            }
        }

        private class ApiResponse
        {
            public IDictionary<string, double> rates;
        }

        public override string ToString()
        {
            return Code;
        }

        protected bool Equals(Currency other)
        {
            return string.Equals(Code, other.Code);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Currency) obj);
        }

        public override int GetHashCode()
        {
            return (Code != null ? Code.GetHashCode() : 0);
        }
    }
}
