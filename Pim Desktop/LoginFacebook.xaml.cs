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
using System.Windows.Shapes;

namespace Pim_Desktop
{
    public partial class LoginFacebook : Window
    {
        public LoginFacebook()
        {
            InitializeComponent();
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Collapsed;
            EmailBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#90949C");

        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailText.Visibility = Visibility.Visible;         
            }
    
        }

        private void SenhaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SenhaText.Visibility = Visibility.Collapsed;
            EmailBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#90949C");
            SenhaBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#90949C");
        }

        private void SenhaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SenhaTextBox.Password))
            {
                SenhaText.Visibility = Visibility.Visible;
            }
        }


        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailError.Visibility = Visibility.Collapsed; 
                SenhaError.Visibility = Visibility.Collapsed; 
            }
        }


        private void SenhaPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SenhaTextBox.Password))
            {
                SenhaError.Visibility = Visibility.Collapsed; 
            }
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string senha = SenhaTextBox.Password;

            if (email == "admin@gmail.com" && senha == "admin")
            {
                TelaInicio novaJanela = new TelaInicio();
                novaJanela.Show(); 
                Application.Current.MainWindow.Close();
                this.Close(); 
            }
            else
            {
                bool emailInvalido = false;
                bool senhaInvalida = false;

                if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; 
                    EmailError.Text = "Email inválido";  
                    EmailError.Visibility = Visibility.Visible; 
                    emailInvalido = true; 
                }


                if (!emailInvalido && !senhaInvalida)
                {
                    if (senha != "admin" || email != "admin@gmail.com")

                    SenhaBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; 
                    EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; 
                    SenhaError.Text = "Suas credenciais estão incorretas ou expiraram";  
                    SenhaError.Visibility = Visibility.Visible; 
                }
            }
        }
    }
}
