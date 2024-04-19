using System.ComponentModel.DataAnnotations.Schema;
using It.Flowy.Engine.Models.Common;

namespace It.Flowy.Engine.Models.Modelling;

[Table("NodeDatas", Schema = "Modelling")]
public class NodeData : Data { 

    [ForeignKey(nameof(Node))]
    public long? IdNode { get; set; }
    public Node? Node { get; set; }

    [ForeignKey(nameof(NodeDataType))]
    public long? IdNodeDataType { get; set; }
    public NodeDataType? NodeDataType { get; set; }
}