using Flowy.Core.Contexts;
using It.Flowy.Camunda.Context;
using It.Flowy.Camunda.Models.Core.Common;
using It.Flowy.Camunda.Models.Core.Modelling;

namespace It.Flowy.Camunda.Services;

public interface IScopesService {
  Result<Scope> Search(Request request);
  Scope? GetScopeById(long id);
}

public class ScopesService : IScopesService {

  private readonly FlowyCamundaContext Context;

  public ScopesService(FlowyCamundaContext context) {
    Context = context;
  }

  public Result<Scope> Search(Request request) {
    Result<Scope> result = new () { Request = request };
    IQueryable<Scope>? queryable = Context.Scopes?
      .OrderBySort(request.Sort)
      .FiltersBy(request.Queries);
    result.Total = queryable != null ? queryable.Count() : 0;
    result.Items = queryable?.Skip(request.Offset).Take(request.Size).ToList();
    return result;
  }

  public Scope? GetScopeById(long id) {
    return Context.Scopes?.FirstOrDefault(s => s.Id.Equals(id));
  }
}