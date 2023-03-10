using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEmpleado : IServiceEmpleado
    {
        public void DeleteEmpleado(int id, BitacoraEmpleados bitacora)
        {
            IRepositoryEmpleado repository = new RepositoryEmpleado();
            repository.DeleteEmpleado(id, bitacora);
        }

        public IEnumerable<Empleado> GetEmpleado()
        {
            IRepositoryEmpleado repository = new RepositoryEmpleado();
            return repository.GetEmpleado();
        }

        public IEnumerable<BitacoraEmpleados> GetBitacoras()
        {
            IRepositoryEmpleado repository = new RepositoryEmpleado();
            return repository.GetBitacoras();
        }

        public Empleado GetEmpleadoByID(int id)
        {
            RepositoryEmpleado repository = new RepositoryEmpleado();
            return repository.GetEmpleadoByID(id);
        }

        public Empleado Save(Empleado empleado, BitacoraEmpleados bitacora)
        {
            RepositoryEmpleado repository = new RepositoryEmpleado();
            Empleado auxEmp = repository.GetEmpleadoByID(empleado.Id);
            if (auxEmp != null)
            {
                if (auxEmp.Contrasenia.Equals(empleado.Contrasenia))
                {
                    //Desencripta la contraseña para prevenir errores durante el proceso de recuperación
                    empleado.Contrasenia = Cryptography.DecrypthAES(empleado.Contrasenia);
                }
            }
            
            //Encripta la contraseña y la envía a la base de datos
            empleado.Contrasenia = Cryptography.EncrypthAES(empleado.Contrasenia);

            return repository.Save(empleado, bitacora);
        }

        public Empleado Save(Empleado empleado)
        {
            RepositoryEmpleado repository = new RepositoryEmpleado();
            Empleado auxEmp = repository.GetEmpleadoByID(empleado.Id);
            if (auxEmp != null)
            {
                if (auxEmp.Contrasenia.Equals(empleado.Contrasenia))
                {
                    //Desencripta la contraseña para prevenir errores durante el proceso de recuperación
                    empleado.Contrasenia = Cryptography.DecrypthAES(empleado.Contrasenia);
                }
            }

            //Encripta la contraseña y la envía a la base de datos
            empleado.Contrasenia = Cryptography.EncrypthAES(empleado.Contrasenia);

            return repository.Save(empleado);
        }

        //Método se que se usará para el login
        public Empleado Login(int id, string contrasenia)
        {
            IRepositoryEmpleado repository = new RepositoryEmpleado();

            // Se encripta el valor que viene y se compara con el valor encriptado en al BD.
            string crytpPasswd = Cryptography.EncrypthAES(contrasenia);

            return repository.Login(id, crytpPasswd);

        }

        public Empleado VerificarEmpleado(string email)
        {
            IRepositoryEmpleado repository = new RepositoryEmpleado();
            return repository.VerificarEmpleado(email);
        }

        public Empleado GetEmpleadoByToken(string token)
        {
            IRepositoryEmpleado repository = new RepositoryEmpleado();
            return repository.GetEmpleadoByToken(token);
        }

        //Fin
    }
}
