using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Controllers
{
    public class StudentsController : GenericController<Student>
    {
        public StudentsController(IStudentsRepository repository) : base(repository)
        {
            _controllerName = "Students";
        }

        // GET: GroupsController
        [HttpGet]
        public override ActionResult Index(int? id = null)
        {
            if (id == null)
            {
                _parrent = null;
                ViewBag.ModelList = _repository.GetList();
            }
            else
            {
                _parrent = _context.Groups.Find(id);
                ViewBag.ModelList = _repository.GetListByParrent((int)id);
            }
            ViewBag.Parrent = _parrent;

            return View();
        }

        // GET: StudentsController/Create
        [HttpGet]
        public override ActionResult Create(int parrentId = 0)
        {
            return CreateWithSelectList(0, parrentId);
        }

        // GET: StudentsController/Edit/5
        [HttpGet]
        public override ActionResult Edit(int id, int parrentId = 0)
        {
            return CreateWithSelectList(id, parrentId);
        }

        // GET: StudentsController/Delete/5
        [HttpGet]
        public override ActionResult Delete(int id, int parrentId = 0)
        {
            return CreateWithSelectList(id, parrentId);
        }

        protected override ActionResult CreateWithSelectList(int id = 0, int parrentId = 0)
        {
            _parrent = _context.Groups.Find(parrentId);
            var group = _repository.GetById(id);
            ViewData["ParrentId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewBag.Parrent = _parrent;
            if (group == null)
                return View();
            else
                return View(group);
        }
    }
}
