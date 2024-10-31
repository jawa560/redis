using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace curd_redis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Route : ControllerBase
    {
        private readonly RedisService _redisService;

        public Route(RedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var value = await _redisService.GetAsync(key);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KeyValue model)
        {
            await _redisService.SetAsync(model.Key, model.Value);
            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _redisService.DeleteAsync(key);
            return Ok();
        }
    }

    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
