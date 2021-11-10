using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelOrdenDetalle
    {
        [Display(Name = "Número de Orden")]
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public byte[] Imagen { get; set; }
        public int Cantidad { get; set; }
        //public decimal Impuesto { get { return 0.13M; } }


        public decimal Precio { get { return (decimal)Producto.PrecioUnidad; } }
        public virtual Producto Producto { get; set; }
        public virtual Orden Orden { get; set; }

        public decimal SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }
        private decimal calculoSubtotal()
        {
            return this.Precio * this.Cantidad;
        }

        public decimal Total
        {
            get
            {
                return total();
            }
        }

        private decimal total()
        {

            decimal sub = calculoSubtotal();
            decimal total;

            total = (decimal)((sub * Producto.IVA) + sub);

            return total;
        }


        public ViewModelOrdenDetalle(int ProductoId)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            this.ProductoId = ProductoId;
            this.Producto = _ServiceProducto.GetProductoByID(ProductoId);
        }

        public int getUltimaOrdenId()
        {
            IServiceOrden serviceOrden = new ServiceOrden();
            if (serviceOrden.GetOrden().Count<Orden>() == 0)
            {
                return 1;
            }else
            return serviceOrden.GetOrden().First<Orden>().Id +1;
        }

        //Fin
    }
}