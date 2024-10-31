

using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Threading.Tasks;
using curd_redis;
using Castle.Core.Configuration;

namespace curd_redis;
public class UnitTest1
{
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
    private readonly RedisService _redisService;

    public UnitTest1()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"Redis:ConnectionString", "127.0.0.1:6379"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _redisService = new RedisService(_configuration);
    }
    
    //public UnitTest1(IConfiguration configuration)
    //{
    //    _redisService = new RedisService(configuration);
    //}

    [Fact]
    public async Task GetAsync_ShouldReturnValue_WhenKeyExists()
    {
        // Arrange
        await _redisService.SetAsync("testKey", "testValue");

        // Act
        var result = await _redisService.GetAsync("testKey");

        // Assert
        Assert.Equal("testValue", result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNull_WhenKeyDoesNotExist()
    {
        // Act
        var result = await _redisService.GetAsync("nonExistentKey");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SetAsync_ShouldSetValue()
    {
        // Act
        await _redisService.SetAsync("testKey", "testValue");

        // Assert
        var result = await _redisService.GetAsync("testKey");
        Assert.Equal("testValue", result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteValue()
    {
        // Arrange
        await _redisService.SetAsync("testKey", "testValue");

        // Act
        await _redisService.DeleteAsync("testKey");

        // Assert
        var result = await _redisService.GetAsync("testKey");
        Assert.Null(result);
    }
}

