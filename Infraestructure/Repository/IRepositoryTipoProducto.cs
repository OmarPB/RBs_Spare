using Infraestructure.Models;
using System.Collections.Generic;

namespace Infraestructure.Repository
{
    public interface IRepositoryTipoProducto
    {
        IEnumerable<TipoProducto> GetTipoProducto();
        TipoProducto GetTipoProductoByID(int id);
        void DeleteTipoProducto(int id);
        TipoProducto Save(TipoProducto TipoProducto);
    }
}
