using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;

namespace Infraestructure.Repository
{
    public interface IRepositoryCondicionOrden
    {
        IEnumerable<CondicionOrden> GetCondicionOrden();
        CondicionOrden GetCondicionOrdenById(int id);
    }
}
