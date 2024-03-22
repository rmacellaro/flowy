using It.Flowy.Camunda.Models.Core.Common;
using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Services;
using log4net;

namespace It.Flowy.Camunda.Logic;

public interface IScopesLogic {
  Result<Scope> Search(Request request);
  Scope? GetScopeById(long id);
}

public class ScopesLogic : IScopesLogic {
  private static readonly ILog Log = LogManager.GetLogger(typeof(ScopesLogic));
  private readonly IScopesService ScopesService ;

  public ScopesLogic(
    IScopesService ss
  ){
    ScopesService = ss;
  }

  public Result<Scope> Search(Request request) {
    try {
      Log.Debug("Start Search");
      return ScopesService.Search(request);
    } catch(Exception ex) {
      Log.Error(ex);
      throw;
    }
  }

  public Scope? GetScopeById(long id) {
    return ScopesService.GetScopeById(id);
  }
}