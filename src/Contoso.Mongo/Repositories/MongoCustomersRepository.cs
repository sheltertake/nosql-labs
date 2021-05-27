using Contoso.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Mongo.Repositories
{
    public interface IMongoCustomersRepository
    {
        Task<IEnumerable<Customer>> FindAsync();
        Task InsertAllAsync(IEnumerable<Customer> items);
    }
    public class MongoCustomersRepository : IMongoCustomersRepository
    {
        private IMongoCollection<Customer> collection;

        public MongoCustomersRepository()
        {
            BsonClassMap.RegisterClassMap<Customer>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            var client = new MongoClient(
                  "mongodb://root:example@localhost:27017"
              //"mongodb://localhost:30001,localhost:30002,localhost:30003/?replicaSet=my-replica-set"
              );
            var database = client.GetDatabase("contoso");
            collection = database.GetCollection<Customer>("customers");

        }
        public async Task<IEnumerable<Customer>> FindAsync()
        {            
            var ret = await collection.Find(_ => true).ToListAsync();
            return ret.AsEnumerable();
        }

        public async Task InsertAllAsync(IEnumerable<Customer> items)
        {
            await collection.InsertManyAsync(items);
        }
    }
}
