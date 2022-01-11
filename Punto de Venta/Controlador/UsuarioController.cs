using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class UsuarioController
    {

        private readonly chinahousedbEntities Context;

        public UsuarioController(chinahousedbEntities context)
        {
            Context = context;  
        }

        public string Login(Usuarios user)
        {
            return Context.Usuarios.FirstOrDefault(x => x.nombre == user.nombre && x.contra == user.contra)?.tipo;
        }
    }
}
