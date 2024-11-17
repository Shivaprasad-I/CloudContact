using ModelLayer.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseInterface
{
    public interface IContactsDB
    {
        Tuple<IEnumerable<Contact>, int> GetAllContactList(GetAllContactListRequest request);
        int InsertContact(Contact request);
        int UpdateContact(Contact request);
        int DeleteContact(DeleteContactRequest request);
    }
}
