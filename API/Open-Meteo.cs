using System.Net.Http.Json;
using System.Text.Json;
namespace OpenMeteo;

public class OpenMeteoResponse
{
    public double Temperature { get; set; }
    public double apparent_Temperature { get; set; }
}