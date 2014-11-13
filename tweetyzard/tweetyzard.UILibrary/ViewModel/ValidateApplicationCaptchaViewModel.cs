using System.Windows.Input;
using System.ComponentModel;
using UILibrary.Commands;

namespace UILibrary.ViewModel
{
    /// <summary>
    /// Captcha ViewModel
    /// </summary>
    public class ValidateApplicationCaptchaViewModel : INotifyPropertyChanged
    {
        private string _captcha;
        public string Captcha
        {
            get { return _captcha; }
            set
            {
                _captcha = value;
                OnPropertyChanged("Captcha");
            }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged("Url");
            }
        }

        private readonly ICommand _validateCommand;
        public ICommand ValidateCommand
        {
            get { return _validateCommand; }
        }

        public delegate void ExitRequestedDelegate(string captcha);
        public event ExitRequestedDelegate ExitRequested;

        public ValidateApplicationCaptchaViewModel()
        {
            Captcha = "ENTER YOUR CAPTCHA HERE!";
            _validateCommand = new DelegateCommand(() => ExitRequested(_captcha));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}