using SmartSave.Application.DTOs;
using SmartSave.Core.Enums;
using System.Text;

namespace SmartSave.Application.Helper
{
    public static class PromptBuilder
    {
        public static string BuildSuggestionPrompt(IEnumerable<GetGoalDto> goals,
                                     IEnumerable<GetTransactionDto> transactions,
                                     IEnumerable<GetDebtDto> debts)
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
            }
            else
            {
                prompt.AppendLine("Aún no tengo metas financieras definidas.");
            }

            if (debts.Any())
            {
                prompt.AppendLine("Mis deudas son:");
                foreach (var debt in debts)
                {
                    prompt.AppendLine($"- Creditor: {debt.Creditor}, tengo que pagar {debt.TotalAmount:C} para {debt.Deadline:MMMM yyyy} y he pagado {debt.TotalAmount:C}.");
                }
            }
            else
            {
                prompt.AppendLine("No tengo deudas.");
            }

            prompt.AppendLine("¿Qué sugerencias tienes para mejorar mis finanzas personales, teniendo en cuenta mi ingreso, gasto, metas y deudas?");

            return prompt.ToString();
        }

        public static string BuildPredictionPrompt(IEnumerable<GetGoalDto> goals, 
                                     IEnumerable<GetTransactionDto> transactions, 
                                     IEnumerable<GetDebtDto> debts)
        {
            var income = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            var savings = income - expense;

            var prompt = new StringBuilder();
            prompt.AppendLine("Actúa como un asesor financiero personal con enfoque en predicciones.");
            prompt.AppendLine($"Mi ingreso mensual total es de {income:C} y mis gastos son de {expense:C}.");
            prompt.AppendLine($"Actualmente estoy ahorrando aproximadamente {savings:C} al mes.");

            if (goals.Any())
            {
                prompt.AppendLine("Mis metas son:");
                foreach (var goal in goals)
                {
                    prompt.AppendLine($"- {goal.Name}: {goal.ObjectiveAmount:C} para {goal.Deadline:MMMM yyyy}");
                }
            }
            else
            {
                prompt.AppendLine("Aún no tengo metas financieras definidas.");
            }

            if (debts.Any())
            {
                prompt.AppendLine("Mis deudas son:");
                foreach (var debt in debts)
                {
                    prompt.AppendLine($"- Creditor: {debt.Creditor}, tengo que pagar {debt.TotalAmount:C} para {debt.Deadline:MMMM yyyy} y he pagado {debt.AmountPaid:C}.");
                }
            }
            else
            {
                prompt.AppendLine("No tengo deudas.");
            }

            prompt.AppendLine("¿Qué predicciones puedes hacer sobre el cumplimiento de mis metas financieras, considerando mis ingresos, gastos, deudas y ahorro mensual?");

            return prompt.ToString();
        }
    }
}
