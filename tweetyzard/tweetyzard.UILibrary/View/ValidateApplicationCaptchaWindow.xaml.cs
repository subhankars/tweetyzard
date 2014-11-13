using System;
using System.Windows;
using System.Windows.Controls;
using UILibrary.ViewModel;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace UILibrary
{
    /// <summary>
    /// Interaction logic for ValidateApplicationCaptcha.xaml
    /// </summary>
    public partial class ValidateApplicationCaptchaWindow : Window
    {
        public int VerifierKey { get; set; }

        public void UpdateVerifierKey(string captcha)
        {
            VerifierKey = Int32.Parse(captcha);
            Close();
        }

        public void CloseApplicationAndReturnCaptach(string captcha)
        {
            int exitReturn;

            if (Int32.TryParse(captcha, out exitReturn))
            {
                UpdateVerifierKey(captcha);
                Application.Current.Shutdown(Int32.Parse(captcha));
            }

            Application.Current.Shutdown(0);
        }

        private ValidateApplicationCaptchaViewModel _vm;

        public ValidateApplicationCaptchaWindow(bool openFromConsoleAppThread)
        {
            InitializeComponent();

            _vm = new ValidateApplicationCaptchaViewModel();

            if (openFromConsoleAppThread)
            {
                _vm.ExitRequested += CloseApplicationAndReturnCaptach;
            }
            else
            {
                _vm.ExitRequested += UpdateVerifierKey;
            }

            DataContext = _vm;
        }

        public ValidateApplicationCaptchaWindow()
        {
            InitializeComponent();

            _vm = new ValidateApplicationCaptchaViewModel();
            _vm.ExitRequested += CloseApplicationAndReturnCaptach;

            DataContext = _vm;
        }

        public ValidateApplicationCaptchaWindow(string url, bool openFromConsoleAppThread)
            : this(openFromConsoleAppThread)
        {
            _vm.Url = url;
        }

        private void focus_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txtBox = sender as TextBox;

                if (txtBox.Text == "ENTER YOUR CAPTCHA HERE!")
                {
                    _vm.Captcha = "";
                }
            }
        }

        private void focus_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txtBox = sender as TextBox;

                if (txtBox.Text == "")
                {
                     _vm.Captcha = "ENTER YOUR CAPTCHA HERE!";
                }
            }
        }

        private void TextBlock_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(_vm.Url);
        }
    }
}