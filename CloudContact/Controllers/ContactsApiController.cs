using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Contact;
using Shared;
using System.Net;

namespace CloudContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsApiController : Controller
    {
        private readonly IContactsLogic contactsLogic;
        public ContactsApiController(IContactsLogic _contactsLogic)
        {
            contactsLogic = _contactsLogic;
        }
        [HttpPost]
        [Route(Routes.GetAllContacts)]
        public ActionResult<GetAllContactListResponse> GetAllContacts(GetAllContactListRequest request)
        {
            try
            {
                var response = contactsLogic.GetAllContactList(request);
                return StatusCode(response.ErrorCode, response);
            }
            catch (Exception ex)
            {
                //Need to implement logging
                throw;
            }
        }

        [HttpPost]
        [Route(Routes.AddEditContact)]
        public ActionResult<AddEditResponse> AddEditContacts(AddEditContentRequest request)
        {
            try
            {
                var response = contactsLogic.AddEditContact(request);
                return StatusCode(response.ErrorCode, response);
            }
            catch (Exception ex)
            {
                //Need to implement logging
                throw;
            }
        }
        [HttpPost]
        [Route(Routes.DeleteContact)]
        public ActionResult<DeleteContactResponse> DeleteContact(DeleteContactRequest request)
        {
            try
            {
                var response = contactsLogic.DeleteContact(request);
                return StatusCode(response.ErrorCode, response);
            }
            catch (Exception ex)
            {
                //Need to implement logging
                throw;
            }
        }
    }
}
