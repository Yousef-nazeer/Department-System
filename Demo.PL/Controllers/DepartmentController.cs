using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepaatrmentRepository _departmentRepository;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        } 
        public async Task< IActionResult> Index()
        {
            ViewData["Message"] = "HEllo from Controller";
            ViewBag.Bozo = "HEllo from ViewBag";
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Department department)
        {
            if (ModelState.IsValid) //Server side Validation
                {
                await _unitOfWork.DepartmentRepository.Add(department);
                TempData["Message"] = "Department is Created Successfully";
                return RedirectToAction(nameof(Index)) ;
            }
            return View(department);
        }


        public async Task <IActionResult> Details( int? id,string ViewName ="Details")
        { 
            if (id==null)
                return NotFound();
            
            var department = await _unitOfWork.DepartmentRepository.Get(id.Value);
            
            if(department == null)
                return NotFound();

            return View(ViewName,department);
            
        }
        public async Task <IActionResult> Edit( int? id)
        {
            return await Details(id,"Edit");
        }


        [HttpPost]
     
        public async Task< IActionResult> Edit([FromRoute] int id,Department department)
        {
            if(id != department.Id)
                return BadRequest();
            if (ModelState.IsValid) //Server side Validation
            {
                try
                {
                  await   _unitOfWork.DepartmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));

                }
                catch(Exception ex) 
                {


                    //1. Log Exception
                    //2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(department);
                     

                }
                    
            }
            else { return View(department); }

        
        }
        public async Task< IActionResult> Delete(int? id)
        {
            //if (id == null)
            //    return BadRequest();
            //var depatment = _unitOfWork.DepartmentRepository.Get(id.Value);
            //if(depatment== null)
            //    return BadRequest();
            //return View(depatment);

            return await Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete ([FromRoute] int id,Department department)
        {
            if(id!=department.Id)
                return BadRequest();
            try
            {
                await _unitOfWork.DepartmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message); 
                return View(department);

            }

        }


    }
}
