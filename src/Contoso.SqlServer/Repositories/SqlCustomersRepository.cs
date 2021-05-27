using Contoso.Model;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.SqlServer.Repositories
{
    public interface ISqlCustomersRepository
    {
        Task<IEnumerable<Customer>> FindAsync();
    }
    public class SqlCustomersRepository : ISqlCustomersRepository
    {
        private static string connectionString = @"Server=localhost;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=Passw@rd;MultipleActiveResultSets=True;Application Name=ContosoApi";

        public async Task<IEnumerable<Customer>> FindAsync()
        {
            var connection = new SqlConnection(connectionString);
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(connection, compiler);

            var customers = await db.Query("Customers").GetAsync<Customer>();

            return customers.ToList();
        }
    }
}
