using System;
using System.Colletions.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
namespace FinanceTracker
{
  public class FinanceManager
  {
    private List<Transaction> transactions = new List<Transaction>();
    private int nextId = 1;
    private string filePath = "transactions.json";
    public void SaveToFile()
    {
      try
      {
        var options = new JsonSerializeOptions
        {
          WriteIndented = true
        };
        string json = JsonSerializer.Serialize(transactions, options);
        File.WriteAllText(filePath, json);
      }

      catch (Exception ex)
      {
        Console.WriteLine("Ошибка при сохранении: " + ex.Message);
      }
    }

    public void LoadFromFile()
    {
      try
      {
        if (!File.Exists(filePath))
          return;
        string json = File.ReadAllText(filePath);
        var loadedTransactions = JsonSerializer.Deserializer<List<Transaction>>(json);
        if (loadedTransactions != null)
        {
          transactions = loadedTransactions;
          if (transactions.Any())
            nextId = transactions.Max(t => t.Id) + 1;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Ошибка при загрузке: " + ex.Message);
      }
    }
    
        
    public void AddTransaction(decimal amount, string category, TransactionType type)
    
    {
      if (amount <= 0)
      {
        Console.WriteLine("Сумма должна быть больше 0.");
        return;
      }
      if (string.IsNullOrWhiteSpace(category))
      {
        Console.WriteLine("Операций пока нет");
        return;
      }
      foreach (var transaction in transactions)
      {
        Console.WriteLine(transaction);
      }
    }
    public decimal GetBalance()
    {
      decimal income = transactions
        .Where(t => t.Type == TransactionType.Income)
        .Sum(t => t.Amount);
      decimal expenses = transactions
        .Where(t => t.Type == TransactionType.Expense)
        .Sum(t => t.Amount);
      return income - expences;
    }
    public void ShowBalance()
    {
      decimal balance = GetBalance();
      Console.WriteLine($"Текущий баланс: {balance}");
    }
    public void ShowMyCategory(string category)
    {
      var filtered = transactions
        .Where(t => t.Category.Equals(category, StringComparison.OriginalIgnoreCase))
        .ToList();

      if(!filtered.Any())
      {
        Console.WriteLine("Операции не найдены.");
        return;
      }
      foreach (var transaction in filtered)
      {
        Console.WriteLine(transaction);
      }
    }

    public void ShowSorted(string sortBy, bool descending)
    {
      if (!transactions.Any())
      {
        Console.WriteLine("Операций пока нет");
        return;
      }
      IEnumerable<Transaction> sorted;
      switch (sortBy.ToLower())
      {
        case "date":
          sorted = descending 
          ? transactions.OrderByDescending(t => t.Date)
          : transactions.OrderBy(t => t.Date);
          break;
        case "amount":
          sorted = descending
            ? transactions.OrderByDescending(t => t.Amount)
            : transations.OrderBy(t => t.Amount);
          break;
        default:
          Console.WriteLine("Неверный параметр сортировки");
          return;
      }
      foreach (var transaction in sorted)
      {
        Console.WriteLine(transaction);
  }
    public void ShowAnalytics()
    {
      if (!transactions.Any())
      {
        Console.WriteLine("Нет данных для анализа");
        return;
      }
      decimal totalIncome = transactions
        .Where(t => t.Type == TransactionType.Income)
        .Sum(t => t.Amount);

      decimal totalExpenses = transactions
        .Where(t => t.Type == TransactionType.Expense)
        .Sum(t => t.Amount);

      decimal balance = totalIncome - totalExpenses;

      Console.WriteLine("\n===Аналитический отчет===");
      Console.WriteLine($"Общий доход: {totalIncome}");
      Console.WriteLine($"Общий расход: {totalExpenses}");
      Console.WriteLine($"Баланс: {balance}");

      var largestExpense = transactions
        .Where(t => t.Type == TransactionType.Expense)
        .OrderByDescending(t => t.Amount)
        .FirstOrDefault();

      if (largestExpense != null)
      {
        Console.WriteLine($"Самая большая трата: {largest.Expense.Category} - {largestExpense.Amount}");
      }
      Console.WriteLine("\nРасходы по категориям:");
      var expensesByCategory = transactions
        .Where(t => t.Type == TransactionType.Expense)
        .GroupBy(t => t.Category)
        .Select(g => new
                {
                  Category = g.Key,
                  Total = g.Sum(t => t.Amount)
                  })
        .OrderByDescending(x => x.Total);

      foreach (var item in expensesByCategory)
      {
        Console.WriteLine($"{item.Category}:{item.Total}");
      }
}

