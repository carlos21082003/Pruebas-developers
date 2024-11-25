using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developers.Models.ViewModels
{
    public class EnrollmentViewModel
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int ClassroomId { get; set; }
        public int InscripcionesId { get; set; }
        public decimal? PreTest { get; set; }
        public decimal? PostTest { get; set; }
        public bool Passed { get; set; }
    }
}
