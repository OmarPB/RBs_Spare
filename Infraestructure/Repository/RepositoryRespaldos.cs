using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
                string url = @"'C:\Users\apica\Downloads\RBsSpare_" + DateTime.Now.ToString("dd-MMMM-yyyy HH-mm") + ".bak'";
                using (MyContext ctx = new MyContext())
                {
                    ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "backup database RBs_Spare to disk = " + url);
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
