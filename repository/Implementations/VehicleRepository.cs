using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using Models.Models.Enums;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IRepository<Vehicle>, IVehicleRepository
    {
        private readonly DbSet<Vehicle> _db;
        private readonly DbSet<Truck> _dbTruck;
        private readonly DbSet<Hatchback> _dbHatchback;
        private readonly DbSet<Sedan> _dbSedan;
        private readonly DbSet<SUV> _dbSuv;

        public VehicleRepository(CASContext context) : base(context)
        {
            _db = context.Set<Vehicle>();
            _dbTruck = context.Set<Truck>();
            _dbHatchback = context.Set<Hatchback>();
            _dbSedan = context.Set<Sedan>();
            _dbSuv = context.Set<SUV>();
        }

        public async Task<IEnumerable<Vehicle>> GetHatchbacksAsync()
            => await _dbHatchback.Where(x => x is Hatchback).ToArrayAsync();

        public async Task<IEnumerable<Vehicle>> GetSedansAsync()
            => await _dbSedan.Where(x => x is Sedan).ToArrayAsync();

        public async Task<IEnumerable<Vehicle>> GetSUVsAsync()
            => await _dbSuv.Where(x => x is SUV).ToArrayAsync();

        public async Task<IEnumerable<Vehicle>> GetTrucksAsync()
            => await _dbTruck.Where(x => x is Truck).ToArrayAsync();

        public async Task<IEnumerable<Vehicle>> GetVehiclesByListOfTypesAsync(VehicleTypeEnum[] types)
            => await _db.Where(x => Array.ConvertAll(types, type => (int)type).Contains(x.VehicleTypeId)).ToArrayAsync();

        public async Task<Vehicle?> GetVehicleByUniqueIdentifier(string uniqueIdentifier)
            => await _db.Where(x => x.UniqueIdentifier == uniqueIdentifier).FirstOrDefaultAsync();
    }
}
