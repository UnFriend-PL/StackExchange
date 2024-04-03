using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using stackExchange.Controllers;
using stackExchange.Database;
using stackExchange.Models.Tag;
using stackExchange.Services.TagService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StackExchangeTests
{
    public class TagControllerTests
    {
        private DbContextOptions<StackOverflowDbContext> _options;
        private ILogger<TagService> _logger;

        public TagControllerTests()
        {
            _options = new DbContextOptionsBuilder<StackOverflowDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _logger = Mock.Of<ILogger<TagService>>();

        }

        [Fact]
        public async Task GetTagsAsync_ReturnsAllTags()
        {
            // Arrange
            using (var context = new StackOverflowDbContext(_options))
            {
                context.Tags.Add(new Tag { Count = 3, Name = "zTag3", HasSynonyms = false, IsModeratorOnly = false, IsRequired = false });
                context.SaveChanges();
            }

            // Act
            using (var context = new StackOverflowDbContext(_options))
            {
                var service = new TagService(context, _logger);
                var controller = new TagController(service, null);

                var result = await controller.GetTagsAsync();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsType<List<Tag>>(okResult.Value);
                var tag = returnValue.FirstOrDefault();
                Assert.Equal("zTag3", tag.Name);
            }
        }

    }

}

