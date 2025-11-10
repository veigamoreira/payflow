using System.Text.Json.Serialization;

public class PaymentRequest
{
    [JsonPropertyName("transaction_amount")]
    public decimal? TransactionAmount { get; set; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }

    [JsonPropertyName("amount_cents")]
    public decimal? AmountCents { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("payer")]
    public Payer? Payer { get; set; }

    [JsonPropertyName("installments")]
    public int? Installments { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

public class Payer
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }
}