using Microsoft.AspNetCore.Mvc.Rendering;

namespace INSCRIPCION_ESTUDIANTIL.Models.ViewModels
{
    public class ProfesoresVM
    {
        public Profesore oProfesores { get; set; }

        public List<SelectListItem> oListaAsignaturas { get; set; }

        public List<SelectListItem> oListaSecciones { get; set; }

    }
}
