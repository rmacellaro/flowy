export const FLOWY_NAMESPACE: any = {
  "name" : "FlowyProperties",
  "uri": "http://flowy/schema/bpmn/flowy",
  "prefix": "flowy",
  "xml": {
  },
  "types":[
    {
      "name": "interaction",
      "superClass": [ "Element" ],
      "properties": [
        {
          "name": "idInteraction",
          "isAttr": true,
          "type": "Number"
        }
      ]
    }
  ]
};

/*
{
      "name": "Property",
      "superClass" : [ "flowy:Properties" ],
      "properties": [
        {
          "name": "name",
          "isAttr": true,
          "type": "String"
        },
        {
          "name": "value",
          "isAttr": true,
          "type": "String"
        },
        {
          "name": "content",
          "isAttr": false,
          "type": "String"
        }
      ]
    },
    {
      "name": "Properties",
      "superClass": ["Element"],
      "properties":[
        { 
          "name": "elements",
          "type": "flowy:Property",
          "isMany": true
        }
      ]
    },
    {
      "name": "AnalyzedNode",
      "extends": [
        "bpmn:FlowNode"
      ],
      "properties": [
        {
          "name": "suitable",
          "isAttr": true,
          "type": "Float"
        }
      ]
    },
    {
      "name": "AnalysisDetails",
      "superClass": [ "Element" ],
      "properties": [
        {
          "name": "lastChecked",
          "isAttr": true,
          "type": "String"
        },
        {
          "name": "nextCheck",
          "isAttr": true,
          "type": "String"
        },
        {
          "name": "comments",
          "isMany": true,
          "type": "Comment"
        }
      ]
    }
*/