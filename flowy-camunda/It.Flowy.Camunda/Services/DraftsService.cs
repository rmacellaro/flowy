using It.Flowy.Camunda.Context;
using It.Flowy.Camunda.Models.Core.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Camunda.Services;

public interface IDraftsService {
  ICollection<Draft>? GetDraftsByIdScope(long idScope);
  Draft? GetDraftById(long id);
  ICollection<DraftTrack>? GetDraftTracksByIdDraft(long idDraft);
  void InsertDraftTrack(DraftTrack track);
  DraftTrack? GetDraftTrackById(long idDraftTrack);
  void InsertDraft(Draft draft);
  void UpdateDraft(Draft draft);
}

public class DraftsService : IDraftsService {

  private readonly FlowyCamundaContext Context;

  public DraftsService(FlowyCamundaContext context) {
    Context = context;
  }

  public ICollection<Draft>? GetDraftsByIdScope(long idScope) {
    return Context.Drafts?.Where(d => d.IdScope.Equals(idScope)).Select(s => new Draft(){
      Id = s.Id,
      Name = s.Name,
      Description = s.Description,
      IdScope = s.IdScope
    }).ToList();
  }

  public Draft? GetDraftById(long id){
    return Context.Drafts?.FirstOrDefault(d => d.Id.Equals(id));
  }

  public ICollection<DraftTrack>? GetDraftTracksByIdDraft(long idDraft) {
    return Context.DraftTracks?.Where(d => d.IdDraft.Equals(idDraft)).Select(s => new DraftTrack(){
      Id = s.Id,
      IdDraft = s.IdDraft,
      Operation = s.Operation,
      Description = s.Description,
      EventAt = s.EventAt,
      UserIdentifier = s.UserIdentifier
    }).ToList();
  }

  public void InsertDraftTrack(DraftTrack track) {
    Context.Entry(track).State = EntityState.Added;
    Context.Add(track);
    Context.SaveChanges();
  }

  public DraftTrack? GetDraftTrackById(long idDraftTrack){
    return Context.DraftTracks?.FirstOrDefault(d => d.Id.Equals(idDraftTrack));
  }
  
  public void InsertDraft(Draft draft){
    Context.Entry(draft).State = EntityState.Added;
    Context.Add(draft);
    Context.SaveChanges();
  }

  public void UpdateDraft(Draft draft){
    if (draft.Id <= 0){ throw new Exception("Draft no update with id 0");}
    Context.Entry(draft).State = EntityState.Modified;
    Context.Update(draft);
    Context.SaveChanges();
  }
}