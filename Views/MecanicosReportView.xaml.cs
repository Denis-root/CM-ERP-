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
    /// Interaction logic for MecanicosReportView.xaml
    /// </summary>
    public partial class MecanicosReportView : UserControl
    {
        public MecanicosReportView()
        {
            InitializeComponent();
            this.DataContext = new MecanicosReporteViewModel();
        }
    }
}
