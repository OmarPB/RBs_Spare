using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public void DeleteProducto(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            repository.DeleteProducto(id);
        }

        public IEnumerable<Producto> GetProducto()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto();
        }

        public Producto GetProductoByID(int id)
        {
            RepositoryProducto repository = new RepositoryProducto();
            Producto Producto = repository.GetProductoByID(id);

            return Producto;
        }

        public Producto Save(Producto Producto)
        {
            RepositoryProducto repository = new RepositoryProducto();
            return repository.Save(Producto);
        }

        public IEnumerable<Producto> GetProductoByDescription(string descripcion)
        {
            RepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.GetProductoByDescription(descripcion);
        }

        public IEnumerable<Producto> GetProductoByIdTipoProducto(int idTipoProducto)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.GetProductoByIdTipoProducto(idTipoProducto);
        }

        //Fin
    }
}
