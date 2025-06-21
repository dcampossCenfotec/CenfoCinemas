using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager : BaseManager
    {
        /*
         * Metodo para la creacion de un usuario
         * Valida que el usuario sea mayor de 18 años
         * Valida que el código de usuario está disponible
         * Valida que el correo electrónico no esté registrado
         * Envia un correo electrónico de bienvenida al usuario
         */

        public void Create(User user)
        {
            try
            {
                // Validar que el usuario sea mayor de 18 años
                if (IsOver18(user))
                {
                    var uCrud = new UsersCrudFactory();

                    // Consultamos en la base de datos si el código de usuario ya existe
                    var uExist = uCrud.RetrieveByUserCode<User>(user);

                    if (uExist == null)
                    {
                        // Consultamos en la base de datos si el correo electrónico ya existe
                        uExist = uCrud.RetrieveByEmail<User>(user);

                        if (uExist == null)
                        {
                            uCrud.Create(user);
                            //Enviar correo electrónico de bienvenida al usuario                            
                            var emailManager = new EmailManager();
                            emailManager.SendWelcomeEmail(user.Email, user.Name).GetAwaiter().GetResult();
                        }
                        else
                        {
                            throw new Exception("El correo electrónico ya está registrado. Por favor, utilice otro correo.");
                        }
                    }
                    else
                    {
                        throw new Exception("El código de usuario ya existe. Por favor, elija otro código.");
                    }
                }
                else
                {
                    throw new Exception("El usuario no cumple con la edad mínima. Debe ser mayor de 18 años");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        private bool IsOver18(User user)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - user.Birth.Year;

            if (user.Birth > currentDate.AddYears(-age))
            {
                age--;
            }

            return age >= 18;
        }

        public List<User> RetrieveAll()
        {
            var uCrud = new UsersCrudFactory();
            return uCrud.RetrieveAll<User>();
        }

        public User RetrieveByUserCode(User user)
        {
            var uCrud = new UsersCrudFactory();
            return uCrud.RetrieveByUserCode<User>(user);
        }

        public User RetrieveByEmail(User user)
        {
            var uCrud = new UsersCrudFactory();
            return uCrud.RetrieveByEmail<User>(user);
        }

        public User Update(User user)
        {
            try
            {
                var uCrud = new UsersCrudFactory();
                var uExist = uCrud.RetrieveById<User>(user.Id);
                if (uExist != null)
                {
                    uCrud.Update(user);
                    return user;
                }
                else
                {
                    throw new Exception("El usuario no existe en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
                return null; // En caso de error, retornamos null
            }
        }

        public User Delete(User user)
        {
            try
            {
                var uCrud = new UsersCrudFactory();
                var uExist = uCrud.RetrieveById<User>(user.Id);
                if (uExist != null)
                {
                    uCrud.Delete(user);
                    return user;
                }
                else
                {
                    throw new Exception("El usuario no existe en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
                return null; // En caso de error, retornamos null
            }
        }
    }
}
