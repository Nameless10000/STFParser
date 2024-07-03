using Quartz;
using Quartz.Simpl;
using Quartz.Spi;
using StateTrafficPoliceApi.Services;

namespace StateTrafficPoliceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddTransient<ParserService>();
            builder.Services.AddMemoryCache();
            builder.Services.AddQuartz(q =>
            {
                q.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();

                // jobs&triggers here
            });

            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
