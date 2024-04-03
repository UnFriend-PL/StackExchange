using Xunit;
using Moq;
using stackExchange.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using stackExchange.Models.Tag;
using stackExchange.Services.TagService;
namespace stackExchange.Tests.Services
{
    public class TagServiceTests
    {
        private readonly DbContextOptions<StackOverflowDbContext> _options;
        private readonly TagService _tagService;

        public TagServiceTests()
        {
            _options = new DbContextOptionsBuilder<StackOverflowDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new StackOverflowDbContext(_options);
            var logger = Mock.Of<ILogger<TagService>>();
            _tagService = new TagService(context, logger);
        }

        [Fact]
        public async Task GetAllTags_ReturnsAllTags()
        {
            // Arrange
            using (var context = new StackOverflowDbContext(_options))
            {
                context.Tags.AddRange(
                     new Tag { Count = 3, Name = "zTag3", HasSynonyms = false, IsModeratorOnly = false, IsRequired = false },
                     new Tag { Count = 2, Name = "aTag2", HasSynonyms = false, IsModeratorOnly = false, IsRequired = false },
                     new Tag { Count = 1, Name = "bTag1", HasSynonyms = false, IsModeratorOnly = false, IsRequired = false }
                 );
                context.SaveChanges();
            }

            // Act
            var result = await _tagService.GetAllTags();

            // Assert
            Assert.Equal(3, result.Count);
        }

    }
}