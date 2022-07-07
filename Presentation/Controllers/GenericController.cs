using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Controllers
{
    public abstract class GenericController<T> : Controller where T : class
    {
        protected delegate void DataBaseAction(T model);

        protected readonly IRepository<T> _repository;
        protected readonly UniversityDbContext _context;
        protected Record _parrent = null;

        protected string _controllerName = String.Empty;
        protected string _subjectСontrollerName = String.Empty;

        public GenericController(IRepository<T> repository)
        {
            _repository = repository;
            _context = repository.Context;
        }

        // GET: CoursesController
        [HttpGet]
        public virtual ActionResult Index(int? id = null)
        {
            ViewBag.ModelList = _repository.GetList();
            ViewBag.Parrent = _parrent;
            return View();
        }

        [HttpGet]
        public virtual ActionResult ViewListByParrent(int id)
        {
            if (_subjectСontrollerName == String.Empty)
                throw new NotImplementedException("Элемент не имеет подчиненных элементов");
            else
                return RedirectToAction("Index", _subjectСontrollerName, new { id });
        }

        // GET: CoursesController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = _repository.GetById(id);
            return View(model);
        }

        // GET: CoursesController/Create
        [HttpGet]
        public virtual ActionResult Create(int parrentId = 0)
        {
            ViewBag.Parrent = _parrent;
            return View();
        }


        // POST: CoursesController/Create
        [HttpPost]
        public ActionResult Create(T model, int parrentId = 0)
        {
            //if(model is Group group)
            //{
            //    group.Students = new List<Student>();
            //}
            //TODO: Разобраться, почему для моделей Group, Student не проходит проверка
            if (ModelState.IsValid)
            {
                _repository.WriteIntoDataBase(model, dataBaseAction => _repository.Add(model));
                return RedirectToAction("Index", _controllerName,
                    (parrentId == 0 ? null : new { id = parrentId }));
            }
            //ViewBag.ErrorMessage = "Некорректный ввод";
            //return View(model);
            return View("~/Views/Shared/Error.cshtml");
        }

        // GET: CoursesController/Edit/5
        [HttpGet]
        public virtual ActionResult Edit(int id, int parrentId = 0)
        {
            ViewBag.Parrent = _parrent;
            var model = _repository.GetById(id);
            return View(model);
        }

        // POST: CoursesController/Edit/5
        [HttpPost]
        public ActionResult Edit(T model, int parrentId = 0)
        {
            if (ModelState.IsValid)
            {
                _repository.WriteIntoDataBase(model, dataBaseAction => _repository.Edit(model));
                return RedirectToAction("Index", _controllerName,
                    (parrentId == 0 ? null : new { id = parrentId }));
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        // GET: CoursesController/Delete/5
        [HttpGet]
        public virtual ActionResult Delete(int id, int parrentId = 0)
        {
            ViewBag.Parrent = _parrent;
            var model = _repository.GetById(id);
            return View(model);
        }

        // POST: CoursesController/Delete
        [HttpPost]
        public ActionResult Delete(T model, int parrentId = 0)
        {
            _repository.WriteIntoDataBase(model, dataBaseAction => _repository.Delete(model));
            return RedirectToAction("Index", _controllerName,
                (parrentId == 0 ? null : new { id = parrentId }));
        }

        protected abstract ActionResult CreateWithSelectList(int id = 0, int parrentId = 0);

    }
}
