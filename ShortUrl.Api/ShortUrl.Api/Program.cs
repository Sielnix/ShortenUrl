using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShortUrl.Api.Infrastructure;
using ShortUrl.Persistence;

namespace ShortUrl.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(typeof(Program).Assembly);
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.Configure<SqlServerOptions>(builder.Configuration.GetSection("SqlServer"));
            builder.Services.AddDbContext<ShortUrlContext>(
                (serviceProvider, dbContextBuilder) =>
                {
                    string connectionString = serviceProvider.GetRequiredService<IOptions<SqlServerOptions>>().Value
                                                  .ConnectionString
                                              ?? throw new ArgumentException($"{nameof(SqlServerOptions.ConnectionString)} not provided.");

                    ShortUrlContext.ConfigureOptions(dbContextBuilder, connectionString);
                });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetPreflightMaxAge(TimeSpan.FromDays(30)));


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                ShortUrlContext context = scope.ServiceProvider.GetRequiredService<ShortUrlContext>();
                await context.Database.MigrateAsync();
            }

            await app.RunAsync();
        }
    }
}