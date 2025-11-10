using System.Text.Json.Serialization;

namespace PayFlow.Domain.ValueObjects;

public class PaymentResult
{
    [JsonPropertyName("externalId")]
    public string ExternalId { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("provider")]
    public string Provider { get; set; }
    
    [JsonPropertyName("grossAmount")]
    public decimal GrossAmount { get; set; }
    
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
    
    [JsonPropertyName("netAmount")]
    public decimal NetAmount { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("status_detail")]
    public string StatusDetail { get; set; }
    
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; }
    
    [JsonPropertyName("result")]
    public string Result { get; set; }

}
