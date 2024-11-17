using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace Shared
{
    public class Base
    {
        private readonly string _connectionString;

        public Base(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> Execute<T>(string query, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<T>(query, parameters, commandType: commandType);
            }
        }
    }
}


/*CREATE TABLE tblContacts (
Id INT IDENTITY (1, 1) NOT NULL,
FirstName VARCHAR(100),
LastName VARCHAR(100),
Email VARCHAR(100),
PhoneNumber BIGINT,
Company VARCHAR(200),
JobTitle VARCHAR(200),
OtherDetails VARCHAR(500),
UserId INT,
DeletedSince DATETIME DEFAULT NULL
)
*/