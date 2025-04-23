﻿using System;
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
    /// <summary>
    /// Lógica interna para LoginGoogle.xaml
    /// </summary>
    public partial class LoginGoogle : Window
    {
        public LoginGoogle()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            TelaInicio LoginWindow = new TelaInicio();
            LoginWindow.Show(); // Abre a nova janela e espera o fechamento
            Application.Current.MainWindow.Close(); // Fecha a MainWindow
            this.Close();
        }
    }
}
