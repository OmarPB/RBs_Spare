using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMarcaProducto : IServiceMarcaProducto
    {
        public void DeleteMarcaProducto(int id)
        {
            IRepositoryMarcaProducto repository = new RepositoryMarcaProducto();
            repository.DeleteMarcaProducto(id);
        }

        public IEnumerable<MarcaProducto> GetMarcaProducto()
        {
            IRepositoryMarcaProducto repository = new RepositoryMarcaProducto();
            return repository.GetMarcaProducto();
        }

        public MarcaProducto GetMarcaProductoByID(int id)
        {
            RepositoryMarcaProducto repository = new RepositoryMarcaProducto();
            return repository.GetMarcaProductoByID(id);
        }

        public MarcaProducto Save(MarcaProducto MarcaProducto)
        {
            RepositoryMarcaProducto repository = new RepositoryMarcaProducto();
            return repository.Save(MarcaProducto);
        }

        public IEnumerable<MarcaProducto> GetMarcaProductoByName(string name)
        {
            IRepositoryMarcaProducto repository = new RepositoryMarcaProducto();
            return repository.GetMarcaProductoByName(name);
        }

        //Fin
    }
}