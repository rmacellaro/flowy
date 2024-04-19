using It.Flowy.Engine.Context;
using It.Flowy.Engine.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace It.Flowy.Engine.Services.Modelling;

public interface INodeDataTypesService {
    List<NodeDataType>? GetAllNodeDataTypes();
    NodeDataType? GetNodeDataTypeByName(string name);
}

public class NodeDataTypesService(FlowyEngineContext context) : INodeDataTypesService {
    
    private readonly FlowyEngineContext Context = context;

    public List<NodeDataType>? GetAllNodeDataTypes(){
        return Context.NodeDataTypes?.ToList();
    }

    public NodeDataType? GetNodeDataTypeByName(string name){
        return Context.NodeDataTypes?.FirstOrDefault(n => n.Name == name);
    }

}