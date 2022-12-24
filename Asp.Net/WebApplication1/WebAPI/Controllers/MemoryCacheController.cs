using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoryCacheController : ControllerBase
    {
        //可以将数据存入内存
        private readonly IMemoryCache memoryCache;
        //加入日志
        private readonly ILogger<MemoryCacheController> logger;
        //使用redis
        private readonly IDistributedCache distributedCache;
        /// <summary>
        /// 一般用构造函数注册
        /// </summary>
        /// <param name="memoryCache"></param>
        public MemoryCacheController(IMemoryCache memoryCache, ILogger<MemoryCacheController> logger,
            IDistributedCache distributedCache)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<Person?>> TestRedis(long id)
        {
            Person? result;
            string? b = await distributedCache.GetStringAsync("book" + id);
            if (b == null)//查询数据库
            {
                result = await MyDBContext.GetByIdAsync(id);
                await distributedCache.SetStringAsync("book" + id, JsonSerializer.Serialize(result));
            }
            else
            {
                //把json 转成实体
                result = JsonSerializer.Deserialize<Person?>(b);
            }
            if (result == null)
            {
                return NotFound("数据不存在");
            }
            else
            { 
                return result;
            }
        }

        /// <summary>
        /// 测试缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Person?>> GetPersonById(long id)
        {
            //Person? person = MyDBContext.GetById(id);
            //if (person == null)
            //{
            //    return NotFound($"找不到ID等于{id}的数");
            //}
            //else
            //{
            //    return person;
            //}
            logger.LogDebug($"开始执行GetPersonById,Id={id}");
            //如何解决数据库的新数据问题1 能不能在合适的时候更新成新的数据
            //GetOrCreateAsync 二合一 从缓存取数据 2 从数据库取数据，并返回给调用者以及保存到缓存
            //GetOrCreateAsync 把null也当成了一个值存入
            Person? person = await memoryCache.GetOrCreateAsync<Person?>("Person" + id, async (e) =>
            {
                //此处的e 可以设置缓存过期时间
                //设置过期时间为10秒绝对缓存时间
                //e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                //滑动缓存时间 意思就是一共10秒我在9秒的时候请求那么可以在续命10秒
                //在高频访问的时候不要使用new 
                //e.SlidingExpiration = TimeSpan.FromSeconds(Random.Shared.Next(10,15));//随机缓存时间
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10, 15));//随机缓存时间 可以解决缓存雪蹦
                logger.LogDebug($"缓存中没有数据，到数据库中查询数据GetPersonById,Id={id}");
                return await MyDBContext.GetByIdAsync(id);
            });
            logger.LogDebug($"结果是GetPersonById,Id={id}");
            if (person == null)
            {
                return NotFound($"找不到ID等于{id}的数");
            }
            else
            {
                return person;
            }
        }
    }
}
