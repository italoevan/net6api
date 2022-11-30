using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/teste")]
public class MainController : ControllerBase
{


    [HttpGet]
    [Route("/api")]
    public string returnValue()
    {
        return "Coeeeee";
    }
}