using DataBaseInterface;
using Microsoft.Extensions.Configuration;
using ModelLayer.Contact;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnect
{
    public class ContactsDB : Base, IContactsDB
    {
        private string getAllContacts = @"SELECT * FROM tblContacts WHERE UserId = @UserId";
        private string insertContact = @"INSERT INTO tblContacts (
                                            FirstName,
                                            LastName,
                                            Email,
                                            PhoneNumber,
                                            Company,
                                            JobTitle,
                                            OtherDetails,
                                            UserId
                                            )
                                            VALUES
                                            (
                                            @FirstName,
                                            @LastName,
                                            @Email,
                                            @PhoneNumber,
                                            @Company,
                                            @JobTitle,
                                            @JobTitle,
                                            @UserId
                                            )
                                            SELECT SCOPE_IDENTITY();";
        private string updateContact = @"UPDATE tblContacts SET
                                            FirstName = @FirstName,
                                            LastName = @LastName,
                                            Email = @Email,
                                            PhoneNumber = @PhoneNumber,
                                            Company = @Company,
                                            JobTitle = @JobTitle,
                                            OtherDetails = @OtherDetails
                                            WHERE Id = @Id AND UserId = @UserId";
        private string softDeleteContact = @"UPDATE tblContacts SET DeletedSince = GETDATE() WHERE Id = @Id AND UserId = @UserID";
        private string hardDeleteContact = @"DELETE FROM tblContacts WHERE Id = @Id AND UserId = @UserID";

        public ContactsDB(IConfiguration configuration) : base(configuration.GetConnectionString("DefaultConnection"))
        {

        }
        public Tuple<IEnumerable<Contact>,int> GetAllContactList(GetAllContactListRequest request)
        {
            
            if (string.IsNullOrEmpty(request.SortKey))
            {
                request.SortKey = "FirstName";
            }
            if (string.IsNullOrEmpty(request.SortOrder))
            {
                request.SortOrder = "ASC";
            }
            string startCTE = ";WITH Contacts AS ( ";
            string endCTE = " )SELECT * FROM Contacts ";
            string queryPagination = @" WHERE RowId between(((@PageNo -1) *@PageSize )+1) and(@PageNo * @PageSize); ";

            string selectQuery = @"SELECT DISTINCT
                                   ROW_NUMBER() OVER(ORDER BY " + request.SortKey + " " + request.SortOrder + $@" ) as RowId,
                                            Id,
                                            FirstName,
                                            LastName,
                                            Email,
                                            PhoneNumber,
                                            Company,
                                            JobTitle,
                                            OtherDetails,
                                            UserId
                                            ";
            string countSelectQuery = @"SELECT COUNT(*) ";
            string tableInnerJoin = " FROM tblContacts ";
            string conditions = " WHERE UserId = @UserId ";
            string deleteFilter;

            if ((bool)request.GetFromBin)
            {
                deleteFilter = " AND DeletedSince IS NOT NULL ";
            }
            else
            {
                deleteFilter = " AND DeletedSince IS NULL ";
            }

            StringBuilder contactsQuery = new StringBuilder();

            contactsQuery.Append(startCTE);
            contactsQuery.Append(selectQuery);
            contactsQuery.Append(tableInnerJoin); 
            contactsQuery.Append(conditions);
            contactsQuery.Append(deleteFilter);
            contactsQuery.Append(endCTE); 
            contactsQuery.Append(queryPagination);

            var sql1 = contactsQuery.ToString();
            var Contacts =  Execute<Contact>(sql1,
            new
            {
                UserId = request.UserId,
                PageNo = request.PageNo,
                PageSize = request.PageSize
            }, System.Data.CommandType.Text);

            StringBuilder totalCountQuery = new StringBuilder();
            totalCountQuery.Append(countSelectQuery);
            totalCountQuery.Append(tableInnerJoin);
            totalCountQuery.Append(conditions);
            totalCountQuery.Append(deleteFilter);

            var sql2 = totalCountQuery.ToString();
            var TotalFilteredCount = Execute<int>(sql2,
            new
            {
                UserId = request.UserId,
                PageNo = request.PageNo,
                PageSize = request.PageSize
            }, System.Data.CommandType.Text).FirstOrDefault();

            return Tuple.Create(Contacts, TotalFilteredCount);
        }

        public int InsertContact(Contact request)
        {
            try
            {
                return Execute<int>(insertContact,
                    new
                    {
                        UserId = request.UserId,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Company = request.Company,
                        JobTitle = request.JobTitle,
                        OtherDetails = request.OtherDetails

                    }, System.Data.CommandType.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UpdateContact(Contact request)
        {
            try
            {
                Execute<int>(updateContact,
                    new
                    {
                        UserId = request.UserId,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Company = request.Company,
                        JobTitle = request.JobTitle,
                        OtherDetails = request.OtherDetails,
                        Id = request.Id
                    }, System.Data.CommandType.Text).FirstOrDefault();
                return request.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int DeleteContact(DeleteContactRequest request)
        {
            var sqlQuery = request.IsSoftDelete ? softDeleteContact : hardDeleteContact;

            try
            {
                Execute<int>(sqlQuery,
                    new
                    {
                        Id = request.Id,
                        UserID = request.UserId,
                    }, System.Data.CommandType.Text).FirstOrDefault();
                return request.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
