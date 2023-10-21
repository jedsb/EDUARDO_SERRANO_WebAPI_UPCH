using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI_UPCH.Config;
using WebAPI_UPCH.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace WebAPI_UPCH.Data
{
    public class UserData
    {
       public List<User> GetUsers()
       {
            var lista = new List<User>();
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("GetUsers", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var user = new User();
                            user.ID_USUARIO = reader.GetInt32(reader.GetOrdinal("ID_USUARIO"));
                            user.NOMBRES = reader.GetString(reader.GetOrdinal("NOMBRES"));
                            user.CORREO = reader.GetString(reader.GetOrdinal("CORREO"));
                            user.USUARIO = reader.GetString(reader.GetOrdinal("USUARIO"));
                            user.CLAVE = reader.GetString(reader.GetOrdinal("CLAVE"));
                            user.ESTADO = reader.GetInt32(reader.GetOrdinal("ESTADO"));
                            lista.Add(user);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lista.Add(new User { ID_USUARIO = 0, USUARIO = "Error", NOMBRES = "Error: " + ex.Message });
            }
                      
            return lista;
       }

        public User GetUser(int id)
        {
            User user = null;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("GetUser", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID_USUARIO", id);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            user = new User
                            {
                                ID_USUARIO = reader.GetInt32(reader.GetOrdinal("ID_USUARIO")),
                                NOMBRES = reader.GetString(reader.GetOrdinal("NOMBRES")),
                                CORREO = reader.GetString(reader.GetOrdinal("CORREO")),
                                USUARIO = reader.GetString(reader.GetOrdinal("USUARIO")),
                                CLAVE = reader.GetString(reader.GetOrdinal("CLAVE")),
                                ESTADO = reader.GetInt32(reader.GetOrdinal("ESTADO")),
                            };
                         
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                user = new User
                {
                    ID_USUARIO = 0,
                    NOMBRES = "Error",
                    USUARIO = ex.Message
                };
            }

            return user;
        }

        public int RegisterUser(User user)
        {
            int newUserId = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("RegisterUser", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NOMBRES", user.NOMBRES);
                        command.Parameters.AddWithValue("@CORREO", user.CORREO);
                        command.Parameters.AddWithValue("@USUARIO", user.USUARIO);
                        command.Parameters.AddWithValue("@CLAVE", user.CLAVE);
                        SqlParameter newUserIdParam = new SqlParameter("@ID_USUARIO", SqlDbType.Int);
                        newUserIdParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(newUserIdParam);
                        command.ExecuteNonQuery();
                        newUserId = (int)newUserIdParam.Value;
                    }
                    con.Close();
                    return newUserId;
                }
             }
            catch (Exception ex)
            {
                return newUserId;
            }
        }

        public int UpdateUser(User user)
        {
            int result = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("UpdateUser", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NOMBRES", user.NOMBRES);
                        command.Parameters.AddWithValue("@CORREO", user.CORREO);
                        command.Parameters.AddWithValue("@USUARIO", user.USUARIO);
                        command.Parameters.AddWithValue("@CLAVE", user.CLAVE);
                        command.Parameters.AddWithValue("@ID_USUARIO", user.ID_USUARIO);
                        SqlParameter resultUpd = new SqlParameter("@RESULT", SqlDbType.Int);
                        resultUpd.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultUpd);
                        command.ExecuteNonQuery();
                        result = (int)resultUpd.Value;
                    }
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int DeleteUser(int id)
        {
            int result = 0;
            try
            {
                using (var con = new SqlConnection(Conexion.connection))
                {
                    con.Open();
                    using (var command = new SqlCommand("DeleteUser", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID_USUARIO", id);                        
                        SqlParameter resultdelete = new SqlParameter("@RESULT", SqlDbType.Int);
                        resultdelete.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultdelete);
                        command.ExecuteNonQuery();
                        result = (int)resultdelete.Value;
                    }
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
