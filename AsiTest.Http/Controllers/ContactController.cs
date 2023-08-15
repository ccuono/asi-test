using Microsoft.AspNetCore.Mvc;

namespace AsiTest.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetContacts")]
    public IEnumerable<string> Get()
    {
        return new List<string>();
    }
}