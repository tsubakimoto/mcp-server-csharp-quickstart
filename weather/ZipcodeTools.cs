using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

using ModelContextProtocol.Server;

namespace QuickstartWeatherServer.Tools;

[McpServerToolType]
public static class ZipcodeTools
{
    [McpServerTool, Description("Get town name for Japan.")]
    public static async Task<string> GetTownName(
        HttpClient client,
        [Description("The zipcode to get town name for.")] string zipcode)
    {
        var response = await client.GetFromJsonAsync<ZipcodeResponse>($"/api/search?zipcode={zipcode}&limit=20");
        if (!response.Results.Any())
        {
            return "No results found.";
        }

        return string.Join("\n--\n", response.Results.Select(result =>
        {
            return $"""
                    Zipcode: {result.Zipcode}
                    Address1: {result.Address1}
                    Address2: {result.Address2}
                    Address3: {result.Address3}
                    Kana1: {result.Kana1}
                    Kana2: {result.Kana2}
                    Kana3: {result.Kana3}
                    Prefcode: {result.Prefcode}
                    """;
        }));
    }
}

public class ZipcodeResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("results")]
    public ZipcodeResult[] Results { get; set; } = [];

    [JsonPropertyName("status")]
    public int Status { get; set; }
}

public class ZipcodeResult
{
    [JsonPropertyName("address1")]
    public string Address1 { get; set; }

    [JsonPropertyName("address2")]
    public string Address2 { get; set; }

    [JsonPropertyName("address3")]
    public string Address3 { get; set; }

    [JsonPropertyName("kana1")]
    public string Kana1 { get; set; }

    [JsonPropertyName("kana2")]
    public string Kana2 { get; set; }

    [JsonPropertyName("kana3")]
    public string Kana3 { get; set; }

    [JsonPropertyName("prefcode")]
    public string Prefcode { get; set; }

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; set; }
}
