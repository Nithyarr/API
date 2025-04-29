namespace RPT.Models
{
    public class RetirementGoal
    {
        public string Id { get; set; } = string.Empty;
        public int CurrentAge { get; set; }
        public int RetirementAge { get; set; }
        public double CurrentSavings { get; set; }
        public double TargetSavings { get; set; }
        public double MonthlyContribution { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}