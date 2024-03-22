using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase {

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
