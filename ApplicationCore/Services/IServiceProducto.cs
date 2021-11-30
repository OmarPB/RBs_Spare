using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> GetProducto();
        IEnumerable<BitacoraProductos> GetBitacoras();
        Producto GetProductoByID(int id);
        void DeleteProducto(int id, BitacoraProductos bitacora);
        Producto Save(Producto Producto, BitacoraProductos bitacora);
        IEnumerable<Producto> GetProductoByDescription(string name);
        IEnumerable<Producto> GetProductoByIdTipoProducto(int idTipoProducto);
    }
}
