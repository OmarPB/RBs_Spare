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
        Producto GetProductoByID(int id);
        void DeleteProducto(int id);
        Producto Save(Producto Producto);
        IEnumerable<Producto> GetProductoByDescription(string name);
        IEnumerable<Producto> GetProductoByIdTipoProducto(int idTipoProducto);
    }
}
