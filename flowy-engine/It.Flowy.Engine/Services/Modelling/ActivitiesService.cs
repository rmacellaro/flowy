using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface IActivitiesService {
  Activity? GetActivityById(long id, List<string>? includes = null);
  void Insert(Activity item);
  void Update(Activity item);
}

public class ActivitiesService(FlowyEngineContext context) : IActivitiesService {

  private readonly FlowyEngineContext Context = context;

  public Activity? GetActivityById(long id, List<string>? includes = null){
    IQueryable<Activity>? query = Context.Activities?.Where(n => n.Id.Equals(id));
    query = Includes(query, includes);
    return query?.FirstOrDefault();
  }

  public void Insert(Activity item){
    item.Id = null;
    Context.Activities?.Add(item);
    Context.Entry(item).State = EntityState.Added;
    Context.SaveChanges();
  }

  public void Update(Activity item){
    if (item.Id == null) { throw new(nameof(Activity.Id)); }
    Context.Activities?.Update(item);
    Context.Entry(item).State = EntityState.Modified;
    Context.SaveChanges();
  }

  private static IQueryable<Activity>? Includes(IQueryable<Activity>? query, List<string>? includes = null) {
    if (includes != null) {
      foreach(string include in includes) {
        query = query?.Include(include);
      }
    }
    return query;
  }
}