﻿//using Microsoft.Data.SqlClient;

//namespace finalfreelancing.Helpers
//{
//    public class CommonHelper
//    {

//        private IConfiguration _config;

//        public CommonHelper(IConfiguration config)
//        {
//            _config = config;
//        }
//        public bool UserAlreadyExists(string query)
//        {
//            bool flag = false;
//            string connectionString = _config["ConnectionStrings:DefaultConnection"];
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string sql = query;

//                SqlCommand command = new SqlCommand(sql, connection);
//                SqlDataReader rd = command.ExecuteReader();
//                if (rd.HasRows)

//                {
//                    flag = true;
//                }
//                connection.Close();
//            }
//            return flag;
//        }



//        public int DMLTransaction(string Query)
//        {
//            int Result;
//            string connectionString = _config["ConnectionStrings:DefaultConnection"];
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {

//                connection.Open();
//                string sql = Query;
//                SqlCommand command = new SqlCommand(sql, connection);
//                Result = command.ExecuteNonQuery();
//                connection.Close();
//            }
//            return Result;
//        }
//    }
//}