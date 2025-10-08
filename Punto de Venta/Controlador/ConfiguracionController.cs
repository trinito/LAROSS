using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                // Buscar usuario existente
                var existente = await context.Usuarios
                    .SingleOrDefaultAsync(u => u.username == usuario);

                if (existente != null)
                {
                    if (existente.estatus)
                    {
                        // Usuario activo, mostrar mensaje y salir
                        MessageBox.Show("El nombre de usuario ya existe y está activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        // Usuario existe pero inactivo → preguntar si desea reactivar
                        var result = MessageBox.Show(
                            "El usuario existe pero está desactivado. ¿Desea reactivarlo?",
                            "Reactivar usuario",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            // Reactivar usuario
                            existente.estatus = true;
                            existente.nombre = nombre;       // Opcional: actualizar datos
                            existente.apellido = apellido;   // Opcional: actualizar datos
                            existente.contra = contraseña;   // Opcional: actualizar datos
                            existente.permisos = permisos;   // Opcional: actualizar datos

                            await context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            // No reactivar
                            return false;
                        }
                    }
                }

                // Usuario no existe → crear nuevo
                var nuevoUsuario = new Usuarios
                {
                    nombre = nombre,
                    apellido = apellido,
                    username = usuario,
                    contra = contraseña, // aquí podrías encriptar
                    tipo = "CAJA",
                    permisos = permisos,
                    estatus = true
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
