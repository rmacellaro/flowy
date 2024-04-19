using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface IActivityDefinitionsService {
  List<ActivityDefinition>? GetActivityDefinitionsWithDataTypes();
  ActivityDefinition? GetActivityDefinitionById(long id);
  void Insert(ActivityDefinition item);
  void Update(ActivityDefinition item);
}

public class ActivityDefinitionsService(FlowyEngineContext context) : IActivityDefinitionsService {

  private readonly FlowyEngineContext Context = context;

  public List<ActivityDefinition>? GetActivityDefinitionsWithDataTypes(){
    return Context.ActivityDefinitions?.Include(a => a.DataTypes).ToList();
  }
  public ActivityDefinition? GetActivityDefinitionById(long id){
    return Context.ActivityDefinitions?.Include(a => a.DataTypes).FirstOrDefault(a => a.Id == id);
  }

  public void Insert(ActivityDefinition item){
    item.Id = null;
    Context.ActivityDefinitions?.Add(item);
    Context.Entry(item).State = EntityState.Added;
    Context.SaveChanges();
  }

  public void Update(ActivityDefinition item){
    if (item.Id == null) { throw new(nameof(Activity.Id)); }
    Context.ActivityDefinitions?.Update(item);
    Context.Entry(item).State = EntityState.Modified;
    Context.SaveChanges();
  }
  
}