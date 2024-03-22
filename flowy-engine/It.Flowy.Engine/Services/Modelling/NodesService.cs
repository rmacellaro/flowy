using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface INodesService {
  Node? GetNodeById(long id, List<string>? includes = null);
  List<Node>? GetNodesByIdDistribution(long idRelase, List<string>? includes = null);
  Node? GetNodeByKeyAndIdDistribution(string key, long idDistribution, List<string>? includes = null);
  void Insert(Node item);
  void Update(Node item);
}

public class NodesService(FlowyEngineContext context) : INodesService {

  private readonly FlowyEngineContext Context = context;

  public Node? GetNodeById(long id, List<string>? includes = null) {
    IQueryable<Node>? query = Context.Nodes?.Where(n => n.Id.Equals(id));
    query = Includes(query, includes);
    return query?.FirstOrDefault();
  }

  public List<Node>? GetNodesByIdDistribution(long idDistribution, List<string>? includes = null) {
    IQueryable<Node>? query = Context.Nodes?.Where(q => q.IdDistribution.Equals(idDistribution));
    query = Includes(query, includes);
    return query?.ToList();
  }

  public Node? GetNodeByKeyAndIdDistribution(string key, long idRelase, List<string>? includes = null) {
    IQueryable<Node>? query = Context.Nodes?.Where(q =>
      q.Key != null &&
      q.Key.Equals(key) &&
      q.IdDistribution.Equals(idRelase)
    );
    query = Includes(query, includes);
    return query?.First();
  }

  public void Insert(Node item) {
    item.Id = null;
    Context.Nodes?.Add(item);
    Context.Entry(item).State = EntityState.Added;
    Context.SaveChanges();
  }

  public void Update(Node item) {
    if (item.Id == null) { throw new(nameof(Node.Id)); }
    Context.Nodes?.Update(item);
    Context.Entry(item).State = EntityState.Modified;
    Context.SaveChanges();
  }  

  private static IQueryable<Node>? Includes(IQueryable<Node>? query, List<string>? includes = null) {
    if (includes != null) {
      foreach(string include in includes) {
        query = query?.Include(include);
      }
    }
    return query;
  }
}