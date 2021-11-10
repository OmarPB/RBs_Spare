using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceModeloMoto : IServiceModeloMoto
    {
        public IEnumerable<ModeloMoto> GetModeloMoto()
        {
            IRepositoryModeloMoto repository = new RepositoryModeloMoto();
            return repository.GetModeloMoto();
        }
    }
}
