using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Camunda.Models.Core.Processing;

[Table("InstanceTraks", Schema = "Processing")]
public class InstanceTrack {
  public long Id { get; set; }

  [ForeignKey("Instance")]
  public long IdInstance { get; set; }
  public Instance? Instance { get; set; }
  public DateTime? EventAt { get; set; }
  public string? Operation { get; set; }
  public string? Note { get; set; }
  public string? Data { get; set; }


  #region CamundaRuntime
  [NotMapped]
  public long? KeyFlowNodeInstance { get; set; }
  [NotMapped]
  public DateTime? StartDate { get; set; }
  [NotMapped]
  public DateTime? EndDate { get; set; }
  [NotMapped]
  public string? FlowNodeId { get; set; }
  [NotMapped]
  public string? FlowNodeName { get; set; }
  [NotMapped]
  public string? Type { get; set; }
  [NotMapped]
  public string? State { get; set; }
  [NotMapped]
  public bool Incident { get; set; }
  [NotMapped]
  public string? TenantId { get; set; }
  #endregion
}