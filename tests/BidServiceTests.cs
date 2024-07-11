using AutoFixture;
using Models.Models.DBEntities;
using Moq;
using repository.Interfaces;
using service.Implementations;
using service.Interfaces;

namespace tests
{
    public class BidServiceTests
    {
        private readonly Mock<IBidRepository> _mockBidRepository = new Mock<IBidRepository>();
        private readonly Mock<IAuctionService> _mockAuctionService = new Mock<IAuctionService>();
        private readonly BidService _service;

        private Fixture _fixture;
        public BidServiceTests()
        {
            _service = new BidService(_mockBidRepository.Object, _mockAuctionService.Object);
        }

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task PlaceBid_If_IsHighestBid_ShouldPlaceBid()
        {
            //Arrange
            var mockBidService = new Mock<BidService>();
            var bid = _fixture.Create<Bid>();

            //This ensures that all already placed bids are smaller than the intended bid to be placed
            var bids = _fixture.Build<Bid>()
                                .With(x => x.OfferedBid, bid.OfferedBid - 1)
                                .With(x => x.AuctionedVehicleId, bid.AuctionedVehicleId)
                                .CreateMany();

            _mockBidRepository.Setup(repo => repo.GetAllBidsForAuctionedVehicle(bid.AuctionedVehicleId))
                                    .ReturnsAsync(bids);

            _mockBidRepository.Setup(repo => repo.AddAsync(bid))
                                    .ReturnsAsync(bid);

            //Act
            var result = await _service.PlaceBid(bid);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Bid>());
            Assert.That(result, Is.EqualTo(bid));
        }

        [Test]
        public async Task PlaceBid_If_NoBidsFound_And_BidIsHigherThan_StartingBid_ShouldPlaceBid()
        {
            //Arrange
            var mockBidService = new Mock<BidService>();
            var startingBid = _fixture.Create<decimal>();

            //This ensures the placing bid is greater that the starting bid of the auctioned vehicle
            var bid = _fixture.Build<Bid>()
                                .With(x => x.OfferedBid, startingBid + 1)
                                .Create();

            //This ensures that all already placed bids are smaller than the intended bid to be placed

            _mockBidRepository.Setup(repo => repo.GetAllBidsForAuctionedVehicle(bid.AuctionedVehicleId))
                                    .ReturnsAsync(Enumerable.Empty<Bid>);

            _mockAuctionService.Setup(repo => repo.GetStartingBidForAuctionedVehicle(bid.AuctionedVehicleId))
                                    .ReturnsAsync(startingBid);

            _mockBidRepository.Setup(repo => repo.AddAsync(bid))
                                    .ReturnsAsync(bid);


            //Act
            var result = await _service.PlaceBid(bid);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Bid>());
            Assert.That(result, Is.EqualTo(bid));
        }

        [Test]
        public async Task PlaceBid_If_NoBidsFound_And_BidIsSmallerThan_StartingBid_ShouldReturnNull()
        {
            //Arrange
            var mockBidService = new Mock<BidService>();
            var startingBid = _fixture.Create<decimal>();

            //This ensures the placing bid is greater that the starting bid of the auctioned vehicle
            var bid = _fixture.Build<Bid>()
                                .With(x => x.OfferedBid, startingBid - 1)
                                .Create();

            //This ensures that all already placed bids are smaller than the intended bid to be placed

            _mockBidRepository.Setup(repo => repo.GetAllBidsForAuctionedVehicle(bid.AuctionedVehicleId))
                                    .ReturnsAsync(Enumerable.Empty<Bid>);

            _mockAuctionService.Setup(repo => repo.GetStartingBidForAuctionedVehicle(bid.AuctionedVehicleId))
                                    .ReturnsAsync(startingBid);

            _mockBidRepository.Setup(repo => repo.AddAsync(bid))
                                    .ReturnsAsync((Bid)null);


            //Act
            var result = await _service.PlaceBid(bid);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task PlaceBid_If_BidsFound_And_BidIsNotTheHighestBid_ShouldReturnNull()
        {
            //Arrange
            var mockBidService = new Mock<BidService>();
            var bid = _fixture.Create<Bid>();

            //This ensures that all already placed bids are smaller than the intended bid to be placed
            var bids = _fixture.Build<Bid>()
                                .With(x => x.OfferedBid, bid.OfferedBid + 1)
                                .With(x => x.AuctionedVehicleId, bid.AuctionedVehicleId)
                                .CreateMany();

            _mockBidRepository.Setup(repo => repo.GetAllBidsForAuctionedVehicle(bid.AuctionedVehicleId))
                                    .ReturnsAsync(bids);

            _mockBidRepository.Setup(repo => repo.AddAsync(bid))
                                    .ReturnsAsync((Bid)null);

            //Act
            var result = await _service.PlaceBid(bid);

            //Assert
            Assert.IsNull(result);
        }
    }
}
