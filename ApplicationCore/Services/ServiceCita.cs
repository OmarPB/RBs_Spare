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

        public IEnumerable<Cita> GetCitas()
        {
            IRepositoryCita repository = new RepositoryCita();
            return repository.GetCitas();
        }

        public IEnumerable<Cita> GetCitasReport()
        {
            IRepositoryCita repository = new RepositoryCita();
            return repository.GetCitasReport();
        }

        public IEnumerable<Cita> GetCitasByFecha(DateTime fecha)
        {
            IRepositoryCita repository = new RepositoryCita();
            return repository.GetCitasByFecha(fecha);
        }

        public Cita Save(Cita cita)
        {
            RepositoryCita repository = new RepositoryCita();
            return repository.Save(cita);
        }
    }
}
