using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface IDistributionsService {
  Distribution? GetDistributionById( long id, List<string>? includes = null);
  List<Distribution>? GetDistributionsByIdProcess(long idProcess, List<string>? includes = null);
}

public class DistributionsService(FlowyEngineContext context) : IDistributionsService {

  private readonly FlowyEngineContext Context = context;

  public Distribution? GetDistributionById(long id, List<string>? includes = null) {
    IQueryable<Distribution>? query = Context.Distributions?.Where(r => r.Id.Equals(id));
    query = Includes(query, includes);
    return query?.FirstOrDefault();
  }

  public List<Distribution>? GetDistributionsByIdProcess(long idProcess, List<string>? includes = null) {
    IQueryable<Distribution>? query = Context.Distributions?.Where(r => r.IdProcess.Equals(idProcess));
    query = Includes(query, includes);
    return query?.OrderByDescending(r => r.Version).ToList();
  }

  /*private IQueryable<Distribution>? Include(IQueryable<Distribution>? query, List<string>? includes = null) {
    if (withNodesInteractions) {
      query = query?.Include(r => r.Nodes!).ThenInclude(n => n.Interactions);
    } else if (withNodes) {
      query = query?.Include(r => r.Nodes);
    }
    return query;
  }

  public Distribution? GetDistributionById(long idDistribution, List<string>? includes = null){
    IQueryable<Distribution>? query = Context.Distributions?.Where(r => r.Id.Equals(idDistribution));
    query = Includes(query, includes);
    return query?.FirstOrDefault();
  }*/

  private static IQueryable<Distribution>? Includes(IQueryable<Distribution>? query, List<string>? includes = null) {
    if (includes != null) {
      foreach(string include in includes) {
        query = query?.Include(include);
      }
    }
    return query;
  }
}