using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class AccesoDatosBDException : Exception
    {
        public const string MESSAGE = "Error en el acceso a la BD. Comuniquese con Administrador";
        public AccesoDatosBDException()
        {
        }
    }
}
