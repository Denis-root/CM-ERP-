﻿using ConstruMarket_ERP_V0._1.Views;
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

namespace ConstruMarket_ERP_V0._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null && item.Header != null)
            {
                switch (item.Header.ToString())
                {
                    case "Productos":
                        contentArea.Content = new ProductosView(); // `contentArea` es el nombre del Grid o Panel donde se mostrará la vista
                        break;
                    case "Mecánicos":
                        contentArea.Content = new MecanicosView(); // Suponiendo que tienes un UserControl para Mecánicos
                        break;
                        // Más casos según tus ítems
                }
            }
        }
    }
}
