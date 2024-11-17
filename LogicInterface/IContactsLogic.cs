using ModelLayer.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface
{
    public interface IContactsLogic
    {
        GetAllContactListResponse GetAllContactList(GetAllContactListRequest request);
        AddEditResponse AddEditContact(AddEditContentRequest request);
        DeleteContactResponse DeleteContact(DeleteContactRequest request);
    }
}
