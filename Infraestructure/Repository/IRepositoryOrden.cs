using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryOrden
    {
        Orden GetOrdenById(int id);
        IEnumerable<Orden> GetOrden();
        Orden Save(Orden orden);
        //IEnumerable<Orden> GetOrdenEnPreparacion();
        //IEnumerable<Orden> GetOrdenFinalizada();
        IEnumerable<Orden> GetOrdenByCondicion(int condicion);
        IEnumerable<Orden> GetOrdenByFecha(DateTime fecha);
        //void GetOrdenCountDate(out string etiquetas, out string valores);
        //IEnumerable<Orden> GetOrdenByFecha(string fechaInicial, string fechaFinal);
    }
}
