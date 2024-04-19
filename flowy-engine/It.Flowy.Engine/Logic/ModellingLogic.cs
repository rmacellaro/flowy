using System.Data.Common;
using It.Flowy.Engine.Models.Modelling;
using It.Flowy.Engine.Services.Modelling;

namespace It.Flowy.Engine.Logic;

public interface IModellingLogic {
  List<Process>? GetProcesses();
  Process? GetProcessById(long id);
  Distribution? GetDistributionById(long idDistribution);

  List<NodeDataType>? GetNodeDataTypes();
  void SaveNodeDatas(List<NodeData> nodeDatas);
  void SaveNodeData(NodeData nodeData);
  List<ActivityDefinition>? GetActivityDefinitions();
  ActivityDefinition? GetActivityDefinitionById(long id);
}

public class ModellingLogic(
    IProcessesService procSrv,
    IDistributionsService relSrv,
    INodesService nodSrv,
    INodeDatasService nodDatSrv,
    INodeDataTypesService nodDatTypSrc,
    IActivityDefinitionsService actDefSrv
) : IModellingLogic {

  private readonly IProcessesService ProcessesService = procSrv;
  private readonly IDistributionsService DistributionsService = relSrv;
  private readonly INodesService NodesService = nodSrv;
  private readonly INodeDatasService NodeDatasService = nodDatSrv;
  private readonly INodeDataTypesService NodeDataTypesService = nodDatTypSrc;
  private readonly IActivityDefinitionsService ActivityDefinitionsService = actDefSrv;

  public List<Process>? GetProcesses() {
    return ProcessesService.GetProcessesWithDistributions();
  }
  
  public Process? GetProcessById(long id){
    return ProcessesService.GeProcessByIdWithDistributions(id);
  }
  
  public Distribution? GetDistributionById(long idDistribution){
    return DistributionsService.GetDistributionById(idDistribution, 
      [ "Nodes", "Nodes.Datas", "Nodes.Activities", "Nodes.Activities.Datas", "Nodes.OutputLinks"]
    );
  }

  public List<NodeDataType>? GetNodeDataTypes(){
    return NodeDataTypesService.GetAllNodeDataTypes();
  }

  public void SaveNodeDatas(List<NodeData> nodeDatas){
    nodeDatas.ForEach((NodeData nc) => { SaveNodeData(nc); }) ;
  }

  public void SaveNodeData(NodeData nodeData){
    if (!nodeData.IdNode.HasValue) { throw new Exception("Id Node not found in nodeData to save!");}
    if (string.IsNullOrEmpty(nodeData.Name)) { throw new Exception("Name Node not found in nodeData to save!");}
    if (nodeData.Id.HasValue && nodeData.Id.Value > 0) {
      NodeDatasService.Update(nodeData);
    } else {
      // recupero l'id nodedatatype
      NodeDataType? datatype = NodeDataTypesService.GetNodeDataTypeByName(nodeData.Name);
      if (datatype != null){
        nodeData.IdNodeDataType = datatype.Id;
        //nodeData.NodeDataType = datatype;
      }
      NodeDatasService.Insert(nodeData);
    }
  }

  public List<ActivityDefinition>? GetActivityDefinitions(){
    return ActivityDefinitionsService.GetActivityDefinitionsWithDataTypes();
  }

  public ActivityDefinition? GetActivityDefinitionById(long id) {
    return ActivityDefinitionsService.GetActivityDefinitionById(id);
  }
}