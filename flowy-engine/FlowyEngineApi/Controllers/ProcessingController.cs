using Microsoft.AspNetCore.Mvc;
using It.Flowy.Engine.Logic;
using It.Flowy.Engine.Models.Modelling;
using Newtonsoft.Json.Linq;
using It.Flowy.Engine.Models.Processing;

namespace FlowyEngineApi.Controllers.Stateman;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles = "")]
public class ProcessingController(IProcessingLogic procLog): Controller {

    private readonly IProcessingLogic ProcessingLogic = procLog;

    [Route("[action]")]
    [ProducesResponseType(typeof(List<Instance>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [HttpGet]
    public IActionResult GetInstancesByIdProcess(long idProcess) {
        var result = ProcessingLogic.GetInstancesByIdProcess(idProcess);        
        return Ok(result);
    }

    [Route("[action]")]
    [ProducesResponseType(typeof(Instance), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [HttpGet]
    public IActionResult GetInstanceByIdWire(long idWire) {
        var result = ProcessingLogic.GetInstanceByIdWire(idWire);        
        return Ok(result);
    }

    [Route("[action]")]
    [ProducesResponseType(typeof(Node), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [HttpGet]
    public IActionResult GetStartNodeByIdDistribution(long idDistribution) {
        var result = ProcessingLogic.GetStartNodeByIdDistribution(idDistribution);
        return Ok(result);
    }   

    /*[Route("[action]")]
    [ProducesResponseType(typeof(Interaction), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [HttpGet]
    public IActionResult GetInteractionWithConfigurationsById(long idInteraction) {
        var result = ProcessingLogic.GetInteractionWithConfigurationsById(idInteraction);
        return Ok(result);
    }   */ 

    [Route("[action]")]
    [ProducesResponseType(typeof(Instance), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [HttpPost]
    public IActionResult Start([FromBody] JObject request) {
        var result = ProcessingLogic.Start(request);
        return Ok(result);
    }

    [Route("[action]")]
    [ProducesResponseType(typeof(Instance), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [HttpPost]
    public IActionResult Continue([FromBody] JObject request) {
        var result = ProcessingLogic.Continue(request);
        return Ok(result);
    }
}
