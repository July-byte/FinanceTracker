using System;
namespace FinanceTracker
{
  class Program
  {
    static void Main(string[] args)
    {
      FinanceManager manager = new FinanceManager();
      while (true)
      {
        Console.WriteLine("\n1 - Добавить доход");
        Console.WriteLine("2 - Добавить расход");
        Console.WriteLine("3 - Показать все операции");
        Console.WriteLine("4 - Показать баланс");
        Console.WriteLine("5 - Фильтр по категории");
        Console.WriteLine("6 - Выход");
        Console.WriteLine("7 - Сортировка");
        Console.WriteLine("Выбор: ");
        string input = Console.ReadLine();
        switch (input)
        {
          case "1":
            AddTransaction(manager, TransactionType.Income);
            break;
          case "2":
            AddTransaction(manager, TransactionType.Expense);
            break;
          case "3":
            manager.ShowAllTransactions();
            break;
          case "4":
            manager.ShowBalance();
            break;
          case "5":
            Console.WriteLine("Введите категорию");
            string category = Console.ReadLine();
            manager.ShowByCategory(category);
            break;
          case "6":
            Console.WriteLine("Сортировать по: ");
            Console.WriteLine("1 - Дата"):
            Console.WriteLine("2 - Сумма");
            string sortOption = Console.ReadLine();
            Console.WriteLine("1 - По возрастанию");
            Console.WriteLine("2 - По убыванию");
            string orderOption = Console.ReadLine();
            bool descending = orderOption == "2";
            if (sortOption == "1")
              manager.ShowSorted("date", descending);
            else if (sortOption == "2")
              manager.ShowSorted("amount", "descending");
            else
              Console.WriteLine("Неверный выбор");
            break;
          case "0":
            return;
        }
      }
    }

    FinanceManager manager = new FinanceManager();
    manager.LoadFromFile();
    
    static void AddTransaction(FinanceManager manager, TransactionType type)
    {
      Console.Write("Введите сумму: ");
      if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
      {
        Console.WriteLine("Некорректная сумма");
        return;
      }
      Console.Write("Введите категорию: ");
      string category = Console.ReadLine();
      manager.AddTransaction(amount, category, type);
      manager.SaveToFile();
    }
  }
}

