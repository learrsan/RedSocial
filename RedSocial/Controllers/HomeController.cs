using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RedSocial.Servicios;
using RedSocialWebApi.Models;

namespace RedSocial.Controllers
{
    public class HomeController : Controller
    {
        BaseServicios<MensajePublico> servicio = new BaseServicios<MensajePublico>
            ("http://localhost:2503/api/MensajesPublicos"); 
        // GET: Home
        public ActionResult Index()
        {
            var us = Session["usuario"] as Usuario;
            if(us==null)
                return RedirectToAction("Index", "Autenticacion");
            var parametros = new Dictionary<String, Object>()
            {
                {"idUsuario", us.id}
            };
            var mensajes = servicio.GetList(parametros);
            
            return View(mensajes);
        }

        public ActionResult ObtenerMensajes()
        {
            var us = Session["usuario"] as Usuario;
            if (us == null)
                return RedirectToAction("Index", "Autenticacion");
            var parametros = new Dictionary<String, Object>()
            {
                {"idUsuario", us.id}
            };
            var mensajes = servicio.GetList(parametros);

            return PartialView("_ListadoMensajesPublicos", mensajes);  
        }
        
        
        [HttpPost]
        public async Task<ActionResult> NuevoMensaje(MensajePublico mensaje)
        {
            var us = Session["Usuario"] as Usuario;
            if (us == null)
                return RedirectToAction("Index", "Autenticacion");
            mensaje.idUsuario = us.id;

            await servicio.Add(mensaje);
            return Json("OK");
        }
    }
}