using It.Flowy.Camunda.Logic;
using It.Flowy.Camunda.Models.Core.Modelling;
using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessesController : ControllerBase {

  private readonly IProcessesLogic ProcessesLogic;

  public ProcessesController(IProcessesLogic pl) {
    ProcessesLogic = pl;
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Process>), 200)]
  public IActionResult GetProcessesByIdScope(long idScope) {
    var r = ProcessesLogic.GetProcessesByIdScope(idScope);
    return Ok(r);
  }

  /*[HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<FlowNodeStatistics>), 200)]
  public IActionResult GetStatisticsByIdProcess(long idProcess) {
    var r = ProcessesManagement.GetStatisticsByIdProcess(idProcess);
    return Ok(r);
  }*/

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(string), 200)]
  public IActionResult GetSchemaByIdProcess(long idProcess){
    var r = ProcessesLogic.GetSchemaByIdProcess(idProcess);
    return Ok(r);
  }

  /*private IProcessDefinitionService OperateService;

  public TestController(
    IProcessDefinitionService ios
  ){
    OperateService = ios;
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(object), 200)]
  public IActionResult PrimoTest(long key) {
    //var r = OperateService.GetProcessInstances(new () { Size = 10});
    var r = OperateService.GetProcessDefinitionSchemaByKey(key);
    return Ok(r);
  }*/
  
}
