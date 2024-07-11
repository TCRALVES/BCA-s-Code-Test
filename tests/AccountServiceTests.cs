using AutoFixture;
using CAS.Models.DBEntities;
using CAS.Models.DTO;
using Moq;
using repository.Interfaces;
using service.Implementations;

namespace tests
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository = new Mock<IAccountRepository>();
        private readonly AccountService _service;

        private Fixture _fixture;
        public AccountServiceTests()
        {
            _service = new AccountService(_mockAccountRepository.Object);
        }

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task CreateAccount_WhenValid_UserEntityDTO_ShouldInsertUser()
        {
            //Arrange
            var account = _fixture.Build<UserEntityDTO>()
                                       .With(u => u.UserName)
                                       .With(u => u.Email)
                                       .With(u => u.Password)
                                       .Create();

            var userEntity = _fixture.Build<UserEntity>()
                                        .With(x => x.UserName, account.UserName)
                                        .With(x => x.Email, account.Email)
                                        .With(x => x.Password, account.Password)
                                        .Create();

            _mockAccountRepository.Setup(repo => repo.AddAsync(It.IsAny<UserEntity>()))
                                    .ReturnsAsync(userEntity);

            //Act
            var result = await _service.CreateAccount(account);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<UserEntity>());
            Assert.That(result, Is.EqualTo(userEntity));
        }

        [Test]
        public async Task CreateAccount_WhenMissingRequiredAttribute_ShouldNotInsertUser()
        {
            //Arrange
            var account = _fixture.Build<UserEntityDTO>()
                                       .Without(u => u.UserName)
                                       .With(u => u.Email)
                                       .With(u => u.Password)
                                       .Create();
            //Act
            var result = await _service.CreateAccount(account);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAccountByEmail_WhenEmailExists_ShouldReturnUser()
        {
            //Arrange
            string email = _fixture.Create<string>();

            var userEntity = _fixture.Create<UserEntity>();

            _mockAccountRepository.Setup(repo => repo.GetUserByEmailAsync(email))
                                    .ReturnsAsync(userEntity);

            //Act
            var result = await _service.GetAccountByEmail(email);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<UserEntity>());
        }

        [Test]
        public async Task GetAccountById_WhenEmailExists_ShouldReturnUser()
        {
            //Arrange
            int Id = _fixture.Create<int>();

            var userEntity = _fixture.Create<UserEntity>();

            _mockAccountRepository.Setup(repo => repo.GetAsync(Id))
                                    .ReturnsAsync(userEntity);

            //Act
            var result = await _service.GetAccountById(Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<UserEntity>());
        }
    }
}