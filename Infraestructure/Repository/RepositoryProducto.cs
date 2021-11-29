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
    public class RepositoryProducto : IRepositoryProducto
    {
        public void DeleteProducto(int id, BitacoraProductos bitacora)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Producto Producto = GetProductoByID(id);
                    Producto.Estado = false;

                    //Datos restantes bitácora
                    bitacora.Accion = "Desactivar";
                    bitacora.DatoAnterior = "Id: " + Producto.Id +
                                               "\n/IdTipoProducto: " + Producto.IdTipoProducto +
                                               "\n/IdMarca: " + Producto.IdMarca +
                                               "\n/Descripcion: " + Producto.Descripcion +
                                               "\n/PrecioUnidad: " + Producto.PrecioUnidad +
                                               "\n/IVA: " + Producto.PrecioUnidad;

                    bitacora.DatosNuevo = "Registro Desactivado";

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
                        .Include("TipoProducto").Include("MarcaProducto").Include("Detalle_Orden").ToList<Producto>();
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
                    Producto = ctx.Producto.Where(p => p.Id == id).Include("TipoProducto").Include("MarcaProducto").Include("Detalle_Orden").FirstOrDefault();
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


        public Producto Save(Producto producto, BitacoraProductos bitacora)
        {
            int retorno = 0;
            Producto oProducto = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    producto.Estado = true;

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoByID(producto.Id);
                    if (oProducto == null)
                    {
                        producto.IVA = producto.IVA / 100;

                        //Datos restantes bitácora
                        bitacora.Accion = "Insertar";
                        bitacora.DatoAnterior = "Nuevo Registro";

                        bitacora.DatosNuevo = "Id: Nuevo" +
                                               "\n/IdTipoProducto: " + producto.IdTipoProducto +
                                               "\n/IdMarca: " + producto.IdMarca +
                                               "\n/Descripcion: " + producto.Descripcion +
                                               "\n/PrecioUnidad: " + producto.PrecioUnidad +
                                               "\n/IVA: " + producto.PrecioUnidad;

                        ctx.BitacoraProductos.Add(bitacora);
                        ctx.Producto.Add(producto);
                    }
                    else
                    {
                        producto.IVA = producto.IVA / 100;

                        //Datos restantes bitácora
                        bitacora.Accion = "Editar";
                        bitacora.DatoAnterior = "Id: " + oProducto.Id + 
                                               "\n/IdTipoProducto: " + oProducto.IdTipoProducto +
                                               "\n/IdMarca: " + oProducto.IdMarca +
                                               "\n/Descripcion: " + oProducto.Descripcion +
                                               "\n/PrecioUnidad: " + oProducto.PrecioUnidad +
                                               "\n/IVA: " + oProducto.PrecioUnidad;

                        bitacora.DatosNuevo = "Id: Nuevo" +
                                               "\n/IdTipoProducto: " + producto.IdTipoProducto +
                                               "\n/IdMarca: " + producto.IdMarca +
                                               "\n/Descripcion: " + producto.Descripcion +
                                               "\n/PrecioUnidad: " + producto.PrecioUnidad +
                                               "\n/IVA: " + producto.PrecioUnidad;

                        ctx.BitacoraProductos.Add(bitacora);
                        ctx.Entry(producto).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oProducto = GetProductoByID(producto.Id);

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
                        .Include("TipoProducto").Include("MarcaProducto").Include("Detalle_Orden").ToList<Producto>();
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
