using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public void DeleteProducto(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Producto Producto = GetProductoByID(id);
                    Producto.Estado = false;
                    ctx.Entry(Producto).State = EntityState.Modified;
                    returno = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }

        public IEnumerable<Producto> GetProducto()
        {

            try
            {
                IEnumerable<Producto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.Producto.Where(p => p.Estado == true)
                        .Include("TipoProducto").Include("MarcaProducto").ToList<Producto>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Producto GetProductoByID(int id)
        {
            Producto Producto = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Producto = ctx.Producto.Where(p => p.Id == id).Include("TipoProducto").Include("MarcaProducto").FirstOrDefault();
                    //Producto = ctx.Producto.Find(id);
                }

                return Producto;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }


        public Producto Save(Producto Producto)
        {
            int retorno = 0;
            Producto oProducto = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    Producto.Estado = true;

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoByID(Producto.Id);
                    if (oProducto == null)
                    {
                        ctx.Producto.Add(Producto);
                    }
                    else
                    {
                        ctx.Entry(Producto).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oProducto = GetProductoByID(Producto.Id);

                return oProducto;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Producto> GetProductoByDescription(string description)
        {
            IEnumerable<Producto> lista = null;

            string sql =
                string.Format("select * from Producto where Descripcion like  '%{0}%' and Estado = 1 ", description);
            using (MyContext ctx = new MyContext())
            {
                lista = ctx.Producto.SqlQuery(sql).ToList<Producto>();
            }

            return lista;
        }

        public IEnumerable<Producto> GetProductoByIdTipoProducto(int idTipoProducto)
        {
            try
            {
                IEnumerable<Producto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.Producto.Where(p => p.IdTipoProducto == idTipoProducto)
                        .Include("TipoProducto").Include("MarcaProducto").ToList<Producto>();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        //Fin
    }
}
