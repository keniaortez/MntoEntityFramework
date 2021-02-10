using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientesEntityFrmwk.Models;

namespace ClientesEntityFrmwk.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Cliente


        //Detalle Empleados sin datos
        [HttpGet]
        public ActionResult Index()
        {
            ListaClientes cliente = new ListaClientes();            
            cliente.BuscarClientes(-1);
            return View(cliente);

            
        }

        //Detalle Clientes con ActionResult
        [HttpGet]
        public ActionResult DetalleCliente(int IdCliente)
        {
            ListaClientes cliente = new ListaClientes();
            cliente.BuscarClientes(IdCliente);
            return View(cliente);


        }


        //Crear nuevo cliente
        [HttpGet]
        public ActionResult CrearCliente()
        {

                Cliente model = new Cliente();
                model._FechaCreacion = DateTime.Now;
                return View(model);

        }


        //Con los datos
        [HttpPost]
        public ActionResult CrearCliente(Cliente modelo)
        {           
                if (ModelState.IsValid)
                {

                    Boolean insert = modelo.InsertClientes();
                    if (insert)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(modelo);
                    }

                }
                else
                {
                    return View(modelo);
                }



           
        }


        //Modificar cliente - mostrar los datos
        [HttpGet]
        public ActionResult ModificarCliente(int idClie)
        {
                if (ModelState.IsValid)
                {
                    Cliente clie = new Cliente();                   
                    clie._FechaModificacion = DateTime.Now.ToShortDateString(); ;
                    clie.GetCliente(idClie);
                    return View(clie);

                }
                else
                {
                    return RedirectToAction("Index");
                }

        }

        //Modificar cliente - para llamar la funcion de modificación
        [HttpPost]
        public ActionResult ModificarCliente(Cliente model)
        {
                if (ModelState.IsValid)
                {
                    Boolean modificar = model.ActualizarClientes();
                    if (modificar)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(model);
                    }

                }
                else
                {
                    return View(model);
                }


        }

        //Eliminar cliente
        [HttpGet]
        public ActionResult EliminarCliente(int idclie)
        {

            if (ModelState.IsValid)
            {
                Cliente clie = new Cliente();
                clie.GetCliente(idclie);
                return View(clie);

            }
            else
            {
                Cliente clie = new Cliente();
                clie.GetCliente(idclie);
                return View(clie);
            }
            
        }

        //Para llamar la funcion de eliminar
        [HttpPost]
        public ActionResult EliminarCliente(Cliente model)
        {
                if (ModelState.IsValid)
                {

                    Boolean eliminar = model.EliminarClientes();
                    if (eliminar)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(model);
                    }

                }
                else
                {
                    return View(model);
                }

        }

        #endregion
    }
}