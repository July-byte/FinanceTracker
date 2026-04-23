using System;
using System.Colletions.Generic;
using System.Linq;

namespace FinanceTracker
{
  public class FinanceManager
  {
    private List<Transaction> transactions = new List<Transaction>();
    private int nextId = 1;
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

