using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoProducto : IServiceTipoProducto
    {
        public void DeleteTipoProducto(int id)
        {
            IRepositoryTipoProducto repository = new RepositoryTipoProducto();
            repository.DeleteTipoProducto(id);
        }

        public IEnumerable<TipoProducto> GetTipoProducto()
        {
            IRepositoryTipoProducto repository = new RepositoryTipoProducto();
            return repository.GetTipoProducto();
        }

        public TipoProducto GetTipoProductoByID(int id)
        {
            RepositoryTipoProducto repository = new RepositoryTipoProducto();
            return repository.GetTipoProductoByID(id);
        }

        public TipoProducto Save(TipoProducto TipoProducto)
        {
            RepositoryTipoProducto repository = new RepositoryTipoProducto();
            return repository.Save(TipoProducto);
        }
    }
}
