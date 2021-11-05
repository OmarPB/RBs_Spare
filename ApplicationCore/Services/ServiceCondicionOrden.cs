using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCondicionOrden : IServiceCondicionOrden
    {
        public IEnumerable<CondicionOrden> GetCondicionOrden()
        {
            IRepositoryCondicionOrden repository = new RepositoryCondicionOrden();
            return repository.GetCondicionOrden();
        }

        public CondicionOrden GetCondicionOrdenById(int id)
        {
            IRepositoryCondicionOrden repository = new RepositoryCondicionOrden();
            return repository.GetCondicionOrdenById(id);
        }
    }
}
