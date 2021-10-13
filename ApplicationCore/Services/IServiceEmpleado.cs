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
        Empleado GetEmpleadoByID(int id);
        void DeleteEmpleado(int id);
        Empleado Save(Empleado Empleado);
        Empleado Login(int id, string contrasenia);
        bool VerificarEmpleado(string email);
    }
}
