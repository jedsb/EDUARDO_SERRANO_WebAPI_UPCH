using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_UPCH.Entities;
using WebAPI_UPCH.Logic;

namespace WebAPI_UPCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserLogic userLogic = new UserLogic();
        #region LISTA USUARIOS
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            List<User> users = new List<User>();
            users = userLogic.GetUsers();
            if (users.Count > 0)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(new { Message = "No se puede obtener la lista de usuarios" });
            }
            
        }
        #endregion

        #region USUARIO POR ID
        [HttpGet("GetUser")]
        public IActionResult GetUser (int id)
        {
            User user = new User();
            user = userLogic.GetUser(id);
            if (user.ID_USUARIO == 99999)
            {
                return BadRequest(new { Message = "No existe usuario" });
            }           
            return Ok(user);
        }
        #endregion


        #region REGISTRAR USUARIO
        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            int newUserId = 0;
            if (user != null)
            {
                newUserId = userLogic.RegisterUser(user);

                if (newUserId == 0)
                {
                    return BadRequest(new { Message = "No se pudo registrar el usuario" });
                   
                }                
                return Ok(new { Message = "Usuario registrado correctamente", NewUserId = newUserId });
            }
            else
            {
                return BadRequest(new { Message = "Los datos del usuario son inválidos" });
            }

        }
        #endregion

        #region ACTUALIZAR USUARIO
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            int result = 0;
            if (user.ID_USUARIO == 0)
            {
                return BadRequest(new { Message = "El id no puede ser 0" });
            }
            result = userLogic.UpdateUser(user);
            if (result == 1)
            {
                return Ok(new { Message = "Usuario actualizado correctamente" });
            }
            else
            {
                return BadRequest(new { Message = "No se pudo actualizar el usuario" });
            }
            
        }
        #endregion

        #region ELIMINAR USUARIO
        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            int result = 0;
            if (id == 0)
            {
                return BadRequest(new { Message = "No se pudo actualizar el usuario, ingresar un ID correcto" });
            }
            else
            {
                result = userLogic.DeleteUser(id);
                if (result == 1)
                {
                    return Ok(new { Message = "Usuario eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { Message = "No se pudo eliminar el usuario" });
                }
            }
                        
        }
        #endregion

    }
}
