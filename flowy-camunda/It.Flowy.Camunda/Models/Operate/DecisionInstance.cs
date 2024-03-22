using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class DecisionInstance {

  [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Id	{ get; set; }

  [JsonProperty("key", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long Key	{ get; set; }

  /// <summary>
  /// FAILED, EVALUATED, UNKNOWN, UNSPECIFIED
  /// </summary>
  [JsonProperty("state", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? State	{ get; set; }

  [JsonProperty("evaluationDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? EvaluationDate { get; set; }

  [JsonProperty("evaluationFailure", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? EvaluationFailure { get; set; }

  [JsonProperty("processDefinitionKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long ProcessDefinitionKey { get; set; }

  [JsonProperty("processInstanceKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long ProcessInstanceKey { get; set; }

  [JsonProperty("decisionId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DecisionId { get; set; }

  [JsonProperty("decisionDefinitionId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DecisionDefinitionId { get; set; }

  [JsonProperty("decisionName", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DecisionName { get; set; }

  [JsonProperty("decisionVersion", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public int DecisionVersion { get; set; }

  /// <summary>
  /// DECISION_TABLE, LITERAL_EXPRESSION, UNSPECIFIED, UNKNOWN
  /// </summary>
  [JsonProperty("decisionType", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DecisionType { get; set; }

  [JsonProperty("result", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Result { get; set; }

  [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TenantId { get; set; }

  [JsonProperty("evaluatedInputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<Input>? EvaluatedInputs{ get; set; }

  [JsonProperty("evaluatedOutputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<Output>? EvaluatedOutputs{ get; set; }

}