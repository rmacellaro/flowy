using Microsoft.AspNetCore.Mvc;
using It.Flowy.Engine.Logic;
using It.Flowy.Engine.Models.Modelling;

namespace FlowyEngineApi.Controllers.Stateman;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles = "")]
public class ModellingController(IModellingLogic modLog): Controller {

  private readonly IModellingLogic ModellingLogic = modLog;

  [Route("[action]")]
  [ProducesResponseType(typeof(List<Process>), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpGet]
  public IActionResult GetProcesses() {
    var result = ModellingLogic.GetProcesses();
    return Ok(result);
  }   

  [Route("[action]")]
  [ProducesResponseType(typeof(Process), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpGet]
  public IActionResult GetProcessById(long id) {
    var result = ModellingLogic.GetProcessById(id);
    return Ok(result);
  }

  [Route("[action]")]
  [ProducesResponseType(typeof(Distribution), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpGet]
  public IActionResult GetDistributionById(long idDistribution) {
    var result = ModellingLogic.GetDistributionById(idDistribution);
    return Ok(result);
  }
}