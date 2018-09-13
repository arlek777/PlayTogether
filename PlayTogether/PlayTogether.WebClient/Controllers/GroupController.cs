using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;

namespace PlayTogether.WebClient.Controllers
{
    public class GroupController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public GroupController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetGroups(Guid userId)
        {
            return Ok(await _crudService.Where<UserToGroup>(p => p.UserId == userId));
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> CreateGroup(Group model)
        {
            var group = await _crudService.CreateOrUpdate<Group>(model);
            return Ok(group);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> RemoveGroup(Guid id)
        {
            await _crudService.RemoveById<Group>(id);
            return Ok();
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateGroup(Group model)
        {
            await _crudService.CreateOrUpdate<Group>(model, (to, from) =>
            {
                to.Profile = from.Profile;
            });
            return Ok();
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> AddUserTo(Guid groupId, Guid userId)
        {
            var userGroup = await _crudService.CreateOrUpdate<UserToGroup>(new UserToGroup()
            {
                GroupId = groupId,
                UserId = userId
            });
            return Ok(userGroup);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> RemoveUserFrom(Guid id)
        {
            await _crudService.RemoveById<UserToGroup>(id);
            return Ok();
        }
    }
}