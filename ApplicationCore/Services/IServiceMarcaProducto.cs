using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceMarcaProducto
    {
        IEnumerable<MarcaProducto> GetMarcaProducto();
        MarcaProducto GetMarcaProductoByID(int id);
        void DeleteMarcaProducto(int id);
        MarcaProducto Save(MarcaProducto MarcaProducto);
        IEnumerable<MarcaProducto> GetMarcaProductoByName(string name);
    }
}
