using AutoFixture;
using Models.Models.DBEntities;
using Models.Models.Enums;
using Moq;
using repository.Interfaces;
using service.Implementations;

namespace tests
{
    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleRepository> _mockVehicleRepository = new Mock<IVehicleRepository>();
        private readonly Mock<ISedanRepository> _mockSedanRepository = new Mock<ISedanRepository>();
        private readonly Mock<ISUVRepository> _mockSUVRepository = new Mock<ISUVRepository>();
        private readonly Mock<ITruckRepository> _mockTruckRepository = new Mock<ITruckRepository>();
        private readonly Mock<IHatchbackRepository> _mockHatchbackRepository = new Mock<IHatchbackRepository>();
        private readonly VehicleService _service;

        private Fixture _fixture;
        public VehicleServiceTests()
        {
            _service = new VehicleService(_mockVehicleRepository.Object,
                                            _mockSedanRepository.Object,
                                            _mockSUVRepository.Object,
                                            _mockTruckRepository.Object,
                                            _mockHatchbackRepository.Object);
        }

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task GetVehiclesByTypeAsync_IfVehicleTypeEnum_IsHatchback_ShouldOnlyReturnHatchbacks()
        {
            //Set up Mock locally to prevent shared resources, sharing mocks was resulting in collective fail of tests
            Mock<IVehicleRepository> _localMockVehicleRepository = new Mock<IVehicleRepository>();
            Mock<ISedanRepository> _localmockSedanRepository = new Mock<ISedanRepository>();
            Mock<ISUVRepository> _localmockSUVRepository = new Mock<ISUVRepository>();
            Mock<ITruckRepository> _localmockTruckRepository = new Mock<ITruckRepository>();
            Mock<IHatchbackRepository> _localmockHatchbackRepository = new Mock<IHatchbackRepository>();
            VehicleService _localService;
            _localService = new VehicleService(_localMockVehicleRepository.Object,
                                            _localmockSedanRepository.Object,
                                            _localmockSUVRepository.Object,
                                            _localmockTruckRepository.Object,
                                            _localmockHatchbackRepository.Object);

            //Arrange
            var type = VehicleTypeEnum.HATCHBACK;
            var hatchbacks = _fixture.CreateMany<Hatchback>();

            _localMockVehicleRepository.Setup(repo => repo.GetHatchbacksAsync())
                                    .ReturnsAsync(hatchbacks);
            //Act
            var result = await _localService.GetVehiclesByTypeAsync(type);

            //Assert
            Assert.IsNotNull(result);
            //Assert.That(result, Is.TypeOf<IEnumerable<Vehicle>>());
            Assert.That(result, Is.EqualTo(hatchbacks));
            _localMockVehicleRepository.Verify(x => x.GetHatchbacksAsync(), Times.Once);
            _localMockVehicleRepository.Verify(x => x.GetSedansAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetSUVsAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetTrucksAsync(), Times.Never);
        }

        [Test]
        public async Task GetVehiclesByTypeAsync_IfVehicleTypeEnum_IsSedan_ShouldOnlyReturnSedans()
        {
            //Set up Mock locally to prevent shared resources, sharing mocks was resulting in collective fail of tests
            Mock<IVehicleRepository> _localMockVehicleRepository = new Mock<IVehicleRepository>();
            Mock<ISedanRepository> _localmockSedanRepository = new Mock<ISedanRepository>();
            Mock<ISUVRepository> _localmockSUVRepository = new Mock<ISUVRepository>();
            Mock<ITruckRepository> _localmockTruckRepository = new Mock<ITruckRepository>();
            Mock<IHatchbackRepository> _localmockHatchbackRepository = new Mock<IHatchbackRepository>();
            VehicleService _localService;
            _localService = new VehicleService(_localMockVehicleRepository.Object,
                                            _localmockSedanRepository.Object,
                                            _localmockSUVRepository.Object,
                                            _localmockTruckRepository.Object,
                                            _localmockHatchbackRepository.Object);

            //Arrange
            var type = VehicleTypeEnum.SEDAN;
            var sedans = _fixture.CreateMany<Sedan>();

            _localMockVehicleRepository.Setup(repo => repo.GetSedansAsync())
                                    .ReturnsAsync(sedans);
            //Act
            var result = await _localService.GetVehiclesByTypeAsync(type);

            //Assert
            Assert.IsNotNull(result);
            //Assert.That(result, Is.TypeOf<IEnumerable<Vehicle>>());
            Assert.That(result, Is.EqualTo(sedans));
            _localMockVehicleRepository.Verify(x => x.GetHatchbacksAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetSedansAsync(), Times.Once);
            _localMockVehicleRepository.Verify(x => x.GetSUVsAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetTrucksAsync(), Times.Never);
        }

        [Test]
        public async Task GetVehiclesByTypeAsync_IfVehicleTypeEnum_IsSUV_ShouldOnlyReturnSUVs()
        {
            //Set up Mock locally to prevent shared resources, sharing mocks was resulting in collective fail of tests
            Mock<IVehicleRepository> _localMockVehicleRepository = new Mock<IVehicleRepository>();
            Mock<ISedanRepository> _localmockSedanRepository = new Mock<ISedanRepository>();
            Mock<ISUVRepository> _localmockSUVRepository = new Mock<ISUVRepository>();
            Mock<ITruckRepository> _localmockTruckRepository = new Mock<ITruckRepository>();
            Mock<IHatchbackRepository> _localmockHatchbackRepository = new Mock<IHatchbackRepository>();
            VehicleService _localService;
            _localService = new VehicleService(_localMockVehicleRepository.Object,
                                            _localmockSedanRepository.Object,
                                            _localmockSUVRepository.Object,
                                            _localmockTruckRepository.Object,
                                            _localmockHatchbackRepository.Object);

            //Arrange
            var type = VehicleTypeEnum.SUV;
            var SUVs = _fixture.CreateMany<SUV>();

            _localMockVehicleRepository.Setup(repo => repo.GetSUVsAsync())
                                    .ReturnsAsync(SUVs);
            //Act
            var result = await _localService.GetVehiclesByTypeAsync(type);

            //Assert
            Assert.IsNotNull(result);
            //Assert.That(result, Is.TypeOf<IEnumerable<Vehicle>>());
            Assert.That(result, Is.EqualTo(SUVs));
            _localMockVehicleRepository.Verify(x => x.GetHatchbacksAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetSedansAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetSUVsAsync(), Times.Once);
            _localMockVehicleRepository.Verify(x => x.GetTrucksAsync(), Times.Never);
        }

        [Test]
        public async Task GetVehiclesByTypeAsync_IfVehicleTypeEnum_IsTruck_ShouldOnlyReturnTrucks()
        {
            //Set up Mock locally to prevent shared resources, sharing mocks was resulting in collective fail of tests
            Mock<IVehicleRepository> _localMockVehicleRepository = new Mock<IVehicleRepository>();
            Mock<ISedanRepository> _localmockSedanRepository = new Mock<ISedanRepository>();
            Mock<ISUVRepository> _localmockSUVRepository = new Mock<ISUVRepository>();
            Mock<ITruckRepository> _localmockTruckRepository = new Mock<ITruckRepository>();
            Mock<IHatchbackRepository> _localmockHatchbackRepository = new Mock<IHatchbackRepository>();
            VehicleService _localService;
            _localService = new VehicleService(_localMockVehicleRepository.Object,
                                            _localmockSedanRepository.Object,
                                            _localmockSUVRepository.Object,
                                            _localmockTruckRepository.Object,
                                            _localmockHatchbackRepository.Object);
            //Arrange
            var type = VehicleTypeEnum.TRUCK;
            var trucks = _fixture.CreateMany<Truck>();

            _localMockVehicleRepository.Setup(repo => repo.GetTrucksAsync())
                                    .ReturnsAsync(trucks);
            //Act
            var result = await _localService.GetVehiclesByTypeAsync(type);

            //Assert
            Assert.IsNotNull(result);
            //Assert.That(result, Is.TypeOf<IEnumerable<Vehicle>>());
            Assert.That(result, Is.EqualTo(trucks));
            _localMockVehicleRepository.Verify(x => x.GetHatchbacksAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetSedansAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetSUVsAsync(), Times.Never);
            _localMockVehicleRepository.Verify(x => x.GetTrucksAsync(), Times.Once);
        }

        [Test]
        public async Task GetVehiclesByListOfTypesAsync_ShouldReturnTypesOfVehicles()
        {
            //Arrange
            var types = _fixture.CreateMany<VehicleTypeEnum>();

            var hatchbacks = _fixture.CreateMany<Hatchback>();

            _mockVehicleRepository.Setup(repo => repo.GetHatchbacksAsync())
                                    .ReturnsAsync(hatchbacks);

            var sedans = _fixture.CreateMany<Sedan>();

            _mockVehicleRepository.Setup(repo => repo.GetSedansAsync())
                                    .ReturnsAsync(sedans);

            var SUVs = _fixture.CreateMany<SUV>();

            _mockVehicleRepository.Setup(repo => repo.GetSUVsAsync())
                                    .ReturnsAsync(SUVs);

            var trucks = _fixture.CreateMany<Truck>();

            _mockVehicleRepository.Setup(repo => repo.GetTrucksAsync())
                                    .ReturnsAsync(trucks);

            //Act
            var result = await _service.GetVehiclesByListOfTypesAsync(types.ToArray());

            //Assert
            Assert.IsNotNull(result);
            _mockVehicleRepository.Verify(x => x.GetHatchbacksAsync(), types.Contains(VehicleTypeEnum.HATCHBACK) ? Times.Once : Times.Never);
            _mockVehicleRepository.Verify(x => x.GetSedansAsync(), types.Contains(VehicleTypeEnum.SEDAN) ? Times.Once : Times.Never);
            _mockVehicleRepository.Verify(x => x.GetSUVsAsync(), types.Contains(VehicleTypeEnum.SUV) ? Times.Once : Times.Never);
            _mockVehicleRepository.Verify(x => x.GetTrucksAsync(), types.Contains(VehicleTypeEnum.TRUCK) ? Times.Once : Times.Never);
        }

        [Test]
        public async Task GetVehicleByUniqueIdentifier_ShouldReturnVehicle()
        {
            //Arrange
            string uniqueIdentifier = _fixture.Create<string>();
            var vehicle = _fixture.Build<Vehicle>()
                                    .With(x => x.UniqueIdentifier, uniqueIdentifier)
                                    .Create();

            _mockVehicleRepository.Setup(repo => repo.GetVehicleByUniqueIdentifier(uniqueIdentifier))
                                    .ReturnsAsync(vehicle);

            //Act
            var result = await _service.GetVehicleByUniqueIdentifier(uniqueIdentifier);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(vehicle));
        }

        [Test]
        public async Task GetVehicleById_ShouldReturnVehicle()
        {
            //Arrange
            int vehicleId = _fixture.Create<int>();
            var vehicle = _fixture.Build<Vehicle>()
                                    .With(x => x.Id, vehicleId)
                                    .Create();

            _mockVehicleRepository.Setup(repo => repo.GetAsync(vehicleId))
                                    .ReturnsAsync(vehicle);

            //Act
            var result = await _service.GetVehicleById(vehicleId);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(vehicle));
        }

        [Test]
        public async Task AddSedanAsync_ShouldAddSedan()
        {
            //Arrange
            var sedan = _fixture.Create<Sedan>();

            _mockSedanRepository.Setup(repo => repo.AddAsync(sedan))
                                    .ReturnsAsync(sedan);

            //Act
            var result = await _service.AddSedanAsync(sedan);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(sedan));
        }

        [Test]
        public async Task AddSUVAsync_ShouldAddSUV()
        {
            //Arrange
            var SUV = _fixture.Create<SUV>();

            _mockSUVRepository.Setup(repo => repo.AddAsync(SUV))
                                    .ReturnsAsync(SUV);

            //Act
            var result = await _service.AddSUVAsync(SUV);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(SUV));
        }

        [Test]
        public async Task AddTruckAsync_ShouldAddTruck()
        {
            //Arrange
            var truck = _fixture.Create<Truck>();

            _mockTruckRepository.Setup(repo => repo.AddAsync(truck))
                                    .ReturnsAsync(truck);

            //Act
            var result = await _service.AddTruckAsync(truck);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(truck));
        }

        [Test]
        public async Task AddHatchbackAsync_ShouldAddHatchback()
        {
            //Arrange
            var hatchback = _fixture.Create<Hatchback>();

            _mockHatchbackRepository.Setup(repo => repo.AddAsync(hatchback))
                                    .ReturnsAsync(hatchback);

            //Act
            var result = await _service.AddHatchbackAsync(hatchback);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(hatchback));
        }

        [Test]
        public async Task FilterVehiclesByAttributes_IfManufacturer_IsProvided_ShouldFilterByManufacturer()
        {
            //Arrange
            string manufacturer = _fixture.Create<string>();

            var vehicles = _fixture.CreateMany<Vehicle>()
                                    .AsQueryable();

            var vehicleToBeFiltered = _fixture.Build<Vehicle>()
                                                .With(x => x.Manufacturer, manufacturer)
                                                .Create();

            vehicles = vehicles.Append(vehicleToBeFiltered);

            IEnumerable<Vehicle> matchResult = [];
            matchResult = matchResult.Append(vehicleToBeFiltered);

            //Act
            var result = _service.FilterVehiclesByAttributes(vehicles, manufacturer: manufacturer, null, null);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(matchResult));
        }

        [Test]
        public async Task FilterVehiclesByAttributes_IfModel_IsProvided_ShouldFilterByModel()
        {
            //Arrange
            string model = _fixture.Create<string>();

            var vehicles = _fixture.CreateMany<Vehicle>()
                                    .AsQueryable();

            var vehicleToBeFiltered = _fixture.Build<Vehicle>()
                                                .With(x => x.Model, model)
                                                .Create();

            vehicles = vehicles.Append(vehicleToBeFiltered);

            IEnumerable<Vehicle> matchResult = [];
            matchResult = matchResult.Append(vehicleToBeFiltered);

            //Act
            var result = _service.FilterVehiclesByAttributes(vehicles, manufacturer: null, model: model, year : null);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(matchResult));
        }

        [Test]
        public async Task FilterVehiclesByAttributes_IfYear_IsProvided_ShouldFilterByYear()
        {
            //Arrange
            int year = _fixture.Create<int>();

            var vehicles = _fixture.CreateMany<Vehicle>()
                                    .AsQueryable();

            var vehicleToBeFiltered = _fixture.Build<Vehicle>()
                                                .With(x => x.Year, year)
                                                .Create();

            vehicles = vehicles.Append(vehicleToBeFiltered);

            IEnumerable<Vehicle> matchResult = [];
            matchResult = matchResult.Append(vehicleToBeFiltered);

            //Act
            var result = _service.FilterVehiclesByAttributes(vehicles, manufacturer: null, model: null, year: year);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(matchResult));
        }
    }
}
