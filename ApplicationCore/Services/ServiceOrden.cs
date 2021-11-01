using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceOrden : IServiceOrden
    {
        public IEnumerable<Orden> GetOrden()
        {
            IRepositoryOrden repository = new RepositoryOrden();
            return repository.GetOrden();
        }

        public IEnumerable<Orden> GetOrdenByCondicion(int condicion)
        {
            IRepositoryOrden repository = new RepositoryOrden();
            return repository.GetOrdenByCondicion(condicion);
        }

        public Orden GetOrdenByID(int id)
        {
            IRepositoryOrden repository = new RepositoryOrden();
            return repository.GetOrdenById(id);
        }

        //public IEnumerable<Orden> GetOrdenEnPreparacion()
        //{
        //    IRepositoryOrden repository = new RepositoryOrden();
        //    return repository.GetOrdenEnPreparacion();
        //}

        //public IEnumerable<Orden> GetOrdenFinalizada()
        //{
        //    IRepositoryOrden repository = new RepositoryOrden();
        //    return repository.GetOrdenFinalizada();
        //}

        public Orden Save(Orden orden)
        {
            IRepositoryOrden repository = new RepositoryOrden();
            return repository.Save(orden);
        }
        //public void GetOrdenCountDate(out string etiquetas1, out string valores1)
        //{
        //    IRepositoryOrden repository = new RepositoryOrden();

        //    repository.GetOrdenCountDate(out string etiquetas, out string valores);
        //    etiquetas1 = etiquetas;
        //    valores1 = valores;
        //}

        //public IEnumerable<Orden> GetOrdenByFecha(string fechaInicial, string fechaFinal)
        //{
        //    IRepositoryOrden repository = new RepositoryOrden();
        //    return repository.GetOrdenByFecha(fechaInicial, fechaFinal);
        //}
    }
}
