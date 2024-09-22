using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();





app.MapHub<SensorHub>("/sensorhub");

app.MapPost("/sendSensorData", async (IHubContext<SensorHub> hubContext, SensorDataDto sensorDataDto) =>
{
    // POST isteği ile gelen mesajı SignalR istemcilerine gönder
    await hubContext.Clients.All.SendAsync("ReceiveSensorData",sensorDataDto.sensorId,sensorDataDto.sensorName,sensorDataDto.sensorData);
    

    return Results.Ok("Message sent to all clients");
})
.WithName("PostSensorData")
.WithOpenApi();




app.Run();


public record SensorDataDto(Guid sensorId,string sensorName,decimal sensorData);
