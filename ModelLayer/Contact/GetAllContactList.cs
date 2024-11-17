using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Contact
{
    public class GetAllContactListRequest 
    {
        public Int64 UserId { get; set; }
        public IEnumerable<Int64>? ContactIds { get; set;}
        public string? SearchTerm { get; set; }
        public int? PageNo { get; set; } = 1;
        public int? PageSize { get; set; } = int.MaxValue;
        public string? SortKey { get; set; }
        public string? SortOrder { get; set; }
        public bool? GetFromBin { get; set; } = false;

    }
    public class GetAllContactListResponse : ErrorModel
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public int TotalFilterCount { get; set; }
    }
}
