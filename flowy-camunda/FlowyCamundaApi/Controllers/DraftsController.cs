using It.Flowy.Camunda.Logic;
using It.Flowy.Camunda.Models.Core.Modelling;
using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DraftsController : ControllerBase {

  private IDraftsLogic DraftsLogic;

  public DraftsController(IDraftsLogic dl) {
    DraftsLogic = dl;
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Draft>), 200)]
  public IActionResult GetDraftsByIdScope(long idScope) {
    var r = DraftsLogic.GetDraftsByIdScope(idScope);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Draft), 200)]
  public IActionResult GetDraftById(long idDraft) {
    var r = DraftsLogic.GetDraftById(idDraft);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Draft>), 200)]
  public IActionResult GetDraftTracksByIdDraft(long idDraft) {
    var r = DraftsLogic.GetDraftTracksByIdDraft(idDraft);
    return Ok(r);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateDraftSchema(Draft draft) {
    DraftsLogic.UpdateDraftSchema(draft);
    return Ok();
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateDraftInfo(Draft draft) {
    DraftsLogic.UpdateDraftInfo(draft);
    return Ok();
  }

  [HttpPut]
  [Route("[action]")]
  [ProducesResponseType(typeof(Draft), 200)]
  public IActionResult CloneDraft([FromQuery] long idDraft) {
    var newDraft = DraftsLogic.CloneDraft(idDraft);
    return Ok(newDraft);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Draft), 200)]
  public IActionResult NewDraft(Draft draft) {
    DraftsLogic.NewDraft(draft);
    return Ok(draft);
  }

  [HttpPut]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Process>), 200)]
  public IActionResult DeployDraft([FromQuery] long idDraft) {
    var r = DraftsLogic.DeployDraft(idDraft);
    return Ok(r);
  }
}
