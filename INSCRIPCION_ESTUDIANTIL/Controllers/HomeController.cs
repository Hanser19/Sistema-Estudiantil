using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INSCRIPCION_ESTUDIANTIL.Models;
using INSCRIPCION_ESTUDIANTIL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace INSCRIPCION_ESTUDIANTIL.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbproyectoContext _DBContext;

        public HomeController(DbproyectoContext context)
        {
            _DBContext = context;
        }

        public IActionResult Index()
        {
            List<Inscripcion> lista = _DBContext.Inscripcion.Include(C => C.oCurso).ToList();
            return View(lista);
        }

        public IActionResult Inscripcion()
        {
            List<Inscripcion> lista = _DBContext.Inscripcion.Include(C => C.oCurso).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Inscripcion_Detalle(int Id_Inscripcion)
        {
            InscripcionVM oInscripcionVM = new InscripcionVM()
            {
                oInscripcion = new Inscripcion(),
                oListaCurso = _DBContext.Cursos.Select(Curso => new SelectListItem()
                {

                    Text = Curso.NombreCurso,
                    Value = Curso.IdCurso.ToString()

                }).ToList()
            };

            if (Id_Inscripcion !=0)
            {
                oInscripcionVM.oInscripcion = _DBContext.Inscripcion.Find(Id_Inscripcion);
            }

            return View(oInscripcionVM);
        }

        [HttpPost]
        public IActionResult Inscripcion_Detalle(InscripcionVM oInscripcionVM)
        {
            if (oInscripcionVM.oInscripcion.IdInscripcion == 0)
            {
                _DBContext.Inscripcion.Add(oInscripcionVM.oInscripcion);
            }

            else
            {
                _DBContext.Inscripcion.Update(oInscripcionVM.oInscripcion);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Inscripcion", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar_Inscripcion(int Id_Inscripcion)
        {
            Inscripcion oInscripcion = _DBContext.Inscripcion.Include(c => c.oCurso).Where(e => e.IdInscripcion == Id_Inscripcion).FirstOrDefault();

            return View(oInscripcion);
        }

        [HttpPost]
        public IActionResult Eliminar_Inscripcion(Inscripcion oInscripcion)
        {

            _DBContext.Inscripcion.Remove(oInscripcion);
            _DBContext.SaveChanges();

            return RedirectToAction("Inscripcion", "Home");
        }

        public IActionResult Cursos()
        {
            List<Curso> lista = _DBContext.Cursos.ToList();
            return View(lista);
        }


        [HttpGet]
        public IActionResult Curso_Detalle(int Id_Curso)
        {
            CursoVM oCursoVM = new CursoVM()
            {
                oCurso = new Curso()
            };

            if (Id_Curso != 0)
            {
                oCursoVM.oCurso = _DBContext.Cursos.Find(Id_Curso);
            }

            return View(oCursoVM);
        }

        [HttpPost]
        public IActionResult Curso_Detalle(CursoVM oCursoVM)
        {
            if (oCursoVM.oCurso.IdCurso == 0)
            {
                _DBContext.Cursos.Add(oCursoVM.oCurso);
            }

            else
            {
                _DBContext.Cursos.Update(oCursoVM.oCurso);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Cursos", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar_Curso(int Id_Curso)
        {
            Curso oCurso = _DBContext.Cursos.FirstOrDefault(c => c.IdCurso == Id_Curso);

            return View(oCurso);
        }

        //Elimina la Inscripcion relacionada primero y luego el curso
        [HttpPost]
        public IActionResult Eliminar_Curso(Curso oCurso)
        {
            var seccionesRelacionadas = _DBContext.Secciones
                .Where(s => s.IdCurso == oCurso.IdCurso)
                .ToList();

            foreach (var seccion in seccionesRelacionadas)
            {
                // Delete related PROFESORES records
                var profesoresRelacionados = _DBContext.Profesores
                    .Where(p => p.IdSecciones == seccion.IdSecciones)
                    .ToList();

                foreach (var profesor in profesoresRelacionados)
                {
                    _DBContext.Profesores.Remove(profesor);
                }

                _DBContext.Secciones.Remove(seccion);
            }

            var inscripcionesRelacionadas = _DBContext.Inscripcion
                .Where(i => i.IdCurso == oCurso.IdCurso)
                .ToList();

            foreach (var inscripcion in inscripcionesRelacionadas)
            {
                // Delete related PAGOS records
                var pagosRelacionados = _DBContext.Pagos
                    .Where(p => p.IdInscripcion == inscripcion.IdInscripcion)
                    .ToList();

                foreach (var pago in pagosRelacionados)
                {
                    _DBContext.Pagos.Remove(pago);
                }

                _DBContext.Inscripcion.Remove(inscripcion);
            }

            _DBContext.Cursos.Remove(oCurso);
            _DBContext.SaveChanges();

            return RedirectToAction("Cursos", "Home");
        }



    }
}