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

            //IConfiguration  c = builder.Configuration;

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

            //builder.Services.AddCors(x => x.AddDefaultPolicy(cors =>
            //{
            //    cors
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .SetPreflightMaxAge(TimeSpan.FromDays(30));
            //}));

            
            
            var app = builder.Build();

            //app.UseMiddleware<MiddlewareCors>();

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

    internal class MiddlewareCors : IMiddleware
    {
        public MiddlewareCors()
        {
            
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.AccessControlAllowHeaders = "*";
            await next.Invoke(context);
        }
    }

    //internal class Startup
    //{
    //    private readonly IConfiguration _configuration;
    //    //public IConfiguration Configuration { get; }

    //    public Startup(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }


    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        // Add services to the container.

    //        services.AddControllers();
    //        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //        services.AddEndpointsApiExplorer();
    //        services.AddSwaggerGen();
    //        services.AddMediatR(typeof(Program).Assembly);
    //        services.AddValidatorsFromAssemblyContaining<Program>();
    //        services.Configure<SqlServerOptions>(_configuration.GetSection("SqlServer"));
    //        services.AddDbContext<ShortUrlContext>(
    //            (serviceProvider, builder) =>
    //            {
    //                string connectionString = serviceProvider.GetRequiredService<IOptions<SqlServerOptions>>().Value
    //                                              .ConnectionString
    //                                          ?? throw new ArgumentException($"{nameof(SqlServerOptions.ConnectionString)} not provided.");

    //                ShortUrlContext.ConfigureOptions(builder, connectionString);
    //            });
    //    }
    //}
}