using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Services;
using log4net;

namespace It.Flowy.Camunda.Logic;

public interface IInteractionsLogic {
  ICollection<Interaction>? GetInteractionsByIdScope(long idScope);
  Interaction? GetInteractionById(long id);
  ICollection<InteractionTrack>? GetInteractionTracksByIdInteraction(long idInteraction);
  void UpdateInteractionData(Interaction interaction);
  void UpdateInteractionInfo(Interaction interaction);
  Interaction CloneInteraction(long idInteraction);
  Interaction NewInteraction(Interaction interaction);
}

public class InteractionsLogic : IInteractionsLogic {

  private static readonly ILog Log = LogManager.GetLogger(typeof(InteractionsLogic));
  
  private readonly IInteractionsService InteractionsService;
  public InteractionsLogic(
    IInteractionsService interactionsService
  ){
    InteractionsService = interactionsService;
  }

  public ICollection<Interaction>? GetInteractionsByIdScope(long idScope) {
    Log.Debug("START idScope:" + idScope);
    return InteractionsService.GetInteractionsByIdScope(idScope);
  }

  public Interaction? GetInteractionById(long id) {
    Log.Debug("START GetInteractionById id:" + id);
    return InteractionsService.GetInteractionById(id);
  }

  public ICollection<InteractionTrack>? GetInteractionTracksByIdInteraction(long idInteraction){
    Log.Debug("START idInteraction:" + idInteraction);
    return InteractionsService.GetInteractionTracksByIdInteraction(idInteraction);
  }

  public void UpdateInteractionData(Interaction interaction) {
    // recupero la interaction dal database
    Interaction? interactionDb = InteractionsService.GetInteractionById(interaction.Id);
    if (interactionDb == null) { throw new Exception("Interaction with id : " + interaction.Id + ", not found!");}
    // aggiorno lo schema
    string? oldData = interaction.Data;
    interactionDb.Data = interaction.Data;
    InteractionsService.UpdateInteraction(interactionDb);
    // aggiungo un a tracciatura
    InteractionsService.InsertInteractionTrack(new (){
      IdInteraction = interactionDb.Id,
      Interaction = interactionDb,
      EventAt = DateTime.Now,
      DataBackup = oldData,
      Operation = "UPDATE_DATA"
    });
  }

  public void UpdateInteractionInfo(Interaction interaction){
    // recupero la interaction dal database
    Interaction? interactionDB = InteractionsService.GetInteractionById(interaction.Id);
    if (interactionDB == null) { throw new Exception("Interaction with id : " + interaction.Id + ", not found!");}
    string oldValues = "Valori precedenti: " + interaction.Name + ";" + interaction.Description;
    // aggiorno lo schema
    interactionDB.Name = interaction.Name;
    interactionDB.Description = interaction.Description;
    InteractionsService.UpdateInteraction(interactionDB);
    // aggiungo un a tracciatura
    InteractionsService.InsertInteractionTrack(new (){
      IdInteraction = interactionDB.Id,
      Interaction = interactionDB,
      EventAt = DateTime.Now,
      Operation = "UPDATE_INFO",
      Description = oldValues
    });
  }

  public Interaction CloneInteraction(long idInteraction) {
    // recupero la bozza dal database
    Interaction? interaction = InteractionsService.GetInteractionById(idInteraction);
    if (interaction == null) { throw new Exception("Interaction with id : " + idInteraction + ", not found!");}

    // creo un nuovo draft
    Interaction newInteraction = new () {
      Name = interaction.Name + " (Copy)",
      Description = interaction.Description,
      IdScope = interaction.IdScope,
      Data = interaction.Data
    };
    InteractionsService.InsertInteraction(newInteraction);

    // aggiungo una tracciatura
    InteractionsService.InsertInteractionTrack(new (){
      IdInteraction = newInteraction.Id,
      Interaction = newInteraction,
      EventAt = DateTime.Now,
      Operation = "CLONE_INTERACTION",
      Description = "Clonate from : " + interaction.Name + " [" + interaction.Id+ "]"
    });

    return newInteraction;
  }

  public Interaction NewInteraction(Interaction interaction) {
    InteractionsService.InsertInteraction(interaction);

    // aggiungo una tracciatura
    InteractionsService.InsertInteractionTrack(new (){
      IdInteraction = interaction.Id,
      Interaction = interaction,
      EventAt = DateTime.Now,
      Operation = "NEW"
    });

    return interaction;
  }

}