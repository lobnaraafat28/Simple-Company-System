using AssignBLL.Interfaces;
using AssignBLL.Rebositories;
using AssignDAL.Models;
using AssignPL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace AssignPL.Controllers
{
	//[Authorize]
	public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentrepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_departmentrepository = departmentrepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            var mappedDeps = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(mappedDeps);
        }
       [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

               await _unitOfWork.DepartmentRepository.Add(mappedDep);
                int count = await _unitOfWork.Complete();

                if (count > 0)
                    TempData["Message"] = "Department is created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(departmentVM);

            }
        }

        public async Task<IActionResult> Details(int? id, string viewName ="Details")
        {
            if (id == null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department == null)
                return NotFound();
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, mappedDep);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                     _unitOfWork.DepartmentRepository.Update(mappedDep);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);        
                }
               
            }
                return View(departmentVM);
            

        }
        public async Task<IActionResult> Delete(int? id)
        {

            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();

            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Delete(mappedDep);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(departmentVM);
            }
        }
    }
}
