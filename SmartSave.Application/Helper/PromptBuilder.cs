using SmartSave.Application.DTOs;
using SmartSave.Core.Enums;
using System.Text;

namespace SmartSave.Application.Helper
{
    public static class PromptBuilder
    {
        public static string BuildPrompt(IEnumerable<GetGoalDto> goals, IEnumerable<GetTransactionDto> transactions)
        {
            var income = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            var savings = income - expense;

            var prompt = new StringBuilder();
            prompt.AppendLine("Actúa como un asesor financiero personal.");
            prompt.AppendLine($"Mi ingreso mensual total es de {income:C} y mis gastos son de {expense:C}.");
            prompt.AppendLine($"Actualmente estoy ahorrando aproximadamente {savings:C} al mes.");

            if (goals.Any())
            {
                prompt.AppendLine("Mis metas son:");
                foreach (var goal in goals)
                {
                    prompt.AppendLine($"- {goal.Name}: {goal.ObjectiveAmount:C} para {goal.Deadline:MMMM yyyy}");
                }
            } else
            {
                prompt.AppendLine("Aún no tengo metas financieras definidas.");
            }

            prompt.AppendLine("¿Qué sugerencias tienes para mejorar mis finanzas personales?");

            return prompt.ToString();
        }
    }
}
