using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryMarcaProducto : IRepositoryMarcaProducto
    {
        public void DeleteMarcaProducto(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    MarcaProducto MarcaProducto = GetMarcaProductoByID(id);
                    MarcaProducto.Estado = false;
                    ctx.Entry(MarcaProducto).State = EntityState.Modified;
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

        public IEnumerable<MarcaProducto> GetMarcaProducto()
        {

            try
            {
                IEnumerable<MarcaProducto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.MarcaProducto.Where(p => p.Estado == true).Include("Producto").ToList<MarcaProducto>();
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

        public MarcaProducto GetMarcaProductoByID(int id)
        {
            MarcaProducto MarcaProducto = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    MarcaProducto = ctx.MarcaProducto.Find(id);
                }

                return MarcaProducto;
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


        public MarcaProducto Save(MarcaProducto MarcaProducto)
        {
            int retorno = 0;
            MarcaProducto oMarcaProducto = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    MarcaProducto.Estado = true;

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMarcaProducto = GetMarcaProductoByID(MarcaProducto.Id);
                    if (oMarcaProducto == null)
                    {
                        ctx.MarcaProducto.Add(MarcaProducto);
                    }
                    else
                    {
                        ctx.Entry(MarcaProducto).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oMarcaProducto = GetMarcaProductoByID(MarcaProducto.Id);

                return oMarcaProducto;
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

        //Método para el filtrado por nombre
        public IEnumerable<MarcaProducto> GetMarcaProductoByName(string name)
        {
            IEnumerable<MarcaProducto> lista = null;

            string sql =
                string.Format("select * from MarcaProducto where Descripcion like  '%{0}%' and Estado = 1", name);
            using (MyContext ctx = new MyContext())
            {
                lista = ctx.MarcaProducto.SqlQuery(sql).ToList<MarcaProducto>();
            }

            return lista;
        }

        //Fin
    }
}
