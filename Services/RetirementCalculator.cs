namespace RPT.Services
{
    public static class RetirementCalculator
    {
        public static double CalculateRequiredMonthlyContribution(
        double currentSavings,
        double targetSavings,
        int currentAge,
        int retirementAge,
        double annualInterestRatePercent)
        {
            double r = annualInterestRatePercent / 100 / 12;
            int n = (retirementAge - currentAge) * 12;
            double compoundFactor = Math.Pow(1 + r, n);
            double futureValueOfCurrentSavings = currentSavings * compoundFactor;
            double annuityFactor = (compoundFactor - 1) / r;
            double monthlyContribution = (targetSavings - futureValueOfCurrentSavings) / annuityFactor;
            return monthlyContribution;
        }

        public static double CalculateFutureValue(
            double currentSavings,
            double monthlySavings,
            double annualInterestRate,
            int retirementAge,
            int currentAge)
        {
            double monthlyRate = annualInterestRate / 12 / 100;
            int totalMonths = (retirementAge - currentAge) * 12;

            double compoundedSavings = currentSavings * Math.Pow(1 + monthlyRate, totalMonths);
            double compoundedContributions = monthlySavings *
                                             (Math.Pow(1 + monthlyRate, totalMonths) - 1) / monthlyRate;

            return compoundedSavings + compoundedContributions;
        }
    }
}

