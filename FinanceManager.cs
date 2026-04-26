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
  }
}

