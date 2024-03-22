using It.Flowy.Camunda.Logic;
using It.Flowy.Camunda.Models.Core.Modelling;
using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InteractionsController : ControllerBase {

  private IInteractionsLogic InteractionsLogic;

  public InteractionsController(IInteractionsLogic il) {
    InteractionsLogic = il;
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Interaction>), 200)]
  public IActionResult GetInteractionsByIdScope(long idScope) {
    var r = InteractionsLogic.GetInteractionsByIdScope(idScope);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult GetInteractionById(long id) {
    var r = InteractionsLogic.GetInteractionById(id);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Interaction>), 200)]
  public IActionResult GetInteractionTracksByIdDraft(long idInteraction) {
    var r = InteractionsLogic.GetInteractionTracksByIdInteraction(idInteraction);
    return Ok(r);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateInteractionData(Interaction interaction) {
    InteractionsLogic.UpdateInteractionData(interaction);
    return Ok();
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateInteractionInfo(Interaction interaction) {
    InteractionsLogic.UpdateInteractionInfo(interaction);
    return Ok();
  }

  [HttpPut]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult CloneInteraction([FromQuery] long idInteraction) {
    var newi = InteractionsLogic.CloneInteraction(idInteraction);
    return Ok(newi);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult NewInteraction(Interaction interaction) {
    InteractionsLogic.NewInteraction(interaction);
    return Ok(interaction);
  }
}