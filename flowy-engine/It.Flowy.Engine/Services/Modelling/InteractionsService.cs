using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface IInteractionsService {
    Interaction? GetInteractionById(long id, bool withConfigurations = false);
    List<Interaction>? GetInteractionsByIdNode(long idNode, bool withConfigurations = false);
    void Insert(Interaction item);
    void Update(Interaction item);
}

public class InteractionsService(FlowyEngineContext context): IInteractionsService {
    
    private readonly FlowyEngineContext Context = context;

    public Interaction? GetInteractionById(long id, bool withConfigurations = false){
        IQueryable<Interaction>? quary = Context.Interactions?.Where(i => i.Id.Equals(id));
        if (withConfigurations){ quary = quary?.Include(i => i.Configurations);}
        return quary?.FirstOrDefault();
    }
    
    public List<Interaction>? GetInteractionsByIdNode(long idNode, bool withConfigurations = false){
        IQueryable<Interaction>? quary = Context.Interactions?.Where(i => i.IdNode.Equals(idNode));
        if (withConfigurations){ quary = quary?.Include(i => i.Configurations);}
        return quary?.ToList();
    }

    public void Insert(Interaction item){
        item.Id = null;
        Context.Interactions?.Add(item);
        Context.Entry(item).State = EntityState.Added;
        Context.SaveChanges();
    }

    public void Update(Interaction item){
        if (item.Id == null) { throw new (nameof(Node.Id)); }
        Context.Interactions?.Update(item);
        Context.Entry(item).State = EntityState.Modified;
        Context.SaveChanges();
    }
}