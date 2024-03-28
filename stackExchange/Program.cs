
using Microsoft.EntityFrameworkCore;
using stackExchange.Database;
using stackExchange.Services.TagService;
using System.Globalization;

namespace stackExchange
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddDbContext<StackOverflowDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<ITagDownloaderService, TagDownloaderService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Logging.AddConsole();
            var app = builder.Build();
            ApplyDatabaseMigrations(app);

            using (var serviceScope = app.Services.CreateAsyncScope())
            {
                var tagService = serviceScope.ServiceProvider.GetRequiredService<ITagDownloaderService>();
                await tagService.UpdateTagsAsync();
                Console.WriteLine("Tags updated successfully.");
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        private static void ApplyDatabaseMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StackOverflowDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
