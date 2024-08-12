using APIGateway.Infrastructure.Data.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace APIGateway.Infrastructure.Data.Services;
public class PayloadService<TPayload, TSettings>
    where TSettings : DatabaseSettings
{
    private readonly IMongoCollection<GenericPayload<TPayload>> _requestsCollection;

    public PayloadService(IOptions<TSettings> gatewayStoreDatabaseSettings)
    {

        var mongoClient = new MongoClient(
            gatewayStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            gatewayStoreDatabaseSettings.Value.DatabaseName);

        _requestsCollection = mongoDatabase.GetCollection<GenericPayload<TPayload>>(
            gatewayStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<GenericPayload<TPayload>>> GetAsync()
    {
        var result = await _requestsCollection.Find(_ => true).ToListAsync();

        return result;
    }

    public async Task<GenericPayload<TPayload>?> GetAsyncId(string id)
    {
        var result = await _requestsCollection.Find(x => x.transactionId == id).FirstOrDefaultAsync();

        return result;
    }

    public async Task CreateAsync(GenericPayload<TPayload> newRequest) => 
        await _requestsCollection.InsertOneAsync(newRequest);
   

    public async Task UpdateAsync(string id, GenericPayload<TPayload> updatedRequest) => 
        await _requestsCollection.ReplaceOneAsync(x => x.transactionId == id, updatedRequest);
    

    public async Task RemoveAsync(string id) =>
        await _requestsCollection.DeleteOneAsync(x => x.transactionId == id);
}