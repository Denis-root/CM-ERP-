using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using ConstruMarket_ERP_V0._1.Models;
using System.Data.Entity;
using System.Windows.Input;
using ConstruMarket_ERP_V0._1.Helpers;
using System.ComponentModel;

namespace ConstruMarket_ERP_V0._1.ViewModels
{
    internal class MecanicosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MECANICO> _mecanicos;
        public ObservableCollection<MECANICO> Mecanicos
        {
            get { return _mecanicos; }
            set { _mecanicos = value; OnPropertyChanged(nameof(Mecanicos)); }
        }

        public MecanicosViewModel()
        {
            LoadMecanicos();
            EliminarMecanicoCommand = new RelayCommand(DeleteMecanico);
        }

        public ICommand EliminarMecanicoCommand { get; private set; }

        public void LoadMecanicos()
        {
            using (var db = new TALLEREntities1())
            {
                Mecanicos = new ObservableCollection<MECANICO>(db.MECANICOes.ToList());
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void DeleteMecanico(object mecanico)
        {
            var mecanicoToDelete = mecanico as MECANICO;
            if (mecanicoToDelete != null)
            {
                Mecanicos.Remove(mecanicoToDelete);
                // Aquí podrías también eliminarlo de la base de datos llamando a tu contexto de EF
                using (var db = new TALLEREntities1())
                {
                    db.Entry(mecanicoToDelete).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }


        public void SaveChanges(MECANICO mecanico)
        {
            using (var db = new TALLEREntities1())
            {
                if (mecanico.idmecanico == 0)  // Si es un nuevo producto
                {
                    db.MECANICOes.Add(mecanico);
                }
                else
                {
                    db.Entry(mecanico).State = EntityState.Modified;  // Marcar como modificado
                }
                db.SaveChanges();  // Guardar los cambios en la base de datos
            }
        }

    }
}
