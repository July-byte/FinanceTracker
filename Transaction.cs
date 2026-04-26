using System;
namespace FinanceTracker
{
  public class Transaction
  {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public TransactionType { get; set; }
    public DateTime Date { get; set; }

    public Transaction()
    {
    }

    public Transaction (int id, decimal amount, string category, TransactionType type)
    {
      Id = id;
      Amount = amount;
      Category = category;
      Type = type;
      Date = DateTime.Now;
    }
    public override string ToString()
    {
      string sign = Type == TransactionType.Expense ? "-" : "+";
      return $"{Id}. {Date:g} | {Category} | {Sign} | {Amount}";
    }
  }
}
