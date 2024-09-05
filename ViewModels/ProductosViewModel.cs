using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using ConstruMarket_ERP_V0._1.Models;
using System.Data.Entity;
using System.Windows.Input;
using ConstruMarket_ERP_V0._1.Helpers;

namespace ConstruMarket_ERP_V0._1.ViewModels

{
    internal class ProductosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PRODUCTO> _productos;
        public ObservableCollection<PRODUCTO> Productos
        {
            get { return _productos; }
            set { _productos = value; OnPropertyChanged(nameof(Productos)); }
        }

        public ICommand EliminarProductoCommand { get; private set; }

        public ProductosViewModel()
        {
            LoadProductos();
            EliminarProductoCommand = new RelayCommand(DeleteProducto);
        }

        public void LoadProductos()
        {
            using (var db = new TALLEREntities1())
            {
                Productos = new ObservableCollection<PRODUCTO>(db.PRODUCTOes.ToList());
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void DeleteProducto(object producto)
        {
            var productoToDelete = producto as PRODUCTO;
            if (productoToDelete != null)
            {
                Productos.Remove(productoToDelete);
                // Aquí podrías también eliminarlo de la base de datos llamando a tu contexto de EF
                using (var db = new TALLEREntities1())
                {
                    db.Entry(productoToDelete).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }


        public void SaveChanges(PRODUCTO product)
        {
            using (var db = new TALLEREntities1())
            {
                if (product.idproducto == 0)  // Si es un nuevo producto
                {
                    db.PRODUCTOes.Add(product);
                }
                else
                {
                    db.Entry(product).State = EntityState.Modified;  // Marcar como modificado
                }
                db.SaveChanges();  // Guardar los cambios en la base de datos
            }
        }


    }
}
