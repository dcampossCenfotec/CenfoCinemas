using CoreApp;
using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using static System.Runtime.CompilerServices.RuntimeHelpers;

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
            Console.WriteLine("3. Retrieve User By Id");
            Console.WriteLine("4. Update User");
            Console.WriteLine("5. Delete User");
            Console.WriteLine("6. Volver");
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

                    var uManager = new UserManager();
                    uManager.Create(user);

                    Console.WriteLine("Usuario creado exitosamente");

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
                    try
        {
                        Console.Write("Ingrese el ID del usuario: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("ID inválido.");
                            return;
                        }

                        var uCrud = new DataAccess.CRUD.UsersCrudFactory();
                        var u = uCrud.RetrieveById<User>(id);

                        if (u == null)
                        {
                            Console.WriteLine("Usuario no encontrado.");
                            return;
                        }

                        Console.WriteLine("Usuario encontrado:\n" + JsonConvert.SerializeObject(u));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al consultar usuario: {ex.Message}");
                    }
                    break;
                case "4":
                    Console.WriteLine("[Delete User] lógica de eliminación aquí...");
                    break;
                case "5":
                    break;
                case "6":
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
            Console.WriteLine("3. Retrieve Movie By Id");
            Console.WriteLine("4. Update Movie");
            Console.WriteLine("5. Delete Movie");
            Console.WriteLine("6. Volver");
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

                    var movie = new Movie() { Title = title, Description = description, ReleaseDate = releaseDate, Genre = genre, Director = director};

                    var mManager = new MovieManager();
                    mManager.Create(movie);

                    Console.WriteLine("Película registrada exitosamente.");

                    break;
                case "2":
                    var moviesCrudFactory = new DataAccess.CRUD.MovieCrudFactory();
                    var lstMovies = moviesCrudFactory.RetrieveAll<Movie>();
                    foreach (var u in lstMovies)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(u));
                    }
                    break;
                case "3":
                    try
                    {
                        Console.Write("Ingrese el ID de la película: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("ID inválido.");
                            return;
                        }

                        var uCrud = new DataAccess.CRUD.MovieCrudFactory(); ;
                        var m = uCrud.RetrieveById<Movie>(id);

                        if (m == null)
                        {
                            Console.WriteLine("Película no encontrada.");
                            return;
                        }

                        Console.WriteLine("Película encontrada:\n" + JsonConvert.SerializeObject(m));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al consultar película: {ex.Message}");
                    }
                    break;
                case "4":
                    Console.WriteLine("[Delete Movie] lógica de eliminación aquí...");
                    break;
                case "5":
                    break;
                case "6":
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