using Contoso.Model;
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
        public async Task<IEnumerable<Customer>> FindAsync()
        {
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);
            var client = new MongoClient(
                  "mongodb://root:example@localhost:27017"
              );
            var database = client.GetDatabase("contoso");
            var collection = database.GetCollection<Customer>("customers");
            var ret = await collection.Find(_ => true).ToListAsync();
            return ret.AsEnumerable();
        }

        public async Task InsertAllAsync(IEnumerable<Customer> items)
        {
            var client = new MongoClient(
                 "mongodb://root:example@localhost:27017"
             );
            var database = client.GetDatabase("contoso");


            var collection = database.GetCollection<Customer>("customers");
            //var documents = items.Select(x => x.ToBsonDocument()).ToList();

            //await database.CreateCollectionAsync("customers");
            await collection.InsertManyAsync(items);
        }
    }
}
