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
    public class RepositoryOrden : IRepositoryOrden
    {
        public IEnumerable<Orden> GetOrden()
        {
            List<Orden> ordenes = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ordenes = ctx.Orden.
                        Include("CondicionOrden").
                               Include("Detalle_Orden").
                               Include("Detalle_Orden.Producto").
                               OrderByDescending(x => x.Id)
                               .ToList<Orden>();

                }
                return ordenes;

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
                throw new Exception(mensaje);
            }
        }

        public IEnumerable<Orden> GetOrdenByCondicion(int condicion)
        {
            List<Orden> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Orden.Include("CondicionOrden").Include("Detalle_Orden").
                               Include("Detalle_Orden.Producto").Where(x => x.IdCondicionOrden == condicion).ToList();
            }
            return lista;
        }

        public IEnumerable<Orden> GetOrdenByFecha(DateTime fecha)
        {

            try
            {
                IEnumerable<Orden> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    //Para definir el primer día del mes
                    DateTime fecha1 = fecha;
                    while (fecha1.Day > 1)
                    {
                        fecha1 = fecha1.AddDays(-1);
                    }

                    //Para definir el último día del mes
                    DateTime fecha2 = fecha;
                    while (fecha2.Day < DateTime.DaysInMonth(fecha.Year, fecha.Month))
                    {
                        fecha2 = fecha2.AddDays(1);
                    }

                    lista = ctx.Orden.Include("CondicionOrden").
                               Include("Detalle_Orden").
                               Include("Detalle_Orden.Producto").
                               Where(p => p.FechaCreacion >= fecha1 && p.FechaCreacion <= fecha2).
                               OrderBy(p => p.FechaCreacion).
                               ToList<Orden>();
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

        public Orden GetOrdenById(int id)
        {
            Orden orden = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    orden = ctx.Orden.
                               Include("CondicionOrden").
                               Include("Detalle_Orden").
                               Include("Detalle_Orden.Producto").
                               Where(p => p.Id == id).
                               FirstOrDefault<Orden>();

                }
                return orden;

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
                throw new Exception(mensaje);
            }
        }

        //public IEnumerable<Orden> GetOrdenEnPreparacion()
        //{
        //    List<Orden> ordenes = null;
        //    try
        //    {
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            ordenes = ctx.Orden.
        //                       Include("Cliente").Include("TipoEntrega").Include("Estado").
        //                      Where(x => x.EstadoID == 1).OrderBy(x => x.FechaOrden).
        //                       ToList<Orden>();

        //        }
        //        return ordenes;

        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //}

        //public IEnumerable<Orden> GetOrdenFinalizada()
        //{
        //    List<Orden> ordenes = null;
        //    try
        //    {
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            ordenes = ctx.Orden.
        //                       Include("Cliente").Include("TipoEntrega").Include("Estado").
        //                      Where(x => x.EstadoID == 2).OrderBy(x => x.FechaOrden).
        //                       ToList<Orden>();

        //        }
        //        return ordenes;

        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //}

        //public Orden Save(Orden pOrden)
        //{
        //    int resultado = 0;
        //    Orden orden = null;
        //    try
        //    {
        //        // Salvar pero con transacción porque son 2 tablas
        //        // 1- Orden
        //        // 2- OrdenDetalle 
        //        using (MyContext ctx = new MyContext())
        //        {
        //            using (var transaccion = ctx.Database.BeginTransaction())
        //            {
        //                ctx.Orden.Add(pOrden);
        //                resultado = ctx.SaveChanges();
        //                foreach (var detalle in pOrden.OrdenDetalle)
        //                {
        //                    detalle.OrdenID = pOrden.OrdenID;
        //                }
        //                foreach (var item in pOrden.OrdenDetalle)
        //                {
        //                    // Busco el producto que está en el detalle por ProductoID
        //                    Producto oProducto = ctx.Producto.Find(item.ProductoID);

        //                    // Se indica que se alteró
        //                    ctx.Entry(oProducto).State = EntityState.Modified;
        //                    // Guardar                         
        //                    resultado = ctx.SaveChanges();
        //                }

        //                // Commit 
        //                transaccion.Commit();
        //            }
        //        }

        //        // Buscar la orden que se salvó y reenviarla
        //        if (resultado >= 0)
        //            orden = GetOrdenByID(pOrden.OrdenID);


        //        return orden;
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //}
        //public void GetOrdenCountDate(out string etiquetas, out string valores)
        //{
        //    String varEtiquetas = "";
        //    String varValores = "";
        //    try
        //    {
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            var resultado = ctx.Orden.GroupBy(x => x.FechaOrden).
        //                       Select(o => new {
        //                           Count = o.Count(),
        //                           FechaOrden = o.Key
        //                       });
        //            foreach (var item in resultado)
        //            {
        //                varEtiquetas += String.Format("{0:dd/MM/yyyy}", item.FechaOrden) + ",";
        //                varValores += item.Count + ",";
        //            }
        //        }
        //        //Ultima coma
        //        varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
        //        varValores = varValores.Substring(0, varValores.Length - 1);
        //        //Asignar valores de salida
        //        etiquetas = varEtiquetas;
        //        valores = varValores;
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //}
        //public IEnumerable<Orden> GetOrdenByFecha(string fechaInicial, string fechaFinal)
        //{
        //    DateTime fechaIn = new DateTime();
        //    DateTime fechaFi = new DateTime();
        //    fechaIn = Convert.ToDateTime(fechaInicial);
        //    fechaFi = Convert.ToDateTime(fechaFinal);

        //    try
        //    {

        //        IEnumerable<Orden> lista = null;
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;

        //            lista = ctx.Orden.Where(p => p.FechaOrden >= fechaIn).Where(p => p.FechaOrden <= fechaFi).
        //                Include("Cliente").Include("TipoEntrega").Include("Estado").ToList<Orden>();
        //        }
        //        return lista;
        //    }

        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw;
        //    }
        //}

        public Orden Save(Orden orden)
        {
            int retorno = 0;
            Orden oOrden = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oOrden = GetOrdenById(orden.Id);
                    if (oOrden == null)
                    {
                        ctx.Orden.Add(orden);
                    }
                    else
                    {
                        ctx.Entry(orden).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oOrden = GetOrdenById(orden.Id);

                return oOrden;
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
