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
    public class RepositoryTipoProducto : IRepositoryTipoProducto
    {
        public void DeleteTipoProducto(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoProducto TipoProducto = GetTipoProductoByID(id);
                    TipoProducto.Estado = false;
                    ctx.Entry(TipoProducto).State = EntityState.Modified;
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

        public IEnumerable<TipoProducto> GetTipoProducto()
        {

            try
            {
                IEnumerable<TipoProducto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.TipoProducto.Where(p => p.Estado == true).Include("Producto").ToList<TipoProducto>();
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

        public TipoProducto GetTipoProductoByID(int id)
        {
            TipoProducto TipoProducto = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoProducto = ctx.TipoProducto.Find(id);
                }

                return TipoProducto;
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


        public TipoProducto Save(TipoProducto TipoProducto)
        {
            int retorno = 0;
            TipoProducto oTipoProducto = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    TipoProducto.Estado = true;

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipoProducto = GetTipoProductoByID(TipoProducto.Id);
                    if (oTipoProducto == null)
                    {
                        ctx.TipoProducto.Add(TipoProducto);
                    }
                    else
                    {
                        ctx.Entry(TipoProducto).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oTipoProducto = GetTipoProductoByID(TipoProducto.Id);

                return oTipoProducto;
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
    }
}
