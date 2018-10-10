using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web.Controllers
{
    [Authorize(Roles = "Group")]
    public class VacancyController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public VacancyController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserVacancyList(Guid userId)
        {
            var vacancies = await _crudService.Where<Vacancy>(v => v.UserId == userId);
            return Ok(Mapper.Map<ICollection<VacancyModel>>(vacancies));
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateVacancy([FromBody] VacancyDetailModel model)
        //{
        //    var u
        //}
    }
}