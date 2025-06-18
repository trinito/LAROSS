using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class UsuarioController
    {
        public string Login(Usuarios user)
        {
            using (var context = new la_ross_dbEntities())
            {
                return context.Usuarios.FirstOrDefault(x => x.nombre == user.nombre && x.contra == user.contra)?.tipo;

            }
        }

        public async Task<Usuarios> LoginAsync(Usuarios user)
        {
            return await Task.Run(() =>
            {
                using (var context = new la_ross_dbEntities())
                {
                    return context.Usuarios
                        .FirstOrDefault(x => x.username == user.nombre && x.contra == user.contra);
                }
            });
        }



    }
}
