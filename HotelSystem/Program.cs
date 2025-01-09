
using Braintree;
using HotelSystem.Data;
using HotelSystem.Data.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace HotelSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(typeof(Program).Assembly);
          
            builder.Services.AddSingleton<IBraintreeGateway>(provider =>
            {
                var config = configuration.GetSection("Braintree");//.Get<BraintreeConfig>();

                return new BraintreeGateway
                {
                    Environment = Braintree.Environment.SANDBOX,
                    MerchantId = config["Braintree:MerchantId"],
                    PublicKey = config["Braintree:PublicKey"],
                    PrivateKey = config["Braintree:PrivateKey"]
                };
            });
            builder.Services.AddDbContext<Context>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
