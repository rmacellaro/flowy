using It.Flowy.Camunda.Context;
using It.Flowy.Camunda.Models.Core.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Camunda.Services;

public interface IProcessesService {
  ICollection<Process>? GetProcessesByIdScope(long idScope);
  Process? GetProcessById(long id);
  Process? GetProcessInScopeByKeyProcessDefinition(long idScope, long key);
  void InsertProcess(Process process);
  void UpdateProcess(Process process);
}

public class ProcessesService : IProcessesService {

  private readonly FlowyCamundaContext Context;

  public ProcessesService(FlowyCamundaContext context) {
    Context = context;
  }

  public ICollection<Process>? GetProcessesByIdScope(long idScope) {
    return Context.Processes?.Where(d => d.IdScope.Equals(idScope)).ToList();
  }

  public Process? GetProcessById(long id){
    return Context.Processes?.FirstOrDefault(d => d.Id.Equals(id));
  }

  public Process? GetProcessInScopeByKeyProcessDefinition(long idScope, long key){
    return Context.Processes?.FirstOrDefault(d => d.IdScope.Equals(idScope) && d.Key.Equals(key));
  }

  public void InsertProcess(Process process){
    Context.Entry(process).State = EntityState.Added;
    Context.Add(process);
    Context.SaveChanges();
  }

  public void UpdateProcess(Process process){
    if (process.Id <= 0){ throw new Exception("process no update with id 0");}
    Context.Entry(process).State = EntityState.Modified;
    Context.Update(process);
    Context.SaveChanges();
  }

}