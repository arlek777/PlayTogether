using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Domain.MasterValues;
using PlayTogether.Web.Models;

namespace PlayTogether.Web.Controllers
{
    [Authorize]
    public class MasterValuesController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public MasterValuesController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [ResponseCache(Duration = 4200)]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Get(MasterValueTypes type)
        {
            switch (type)
            {
                case MasterValueTypes.MusicGenres:
                    var genres = await _crudService.GetAll<MusicGenre>();
                    return Ok(genres.ToList());

                case MasterValueTypes.MusicianRoles:
                    var roles = await _crudService.GetAll<MusicianRole>();
                    return Ok(roles.ToList());

                case MasterValueTypes.WorkTypes:
                    var workTypes = await _crudService.GetAll<WorkType>();
                    return Ok(workTypes.ToList());

                case MasterValueTypes.Cities:
                    var cities = await _crudService.GetAll<City>();
                    return Ok(cities.ToList());

                case MasterValueTypes.ContactTypes:
                    var types = await _crudService.GetAll<ContactType>();
                    return Ok(types.ToList());
            }

            return NotFound();
        }
    }
}