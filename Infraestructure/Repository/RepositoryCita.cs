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
