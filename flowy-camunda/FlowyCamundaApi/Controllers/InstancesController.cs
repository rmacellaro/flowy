using It.Flowy.Camunda.Logic;
using It.Flowy.Camunda.Models.Core.Common;
using It.Flowy.Camunda.Models.Core.Processing;
using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InstancesController : ControllerBase {

  private IInstancesLogic InstancesLogic;

  public InstancesController(IInstancesLogic il) {
    InstancesLogic = il;
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Result<Instance>), 200)]
  public IActionResult GetInstancesByIdProcess(Request request) {
    var r = InstancesLogic.GetInstancesByIdProcess(request);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Instance), 200)]
  public IActionResult GetInstanceById(long id) {
    var r = InstancesLogic.GetInstanceById(id);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<InstanceData>), 200)]
  public IActionResult GetInstanceDatasByIdInstance(long idInstance) {
    var r = InstancesLogic.GetInstanceDatasByIdInstance(idInstance);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<InstanceTrack>), 200)]
  public IActionResult GetInstanceTracksByIdInstance(long idInstance) {
    var r = InstancesLogic.GetInstanceTracksByIdInstance(idInstance);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<InstanceTask>), 200)]
  public IActionResult GetInstanceTasksByIdInstance(long idInstance) {
    var r = InstancesLogic.GetInstanceTasksByIdInstance(idInstance);
    return Ok(r);
  }
  
}