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

  [Route("[action]")]
  [ProducesResponseType(typeof(List<NodeDataType>), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpGet]
  public IActionResult GetNodeDataTypes() {
    var result = ModellingLogic.GetNodeDataTypes();
    return Ok(result);
  }

  [Route("[action]")]
  [ProducesResponseType(typeof(List<NodeData>), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpPost]
  public IActionResult SaveNodeDatas([FromBody] List<NodeData> nodeConfigs) {
    ModellingLogic.SaveNodeDatas(nodeConfigs);
    return Ok(nodeConfigs);
  }
  
  [Route("[action]")]
  [ProducesResponseType(typeof(NodeData), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpPost]
  public IActionResult SaveNodeData([FromBody] NodeData nodeData) {
    ModellingLogic.SaveNodeData(nodeData);
    return Ok(nodeData);
  }


  [Route("[action]")]
  [ProducesResponseType(typeof(List<ActivityDefinition>), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpGet]
  public IActionResult GetActivityDefinitions() {
    var result = ModellingLogic.GetActivityDefinitions();
    return Ok(result);
  }

  [Route("[action]")]
  [ProducesResponseType(typeof(ActivityDefinition), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(401)]
  [ProducesResponseType(500)]
  [HttpGet]
  public IActionResult GetActivityDefinitionById(long idActivityDefinition) {
    var result = ModellingLogic.GetActivityDefinitionById(idActivityDefinition);
    return Ok(result);
  }
}