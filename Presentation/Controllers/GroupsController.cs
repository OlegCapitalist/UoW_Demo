using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Services.Interfaces;

namespace Presentation.Controllers
{
    public class GroupsController : GenericController<Group>
    {
        public GroupsController(IGroupsRepository repository) : base(repository)
        {
            _controllerName = "Groups";
            _subjectСontrollerName = "Students";
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
                _parrent = _context.Courses.Find(id);
                ViewBag.ModelList = _repository.GetListByParrent((int)id);
            }
            ViewBag.Parrent = _parrent;

            return View();
        }

        // GET: GroupsController/Create
        [HttpGet]
        public override ActionResult Create(int parrentId = 0)
        { 
            return CreateWithSelectList(0, parrentId);
        }

        // GET: GroupsController/Edit/5
        [HttpGet]
        public override ActionResult Edit(int id, int parrentId = 0)
        {
            return CreateWithSelectList(id, parrentId);
        }

        // GET: GroupsController/Delete/5
        [HttpGet]
        public override ActionResult Delete(int id = 0, int parrentId = 0)
        {
            return CreateWithSelectList(id, parrentId);
        }

        protected override ActionResult CreateWithSelectList(int id = 0, int parrentId = 0)
        {
            _parrent = _context.Courses.Find(parrentId);
            var group = _repository.GetById(id);
            ViewData["ParrentId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewBag.Parrent = _parrent;
            if (group == null)
                return View();
            else
                return View(group);
        }
    }
}
