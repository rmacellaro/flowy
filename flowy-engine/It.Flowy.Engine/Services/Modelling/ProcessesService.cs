using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface IProcessesService{
  List<Process>? GetProcessesWithDistributions();
  Process? GeProcessByIdWithDistributions(long id);
  
  void Insert(Process process);
  void Update(Process process);
}

public class ProcessesService(FlowyEngineContext context) : IProcessesService {
  private readonly FlowyEngineContext Context = context;
  
  public List<Process>? GetProcessesWithDistributions(){
    return Context.Processes?.Include(p => p.Distributions).ToList();
  }
  
  public Process? GeProcessByIdWithDistributions(long id){
    return Context.Processes?
    .Include(p => p.Distributions)
    .FirstOrDefault(p => p.Id.Equals(id));
  }

  public void Insert(Process process) {
    process.Id = null;
    Context.Processes?.Add(process);
    Context.Entry(process).State = EntityState.Added;
    Context.SaveChanges();
  }

  public void Update(Process process) {
    if (process.Id == null) { throw new(nameof(process.Id)); }
    Context.Processes?.Update(process);
    Context.Entry(process).State = EntityState.Modified;
    Context.SaveChanges();
  }
}