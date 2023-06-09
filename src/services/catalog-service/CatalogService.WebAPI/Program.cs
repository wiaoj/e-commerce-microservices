using CatalogService.Application;
using CatalogService.Persistence;
using MassTransit;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices()
				.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options => {
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	//options.JsonSerializerOptions.Converters.AddApplicationJsonConverters();
});

builder.Services.AddMassTransit(busRegistrationConfigurator => {
	busRegistrationConfigurator.SetKebabCaseEndpointNameFormatter();
	busRegistrationConfigurator.UsingRabbitMq((context, configurator) => {
		configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
		configurator.ConfigureEndpoints(context);
	});
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
