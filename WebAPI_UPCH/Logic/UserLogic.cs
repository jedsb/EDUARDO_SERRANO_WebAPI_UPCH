using WebAPI_UPCH.Data;
using WebAPI_UPCH.Entities;

namespace WebAPI_UPCH.Logic
{
    public class UserLogic
    {
        private UserData userData;
        public UserLogic()
        {
            userData = new UserData();
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            users = userData.GetUsers();
            return users;
        }
        public User GetUser(int id)
        {
            User user = new User();
            if (id > 0)
            {
                user = userData.GetUser(id);
                if (user == null)
                {
                    user = new User
                    {
                        ID_USUARIO = 99999,
                        NOMBRES = "NO SE ENCONTRÓ NOMBRES CON ID BUSCADO",
                        CORREO = "NO SE ENCONTRÓ CORREO CON ID BUSCADO",
                        USUARIO = "NO SE ENCONTRÓ USUARIO CON ID BUSCADO"
                    };
                }
            }
            else
            {               
                user.NOMBRES = "NO EXISTE NOMBRE";
                user.CORREO = "NO EXISTE CORREO";
                user.USUARIO = "NO EXISTE USUARIO";
            }
            return user;
        }

        public int RegisterUser(User user)
        {
            int newUserId = 0;
            newUserId = userData.RegisterUser(user);
            return newUserId;
        }

        public int UpdateUser(User user)
        {
            int newUserId = 0;
            newUserId = userData.UpdateUser(user);
            return newUserId;
        }

        public int DeleteUser(int id)
        {
            int newUserId = 0;
            newUserId = userData.DeleteUser(id);
            return newUserId;
        }
    }
}
