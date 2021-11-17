using Infraestructure.Models;
using Infraestructure.Utils;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        //public Empleado GetEmpleadoByID(int idEmpleado)
        //{
        //    Empleado empleado = null;
        //    List<Empleado> lista = null;
        //    if (HttpContext.Current.Session["Empleado"] != null)
        //    {
        //        lista = (List<Empleado>)HttpContext.Current.Session["Empleado"];
        //        empleado = lista.Find(f => f.Id == idEmpleado);
        //    }
        //    return empleado;
        //}

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
                    empleado = ctx.Empleado.Where(p => p.Id == id && p.Contrasenia == contrasenia).Include("Rol").FirstOrDefault();
                }
                return empleado;
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

        public Empleado VerificarEmpleado(string email)
        {
            try
            {
                Empleado empleado = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    empleado = ctx.Empleado.Where(p => p.Email == email).Include("Rol").FirstOrDefault();
                }
                if (empleado != null)
                {
                    empleado.TokenRecuperacion = GetSha256(Guid.NewGuid().ToString());
                    SendEmail(email, empleado.TokenRecuperacion);

                    return empleado;
                }
                else
                    return null;
                
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


        public Empleado GetEmpleadoByToken(string token)
        {
            try
            {
                Empleado empleado = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    empleado = ctx.Empleado.Where(p => p.TokenRecuperacion == token).Include("Rol").FirstOrDefault();
                }
                return empleado;

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

        //Método para pasar el token a un hash
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestino, string token)
        {
            string urlDomain = "http://localhost:64990/";
            string EmailOrigen = "autodo.web@gmail.com";
            string Contraseña = "omaresteban*";
            string url = urlDomain + "/Login/Recuperacion/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de Contraseña",
                "<p>Estimado usuario,</br></br>Ha iniciado el proceso para recuperación de contraseña del sistema RB's Spare, a continuación se le presentará el link para que proceda a realizar el cambio de su respectiva contraseña.</p><br>" +
                "<a href='" + url + "'>Click para recuperar la contraseña</a></br>" +
                "<p>De no haber sido usted quién inició el proceso de recuperación, por favor comuníquelo a su administrador.</p>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }


        //Fin
    }
}
