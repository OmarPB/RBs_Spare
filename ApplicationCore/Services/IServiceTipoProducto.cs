using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceTipoProducto
    {
        IEnumerable<TipoProducto> GetTipoProducto();
        TipoProducto GetTipoProductoByID(int id);
        void DeleteTipoProducto(int id);
        TipoProducto Save(TipoProducto TipoProducto);
    }
}
