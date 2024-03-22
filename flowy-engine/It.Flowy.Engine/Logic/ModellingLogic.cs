using It.Flowy.Engine.Models.Modelling;
using It.Flowy.Engine.Services.Modelling;

namespace It.Flowy.Engine.Logic;

public interface IModellingLogic {
  List<Process>? GetProcesses();
  Process? GetProcessById(long id);
  Distribution? GetDistributionById(long idDistribution);
}

public class ModellingLogic(
    IProcessesService procSrv,
    IDistributionsService relSrv,
    INodesService nodSrv
) : IModellingLogic {

  private readonly IProcessesService ProcessesService = procSrv;
  private readonly IDistributionsService DistributionsService = relSrv;
  private readonly INodesService NodesService = nodSrv;

  public List<Process>? GetProcesses() {
    return ProcessesService.GetProcessesWithDistributions();
  }
  
  public Process? GetProcessById(long id){
    return ProcessesService.GeProcessByIdWithDistributions(id);
  }
  
  public Distribution? GetDistributionById(long idDistribution){
    return DistributionsService.GetDistributionById(idDistribution, 
      [ "Nodes", "Nodes.Interactions", "Nodes.Interactions.Configurations"]
    );
  }
}