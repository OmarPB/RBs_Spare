using Infraestructure.Models;
using System;
using System.Collections.Generic;

namespace Infraestructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProducto();
        IEnumerable<BitacoraProductos> GetBitacoras();
        Producto GetProductoByID(int id);
        void DeleteProducto(int id, BitacoraProductos bitacora);
        Producto Save(Producto Producto, BitacoraProductos bitacora);
        IEnumerable<Producto> GetProductoByDescription(string descripction);
        //IEnumerable<Producto> GetProductoByRangoFechas(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Producto> GetProductoByIdTipoProducto(int idTipoProducto);
        //IEnumerable<Producto> GetProductoAnalitycs();
    }
}