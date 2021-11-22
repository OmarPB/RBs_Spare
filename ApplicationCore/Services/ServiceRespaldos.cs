using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRespaldos : IServiceRespaldos
    {
        public void guardarRespaldo()
        {
            IRepositoryRespaldos repositoryRespaldos = new RepositoryRespaldos();
            repositoryRespaldos.guardarRespaldo();
        }
    }
}
