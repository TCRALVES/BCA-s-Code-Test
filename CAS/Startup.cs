using repository.Implementations;
using repository.Interfaces;
using service.Implementations;
using service.Interfaces;

namespace CAS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<IAuctionService, AuctionService>();
            services.AddTransient<IResponseService, ResponseService>();
            services.AddTransient<IBidService, BidService>();

            // Repositories
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAuctionRepository, AuctionRepository>();
            services.AddTransient<IHatchbackRepository, HatchbackRepository>();
            services.AddTransient<ISedanRepository, SedanRepository>();
            services.AddTransient<ITruckRepository, TruckRepository>();
            services.AddTransient<ISUVRepository, SUVRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddTransient<IBidRepository, BidRepository>();
            services.AddTransient<IAuctionedVehicleRepository, AuctionedVehicleRepository>();
        }
    }
}
