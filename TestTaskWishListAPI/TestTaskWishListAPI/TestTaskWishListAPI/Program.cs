
using Microsoft.EntityFrameworkCore;
using TestTaskWishListAPI.Data;
using TestTaskWishListAPI.Repository.Interfaces;
using TestTaskWishListAPI.Repository;

namespace TestTaskWishListAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<DBContext>(options => {
                options.UseNpgsql(builder.Configuration.GetConnectionString("WishListDataBase"));
            });
            builder.Services.AddScoped<IWishListRepository, WishListRepository>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
