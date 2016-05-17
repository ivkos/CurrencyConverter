using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CurrencyConverter.Model;

namespace CurrencyConverter.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<Currency> Currencies => Currency.Rates;

        private double fromValue;
        public double FromValue
        {
            get
            {
                return fromValue;
            }
            set { fromValue = value; NotifyProperyChanged("ToValue"); }
        }

        private Currency fromCurrency = Currency.EUR;

        public Currency FromCurrency
        {
            get { return fromCurrency; }
            set { fromCurrency = value; NotifyProperyChanged("ToValue"); }
        }

        private Currency toCurrency = Currency.EUR;
        public Currency ToCurrency
        {
            get { return toCurrency; }
            set { toCurrency = value; NotifyProperyChanged("ToValue"); }
        }

        public double ToValue => new Price(FromValue, FromCurrency).Convert(ToCurrency).Value;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
