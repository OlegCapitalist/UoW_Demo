using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace Presentation.Controllers
{
    public class CoursesController : GenericController<Course>
    {

        public CoursesController(ICoursesRepository repository) : base(repository)
        {
            _controllerName = "Courses";
            _subjectСontrollerName = "Groups";
        }

        protected override ActionResult CreateWithSelectList(int id = 0, int parrentId = 0)
        {
            throw new NotImplementedException();
        }
    }
}
