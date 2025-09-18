using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class ConfiguracionController
    {

        public async Task<List<UsuarioDTO>> ObtenerTodosLosUsuariosAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Usuarios
                    .Where(u => u.tipo != "ADMIN") // 👈 filtramos aquí
                    .Select(u => new UsuarioDTO
                    {
                        Id = u.id,
                        Nombres = u.nombre + " " + u.apellido,
                        Usuario = u.username,
                        Permisos = u.permisos ?? "Sin permisos", // si es NULL le ponemos un texto
                        Estatus = u.estatus ? "Activo" : "Inactivo"
                    })
                    .ToListAsync();
            }
        }

        public async Task<bool> AgregarUsuarioAsync(string nombre, string apellido, string usuario, string contraseña, string permisos)
        {
            using (var context = new la_ross_dbEntities())
            {
                // Validar que no exista el mismo username
                bool existe = await context.Usuarios.AnyAsync(u => u.username == usuario);
                if (existe)
                    throw new Exception("El nombre de usuario ya existe.");

                var nuevoUsuario = new Usuarios
                {
                    nombre = nombre,
                    apellido = apellido,
                    username = usuario,
                    contra = contraseña, // 👈 si quieres, aquí deberías encriptar
                    tipo = "CAJA",       // o el rol por defecto
                    permisos = permisos,
                    estatus = true       // activo por defecto
                };

                context.Usuarios.Add(nuevoUsuario);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Usuarios> ObtenerUsuarioPorIdAsync(int id)
        {
            using (var context = new la_ross_dbEntities())
            {
                var usuario = await context.Usuarios
                                           .FirstOrDefaultAsync(u => u.id == id && u.estatus == true);
                return usuario; // será null si está inactivo
            }
        }

        public async Task<bool> ModificarUsuarioAsync(int id, string nombre, string apellido, string usuario, string contraseña, string permisos)
        {
            using (var context = new la_ross_dbEntities())
            {
                var user = await context.Usuarios.FindAsync(id);
                if (user == null)
                    throw new Exception("Usuario no encontrado.");

                // Validar username duplicado
                bool existe = await context.Usuarios.AnyAsync(u => u.username == usuario && u.id != id);
                if (existe)
                    throw new Exception("El nombre de usuario ya existe.");

                user.nombre = nombre;
                user.apellido = apellido;
                user.username = usuario;
                user.contra = contraseña; // opcional: encriptar
                user.permisos = permisos;

                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            using (var context = new la_ross_dbEntities())
            {
                var user = await context.Usuarios.FindAsync(id);
                if (user == null)
                    throw new Exception("Usuario no encontrado.");

                // Eliminación lógica
                user.estatus = false;

                await context.SaveChangesAsync();
                return true;
            }
        }


    }
}
