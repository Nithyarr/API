using RPT.Models;
using System.Text.Json;

namespace RPT.Data
{
    public class JsonDataService
    {
        private readonly string _filePath = "data.json";

        public List<RetirementGoal> LoadAllGoals()
        {
            if (!File.Exists(_filePath)) return new List<RetirementGoal>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<RetirementGoal>>(json) ?? new List<RetirementGoal>();
        }

        public void SaveGoal(RetirementGoal goal)
        {
            var goals = LoadAllGoals();
        
            // Always generate a new ID no matter what
            int nextNumber = 1;
        
            if (goals.Any())
            {
                // Extract the numeric parts safely
                var lastId = goals
        .Select(g => int.TryParse(g.Id.Replace("RPT", ""), out var num) ? num : 0)
                    .Max();
        
                nextNumber = lastId + 1;
            }
        
        goal.Id = $"RPT{nextNumber.ToString("D3")}";
        
            goals.Add(goal);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(goals));
        }

        public RetirementGoal? GetGoalById(string id)
        {
            return LoadAllGoals().FirstOrDefault(g => g.Id == id);
        }
    }
}