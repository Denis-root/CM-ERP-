using ConstruMarket_ERP_V0._1.Helpers;
using ConstruMarket_ERP_V0._1.Models.MecaRepo01;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstruMarket_ERP_V0._1.ViewModels
{
    public class MecanicosReporteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<MecanicoReporte> _mecanicosReporte;
        public List<MecanicoReporte> MecanicosReporte
        {
            get { return _mecanicosReporte; }
            set
            {
                _mecanicosReporte = value;
                OnPropertyChanged("MecanicosReporte");
            }
        }

        private TotalTarifa _totalTarifa;
        public TotalTarifa TotalTarifa
        {
            get { return _totalTarifa; }
            set
            {
                _totalTarifa = value;
                OnPropertyChanged("TotalTarifa");
            }
        }

        private DateTime _reportDate = DateTime.Now;
        public DateTime ReportDate
        {
            get { return _reportDate; }
            set
            {
                _reportDate = value;
                OnPropertyChanged("ReportDate");
            }
        }

        public MecanicosReporteViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {            
            MecanicoReporteRepository repo = new MecanicoReporteRepository();
            var result = repo.GetMecanicosReport();
            MecanicosReporte = result.Item1;
            TotalTarifa = result.Item2;
            ReportDate = DateTime.Now;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
