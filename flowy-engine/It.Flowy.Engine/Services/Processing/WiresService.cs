using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Processing;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Processing;

public interface IWiresService {
  Wire? GetWireById(long id, bool withFull = false);
  void Insert(Wire item);
  void Update(Wire item);
}

public class WiresService(FlowyEngineContext context) : IWiresService {

  private readonly FlowyEngineContext Context = context;

  public Wire? GetWireById(long id, bool withFull = false) {
    IQueryable<Wire>? quary = Context.Wires?.Where(w => w.Id.Equals(id));
    if (withFull) {
      quary = quary?.Include(w => w.Instance!).ThenInclude(i => i.Distribution);
      quary = quary?.Include(w => w.Instance!).ThenInclude(i => i.Datas);
    }
    return quary?.FirstOrDefault();
  }

  public void Insert(Wire item) {
    item.Id = null;
    Context.Wires?.Add(item);
    Context.Entry(item).State = EntityState.Added;
    Context.SaveChanges();
  }

  public void Update(Wire item) {
    if (item.Id == null) { throw new(nameof(Wire.Id)); }
    item.UpdatedDateTime = DateTime.Now;
    Context.Wires?.Update(item);
    Context.Entry(item).State = EntityState.Modified;
    Context.SaveChanges();
  }
}