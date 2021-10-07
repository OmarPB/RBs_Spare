using Infraestructure.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryEmpleado : IRepositoryEmpleado
    {
        public void DeleteEmpleado(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Empleado Empleado = GetEmpleadoByID(id);
                    Empleado.Estado = false;
                    ctx.Entry(Empleado).State = EntityState.Modified;
                    returno = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                //Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                //Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }

        public IEnumerable<Empleado> GetEmpleado()
        {

            try
            {
                IEnumerable<Empleado> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.Empleado.Where(p => p.Estado == true).Include("Rol").ToList<Empleado>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                //Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                //Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Empleado GetEmpleadoByID(int id)
        {
            Empleado empleado = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    empleado = ctx.Empleado.Where(p => p.Id == id).Include("Rol").FirstOrDefault();
                }

                return empleado;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                //Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                //Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Empleado Save(Empleado empleado)
        {
            int retorno = 0;
            Empleado oEmpleado = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    empleado.Estado = true;

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEmpleado = GetEmpleadoByID(empleado.Id);
                    if (oEmpleado == null)
                    {
                        ctx.Empleado.Add(empleado);
                    }
                    else
                    {
                        ctx.Entry(empleado).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oEmpleado = GetEmpleadoByID(empleado.Id);

                return oEmpleado;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                //Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                //Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        //Método para el Login
        public Empleado Login(int id, string contrasenia)
        {

            try
            {
                Empleado empleado = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    //empleado = ctx.Empleado.Where(p => p.Id == id && p.Contrasenia == contrasenia).Include("Rol").FirstOrDefault();
                }
                return empleado;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                //Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                //Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        //Fin
    }
}
