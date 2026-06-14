namespace OpenMeteo;

public class OpenMeteoResponse
{
    public CurrentWeather Current { get; set; }
}

public class CurrentWeather
{
    public double Temperature_2m { get; set; }
    public double ApparentTemperature { get; set; }
}