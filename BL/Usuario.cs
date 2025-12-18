using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result UsuarioAdd(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MOrocioThonaSegurosEntities1 context = new DL.MOrocioThonaSegurosEntities1())
                {
                    var filasAfectadas = context.UsuarioAdd(usuario.NombreCompleto, usuario.Hobby, Convert.ToDateTime(usuario.FechaNacimiento), usuario.Email);

                    if (filasAfectadas != null)
                    {
                        result.Correct = true;
                        result.Object = usuario;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro del usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.exception = ex;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MOrocioThonaSegurosEntities1 context = new DL.MOrocioThonaSegurosEntities1())
                {
                    var lista = context.UsuarioGetAll().ToList();
                    result.Objects = new List<object>();

                    if (lista.Count > 0)
                    {
                        foreach (var empleado in lista)
                        {
                            ML.Usuario user = new ML.Usuario();
                            user.IdUsuario = empleado.IdUsuario.ToString();
                            user.NombreCompleto = empleado.NombreCompleto;
                            user.Hobby = empleado.Hobby;
                            user.FechaNacimiento = empleado.FechaNacimiento;
                            user.Email = empleado.Email;
                            result.Objects.Add(user);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros de Empleados.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.exception = ex;
            }
            return result;
        }
    }
}
