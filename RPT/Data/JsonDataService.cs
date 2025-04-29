using RPT.Models;
using System.Text.Json;

namespace RPT.Data
{
    public class JsonDataService
    {
        private readonly string _filePath = "retirementData.json";

        public List<RetirementGoal> LoadAllGoals()
        {
            if (!File.Exists(_filePath)) return new List<RetirementGoal>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<RetirementGoal>>(json) ?? new List<RetirementGoal>();
        }

        public void SaveGoal(RetirementGoal goal)
        {
            var goals = LoadAllGoals();
            goals.Add(goal);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(goals));
        }

        public RetirementGoal GetGoalById(string id)
{
   return LoadAllGoals().FirstOrDefault(g => g.Id == id);
}
        
    }
}