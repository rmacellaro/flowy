using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface IConfigurationsService {
    Configuration? GetConfigurationById(long id);
    List<Configuration>? GetConfigurationsByIdInteraction(long idInteraction);
    void Insert(Configuration item);
    void Update(Configuration item);
}

public class ConfigurationsService(FlowyEngineContext context) : IConfigurationsService {
    
    private readonly FlowyEngineContext Context = context;

    public Configuration? GetConfigurationById(long id){
        return Context.Configurations?.FirstOrDefault(c => c.Id.Equals(id));
    }

    public List<Configuration>? GetConfigurationsByIdInteraction(long idInteraction){
        return Context.Configurations?.Where(c => c.IdInteraction.Equals(idInteraction)).ToList();
    }

    public void Insert(Configuration item){
        item.Id = null;
        Context.Configurations?.Add(item);
        Context.Entry(item).State = EntityState.Added;
        Context.SaveChanges();
    }

    public void Update(Configuration item){
        if (item.Id == null) { throw new (nameof(Configuration.Id)); }
        Context.Configurations?.Update(item);
        Context.Entry(item).State = EntityState.Modified;
        Context.SaveChanges();
    }

}