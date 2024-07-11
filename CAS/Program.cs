using Microsoft.EntityFrameworkCore;
using repository;
using repository.Implementations;
using repository.Interfaces;
using service.Implementations;
using service.Interfaces;
using System.Text.Json.Serialization;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<CASContext>(o =>
        {
            o.UseSqlServer(Environment.GetEnvironmentVariable("CAS_CONNECTION_STRING", EnvironmentVariableTarget.Machine));
        });

        // Services
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<IVehicleService, VehicleService>();
        builder.Services.AddTransient<IAuctionService, AuctionService>();
        builder.Services.AddTransient<IResponseService, ResponseService>();
        builder.Services.AddTransient<IBidService, BidService>();

        // Repositories
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
        builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
        builder.Services.AddTransient<IHatchbackRepository, HatchbackRepository>();
        builder.Services.AddTransient<ISedanRepository, SedanRepository>();
        builder.Services.AddTransient<ITruckRepository, TruckRepository>();
        builder.Services.AddTransient<ISUVRepository, SUVRepository>();
        builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();
        builder.Services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();
        builder.Services.AddTransient<IBidRepository, BidRepository>();
        builder.Services.AddTransient<IAuctionedVehicleRepository, AuctionedVehicleRepository>();

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