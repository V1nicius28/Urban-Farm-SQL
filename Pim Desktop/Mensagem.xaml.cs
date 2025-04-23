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

    public partial class Mensagem : Window
    {
        private string ?v;

        public bool Resultado { get; private set; } = false;



        public Mensagem(string mensagem)
        {
            InitializeComponent();
            MensagemTextBlock.Text = mensagem; // Define a mensagem exibida
        }

        public Mensagem(string mensagem, string v) : this(mensagem)
        {
            this.v = v;
        }

        private void AceitarButton_Click(object sender, RoutedEventArgs e)
        {
            Resultado = true; // Define que o usuário aceitou
            this.Close(); // Fecha a janela
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false; // Define que o usuário cancelou
            this.Close(); // Fecha a janela
        }
    }
}
