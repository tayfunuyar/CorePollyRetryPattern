using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestController : ControllerBase
{
    private readonly ClientPolicy _clientPolicy;
    private readonly IHttpClientFactory _clientFactory;

    // Get api/request

    public RequestController(ClientPolicy clientPolicy, IHttpClientFactory clientFactory)
    {
        _clientPolicy = clientPolicy;
        _clientFactory = clientFactory;
    }
    [HttpGet]
    public async Task<ActionResult> MakeARequest()
    {
        // Basic Client 
        //var client = new HttpClient();
        // Inject Polly Client 
        var client = _clientFactory.CreateClient("ResponseClient");
        
        // ImmediateRetry
        // var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
        //     () => client.GetAsync("https://localhost:7086/api/response/25"));

        // LinearRetry
        // var response = await _clientPolicy.LinearHttpRetry.ExecuteAsync(
        //    () => client.GetAsync("https://localhost:7086/api/response/25"));

        // ExponentialRetry   
        // var response = await _clientPolicy.ExponentialHttpRetry.ExecuteAsync(
        //    () => client.GetAsync("https://localhost:7086/api/response/25"));

        var response = await client.GetAsync("https://localhost:7086/api/response/25");
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Response service return SUCCESS");
            return Ok();
        }
        Console.WriteLine("--> Response service return FAILURE");
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}