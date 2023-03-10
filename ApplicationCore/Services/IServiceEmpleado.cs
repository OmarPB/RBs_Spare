using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEmpleado
    {
        IEnumerable<Empleado> GetEmpleado();
        IEnumerable<BitacoraEmpleados> GetBitacoras();
        Empleado GetEmpleadoByID(int id);
        void DeleteEmpleado(int id, BitacoraEmpleados bitacora);
        Empleado Save(Empleado Empleado, BitacoraEmpleados bitacora);
        Empleado Save(Empleado Empleado);
        Empleado Login(int id, string contrasenia);
        Empleado VerificarEmpleado(string email);
        Empleado GetEmpleadoByToken(string token);
    }
}
