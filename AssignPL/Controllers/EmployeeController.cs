using AssignBLL.Interfaces;
using AssignDAL.Models;
using AssignPL.Helpers;
using AssignPL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssignPL.Controllers
{
	public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeerepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper )
        {
            //_employeerepository = employeerepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAll();
            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmps);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
             //ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                //var mappedEmp = new Employee()
                //{
                //    Name = employeeVM.Name,
                //    Address = employeeVM.Address,
                //    Salary = employeeVM.Salary,
                //    Email = employeeVM.Email,
                //    Age = employeeVM.Age,
                //    DepartmentId = employeeVM.DepartmentId,
                //    IsActive = employeeVM.IsActive,
                //    HireDate = employeeVM.HireDate,
                //    PhoneNumber = employeeVM.PhoneNumber
                //};
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
               
                  await  _unitOfWork.EmployeeRepository.Add(mappedEmp);
               int count = await _unitOfWork.Complete();

                if (count > 0)
                {
                    TempData["message"] = "employee is added successfully";
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(employeeVM);

            }
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee == null)
                return NotFound();
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(viewName, mappedEmp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    await _unitOfWork.Complete();

                   
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVM);


        }
        public async Task<IActionResult> Delete(int? id)
        {

            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                 int count = await _unitOfWork.Complete();
                if (count > 0)
                    DocumentSettings.DeleteFile(mappedEmp.ImageName , "images");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        }
    }
}
