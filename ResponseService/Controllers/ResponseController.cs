using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponseController : ControllerBase
{
    [Route("{id:int}")]
    [HttpGet]
    public ActionResult GetAResponse(int id)
    {
        Random rnd = new Random();
        var rndInteger = rnd.Next(1,101);
        if(rndInteger>=id){
            Console.WriteLine("-->  Failure - Generate a HTTP 500");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        Console.WriteLine("--> Success - Generate a HTTP 200");
        return Ok();
    }
}