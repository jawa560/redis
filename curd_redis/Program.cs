using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace curd_redis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Program.cs
         

            var builder = WebApplication.CreateBuilder(args);

            // 其他服務注入
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<RedisService>();

            var app = builder.Build();

            // 其他中間件配置
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();

        }
    }
}
