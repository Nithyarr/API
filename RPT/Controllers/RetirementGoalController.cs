using Microsoft.AspNetCore.Mvc;
using RPT.Data;
using RPT.Models;
using RPT.Services;
using System;

namespace RPT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetirementGoalController : ControllerBase
    {
        private readonly JsonDataService _dataService;

        public RetirementGoalController(JsonDataService dataService)
        {
            _dataService = dataService;
        }

        // POST /api/retirementgoal
        [HttpPost]
        public IActionResult CreateGoal([FromBody] RetirementGoal goal)
        {
            // Generate a unique reference ID
            goal.Id = ReferenceIdGenerator.GenerateReferenceId();

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
            goal.CurrentAge, goal.RetirementAge, 6),
        6, goal.RetirementAge, goal.CurrentAge);

    return Ok(new
    {
        CurrentProjection = projected,
        RequiredProjection = idealProjected,
        Target = goal.TargetSavings
    });
}
    }

    public static class ReferenceIdGenerator
    {
        public static string GenerateReferenceId()
        {
            // Generate a random number between 10000 and 99999
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            // Format the reference ID
            return $"RPT-{randomNumber}";
        }
    }
}