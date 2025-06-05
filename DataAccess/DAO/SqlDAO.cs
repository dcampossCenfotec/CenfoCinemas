using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class SqlDAO
    {
        private static SqlDAO _instance;
        private string _connectionString;

        private SqlDAO() {
            _connectionString = string.Empty;
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
            //TBD
        }

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation) {
            var list = new List<Dictionary<string, object>>();
            return list;
        }
    }
}
