using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INSCRIPCION_ESTUDIANTIL.Models;
using INSCRIPCION_ESTUDIANTIL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;

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

        //Modulo correspondiente a Inscripcion

        public IActionResult Inscripcion()
        {
            List<Inscripcion> lista = _DBContext.Inscripcion.Include(C => C.oCurso).ToList();
            return View(lista);
        }

        //HttpGet para exportar las inscripciones al excel
        [HttpGet]
        public async Task<FileResult> ExportarInscripcionesAlExcel()
        {
            var inscripciones = await _DBContext.Inscripcion.ToListAsync();
            var nombreArchivo = $"Listado de Inscripciones.xlsx";
            return GenerarExcelInscripcion(nombreArchivo, inscripciones);
        }

        //Generar excel de Inscripciones
        private FileResult GenerarExcelInscripcion(string nombreArchivo, IEnumerable<Inscripcion> inscripciones)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_INSCRIPCION"),
                new DataColumn("FECHA_INSCRIPCION"),
                new DataColumn("ID_ESTUDIANTE"),
                new DataColumn("ID_CURSO"),
                new DataColumn("ESTADO_INSCRIPCION"),
            });

            foreach (var inscripcion in inscripciones)
            {
                dataTable.Rows.Add(inscripcion.IdInscripcion,
                    inscripcion.FechaInscripcion,
                    inscripcion.IdEstudiante,
                     inscripcion.IdCurso,
                      inscripcion.EstadoInscripcion);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "Lista de Inscripciones");

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
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

        //Modulo correspondiente a Curso
        public IActionResult Cursos()
        {
            List<Curso> lista = _DBContext.Cursos.ToList();
            return View(lista);
        }

        //HttpGet para exportar los cursos al excel
        [HttpGet]
        public async Task<FileResult> ExportarCursosAlExcel()
        {
            var cursos = await _DBContext.Cursos.ToListAsync();
            var nombreArchivo = $"Listado de Cursos.xlsx";
            return GenerarExcelCurso(nombreArchivo, cursos);
        }

        //Generar excel de Curso
        private FileResult GenerarExcelCurso(string nombreArchivo, IEnumerable<Curso> cursos)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_CURSO"),
                new DataColumn("NOMBRE_CURSO"),
                new DataColumn("DESCRIPCION_CURSO")

            });

            foreach (var curso in cursos)
            {
                dataTable.Rows.Add(curso.IdCurso,
                    curso.NombreCurso,
                    curso.DescripcionCurso);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "Lista de Cursos");

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
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
            Curso oCurso = _DBContext.Cursos.Where(c => c.IdCurso == Id_Curso).FirstOrDefault();

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

        //Modulo correspondiente a secciones
        public IActionResult Secciones()
        {
            List<Seccione> lista = _DBContext.Secciones.Include(C => C.oCurso).ToList();
            return View(lista);
        }


        //HttpGet para exportar las inscripciones al excel
        [HttpGet]
        public async Task<FileResult> ExportarSeccionesAlExcel()
        {
            var secciones = await _DBContext.Secciones.ToListAsync();
            var nombreArchivo = $"Listado de Secciones.xlsx";
            return GenerarExcelSecciones(nombreArchivo, secciones);
        }

        //Generar excel de Inscripciones
        private FileResult GenerarExcelSecciones(string nombreArchivo, IEnumerable<Seccione> secciones)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_SECCIONES"),
                new DataColumn("NOMBRE_SECCION"),
                new DataColumn("CUPOS_SECCION"),
                new DataColumn("ID_CURSO"),
            });

            foreach (var seccion in secciones)
            {
                dataTable.Rows.Add(seccion.IdSecciones,
                    seccion.NombreSeccion,
                    seccion.CuposSeccion,
                     seccion.IdCurso);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "Lista de Secciones");

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
        }


        [HttpGet]
        public IActionResult Secciones_Detalle(int Id_Secciones)
        {
            SeccionesVM oSeccionesVM = new SeccionesVM()
            {
                oSecciones = new Seccione(),
                oListaCursos = _DBContext.Cursos.Select(Curso => new SelectListItem()
                {

                    Text = Curso.NombreCurso,
                    Value = Curso.IdCurso.ToString()

                }).ToList()
            };

            if (Id_Secciones != 0)
            {
                oSeccionesVM.oSecciones = _DBContext.Secciones.Find(Id_Secciones);
            }

            return View(oSeccionesVM);
        }

        [HttpPost]
        public IActionResult Secciones_Detalle(SeccionesVM oSeccionesVM)
        {
            if (oSeccionesVM.oSecciones.IdSecciones == 0)
            {
                _DBContext.Secciones.Add(oSeccionesVM.oSecciones);
            }

            else
            {
                _DBContext.Secciones.Update(oSeccionesVM.oSecciones);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Secciones", "Home");
        }


        [HttpGet]
        public IActionResult Eliminar_Secciones(int Id_Secciones)
        {
            Seccione oSecciones = _DBContext.Secciones.Include(c => c.oCurso).Where(e => e.IdSecciones == Id_Secciones).FirstOrDefault();

            return View(oSecciones);
        }

        [HttpPost]
        public IActionResult Eliminar_Secciones(Seccione oSecciones)
        {

            _DBContext.Secciones.Remove(oSecciones);
            _DBContext.SaveChanges();

            return RedirectToAction("Secciones", "Home");
        }

        //Modulo correspondiente a Asignaturas
        public IActionResult Asignaturas()
        {
            List<Asignatura> lista = _DBContext.Asignaturas.ToList();
            return View(lista);
        }

        //HttpGet para exportar las Asignaturas al excel
        [HttpGet]
        public async Task<FileResult> ExportarAsignaturasAlExcel()
        {
            var asignaturas = await _DBContext.Asignaturas.ToListAsync();
            var nombreArchivo = $"Listado de asignaturas.xlsx";
            return GenerarExcelAsignaturas(nombreArchivo, asignaturas);
        }

        //Generar excel de Asignaturas
        private FileResult GenerarExcelAsignaturas(string nombreArchivo, IEnumerable<Asignatura> asignaturas)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_ASIGNATURAS"),
                new DataColumn("NOMBRE_ASIGNATURA"),
                new DataColumn("DESCRIPCION"),
                new DataColumn("CREDITOS"),
            });

            foreach (var asignatura in asignaturas)
            {
                dataTable.Rows.Add(asignatura.IdAsignaturas,
                    asignatura.NombreAsignatura,
                    asignatura.Descripcion,
                     asignatura.Creditos);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "Lista de Asignaturas");

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
        }

        [HttpGet]
        public IActionResult Asignaturas_Detalle(int Id_Asignaturas)
        {
            AsignaturasVM oAsignaturaVM = new AsignaturasVM()
            {
                oAsignatura = new Asignatura()
            };

            if (Id_Asignaturas != 0)
            {
                oAsignaturaVM.oAsignatura = _DBContext.Asignaturas.Find(Id_Asignaturas);
            }

            return View(oAsignaturaVM);
        }

        [HttpPost]
        public IActionResult Asignaturas_Detalle(AsignaturasVM oAsignaturaVM)
        {
            if (oAsignaturaVM.oAsignatura.IdAsignaturas == 0)
            {
                _DBContext.Asignaturas.Add(oAsignaturaVM.oAsignatura);
            }

            else
            {
                _DBContext.Asignaturas.Update(oAsignaturaVM.oAsignatura);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Asignaturas", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar_Asignaturas(int Id_Asignaturas)
        {
            Asignatura oAsignaturas = _DBContext.Asignaturas.Where(e => e.IdAsignaturas == Id_Asignaturas).FirstOrDefault();

            return View(oAsignaturas);
        }

        [HttpPost]
        public IActionResult Eliminar_Asignaturas(Asignatura oAsignaturas)
        {

            _DBContext.Asignaturas.Remove(oAsignaturas);
            _DBContext.SaveChanges();

            return RedirectToAction("Asignaturas", "Home");
        }

        //Modulo correspondiente a Profesores
        public IActionResult Profesores()
        {
            List<Profesore> lista = _DBContext.Profesores.ToList();
            return View(lista);
        }

        //HttpGet para exportar las Profesores al excel
        [HttpGet]
        public async Task<FileResult> ExportarProfesoresAlExcel()
        {
            var profesores = await _DBContext.Profesores.ToListAsync();
            var nombreArchivo = $"Listado de Profesores.xlsx";
            return GenerarExcelProfesores(nombreArchivo, profesores);
        }

        //Generar excel de Profesores
        private FileResult GenerarExcelProfesores(string nombreArchivo, IEnumerable<Profesore> profesores)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_PROFESOR"),
                new DataColumn("NOMBRE_PROFESOR"),
                new DataColumn("CORREO_PROFESOR"),
                new DataColumn("ID_SECCIONES"),
                new DataColumn("ID_ASIGNATURAS"),

            });

            foreach (var profesor in profesores)
            {
                dataTable.Rows.Add(profesor.IdProfesor,
                    profesor.NombreProfesor,
                    profesor.CorreoProfesor,
                     profesor.IdSecciones,
                     profesor.IdAsignaturas);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "Lista de Profesores");

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
        }

        [HttpGet]
        public IActionResult Profesores_Detalle(int Id_Profesor)
        {
            ProfesoresVM oProfesoresVM = new ProfesoresVM()
            {
                oProfesores = new Profesore(),
                oListaSecciones = _DBContext.Secciones.Select(Seccione => new SelectListItem()
                {

                    Text = Seccione.NombreSeccion,
                    Value = Seccione.IdSecciones.ToString()

                }).ToList(),

                oListaAsignaturas = _DBContext.Asignaturas.Select(Asignatura => new SelectListItem()
                {

                    Text = Asignatura.NombreAsignatura,
                    Value = Asignatura.IdAsignaturas.ToString()

                }).ToList(),
            };

            if (Id_Profesor != 0)
            {
                oProfesoresVM.oProfesores = _DBContext.Profesores.Find(Id_Profesor);
            }

            return View(oProfesoresVM);
        }

        [HttpPost]
        public IActionResult Profesores_Detalle(ProfesoresVM oProfesoresVM)
        {
            if (oProfesoresVM.oProfesores.IdProfesor == 0)
            {
                _DBContext.Profesores.Add(oProfesoresVM.oProfesores);
            }

            else
            {
                _DBContext.Profesores.Update(oProfesoresVM.oProfesores);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Profesores", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar_Profesores(int Id_Profesor)
        {
            Profesore oProfesores = _DBContext.Profesores.Include(s => s.oSecciones).Include(a => a.oAsignatura).Where(e => e.IdProfesor == Id_Profesor).FirstOrDefault();

            return View(oProfesores);
        }

        [HttpPost]
        public IActionResult Eliminar_Profesores(Profesore oProfesores)
        {
            _DBContext.Profesores.Remove(oProfesores);
            _DBContext.SaveChanges();

            return RedirectToAction("Profesores", "Home");
        }

        //Modulo correspondiente a Pagos
        public IActionResult Pagos()
        {
            List<Pago> lista = _DBContext.Pagos.ToList();
            return View(lista);
        }


        //HttpGet para exportar los pagos al excel
        [HttpGet]
        public async Task<FileResult> ExportarPagosAlExcel()
        {
            var pagos = await _DBContext.Pagos.ToListAsync();
            var nombreArchivo = $"Listado de Pagos.xlsx";
            return GenerarExcelProfesores(nombreArchivo, pagos);
        }

        //Generar excel de Profesores
        private FileResult GenerarExcelProfesores(string nombreArchivo, IEnumerable<Pago> pagos)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_PAGO"),
                new DataColumn("FECHA_PAGO"),
                new DataColumn("ID_INSCRIPCION"),
                new DataColumn("MONTO"),
            });

            foreach (var pago in pagos)
            {
                dataTable.Rows.Add(pago.IdPago,
                    pago.FechaPago,
                    pago.IdInscripcion,
                     pago.Monto);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "Lista de Pagos");

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
        }

        [HttpGet]
        public IActionResult Pagos_Detalle(int Id_Pago)
        {
            PagosVM oPagosVM = new PagosVM()
            {
                oPagos = new Pago(),
                oListaInscripciones = _DBContext.Inscripcion.Select(Inscripcion => new SelectListItem()
                {

                    Text = Inscripcion.IdEstudiante,
                    Value = Inscripcion.IdInscripcion.ToString()

                }).ToList(),
            };

            if (Id_Pago != 0)
            {
                oPagosVM.oPagos = _DBContext.Pagos.Find(Id_Pago);
            }

            return View(oPagosVM);
        }

        [HttpPost]
        public IActionResult Pagos_Detalle(PagosVM oPagosVM)
        {
            if (oPagosVM.oPagos.IdPago == 0)
            {
                _DBContext.Pagos.Add(oPagosVM.oPagos);
            }

            else
            {
                _DBContext.Pagos.Update(oPagosVM.oPagos);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Pagos", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar_Pagos(int Id_Pago)
        {
            Pago oPagos = _DBContext.Pagos.Include(c => c.oInscripcion).Where(e => e.IdPago == Id_Pago).FirstOrDefault();

            return View(oPagos);
        }

        [HttpPost]
        public IActionResult Eliminar_Pagos(Pago oPago)
        {

            _DBContext.Pagos.Remove(oPago);
            _DBContext.SaveChanges();

            return RedirectToAction("Pagos", "Home");
        }
    }
}