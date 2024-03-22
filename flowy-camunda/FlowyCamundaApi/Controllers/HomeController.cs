using Microsoft.AspNetCore.Mvc;

namespace FlowyCamundaApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController: Controller {

  [Route("/")]
  [Route("/docs")]
  [Route("/swagger")]
  public IActionResult Index() {
    return new RedirectResult("~/swagger");
  }
  
}