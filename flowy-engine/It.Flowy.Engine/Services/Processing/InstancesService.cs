using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Processing;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Processing;

public interface IInstancesService {
  List<Instance>? GetInstancesByIdProcess(long idProcess, List<string>? includes = null);
  Instance? GetInstanceById(long? id, List<string>? includes = null);
  Instance? GetInstanceByIdWire(long idWire, List<string>? includes = null);
  void Insert(Instance item);
  void Update(Instance item);
}

public class InstancesService(FlowyEngineContext context) : IInstancesService {

  private readonly FlowyEngineContext Context = context;

  public List<Instance>? GetInstancesByIdProcess(long idProcess, List<string>? includes = null){
    var query = Context.Instances?.Where(i => i.Distribution != null && i.Distribution.IdProcess.Equals(idProcess));
    query = Includes(query, includes);
    return query?.ToList();
  }

  public Instance? GetInstanceByIdWire(long idWire, List<string>? includes = null) {
    IQueryable<Instance>? query = Context.Instances?
    .Where(i => i.Wires != null && i.Wires.FirstOrDefault(w => w.Id != null && w.Id.Equals(idWire)) != null);
    query = Includes(query, includes);
    return query?.FirstOrDefault();
  }

  public Instance? GetInstanceById(long? id, List<string>? includes = null) {
    IQueryable<Instance>? query = Context.Instances?.Where(i => i.Id.Equals(id));
    query = Includes(query, includes);
    return query?.FirstOrDefault();
  }

  public void Insert(Instance item) {
    item.Id = null;
    Context.Instances?.Add(item);
    Context.Entry(item).State = EntityState.Added;
    Context.SaveChanges();
  }

  public void Update(Instance item) {
    if (item.Id == null) { throw new(nameof(Instance.Id)); }
    Context.Instances?.Update(item);
    Context.Entry(item).State = EntityState.Modified;
    Context.SaveChanges();
  }

  private static IQueryable<Instance>? Includes(IQueryable<Instance>? query, List<string>? includes = null) {
    if (includes != null) {
      foreach(string include in includes) {
        query = query?.Include(include);
      }
    }
    return query;
  }
}