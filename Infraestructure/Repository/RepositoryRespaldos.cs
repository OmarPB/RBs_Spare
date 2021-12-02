using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryRespaldos : IRepositoryRespaldos
    {
        public void guardarRespaldo()
        {
            try
            {
                string path = @"C:\RespaldosRBsSpare";
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string url = @"'C:\RespaldosRBsSpare\RBsSpare_" + DateTime.Now.ToString("dd-MMMM-yyyy HH-mm") + ".bak'";
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "backup database RBs_Spare to disk = " + url);
                    }

                }
                catch (DirectoryNotFoundException ex)
                {
                    throw new Exception(ex.Message);

                }
            }
            catch (Exception dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public void restaurarRespaldo(string ruta)
        {
            try
            {


                SqlConnection con = new SqlConnection("data source=.;initial catalog=RBs_Spare;user id=sa;password=123456;MultipleActiveResultSets=True;");

                con.Open();

                string sqlStmt2 = string.Format("ALTER DATABASE RBs_Spare SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, con);
                bu2.ExecuteNonQuery();

                string sqlStmt3 = "USE MASTER RESTORE DATABASE RBs_Spare FROM DISK='" + ruta + "'WITH REPLACE;";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, con);
                bu3.ExecuteNonQuery();

                string sqlStmt4 = string.Format("ALTER DATABASE RBs_Spare SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, con);
                bu4.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        //Fin
    }
}
