using ConstruMarket_ERP_V0._1.Models;
using ConstruMarket_ERP_V0._1.ViewModels;
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

namespace ConstruMarket_ERP_V0._1.Views
{
    /// <summary>
    /// Interaction logic for ProductosView.xaml
    /// </summary>
    public partial class ProductosView : UserControl
    {
        public ProductosView()
        {
            InitializeComponent();
            this.DataContext = new ProductosViewModel();  // Asigna el ViewModel como DataContext del UserControl.
        }

        private void productosDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var product = e.Row.Item as PRODUCTO;
                if (product != null)
                {
                    var viewModel = this.DataContext as ProductosViewModel;
                    viewModel.SaveChanges(product);
                }
            }
        }


    }
}
