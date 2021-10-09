using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryMarcaProducto
    {
        IEnumerable<MarcaProducto> GetMarcaProducto();
        MarcaProducto GetMarcaProductoByID(int id);
        void DeleteMarcaProducto(int id);
        MarcaProducto Save(MarcaProducto MarcaProducto);
        IEnumerable<MarcaProducto> GetMarcaProductoByName(string name);
    }
}
