using DataBaseInterface;
using LogicInterface;
using ModelLayer.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ContactsLogicComponent : IContactsLogic
    {
        private readonly IContactsDB contactsDb;
        public ContactsLogicComponent(IContactsDB _contactsDb)
        {
            contactsDb = _contactsDb;
        }
        public GetAllContactListResponse GetAllContactList(GetAllContactListRequest request)
        {
            var contactsList = contactsDb.GetAllContactList(request);
            var response = new GetAllContactListResponse()
            {
                Contacts = contactsList.Item1,
                TotalFilterCount = contactsList.Item2,
                Success = true,
                ErrorCode = 200,
                ErrorMessage = string.Empty
            };
            return response;
        }

        public AddEditResponse AddEditContact(AddEditContentRequest request)
        {
            int Id = 0;
            if (request.ContactDetails != null)
            {
                if (request.ContactDetails.Id > 0)
                {
                    Id =  contactsDb.UpdateContact(request.ContactDetails);
                }
                else
                {
                    Id = contactsDb.InsertContact(request.ContactDetails);
                }
            }
            return new AddEditResponse()
            {
                ContactId = Id,
                Success = true,
                ErrorCode = 200,
                ErrorMessage = string.Empty
            };
        }
        public DeleteContactResponse DeleteContact(DeleteContactRequest request)
        {
            int Id = 0;
            if (request.Id != null && request.Id > 0)
            {
                Id = contactsDb.DeleteContact(request);
            }
            return new DeleteContactResponse()
            {
                Id = Id,
                ErrorCode = 200,
                Success = true,
                ErrorMessage = string.Empty
            };
        }
    }
}
