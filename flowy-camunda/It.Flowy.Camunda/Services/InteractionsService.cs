using It.Flowy.Camunda.Context;
using It.Flowy.Camunda.Models.Core.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Camunda.Services;

public interface IInteractionsService {
  ICollection<Interaction>? GetInteractionsByIdScope(long idScope);
  Interaction? GetInteractionById(long id);
  Interaction? GetInteractionByName(string name);
  void InsertInteraction(Interaction interaction);
  void UpdateInteraction(Interaction interaction);
  ICollection<InteractionTrack>? GetInteractionTracksByIdInteraction(long idInteraction);
  void InsertInteractionTrack(InteractionTrack track);
}

public class InteractionsService : IInteractionsService {

  private readonly FlowyCamundaContext Context;

  public InteractionsService(FlowyCamundaContext context) {
    Context = context;
  }
  
  public ICollection<Interaction>? GetInteractionsByIdScope(long idScope){
    return Context.Interactions?.Where(i => i.IdScope.Equals(idScope)).Select(i => new Interaction(){
      Id = i.Id,
      IdScope = i.IdScope,
      Name = i.Name,
      Description = i.Description,
      Type = i.Type
    }).ToList();
  }

  public Interaction? GetInteractionById(long id) {
    return Context.Interactions?.FirstOrDefault(i => i.Id.Equals(id));
  }

  public Interaction? GetInteractionByName(string name) {
    return Context.Interactions?.FirstOrDefault(i => i.Name != null && i.Name.Equals(name));
  }

  public void InsertInteraction(Interaction interaction){
    Context.Entry(interaction).State = EntityState.Added;
    Context.Add(interaction);
    Context.SaveChanges();
  }

  public void UpdateInteraction(Interaction interaction){
    if (interaction.Id <= 0){ throw new Exception("Interaction no update with id 0");}
    Context.Entry(interaction).State = EntityState.Modified;
    Context.Update(interaction);
    Context.SaveChanges();
  }

  public ICollection<InteractionTrack>? GetInteractionTracksByIdInteraction(long idInteraction) {
    return Context.InteractionTracks?.Where(d => d.IdInteraction.Equals(idInteraction)).Select(s => new InteractionTrack(){
      Id = s.Id,
      IdInteraction = s.IdInteraction,
      Operation = s.Operation,
      Description = s.Description,
      EventAt = s.EventAt,
      UserIdentifier = s.UserIdentifier
    }).ToList();
  }

  public void InsertInteractionTrack(InteractionTrack track) {
    Context.Entry(track).State = EntityState.Added;
    Context.Add(track);
    Context.SaveChanges();
  }
}