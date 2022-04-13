using Microsoft.AspNetCore.Mvc;
using RestAPIASP.Model;
using RestAPIASP.Services;

namespace RestAPIASP.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase {


        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService) {
            _logger = logger;
            _personService = personService;
        }

        //[HttpGet("sum/{firstNumber}/{secondNumber}")]
        //public IActionResult Sum(string firstNumber, string secondNumber) {
        //    if ((IsNumeric(firstNumber) && IsNumeric(secondNumber))) {
        //        var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
        //        return Ok(sum.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("sub/{firstNumber}/{secondNumber}")]
        //public IActionResult Sub(string firstNumber, string secondNumber) {
        //    if ((IsNumeric(firstNumber) && IsNumeric(secondNumber))) {
        //        var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
        //        return Ok(sub.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("mult/{firstNumber}/{secondNumber}")]
        //public IActionResult Mult(string firstNumber, string secondNumber) {
        //    if ((IsNumeric(firstNumber) && IsNumeric(secondNumber))) {
        //        var mult = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
        //        return Ok(mult.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("div/{firstNumber}/{secondNumber}")]
        //public IActionResult Div(string firstNumber, string secondNumber) {
        //    if ((IsNumeric(firstNumber) && IsNumeric(secondNumber))) {
        //        var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
        //        return Ok(div.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("media/{firstNumber}/{secondNumber}")]
        //public IActionResult Media(string firstNumber, string secondNumber) {
        //    if ((IsNumeric(firstNumber) && IsNumeric(secondNumber))) {
        //        var media = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
        //        return Ok(media.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("raiz/{firstNumber}")]
        //public IActionResult Raiz(string firstNumber) {
        //    if ((IsNumeric(firstNumber))) {
        //        var media = Math.Sqrt((double)ConvertToDecimal(firstNumber));
        //        return Ok(media.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}
        //private bool IsNumeric(string strNumber) {
        //    double number;
        //    bool isNumber = double.TryParse(
        //        strNumber,
        //        System.Globalization.NumberStyles.Any,
        //        System.Globalization.NumberFormatInfo.InvariantInfo,
        //        out number);
        //    return isNumber;

        //}

        //private decimal ConvertToDecimal(string strNumber) {

        //    decimal decimalValue;
        //    if (decimal.TryParse(strNumber, out decimalValue)) {
        //        return decimalValue;
        //    }
        //    return 0;
        //}

        [HttpGet]
        public IActionResult Get() {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id) {
            var person = _personService.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person) {
            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));
        } 
        [HttpPut]
        public IActionResult Put([FromBody] Person person) {
            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
           _personService.Delete(id);
            return NoContent();
        }
    }
}