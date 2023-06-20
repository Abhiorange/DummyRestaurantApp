using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;
using ViewModels.Models;

namespace dataRepository.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public string connections = "Server=LAPTOP-HP9R4JU3\\SQLEXPRESS;Database=RestaurantPOS;Trusted_Connection=True;Encrypt=False";

         public int loginrepo(CompanyLoginVm model)
         {
            int userId = 0;
          
                using (SqlConnection con = new SqlConnection(connections))
                {
                    SqlCommand cmd = new SqlCommand("ValidateCompanyCredential", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", model.name);
                    cmd.Parameters.AddWithValue("@Password", model.password);
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@UserID"].Value != DBNull.Value)
                    {
                        userId = (int)cmd.Parameters["@UserID"].Value;
                    }

                    con.Close();
                    return userId;
                }
            
         }
        public int userloginrepo(UserLoginVm model)//user is logged in
        {
            int userId = 0;

            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("ValidateUserCredentials", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@Password", model.password);
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["@UserID"].Value != DBNull.Value)
                {
                    userId = (int)cmd.Parameters["@UserID"].Value;
                }

                con.Close();
                return userId;
            }
        }
        public int registerrepo(CompanyRegisterVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.companyName);
                cmd.Parameters.AddWithValue("@companyCode", model.companyCode);
                cmd.Parameters.AddWithValue("@password", model.password);
                con.Open();

                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }

        public List<SelectListItem> GetCompanyList()
        {
            List<SelectListItem> companies = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("CompanyData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int companyId = Convert.ToInt32(rdr["id"]);
                    string companyName = rdr["name"].ToString();
                    companies.Add(new SelectListItem { Value = companyId.ToString(), Text = companyName });
                }
                
            }
            return companies;
        }
        public List<Userinfo> GetUsersList()
        {
            List<Userinfo> model = new List<Userinfo>();
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetAllUserInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Userinfo user = new Userinfo
                    {  
                        UserId= Convert.ToInt32(rdr["id"]),
                        name = rdr["UserName"].ToString(),
                        email = rdr["UserEmail"].ToString(),
                        contact = Convert.ToInt64(rdr["UserContact"]),
                        companyname = rdr["UserCompany"].ToString()

                    };

                    model.Add(user);
                }
                con.Close();
            }
            return model;
        } 
        public UserEditVm GetUserById(int id)
        {
            UserEditVm model = null;
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("GetUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    model= new UserEditVm
                    {
                        name = rdr["name"].ToString(),
                        contact = rdr["contact"].ToString(),
                        password = rdr["password"].ToString(),
                        email = rdr["email"].ToString(),
                        companyId = Convert.ToInt32(rdr["companyId"])
                    };
                 
                }
                con.Close();

            }
            return model;
        }
        public int adduser(PostUserRegisterVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("spInsertUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@userCode", model.userCode);
                cmd.Parameters.AddWithValue("@contact", model.contact);
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@companyId", model.companyId);
                cmd.Parameters.AddWithValue("@password", model.password);
                con.Open();

                int i = cmd.ExecuteNonQuery();

                return i;
            }
        }
        public int EditUser(PostUserEditVm model)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("Updateuser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.UserId);
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@contact", model.contact);
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@companyId", model.companyId);
                cmd.Parameters.AddWithValue("@password", model.password);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
        public int deleteUserById(int id)
        {
            using (SqlConnection con = new SqlConnection(connections))
            {
                SqlCommand cmd = new SqlCommand("DeleteUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
    }
}
