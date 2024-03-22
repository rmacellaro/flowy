using It.Flowy.Camunda.Models.Operate;

namespace It.Flowy.Camunda.Apis.Operate;

public interface IIncidentsService {
  Results<Incident>? GetIncidents(Quary<Incident>? quary = null);
  Incident? GetIncidentByKey(long key);
}

public class IncidentsService : OperateApi, IIncidentsService {

  public IncidentsService(IAuthApi ias) : base(ias) {}
  
  public Results<Incident>? GetIncidents(Quary<Incident>? quary = null){
    return Post<Results<Incident>>(
      GetCompleteUrl("/incidents/search"),
      quary != null ? quary : new {}
    );
  }

  public Incident? GetIncidentByKey(long key) {
    return Get<Incident>(GetCompleteUrl("/incidents/" + key));
  }

}
