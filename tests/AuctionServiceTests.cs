using AutoFixture;
using Models.Models.DBEntities;
using Moq;
using repository.Interfaces;
using service.Implementations;
using service.Interfaces;

namespace tests
{
    public class AuctionServiceTests
    {
        private readonly Mock<IAuctionRepository> _mockAuctionRepository = new Mock<IAuctionRepository>();
        private readonly Mock<IAuctionedVehicleRepository> _mockAuctionedVehicleRepository = new Mock<IAuctionedVehicleRepository>();
        private readonly Mock<IVehicleService> _mockVehicleService = new Mock<IVehicleService>();
        private readonly AuctionService _service;

        private Fixture _fixture;
        public AuctionServiceTests()
        {
            _service = new AuctionService(_mockAuctionRepository.Object,
                                            _mockAuctionedVehicleRepository.Object,
                                            _mockVehicleService.Object);
        }

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task CreateAuctionAsync_WhenValid_Auction_ShouldInsertAuction()
        {
            //Arrange
            var auction = _fixture.Build<Auction>()
                                        .With(x => x.StartDate)
                                        .With(x => x.AuctionedVehicles, new List<AuctionedVehicle>())
                                       .Create();

            _mockAuctionRepository.Setup(repo => repo.AddAsync(It.IsAny<Auction>()))
                                    .ReturnsAsync(auction);

            //Act
            var result = await _service.CreateAuctionAsync(auction);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Auction>());
            Assert.That(result, Is.EqualTo(auction));
        }

        [Test]
        public async Task GetAuctionByIdWithIncludeAsync_IfAuctionExists_ShouldReturnAuction()
        {
            //Arrange
            int auctionId = _fixture.Create<int>();

            var auction = _fixture.Create<Auction>();

            _mockAuctionRepository.Setup(repo => repo.GetAuctionByIdAsync(auctionId))
                                    .ReturnsAsync(auction);

            //Act
            var result = await _service.GetAuctionByIdWithIncludeAsync(auctionId);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Auction>());
            Assert.That(result, Is.EqualTo(auction));
        }

        [Test]
        public async Task UpdateAuctionAsync_ShouldReturnUpdatedAuction()
        {
            //Arrange
            var auction = _fixture.Create<Auction>();

            _mockAuctionRepository.Setup(repo => repo.UpdateAsync(auction))
                                    .ReturnsAsync(auction);

            //Act
            var result = await _service.UpdateAuctionAsync(auction);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Auction>());
            Assert.That(result, Is.EqualTo(auction));
        }

        [Test]
        public async Task UpdateAuctionedVehicleAsync_ShouldReturnUpdatedAuctionedVehicle()
        {
            //Arrange
            var auctionedVehicle = _fixture.Create<AuctionedVehicle>();

            _mockAuctionedVehicleRepository.Setup(repo => repo.UpdateAsync(It.IsAny<AuctionedVehicle>()))
                                    .ReturnsAsync(auctionedVehicle);

            //Act
            var result = await _service.UpdateAuctionedVehicleAsync(auctionedVehicle);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<AuctionedVehicle>());
            Assert.That(result, Is.EqualTo(auctionedVehicle));
        }

        [Test]
        public async Task GetAuctionedVehicleByVehicleIdAsync_ShouldReturnAuctionedVehicle()
        {
            //Arrange
            int auctionedVehicleId = _fixture.Create<int>();
            var auctionedVehicle = _fixture.Create<AuctionedVehicle>();

            _mockAuctionedVehicleRepository.Setup(repo => repo.GetAuctionedVehicleByVehicleIdAsync(auctionedVehicleId))
                                    .ReturnsAsync(auctionedVehicle);

            //Act
            var result = await _service.GetAuctionedVehicleByVehicleIdAsync(auctionedVehicleId);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<AuctionedVehicle>());
            Assert.That(result, Is.EqualTo(auctionedVehicle));
        }

        [Test]
        public async Task IsVehicleAuctioned_IfCannotFind_AuctionedVehicle_ShouldReturnFalse()
        {
            //Arrange
            var vehicle = _fixture.Create<Vehicle>();

            _mockAuctionedVehicleRepository.Setup(repo => repo.GetAuctionedVehicleByVehicleIdAsync(vehicle.Id))
                                    .ReturnsAsync((AuctionedVehicle)null);

            //Act
            var result = await _service.IsVehicleAuctioned(vehicle);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsVehicleAuctioned_IfAuctionedVehicle_IsRemovedFromAuction_ShouldReturnFalse()
        {
            //Arrange
            var vehicle = _fixture.Create<Vehicle>();
            var auctionedVehicle = _fixture.Build<AuctionedVehicle>()
                                            .With(x => x.IsRemovedFromAuction, true)
                                            .Create();

            _mockAuctionedVehicleRepository.Setup(repo => repo.GetAuctionedVehicleByVehicleIdAsync(vehicle.Id))
                                    .ReturnsAsync(auctionedVehicle);

            //Act
            var result = await _service.IsVehicleAuctioned(vehicle);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsVehicleAuctioned_IfAuction_IsClosed_ShouldReturnFalse()
        {
            //Arrange
            var vehicle = _fixture.Create<Vehicle>();
            var auctionedVehicle = _fixture.Build<AuctionedVehicle>()
                                            .With(x => x.IsRemovedFromAuction, false)
                                            .Create();

            var auction = _fixture.Build<Auction>()
                                    .With(x => x.EndDate, DateTime.UtcNow.AddDays(-1))
                                    .Create();

            _mockAuctionedVehicleRepository.Setup(repo => repo.GetAuctionedVehicleByVehicleIdAsync(vehicle.Id))
                                    .ReturnsAsync(auctionedVehicle);

            _mockAuctionRepository.Setup(repo => repo.GetAsync(auctionedVehicle.AuctionId))
                                    .ReturnsAsync(auction);

            //Act
            var result = await _service.IsVehicleAuctioned(vehicle);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsVehicleAuctioned_IfAuction_IsOpen_ShouldReturnTrue()
        {
            //Arrange
            var vehicle = _fixture.Create<Vehicle>();
            var auctionedVehicle = _fixture.Build<AuctionedVehicle>()
                                            .With(x => x.IsRemovedFromAuction, false)
                                            .Create();

            var auction = _fixture.Build<Auction>()
                                    .With(x => x.EndDate, DateTime.UtcNow.AddDays(1))
                                    .Create();

            _mockAuctionedVehicleRepository.Setup(repo => repo.GetAuctionedVehicleByVehicleIdAsync(vehicle.Id))
                                    .ReturnsAsync(auctionedVehicle);

            _mockAuctionRepository.Setup(repo => repo.GetAsync(auctionedVehicle.AuctionId))
                                    .ReturnsAsync(auction);

            //Act
            var result = await _service.IsVehicleAuctioned(vehicle);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetStartingBidForAuctionedVehicle_AuctionedVehicleExists_ShouldReturnItsStartingBid()
        {
            //Arrange
            int auctionedVehicleId = _fixture.Create<int>();
            int vehicleStartingBid = _fixture.Create<int>();
            var auctionedVehicle = _fixture.Build<AuctionedVehicle>()
                                            .With(x => x.Id, auctionedVehicleId)
                                            .Create();

            var vehicle = _fixture.Build<Vehicle>()
                                    .With(x => x.Id, auctionedVehicle.VehicleId)
                                    .With(x => x.StartingBid, vehicleStartingBid)
                                    .Create();

            _mockAuctionedVehicleRepository.Setup(repo => repo.GetAsync(auctionedVehicleId))
                                    .ReturnsAsync(auctionedVehicle);

            _mockVehicleService.Setup(repo => repo.GetVehicleById(auctionedVehicle.VehicleId))
                                    .ReturnsAsync(vehicle);

            //Act
            var result = await _service.GetStartingBidForAuctionedVehicle(auctionedVehicleId);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<decimal>());
            Assert.That(result, Is.EqualTo(vehicleStartingBid));
        }
    }
}
