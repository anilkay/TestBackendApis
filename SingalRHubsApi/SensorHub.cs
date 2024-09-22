using Microsoft.AspNetCore.SignalR;


public interface ISensorHub
{
    Task ReceiveSensorData(Guid sensorId, string sensorName, decimal sensorData);
}

public class SensorHub: Hub<ISensorHub>
{
    public async Task SendSensorData(Guid sensorId,string sensorName,decimal sensorData)
    {
        await  Clients.All.ReceiveSensorData(sensorId, sensorName, sensorData);
    }

}