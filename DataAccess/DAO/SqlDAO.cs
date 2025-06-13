using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.DAO
{
    public class SqlDAO
    {
        private static SqlDAO _instance;
        private string _connectionString;

        private SqlDAO() {
            _connectionString = @"Data Source=srv-sqldb-dcampos.database.windows.net;Initial Catalog=cenfocinemas-db;Persist Security Info=True;User ID=sysman;Password=Cenfotec123!;Encrypt=True;Trust Server Certificate=True";
        }

        public static SqlDAO GetInstance() {
            if (_instance == null)
            {
                _instance = new SqlDAO();
            }
            return _instance;
        }

        public void ExecuteProcedure(SqlOperation operation)
        {
            using (var conn = new SqlConnection(_connectionString)) 
            {
                using (var command = new SqlCommand(operation.ProcedureName, conn) 
                { 
                    CommandType = System.Data.CommandType.StoredProcedure
                }) 
                {
                    //set de los params
                    foreach (var param in operation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    //exec SP
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {

            var lstResults = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(_connectionString))

            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    //Set de los parametros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    //Ejectura el SP
                    conn.Open();

                    //de aca en adelante la implementacion es distinta con respecto al procedure anterior
                    // sentencia que ejectua el SP y captura el resultado
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            var rowDict = new Dictionary<string, object>();

                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);
                                //aca agregamos los valores al diccionario de esta fila
                                rowDict[key] = value;
                            }
                            lstResults.Add(rowDict);
                        }
                    }

                }
            }

            return lstResults;
        }
    }
}
