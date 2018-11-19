using System;
using System.Collections.Generic;
using System.Linq;
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
            var contactRequests =
                (await _crudService.Where<ContactRequest>(v => v.ToUserId == _webSession.UserId 
                                                               || v.UserId == _webSession.UserId)).OrderBy(
                    c => c.Created);

            var mappedContactRequests = contactRequests.Select(c =>
            {
                var mappedContact = Mapper.Map<ContactRequestModel>(c);
                mappedContact.IsUserOwner = mappedContact.UserId == _webSession.UserId;
                return mappedContact;
            });
            return Ok(mappedContactRequests);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetUserNewContactRequestCount()
        {
            var contactRequests = await _crudService.Where<ContactRequest>(v => v.ToUserId == _webSession.UserId 
                && v.Status == ContactRequestStatus.Open);
            return Ok(contactRequests.Count);
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
                await _crudService.Find<ContactRequest>(v =>
                    (v.UserId == _webSession.UserId && v.ToUserId == toUserId) ||
                    (v.UserId == toUserId && v.ToUserId == _webSession.UserId));
            return Ok(contactRequest != null && contactRequest.Status == ContactRequestStatus.Approved);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> SendRequest([FromBody] SendContactRequestModel model)
        {
            var contactRequest =
               await _crudService.Find<ContactRequest>(v => v.UserId == _webSession.UserId 
                && v.ToUserId == model.ToUserId);
            if (contactRequest != null)
            {
                return BadRequest(ValidationResultMessages.InvalidModelState);
            }

            if (model.ToUserId == _webSession.UserId)
            {
                return BadRequest(ValidationResultMessages.InvalidModelState);
            }

            await _crudService.Create<ContactRequest>(new ContactRequest()
            {
                Status = ContactRequestStatus.Open,
                ToUserId = model.ToUserId,
                UserId = _webSession.UserId,
                Created = DateTime.UtcNow
            });
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> ManageRequest([FromBody] ManageContactRequestModel model)
        {
            await _crudService.Update<ContactRequestModel, ContactRequest>(model.Id, new ContactRequestModel(),
                (to, from) =>
                {
                    if (to.ToUserId == _webSession.UserId && to.Status == ContactRequestStatus.Open)
                    {
                        to.Status = model.IsApproved ? ContactRequestStatus.Approved : ContactRequestStatus.Rejected;
                        var requestFromUser = to.User.ContactRequests.FirstOrDefault(c => c.UserId == _webSession.UserId);
                        if (requestFromUser != null)
                        {
                            requestFromUser.Status = to.Status;
                        }
                    }
                });
            return Ok();
        }
    }
}