using Infraestructure.Models;
using System;
using System.Collections.Generic;

namespace Infraestructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(int id);
        void DeleteProducto(int id);
        Producto Save(Producto Producto);
        IEnumerable<Producto> GetProductoByDescription(string descripction);
        //IEnumerable<Producto> GetProductoByRangoFechas(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Producto> GetProductoByIdTipoProducto(int idTipoProducto);
        //IEnumerable<Producto> GetProductoAnalitycs();
    }
}