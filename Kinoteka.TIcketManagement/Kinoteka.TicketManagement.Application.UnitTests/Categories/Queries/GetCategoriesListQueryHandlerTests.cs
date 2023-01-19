using AutoMapper;
using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using Kinoteka.TicketManagement.Application.Profiles;
using Kinoteka.TicketManagement.Application.UnitTests.Mocks;
using Kinoteka.TicketManagement.Domain.Entities;
using Moq;
using Shouldly;

namespace Kinoteka.TicketManagement.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;

        public GetCategoriesListQueryHandlerTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListTest()
        {
            var handler = new GetCategoriesListQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CategoryListVm>>();

            result.Count.ShouldBe(4);
        }
    }
}