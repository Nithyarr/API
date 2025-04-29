namespace RPT.Models;

public class CreateRetirementGoalRequest
{
        public int CurrentAge { get; set; }
        public int RetirementAge { get; set; }
        public double CurrentSavings { get; set; }
        public double TargetSavings { get; set; }
        public double MonthlyContribution { get; set; }
}