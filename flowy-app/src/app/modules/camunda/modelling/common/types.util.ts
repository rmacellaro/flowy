export class Type {
  public type: string;
  public title: string;
  public icon: string;
  public properties: Array<string>;
  public filters?: Array<string>;

  constructor(){
    this.type = this.title = this.icon = '';
    this.properties = [];
    this.filters = [];
  }
}

export const ELEMENT_TYPES: Array<Type> = [
  { 
    type: 'bpmn:Process', 
    title:'Process', 
    properties: [ 'id', 'name', 'documentation', 'interaction'],
    filters: [ 'MaskForm', 'MaskCustom' ],
    icon: 'bpmn-icon bpmn-icon-Process'
  },
  { 
    type: 'bpmn:SubProcess', 
    title: 'SubProcess', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-SubProcess'
  },
  { 
    type: 'bpmn:SubProcess:Expanded', 
    title: 'Sub Process Expanded', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-SubProcessExpanded'
  },
  { 
    type: 'bpmn:StartEvent', 
    title: 'Start Event', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-StartEvent' 
  },
  { 
    type: 'bpmn:EventBasedGateway', 
    title: 'Event Based Gateway', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-EventBasedGateway'
  },
  { 
    type: 'bpmn:IntermediateCatchEvent', 
    title: 'Intermediate Catch Event', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-IntermediateCatchEvent'
  },
  {
    type: 'bpmn:IntermediateCatchEvent:TimerEventDefinition',
    title: 'Intermediate Catch Event - Timer Event Definition',
    properties: [ 'id', 'name', 'documentation', 'timer'],
    icon: 'bpmn-icon bpmn-icon-IntermediateCatchEventTimerEventDefinition'
  },
  { 
    type: 'bpmn:IntermediateThrowEvent', 
    title: 'Intermediate Throw Event', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-IntermediateThrowEvent'
  },
  { 
    type: 'bpmn:EndEvent', 
    title: 'End Event', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-EndEvent'
  },
  { 
    type: 'bpmn:Task', 
    title: 'Task', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-Task'
  },
  { 
    type: 'bpmn:UserTask', 
    title: 'User Task', 
    properties: [ 'id', 'name', 'documentation', 'interaction'],
    filters: [ 'MaskForm', 'MaskCustom' ],
    icon: 'bpmn-icon bpmn-icon-UserTask'
  },
  { 
    type: 'bpmn:ServiceTask', 
    title: 'Service Task', 
    properties: [ 'id', 'name', 'documentation', 'interaction'],
    filters: [ 'Automation' ],
    icon: 'bpmn-icon bpmn-icon-ServiceTask'
  },
  { 
    type: 'bpmn:ScriptTask', 
    title: 'Script Task', 
    properties: [ 'id', 'name', 'documentation', 'interaction'],
    filters: [ 'FunctionScript', 'FunctionLibrary' ],
    icon: 'bpmn-icon bpmn-icon-ScriptTask'
  },
  { 
    type: 'bpmn:SendTask', 
    title: 'Send Task', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-SendTask'
  },
  { 
    type: 'bpmn:ReceiveTask', 
    title: 'Receive Task', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-ReceiveTask'
  },
  { 
    type: 'bpmn:ManualTask', 
    title: 'Manual Task', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-ManualTask'
  },
  { 
    type: 'bpmn:BusinessRuleTask', 
    title: 'Business Rule Task', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-BusinessRuleTask'
  },
  { 
    type: 'bpmn:SequenceFlow', 
    title: 'Sequence Flow', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-SequenceFlow'
  },
  { 
    type: 'bpmn:ExclusiveGateway', 
    title: 'Exclusive Gateway', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-ExclusiveGateway'
  },
  { 
    type: 'bpmn:InclusiveGateway', 
    title: 'Inclusive Gateway', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-InclusiveGateway'
  },
  { 
    type: 'bpmn:ParallelGateway', 
    title: 'Parallel Gateway', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-ParallelGateway'
  },
  { 
    type: 'bpmn:ComplexGateway', 
    title: 'Complex Gateway', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-ComplexGateway'
  },
  { 
    type: 'bpmn:CallActivity', 
    title: 'CallActivity', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-CallActivity'
  },
  { 
    type: 'bpmn:DataStoreReference', 
    title: 'Data Store Reference', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-DataStoreReference'
  },
  { 
    type: 'bpmn:TextAnnotation', 
    title: 'Text Annotation', 
    properties: [ 'id', 'name', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-TextAnnotation'
  },
  { 
    type: 'bpmn:Association', 
    title: 'Association', 
    properties: [ 'id', 'documentation'],
    icon: 'bpmn-icon bpmn-icon-Association'
  },
  { 
    type: '', 
    title: '', 
    properties: [ 'id', 'name', 'documentation'],
    icon: ''
  },
  { 
    type: '', 
    title: '', 
    properties: [ 'id', 'name', 'documentation'],
    icon: ''
  },
  { 
    type: '', 
    title: '', 
    properties: [ 'id', 'name', 'documentation'],
    icon: ''
  }
];
