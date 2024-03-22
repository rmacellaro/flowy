using It.Flowy.Camunda.Logic;
using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Models.Core.Processing;
using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessingController : ControllerBase {

  private IProcessingLogic ProcessingLogic;

  public ProcessingController(IProcessingLogic pl) {
    ProcessingLogic = pl;
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult Start(long idProcess) {
    var res = ProcessingLogic.Start(idProcess);
    return Ok(res);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(InstanceTask), 200)]
  public IActionResult GetInstanceTaskById(long idTask) {
    var res = ProcessingLogic.GetInstanceTaskById(idTask);
    return Ok(res);
  }
  

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult GetInteractionByIdTask(long idTask) {
    var res = ProcessingLogic.GetInteractionByIdTask(idTask);
    return Ok(res);
  }
}
