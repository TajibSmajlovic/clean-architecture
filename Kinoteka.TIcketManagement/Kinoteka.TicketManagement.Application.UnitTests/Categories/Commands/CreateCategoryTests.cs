using AutoMapper;
using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Application.Features.Categories.Commands.CreateCateogry;
using Kinoteka.TicketManagement.Application.Profiles;
using Kinoteka.TicketManagement.Application.UnitTests.Mocks;
using Kinoteka.TicketManagement.Domain.Entities;
using Moq;
using Shouldly;

namespace Kinoteka.TicketManagement.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;

        public CreateCategoryTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

            await handler.Handle(new CreateCategoryCommand() { Name = "Test" }, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(5);
        }
    }
}