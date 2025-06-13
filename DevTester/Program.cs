using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Xml.Linq;

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        var sqlOperation = new SqlOperation();
//        sqlOperation.ProcedureName = "CRE_USER_PR";

//        sqlOperation.AddStringParameter("P_UserCode", "fzuniga");
//        sqlOperation.AddStringParameter("P_Name", "Fabiola");
//        sqlOperation.AddStringParameter("P_Email", "fzunigav@ucenfotec.ac.cr");
//        sqlOperation.AddStringParameter("P_Password", "Fabiola123!");
//        sqlOperation.AddDateTimeParam("P_BirthDate", DateTime.Now);
//        sqlOperation.AddStringParameter("P_Status", "AC");

//        var sqlDao = SqlDAO.GetInstance();

//        sqlDao.ExecuteProcedure(sqlOperation);
//    }
//}

class Program
{
    static SqlOperation sqlOperation = new SqlOperation();
    static SqlDAO sqlDao = SqlDAO.GetInstance();

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== Menú Principal ===");
            Console.WriteLine("1. Users");
            Console.WriteLine("2. Movies");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
            string mainChoice = Console.ReadLine();

            switch (mainChoice)
            {
                case "1":
                    UsersMenu();
                    break;
                case "2":
                    MoviesMenu();
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void UsersMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("--- Users Menu ---");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. Retrieve User");
            Console.WriteLine("3. Update User");
            Console.WriteLine("4. Delete User");
            Console.WriteLine("5. Volver");
            Console.Write("Seleccione una opción: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Digite el UserCode del User");
                    string userCode = Console.ReadLine();

                    Console.WriteLine("Digite el Name del User");
                    string name = Console.ReadLine();

                    Console.WriteLine("Digite el Email del User");
                    string email = Console.ReadLine();

                    Console.WriteLine("Digite el Password del User");
                    string password = Console.ReadLine();

                    Console.WriteLine("Digite el Status del User");
                    string status = Console.ReadLine();

                    Console.WriteLine("Digite el bday del User, formato yyyy-MM-dd");
                    DateTime bday;
                    string input = Console.ReadLine();

                    // Intentamos parsear; si falla, volvemos a pedir hasta que sea válido
                    while (!DateTime.TryParse(input, out bday))
                    {
                        Console.WriteLine("Fecha inválida. Por favor ingrésala en formato yyyy-MM-dd o similar:");
                        input = Console.ReadLine();
                    }

                    var user = new User() { UserCode = userCode, Name = name, Email = email , Password = password, Status = status, Birth = bday};

                    var UserCrudFactory = new DataAccess.CRUD.UsersCrudFactory();
                    UserCrudFactory.Create(user);

                    break;
                case "2":
                    var userCrudFactory = new DataAccess.CRUD.UsersCrudFactory();
                    var lstUsers = userCrudFactory.RetrieveAll<User>();
                    foreach (var u in lstUsers)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(u));
                    }
                    break;
                case "3":
                    Console.WriteLine("[Update User] lógica de actualización aquí...");
                    break;
                case "4":
                    Console.WriteLine("[Delete User] lógica de eliminación aquí...");
                    break;
                case "5":
                    back = true;
                    continue;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void MoviesMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("--- Movies Menu ---");
            Console.WriteLine("1. Create Movie");
            Console.WriteLine("2. Retrieve Movie");
            Console.WriteLine("3. Update Movie");
            Console.WriteLine("4. Delete Movie");
            Console.WriteLine("5. Volver");
            Console.Write("Seleccione una opción: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Digite el Titulo de la Pelicula");
                    string title = Console.ReadLine();

                    Console.WriteLine("Digite la Descripcion de la Pelicula");
                    string description = Console.ReadLine();

                    Console.WriteLine("Digite el Genero de la Pelicula");
                    string genre = Console.ReadLine();

                    Console.WriteLine("Digite el Director de la Pelicula");
                    string director = Console.ReadLine();

                    Console.WriteLine("Digite la Fecha de Lanzamiento de la Pelicula, formato yyyy-MM-dd");
                    DateTime releaseDate;
                    string input = Console.ReadLine();

                    // Intentamos parsear; si falla, volvemos a pedir hasta que sea válido
                    while (!DateTime.TryParse(input, out releaseDate))
                    {
                        Console.WriteLine("Fecha inválida. Por favor ingrésala en formato yyyy-MM-dd o similar:");
                        input = Console.ReadLine();
                    }
                    try
                    {
                        sqlOperation.ProcedureName = "CRE_MOVIE_PR";

                        sqlOperation.AddStringParameter("P_Title", title);
                        sqlOperation.AddStringParameter("P_Description", description);
                        sqlOperation.AddDateTimeParam("P_ReleaseDate", releaseDate);
                        sqlOperation.AddStringParameter("P_Genre", genre);
                        sqlOperation.AddStringParameter("P_Director", director);
                        sqlDao.ExecuteProcedure(sqlOperation);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error");
                        throw;
                    }
                    
                    break;
                case "2":
                    Console.WriteLine("[Retrieve Movie] lógica de consulta aquí...");
                    break;
                case "3":
                    Console.WriteLine("[Update Movie] lógica de actualización aquí...");
                    break;
                case "4":
                    Console.WriteLine("[Delete Movie] lógica de eliminación aquí...");
                    break;
                case "5":
                    back = true;
                    continue;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}