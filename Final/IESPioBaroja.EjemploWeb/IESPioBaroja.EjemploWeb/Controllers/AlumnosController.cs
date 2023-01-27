using IESPioBaroja.EjemploWeb.Model;
using IESPioBaroja.EjemploWeb.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace IESPioBaroja.EjemploWeb.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly IRepositorio<Alumno> _repositorio;
        public AlumnosController(IConfiguration configuration)
        {
            var strategy = new RepositoryStrategy<Alumno>(configuration);
            _repositorio = strategy.ObtenerRepositorio();
        }

        // GET: Alumnos
        public IActionResult Index()
        {
            var alumnos = _repositorio.ObtenerElementos();

            return View(alumnos);
        }

        // GET: Alumnos/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = _repositorio.ObtenerPorId(id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([Bind("Nombre,Apellido,FechaNacimiento,Dni")] Alumno alumno)
        {
            alumno.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                _repositorio.Anadir(alumno);
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumnos/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = _repositorio.ObtenerPorId(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(string id, [Bind("Nombre,Apellido,FechaNacimiento,Dni,Id")] Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repositorio.Actualizar(alumno);
                }
                catch (Exception)
                {
                    if (!AlumnoExists(alumno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumnos/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = _repositorio.ObtenerPorId(id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _repositorio.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(string id)
        {
            return _repositorio.ObtenerElementos().Any(e => e.Id == id);
        }
    }
}
