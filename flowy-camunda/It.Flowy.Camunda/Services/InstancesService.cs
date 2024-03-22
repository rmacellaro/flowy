using Flowy.Core.Contexts;
using It.Flowy.Camunda.Context;
using It.Flowy.Camunda.Models.Core.Common;
using It.Flowy.Camunda.Models.Core.Processing;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Camunda.Services;

public interface IInstancesService {
  Instance? GetInstanceById(long id);
  Instance? GetInstanceByKey(long key);
  List<InstanceData>? GetInstanceDatasByIdInstance(long id);
  List<InstanceTrack>? GetInstanceTracksByIdInstance(long id);
  Result<Instance> Search(Request request);
  void Insert(Instance instance);
  void Update(Instance instance);
}

public class InstancesService : IInstancesService {

  private readonly FlowyCamundaContext Context;

  public InstancesService(FlowyCamundaContext context) {
    Context = context;
  }
  
  public Instance? GetInstanceById(long id){
    return Context.Instances?.FirstOrDefault(i => i.Id.Equals(id));
  }

  public Instance? GetInstanceByKey(long key){
    return Context.Instances?.FirstOrDefault(i => i.Key.Equals(key));
  }

  public List<InstanceData>? GetInstanceDatasByIdInstance(long id) {
    return Context.InstanceDatas?.Where(idat => idat.IdInsatnce.Equals(id)).ToList();
  }
  
  public List<InstanceTrack>? GetInstanceTracksByIdInstance(long id){
    return Context.InstanceTracks?.Where(it => it.IdInstance.Equals(id)).ToList();
  }

  public Result<Instance> Search(Request request) {
    Result<Instance> result = new () { Request = request };
    IQueryable<Instance>? queryable = Context.Instances?
      .OrderBySort(request.Sort)
      .FiltersBy(request.Queries);
    result.Total = queryable != null ? queryable.Count() : 0;
    result.Items = queryable?.Skip(request.Offset).Take(request.Size).ToList();
    return result;
  }


  public void Insert(Instance instance){
    Context.Entry(instance).State = EntityState.Added;
    Context.Add(instance);
    Context.SaveChanges();
  }

  public void Update(Instance instance){
    if (instance.Id <= 0){ throw new Exception("instance no update with id 0");}
    Context.Entry(instance).State = EntityState.Modified;
    Context.Update(instance);
    Context.SaveChanges();
  }
}