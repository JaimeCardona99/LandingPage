using LandingPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LandingPage.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarTodos()
        {
            MantenimientoUsuario ma = new MantenimientoUsuario();
            return View(ma.RecuperarTodos());
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            MantenimientoUsuario ma = new MantenimientoUsuario();
            Usuario usu = new Usuario
            {
                Nombre = collection["nombre"],
                Correo = collection["correo"],
                Comentario = collection["comentario"]
            };
            ma.Insertar(usu);
            return RedirectToAction("Mensaje");
        }
        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            MantenimientoUsuario ma = new MantenimientoUsuario();
            Usuario usu = ma.Recuperar(id);
            return View(usu);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            MantenimientoUsuario ma = new MantenimientoUsuario();
            ma.Borrar(id);
            return RedirectToAction("ListarTodos");
        }
        public ActionResult Mensaje()
        {
            return View();
        }
    }
}
