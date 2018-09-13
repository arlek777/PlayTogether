using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;

namespace PlayTogether.WebClient.Controllers
{
    public class SearchRequestController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public SearchRequestController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetGroupSearchRequests()
        {
            var requests = await _crudService.Where<SearchRequest>(
                s => s.Status == SearchRequestStatus.Active && s.UserId.HasValue);
            return Ok(requests);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetUserSearchRequests()
        {
            var requests = await _crudService.Where<SearchRequest>(
                s => s.Status == SearchRequestStatus.Active && s.GroupId.HasValue);
            return Ok(requests);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> CreateRequest(SearchRequest model)
        {
            model.Date = DateTime.Now;
            var group = await _crudService.CreateOrUpdate<SearchRequest>(model);
            return Ok(group);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> RemoveRequest(Guid id)
        {
            await _crudService.RemoveById<SearchRequest>(id);
            return Ok();
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateRequest(SearchRequest model)
        {
            await _crudService.CreateOrUpdate<SearchRequest>(model, (to, from) =>
            {
                to.Status = from.Status;
                to.Description = from.Description;
                to.Title = from.Title;
            });
            return Ok();
        }
    }
}