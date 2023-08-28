using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Gmsca.HelpMeChoose.Individual.Configs;
using Gmsca.HelpMeChoose.Individual.Interfaces;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    NullValueHandling = NullValueHandling.Ignore
};

var webBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

webBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
webBuilder.Services.AddEndpointsApiExplorer();
webBuilder.Services.AddSwaggerGen();

webBuilder.Services.AddMvc()
  .AddJsonOptions(options =>
  {
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });

webBuilder.Services.Configure<CosmosDbConfig>(
    webBuilder.Configuration.GetSection(CosmosDbConfig.CosmosDbSection)
);

webBuilder.Services.AddSingleton(_ =>
{
    return new CosmosClient(webBuilder.Configuration["cosmos-connection-string"],
            new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Gateway
            });
});

webBuilder.Services.AddSingleton<ICosmosService, CosmosService>();
webBuilder.Services.AddSingleton<IPricingService, PricingService>();
webBuilder.Services.AddSingleton<HttpClient, HttpClient>();
webBuilder.Services.AddSingleton<IVisionRecommendation, VisionRecommendation>();
webBuilder.Services.AddSingleton<ITravelRecommendation, TravelRecommendation>();
webBuilder.Services.AddSingleton<IHealthRecommendation, HealthRecommendation>();
webBuilder.Services.AddSingleton<IMentalHealthRecommendation, MentalHealthRecommendation>();
webBuilder.Services.AddSingleton<IDrugRecommendation, DrugRecommendation>();
webBuilder.Services.AddSingleton<IDentalRecommendation, DentalRecommendation>();
webBuilder.Services.AddSingleton<IRecommendationService, RecommendationService>();

if (webBuilder.Environment.IsDevelopment())
{
    webBuilder.Services.AddCors(options => options.AddPolicy(name: "developmentOrigins", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
}

var app = webBuilder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("developmentOrigins");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
