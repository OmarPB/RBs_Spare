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
    public class RepositoryCita : IRepositoryCita
    {
        public Cita GetCita(Cita cita)
        {
            try
            {
                Cita oCita = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCita = ctx.Cita.Where(p => p.FechaCita == cita.FechaCita && p.HoraCita == cita.HoraCita).Include("ModeloMoto").FirstOrDefault();
                }
                return oCita;
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

        public IEnumerable<Cita> GetCitas()
        {
            try
            {
                IEnumerable<Cita> oCita = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCita = ctx.Cita.Where(p => p.FechaCita == DateTime.Today).Include("ModeloMoto").Include("ModeloMoto.MarcaMoto").
                        OrderByDescending(p => p.Id).ToList<Cita>();
                }
                return oCita;
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


        public IEnumerable<Cita> GetCitasReport()
        {
            try
            {
                IEnumerable<Cita> oCita = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCita = ctx.Cita.Include("ModeloMoto").Include("ModeloMoto.MarcaMoto").
                        OrderByDescending(p => p.Id).ToList<Cita>();
                }
                return oCita;
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

        public IEnumerable<Cita> GetCitasByFecha(DateTime fecha)
        {
            try
            {
                IEnumerable<Cita> lista = null;
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

                    lista = ctx.Cita.Include("ModeloMoto").Include("ModeloMoto.MarcaMoto").
                               Where(p => p.FechaCita >= fecha1 && p.FechaCita <= fecha2).
                               OrderBy(p => p.FechaCita).
                               ToList<Cita>();
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

        public Cita Save(Cita cita)
        {
            int retorno = 0;
            Cita oCita = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    cita.Condicion = true;

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCita = GetCita(cita);
                    if (oCita == null)
                    {
                        ctx.Cita.Add(cita);
                    }
                    else
                    {
                        ctx.Entry(cita).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oCita = GetCita(cita);

                return oCita;
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
