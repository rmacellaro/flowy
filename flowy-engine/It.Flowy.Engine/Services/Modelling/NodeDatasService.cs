using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface INodeDatasService {
    NodeData? GetById(long id);
    List<NodeData>? GetByIdNode(long idNode);
    void Insert(NodeData item);
    void Update(NodeData item);
}

public class NodeDatasService(FlowyEngineContext context) : INodeDatasService {
    
    private readonly FlowyEngineContext Context = context;

    public NodeData? GetById(long id){
        return Context.NodeDatas?.FirstOrDefault(c => c.Id.Equals(id));
    }

    public List<NodeData>? GetByIdNode(long idNode){
        return Context.NodeDatas?.Where(c => c.IdNode.Equals(idNode)).ToList();
    }

    public void Insert(NodeData item){
        item.Id = null;
        Context.NodeDatas?.Add(item);
        Context.Entry(item).State = EntityState.Added;
        Context.SaveChanges();
    }

    public void Update(NodeData item){
        if (item.Id == null) { throw new (nameof(NodeData.Id)); }
        Context.NodeDatas?.Update(item);
        Context.Entry(item).State = EntityState.Modified;
        Context.SaveChanges();
    }

}