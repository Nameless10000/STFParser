using AutoMapper;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;
using StateTrafficPoliceApi.Configured;
using StateTrafficPoliceApi.Jobs;
using StateTrafficPoliceApi.Services;

namespace StateTrafficPoliceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var mapper = new MapperConfiguration(mc => mc.AddProfile<MapperProfile>())
                .CreateMapper();

            builder.Services.AddSingleton(mapper);

            builder.Services.AddTransient<ParserService>();
            builder.Services.AddTransient<FlaskService>();

            var flaskData = builder.Configuration.GetSection("FlaskAPI");
            builder.Services.Configure<FlaskData>(flaskData);

            builder.Services.AddMemoryCache();
            builder.Services.AddQuartz(q =>
            {
                q.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();

                var jobKey = new JobKey("CaptchaRenewalJob", "group1");
                q.AddJob<CaptchaRenewalJob>(opts => opts
                    .WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("CaptchaRenewalTrigger", "group1")
                    .WithSimpleSchedule(x =>
                        x.WithIntervalInSeconds(55)
                        .RepeatForever())
                    .StartAt(DateTimeOffset.ParseExact("00:00:10", "HH:mm:ss", null))
                    );
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
