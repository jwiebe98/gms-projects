using Gmsca.Group.GA.Backend.Configs;
using Gmsca.Group.GA.Backend.Services.Cosmos;
using Gmsca.Group.GA.Backend.Services.Pricing;
using Gmsca.Group.GA.Backend.Services.Rates;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
webBuilder.Services.AddSingleton<IRateService, RateService>();
webBuilder.Services.AddSingleton<IPricingService, PricingService>();

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
