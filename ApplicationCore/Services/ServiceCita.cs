using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCita:IServiceCita
    {
        public Cita GetCita(Cita cita)
        {
            IRepositoryCita repository = new RepositoryCita();
            return repository.GetCita(cita);
        }

        public Cita Save(Cita cita)
        {
            RepositoryCita repository = new RepositoryCita();
            return repository.Save(cita);
        }
    }
}
