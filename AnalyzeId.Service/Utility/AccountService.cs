using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyzeId.Shared;

namespace AnalyzeId.Service
{
    public class AccountService 
    {
        private SqlConnection cnn;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> IsValidUser(string userName,string password)
        {
            GetConnection();
            SqlCommand command = new SqlCommand($"SELECT Id FROM Users_02_SingleUser where account_login_Username='{userName}' and account_login_password='{password}'", cnn);
            var reader = await command.ExecuteReaderAsync();
            string userId = null;
            while (reader.Read())
            {
                userId = reader[0].ToString();
            }
            if (userId!=null)
            {
                cnn.Close();
                return true;
            }
            cnn.Close();
            return false;
        }






        public void Close()
        {
            cnn.Close();
        }
        private SqlConnection GetConnection()
        {
            if (cnn != null && cnn.State != System.Data.ConnectionState.Closed)
            {
                return cnn;
            }
            string connetionString = @"Data Source=icoreprod.cpleldjvbku8.ap-southeast-2.rds.amazonaws.com;Initial Catalog=idv_administrator;User ID=J64Q6N9HTCA;Password=NRNQUFX6S56";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            return cnn;
        }
    }
}
