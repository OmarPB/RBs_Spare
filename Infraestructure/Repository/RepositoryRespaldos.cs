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

        //Fin
    }
}
