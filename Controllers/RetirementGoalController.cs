using Microsoft.AspNetCore.Mvc;
using RPT.Data;
using RPT.Models;
using RPT.Services;

namespace RPT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetirementGoalController: ControllerBase
    {
    private readonly JsonDataService _dataService;

    public RetirementGoalController(JsonDataService dataService)
    {
        _dataService = dataService;
    }

    // POST /api/retirementgoal
    // [HttpPost]
    // [ProducesResponseType(typeof(RetirementGoal),StatusCodes.Status201Created)]
    // public IActionResult CreateGoal([FromBody] RetirementGoal goal)
    // {
    //     _dataService.SaveGoal(goal);
    //     return CreatedAtAction(nameof(GetGoal), new { id = goal.Id }, goal);
    // }

    [HttpPost]
    [ProducesResponseType(typeof(RetirementGoal),StatusCodes.Status201Created)]
    public IActionResult CreateGoal([FromBody] CreateRetirementGoalRequest request)
    {
        var goal= new RetirementGoal
        {
        
            CurrentAge=request.CurrentAge,
            RetirementAge=request.RetirementAge,
            CurrentSavings=request.CurrentSavings,
            TargetSavings=request.TargetSavings,
            MonthlyContribution=request.MonthlyContribution
        };

        _dataService.SaveGoal(goal);
        return CreatedAtAction(nameof(GetGoal), new { id = goal.Id }, goal);
    }


    // GET /api/retirementgoal/{id}
    [HttpGet("{id}")]
    public IActionResult GetGoal(string id)
    {
        var goal = _dataService.GetGoalById(id);
        return goal != null ? Ok(goal) : NotFound();
    }

    // POST /api/monthlysavings
    [HttpPost("/api/monthlysavings")]
    // [ProducesResponseType(typeof(RetirementGoal),StatusCodes.Status201Created)]
    public IActionResult CalculateMonthlySavings([FromBody] RetirementGoal input)
    {
        double monthly = RetirementCalculator.CalculateRequiredMonthlyContribution(
            input.CurrentSavings, input.TargetSavings, input.CurrentAge, input.RetirementAge, 6);

        return Ok(new { RequiredMonthlySavings = monthly });
    }

    // GET /api/retirementprogress/{id}
    [HttpGet("/api/retirementprogress/{id}")]
    public IActionResult GetProgress(string id)
    {
        var goal = _dataService.GetGoalById(id);
        if (goal == null) return NotFound();

        double projected = RetirementCalculator.CalculateFutureValue(
            goal.CurrentSavings, goal.MonthlyContribution, 6, goal.RetirementAge, goal.CurrentAge);

        double idealProjected = RetirementCalculator.CalculateFutureValue(
            goal.CurrentSavings,
            RetirementCalculator.CalculateRequiredMonthlyContribution(goal.CurrentSavings, goal.TargetSavings,
                goal.CurrentAge, goal.RetirementAge, 6), 6, goal.RetirementAge, goal.CurrentAge);

        return Ok(new
        {
            CurrentProjection = projected,
            RequiredProjection = idealProjected,
            Target = goal.TargetSavings
        });
    }

}
}






