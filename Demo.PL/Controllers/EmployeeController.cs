using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper _mapper)
        {
            _unitOfWork = unitOfWork;
            mapper = _mapper;
        }
        public async Task<IActionResult> IndexAsync(string Searchvalue)
        {
            var Employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(Searchvalue))
                Employees = await _unitOfWork.EmployeeRepository.GetAll();
            else
                Employees = _unitOfWork.EmployeeRepository.SearchEmployeesByName(Searchvalue);

            var mappedEmps = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(mappedEmps);
        }

        public async Task <IActionResult> Create()
        {
            ViewBag.Departments =await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
  
        public async Task< IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) //Server side Validation
            {
                 employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "imgs");
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
               await _unitOfWork.EmployeeRepository.Add(mappedEmp);
                return RedirectToAction(nameof(IndexAsync));
            }
            return View(employeeVM);
        }

        public async Task< IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();

            var employee =await _unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee == null)
                return NotFound();

            var mappedEmployee = mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, mappedEmployee);

        }
        public async Task< IActionResult> Edit(int? id)

        {
            ViewBag.Departments =await _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");
        }


        [HttpPost]
        public async Task< IActionResult> Edit(int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid) //Server side Validation
            {

                try
                {

                    string[] files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\imgs"));
                    if (Array.Exists(files, file => file.EndsWith(employeeVM.ImageName)))
                        DocumentSettings.DeleteFile(employeeVM.ImageName, "imgs");

                    employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "imgs");

                    var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                   await  _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    return RedirectToAction(nameof(IndexAsync));

                }
                catch (Exception ex)
                {


                    //1. Log Exception
                    //2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeVM);


                }

            }
            else { return View(employeeVM); }


        }
        public async Task< IActionResult> Delete(int? id)
        {
            //if (id == null)
            //    return BadRequest();
            //var depatment = _unitOfWork.EmployeeRepository.Get(id.Value);
            //if(depatment== null)
            //    return BadRequest();
            //return View(depatment);

            return await Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
              int count=  await _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                if (count > 0)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "imgs");
                return RedirectToAction(nameof(IndexAsync));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);

            }

        }

    }
}
