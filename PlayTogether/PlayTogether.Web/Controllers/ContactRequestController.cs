using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Infrastructure.Models;
using PlayTogether.Web.Models.ContactRequest;

namespace PlayTogether.Web.Controllers
{
    [Authorize]
    public class ContactRequestController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly WebSession _webSession;

        public ContactRequestController(ISimpleCRUDService crudService, WebSession webSession)
        {
            _crudService = crudService;
            _webSession = webSession;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetUserContactRequests()
        {
            var contactRequests = await _crudService.Where<ContactRequest>(v => v.ToUserId == _webSession.UserId);
            return Ok(Mapper.Map<ICollection<ContactRequestModel>>(contactRequests));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> IsContactRequestSent(Guid toUserId)
        {
            var contactRequest =
                await _crudService.Find<ContactRequest>(v => v.UserId == _webSession.UserId && v.ToUserId == toUserId);
            return Ok(contactRequest != null);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> IsContactRequestApproved(Guid toUserId)
        {
            var contactRequest =
                await _crudService.Find<ContactRequest>(v => v.UserId == _webSession.UserId && v.ToUserId == toUserId);
            return Ok(contactRequest != null && contactRequest.Status == ContactRequestStatus.Approved);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> SendRequest(SendContactRequestModel model)
        {
            var contactRequest =
               await _crudService.Find<ContactRequest>(v => v.UserId == _webSession.UserId && v.ToUserId == model.ToUserId);
            if (contactRequest != null)
            {
                return BadRequest(ValidationResultMessages.InvalidModelState);
            }

            await _crudService.Create<ContactRequest>(new ContactRequest()
            {
                Status = ContactRequestStatus.Open,
                ToUserId = model.ToUserId,
                UserId = _webSession.UserId
            });
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> ManageRequest(ContactRequestModel model)
        {
            await _crudService.Update<ContactRequestModel, ContactRequest>(model.Id, new ContactRequestModel(),
                (to, from) =>
                {
                    to.Status = model.IsApproved ? ContactRequestStatus.Approved : ContactRequestStatus.Rejected;
                });
            return Ok();
        }
    }
}