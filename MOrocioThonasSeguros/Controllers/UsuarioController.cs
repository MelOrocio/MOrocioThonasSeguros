using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOrocioThonasSeguros.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();  
            usuario.Usuarios = new List<object>();

            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al cargar la información: " + result.ErrorMessage;
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Validamos que la fecha no sea nula
                    if (!usuario.FechaNacimiento.HasValue)
                    {
                        ViewBag.Message = "La fecha de nacimiento es requerida";
                        return View(usuario);
                    }

                    ML.Result result = BL.Usuario.UsuarioAdd(usuario);

                    if (result.Correct)
                    {
                        ML.Usuario usuarioRegistrado = (ML.Usuario)result.Object;

                        ViewBag.Message = result.ErrorMessage;
                        ViewBag.IdUsuario = usuarioRegistrado.IdUsuario;

                        return RedirectToAction("GetAll");

                    }
                    else
                    {
                        ViewBag.Message = result.ErrorMessage;
                        return View(usuario);
                    }
                }
                else
                {
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return View(usuario);
            }
        }
    }
}
