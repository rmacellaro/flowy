using It.Flowy.Camunda.Logic;
using It.Flowy.Camunda.Models.Core.Common;
using It.Flowy.Camunda.Models.Core.Modelling;
using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ScopesController : ControllerBase {

  private readonly IScopesLogic ScopesLogic;

  public ScopesController(IScopesLogic sl) {
    ScopesLogic = sl;
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Result<Scope>), 200)]
  public IActionResult Search(Request request) {
    Result<Scope> result = ScopesLogic.Search(request);
    return Ok(result);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Scope), 200)]
  public IActionResult GetScopeById(long id) {
    return Ok(ScopesLogic.GetScopeById(id));
  }
  
}
