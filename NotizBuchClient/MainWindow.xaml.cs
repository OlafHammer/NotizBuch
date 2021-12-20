using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotizBuchClient
{
    /// <summary>
    /// Anmeldefenster / Startfenster 
    /// </summary>
    public partial class MainWindow : Window
    {
        public Client Client { get; set; }

        public MainWindow()
        {
            Client = new Client();
            InitializeComponent();
        }

        // Überprüfe die Logindaten und wechsel anschließend zum Notizbuch Fenster
        private void LoginButtonClicked(object sender, RoutedEventArgs e)
        {
            Client.User = UserField.Text;
            Client.Password = PasswordField.Password;

            if (Client.AuthUser())
            {
                NotesWindow window = new NotesWindow(Client);
                window.Show();
                Close();
            }
        }

        // Erstelle einen neuen Account mit den Logindaten
        private void CreateButtonClicked(object sender, RoutedEventArgs e)
        {
            Client.CreateAccount(UserField.Text, PasswordField.Password);
        }

        // Anzeigen von Eingabehilfen 
        #region InputTip
        private void PasswordFieldInfoMouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordFieldInfo.Visibility = Visibility.Hidden;
            PasswordField.Focus();
        }

        private void UserFieldInfoMouseDown(object sender, MouseButtonEventArgs e)
        {
            UserFieldInfo.Visibility = Visibility.Hidden;
            UserField.Focus();
        }

        private void UserFieldLostFocus(object sender, RoutedEventArgs e)
        {
            if (UserField.Text == "")
                UserFieldInfo.Visibility = Visibility.Visible;
        }

        private void PasswordBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Password == "")
                PasswordFieldInfo.Visibility = Visibility.Visible;
        }
        #endregion 
    }
}
