using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CurrencyConverter.Model;

namespace CurrencyConverter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                Currency.UpdateRates();
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not download exchange rates: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            InitializeComponent();
        }

        private void FromValue_OnKeyUp(object sender, KeyEventArgs e)
        {
            var be = ((TextBox) sender).GetBindingExpression(TextBox.TextProperty);

            be?.UpdateSource();
        }
    }
}
