using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace curd_redis;

public class RedisService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    public RedisService(IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetSection("Redis").GetValue<string>("ConnectionString");
        if (string.IsNullOrEmpty(redisConnectionString))
        {
            throw new InvalidOperationException("Redis connection string is not configured.");
        }
        _redis = ConnectionMultiplexer.Connect(redisConnectionString);
        _database = _redis.GetDatabase();
    }

    public async Task<string> GetAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        await _database.StringSetAsync(key, value);
    }

    public async Task DeleteAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}


