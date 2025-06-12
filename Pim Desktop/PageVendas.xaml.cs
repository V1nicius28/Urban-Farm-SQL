﻿using Microsoft.Win32;
using Newtonsoft.Json;
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
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace Pim_Desktop
{
    public partial class PageVendas : Page
    {
        private List<Border> selectedBorders = new List<Border>();
        private Border ?selectedBorder = null;
        private ComboBox escolhaComboBox;
        private string filtroAtual = "Todos"; 

        public PageVendas()
        {
            InitializeComponent();
            CarregarVendas();
            CarregarImagem();
        }


        public class Venda
        {
            public int Id { get; set; }
            public string Alimento { get; set; }
            public string Preco { get; set; }
            public string Quantidade { get; set; }
            public string Tipo { get; set; }
        }


        private void AdicionarVenda(Venda venda)
        {
            Border foodBorder = new Border
            {
                Name = "FoodBorder",
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 206,
                Background = (Brush)FindResource("BackgroundLinear"),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 170,
                Margin = new Thickness(0, 0, 20, 20),
                CornerRadius = new CornerRadius(5),
                BorderBrush = new SolidColorBrush(Color.FromRgb(37, 167, 84)), 
                BorderThickness = new Thickness(1),
                Tag = venda.Id
            };

            Grid contentGrid = new Grid();


            Image buttonImage = new Image
            {
                Source = new BitmapImage(new Uri("/Images/AddImage.png", UriKind.RelativeOrAbsolute)), 
                Width = 50, 
                Height = 50, 
                Stretch = Stretch.Uniform,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Button imageButton = new Button
            {
                Width = 102, 
                Height = 102,
                Content = buttonImage,
                BorderBrush = Brushes.Transparent,
                Background = Brushes.Transparent,
                Style = (Style)FindResource("RoundedButtonStyle"),
                Padding = new Thickness(0), 
                Margin = new Thickness(0, 0, 0, 50),
                Cursor = Cursors.Hand
            };
            imageButton.Click += ImageButton_Click; 


            TextBox alimentoTextBox = new TextBox
            {
                Name = "Alimento",
                Text = venda.Alimento, 
                Style = (Style)FindResource("CustomTextBoxStyle"),
                Width = 80,
                Height = 20,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 80, -115) 
            };

            TextBox precoTextBox = new TextBox
            {
                Name = "preco",
                Text = venda.Preco,
                Style = (Style)FindResource("CustomTextBoxStyle"),
                Width = 70,
                Height = 20,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 90, -165) 
            };

            TextBox quantTextBox = new TextBox
            {
                Name = "quant",
                Text = venda.Quantidade, 
                Style = (Style)FindResource("CustomTextBoxStyle"),
                Width = 80,
                Height = 20,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(90, 0, 0, -115) 
            };

            escolhaComboBox = new ComboBox
            {
                Name = "escolha",
                Width = 78,
                Height = 23,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(85, 0, 0, -175),
                Style = (Style)FindResource("CustomComboBoxStyle")
            };

            alimentoTextBox.LostFocus += AlimentoTextBox_LostFocus;
            quantTextBox.LostFocus += quantTextBox_LostFocus;
            precoTextBox.LostFocus += PrecoTextBox_LostFocus;
            escolhaComboBox.SelectionChanged += EscolhaComboBox_SelectionChanged;


            escolhaComboBox.Items.Add("Tipo");
            escolhaComboBox.Items.Add("Fruta");
            escolhaComboBox.Items.Add("Legume");
            escolhaComboBox.Items.Add("Verdura");

            escolhaComboBox.SelectedItem = venda.Tipo;


            contentGrid.Children.Add(imageButton);
            contentGrid.Children.Add(alimentoTextBox);
            contentGrid.Children.Add(precoTextBox);
            contentGrid.Children.Add(quantTextBox);
            contentGrid.Children.Add(escolhaComboBox);

            foodBorder.Child = contentGrid;

            foodBorder.MouseLeftButtonDown += FoodBorder_MouseLeftButtonDown;

            VendasWrapPanel.Children.Add(foodBorder);

        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                Button clickedButton = sender as Button;

                Border parentBorder = FindParent<Border>(clickedButton);
                if (parentBorder != null)
                {
                    int borderId = (int)parentBorder.Tag;

                    Image selectedImage = new Image
                    {
                        Source = new BitmapImage(new Uri(openFileDialog.FileName)),
                        Stretch = Stretch.Uniform,
                        Height = 100,
                        Width = 100
                    };

                    clickedButton.Content = selectedImage;

                    SaveImagePath(borderId, openFileDialog.FileName);
                }
            }
        }



        private void SalvarAlteracoesVenda(Border vendaBorder, string campo, string novoValor)
        {
            if (vendaBorder.Tag is int idVenda)
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                dbHelper.AtualizarVenda(idVenda, campo, novoValor);
            }
        }



        private void AdicionarVenda_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Venda novaVenda = new Venda
                {
                    Alimento = "Alimento",
                    Preco = "R$ preço",
                    Quantidade = "quantidade",
                    Tipo = "Tipo",
                };

                int idGerado = SalvarVenda(novaVenda);

                novaVenda.Id = idGerado;

                AdicionarVenda(novaVenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar venda: {ex.Message}");
            }
        }

        private int SalvarVenda(Venda venda)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            return dbHelper.SalvarVenda(venda);
        }

        public class DatabaseHelper
        {
            private readonly string connectionString = "Server=localhost\\SQLEXPRESS;Database=PimDatabase;Trusted_Connection=True;";

            public int SalvarVenda(Venda venda)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Vendas (Alimento, Preco, Quantidade, Tipo) OUTPUT INSERTED.Id VALUES (@Alimento, @Preco, @Quantidade, @Tipo)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Alimento", venda.Alimento);
                        command.Parameters.AddWithValue("@Preco", venda.Preco);
                        command.Parameters.AddWithValue("@Quantidade", venda.Quantidade);
                        command.Parameters.AddWithValue("@Tipo", venda.Tipo);
                        return (int)command.ExecuteScalar(); 
                    }
                }
            }

            public List<Venda> ObterVendas()
            {    
                List<Venda> vendas = new List<Venda>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Alimento, Preco, Quantidade, Tipo FROM Vendas";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vendas.Add(new Venda
                            {
                                Id = (int)reader["Id"],
                                Alimento = reader["Alimento"].ToString(),
                                Preco = reader["Preco"].ToString(),
                                Quantidade = reader["Quantidade"].ToString(),
                                Tipo = reader["Tipo"].ToString(),
                            });
                        }
                    }
                }
                return vendas;
            }



            public void AtualizarVenda(int id, string campo, string novoValor)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"UPDATE Vendas SET {campo} = @NovoValor WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NovoValor", novoValor);
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            public void RemoverVenda(int id)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Vendas WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        private void CarregarVendas()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            var vendas = dbHelper.ObterVendas();

            foreach (var venda in vendas)
            {
                AdicionarVenda(venda);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBorders.Count > 0)
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                foreach (var border in selectedBorders.ToList())
                {
                    if (border.Tag is int idVenda)
                    {
                        dbHelper.RemoverVenda(idVenda);

                        var parent = border.Parent as Panel;
                        parent?.Children.Remove(border);
                    }
                }
                selectedBorders.Clear();
            }
            else
            {
                MensagemPopup.Text = "Nenhum item selecionado para remover";
                AvisoPopup.HorizontalOffset = 260;
                AvisoPopup.VerticalOffset = 80;
                AvisoPopup.IsOpen = true;
                Task.Delay(2000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        AvisoPopup.IsOpen = false;
                    });
                });
                return;
            }
        }


        private void AlimentoTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
                    
            TextBox alimentoTextBox = sender as TextBox;
            if (alimentoTextBox != null)
            {
                string novoAlimento = alimentoTextBox.Text;
                Border parentBorder = FindParent<Border>(alimentoTextBox);
                if (parentBorder != null)
                {
                    SalvarAlteracoesVenda(parentBorder, "Alimento", novoAlimento);
                }           
            }
        }


        private void quantTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox quantTextBox = sender as TextBox;
            if (quantTextBox != null)
            {
                string novoQuant = quantTextBox.Text;
                Border parentBorder = FindParent<Border>(quantTextBox);
                if (parentBorder != null)
                {
                    SalvarAlteracoesVenda(parentBorder, "Quantidade", novoQuant);
                }
            }
        }


        private void PrecoTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox precoTextBox = sender as TextBox;
            if (precoTextBox != null)
            {
                string novoPreco = precoTextBox.Text;
                Border parentBorder = FindParent<Border>(precoTextBox);
                if (parentBorder != null)
                {
                    SalvarAlteracoesVenda(parentBorder, "Preco", novoPreco);
                }
            }
        }

        private void EscolhaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox escolhaComboBox = sender as ComboBox;
            if (escolhaComboBox != null)
            {
                string novoTipo = escolhaComboBox.SelectedItem.ToString();
                Border parentBorder = FindParent<Border>(escolhaComboBox);
                if (parentBorder != null)
                {
                    SalvarAlteracoesVenda(parentBorder, "Tipo", novoTipo);
                }
            }
        }

        
        private T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }

                T ?childOfChild = FindVisualChild<T>(parent: child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }


        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;
            T ?parent = parentObject as T;
            return parent != null ? parent : FindParent<T>(parentObject);
        }


        private void FoodBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border clickedBorder = sender as Border;

            if (selectedBorders.Contains(clickedBorder))
            {
                clickedBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(37, 167, 84)); 
                clickedBorder.BorderThickness = new Thickness(1);
                selectedBorders.Remove(clickedBorder); 
            }
            else
            {
                clickedBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                clickedBorder.BorderThickness = new Thickness(2);
                selectedBorders.Add(clickedBorder); 
            }
        }

        private void Frutas_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros("Fruta");
            FiltrarBorders("Fruta");
        }

        private void Verduras_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros("Verdura");
            FiltrarBorders("Verdura");
        }

        private void Legumes_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros("Legume");
            FiltrarBorders("Legume");
        }

        private void Todos_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros("Todos");
            FiltrarBorders("Todos");
        }

        private void DesmarcarOutros(string buttonClicked)
        {
            if (buttonClicked != "Fruta") Frutas.IsChecked = false;
            if (buttonClicked != "Verdura") Verduras.IsChecked = false;
            if (buttonClicked != "Legume") Legumes.IsChecked = false;
            if (buttonClicked != "Todos") Todos.IsChecked = false;

            switch (buttonClicked)
            {
                case "Fruta":
                    Frutas.IsChecked = true;
                    break;
                case "Verdura":
                    Verduras.IsChecked = true;
                    break;
                case "Legume":
                    Legumes.IsChecked = true;
                    break;
                case "Todos":
                    Todos.IsChecked = true;
                    break;
            }
        }

        private void LocalTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            {
                FiltrarBorders(filtroAtual);
            }
        }

        private void FiltrarBorders(string tipo)
        {
            filtroAtual = tipo;

            string searchText = LocalTextBox.Text.ToLower();

            foreach (Border border in VendasWrapPanel.Children.OfType<Border>())
            {
                ComboBox comboBox = FindVisualChild<ComboBox>(border);
                TextBox alimentoTextBox = FindVisualChild<TextBox>(border);

                if (comboBox != null && alimentoTextBox != null)
                {
                    bool tipoCorresponde = tipo == "Todos" || comboBox.SelectedItem.ToString() == tipo;
                    bool pesquisaCorresponde = alimentoTextBox.Text.ToLower().Contains(searchText);

                    if (tipoCorresponde && pesquisaCorresponde)
                    {
                        border.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        border.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }


        private void PesquisaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LocalText.Visibility = Visibility.Collapsed;
        }

        private void PesquisaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LocalTextBox.Text))
            {
                LocalText.Visibility = Visibility.Visible;
            }
        }

        public class ImageSettings
        {
            public Dictionary<int, string> BorderImagePaths { get; set; } = new Dictionary<int, string>();
        }


        public void SaveImagePath(int borderId, string path)
        {
            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PageVendasSettings.json");

            ImageSettings settings;

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                settings = JsonConvert.DeserializeObject<ImageSettings>(json);
            }
            else
            {
                settings = new ImageSettings();
            }

            settings.BorderImagePaths[borderId] = path;

            var jsonSettings = JsonConvert.SerializeObject(settings);
            File.WriteAllText(filePath, jsonSettings);
        }


        public void CarregarImagem()
        {
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PageVendasSettings.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var settings = JsonConvert.DeserializeObject<ImageSettings>(json);

                foreach (Border foodBorder in VendasWrapPanel.Children)
                {
                    int borderId = (int)foodBorder.Tag;

                    if (settings?.BorderImagePaths.ContainsKey(borderId) == true)
                    {
                        string imagePath = settings.BorderImagePaths[borderId];

                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            Button imageButton = foodBorder?.Child is Grid grid ? grid.Children.OfType<Button>().FirstOrDefault() : null;

                            if (imageButton != null)
                            {
                                Image selectedImage = new Image
                                {
                                    Source = new BitmapImage(new Uri(imagePath)),
                                    Stretch = Stretch.Uniform,
                                    Height = 100, 
                                    Width = 100 
                                };

                                imageButton.Content = selectedImage;
                            }
                        }
                    }
                }
            }
        }
    }
}

