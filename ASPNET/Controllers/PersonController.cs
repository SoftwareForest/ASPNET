using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ASPNET.Controllers;


[ApiController]
[Route("[controller]")]
[CustomActionFilter]
public class PersonController : ControllerBase
{
    private static readonly List<Person> people = new List<Person>();

    [HttpGet]
    [CustomActionFilter]
    public ActionResult<List<Person>> GetAll()
    {
        return Ok(people);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Person> GetById(int id)
    {

        var person = people.Find(x => x.Id == id);
        if (person is null)
            return NotFound();

        return Ok(person);
    }

    [HttpGet("/raiseexception")]
    public IActionResult RaiseExceptionAsync(int age)
    {

        if (age < 18)
            throw new InvalidOperationException("Your Are Not Eligable");
        return Ok();
    }

    [HttpGet("{firstname:alpha}/{lastname:alpha?}")]
    public ActionResult<Person> GetByName(string firstname, string? lastname)
    {
        var person = people.Find(x => x.Firstname == firstname && (string.IsNullOrEmpty(lastname) || x.Lastname == lastname));
        if (person is null)
            return NotFound();

        return Ok(person);
    }


    [HttpPost]
    public IActionResult Add(Person item)
    {
        if (people.Any(x => x.Firstname == item.Firstname && x.Lastname == item.Lastname))
        {
            //return BadRequest("Duplicate Person Cant be add");
            return this.ValidationProblem(new ValidationProblemDetails
            {
                Type = "",
                Title = "",
            });
        }

        people.Add(item);

        return Ok();
    }

}






public class Person : IValidatableObject
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> validationResults = [];

        if (string.IsNullOrEmpty(Firstname))
            validationResults.Add(new ValidationResult("Firstname Is Required"));

        if (string.IsNullOrEmpty(Lastname))
            validationResults.Add(new ValidationResult("Lastname Is Required"));

        return validationResults.Count == 0 ? [ValidationResult.Success] : validationResults;
    }
}


