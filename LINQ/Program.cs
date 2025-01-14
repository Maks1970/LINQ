using System.Diagnostics.Tracing;
using System.Linq;

namespace LInqT
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("1.Виведiть усi числа вiд 10 до 50 через кому");
            Console.WriteLine(string.Join(", ", Enumerable.Range(10, 41)));
            Console.WriteLine();
            Console.WriteLine("2.Виведiть лише тi числа від 10 до 50, якi можна подiлити на 3");
            Console.WriteLine(string.Join(" ", Enumerable.Range(10, 41).Where(x => x % 3 == 0)));
            Console.WriteLine();
            Console.WriteLine("3.Виведiть слово \"Linq\" 10 разiв");
            Console.WriteLine(string.Join(" ", Enumerable.Repeat("Linq", 10)));
            Console.WriteLine();
            Console.WriteLine("4.Вивести всi слова з буквою «а» в рядку «aaa;abb;ccc;dap»");
            Console.WriteLine(string.Join(" ", "aaa;abb;ccc;dap".Split(';').Where(x => x.Contains('a'))));
            Console.WriteLine();
            Console.WriteLine("5.Виведiть кiлькiсть лiтер «а» у словах з цiєю літерою в рядку «aaa;abb;ccc;dap» через кому");
            Console.WriteLine(string.Join(" ", "aaa;abb;ccc;dap".Split(';')
                .Where(x => x.Contains('a'))
                .Select(word => word.Count(c => c == 'a'))));
            Console.WriteLine();
            Console.WriteLine("6.Вивести true, якщо слово \"abb\" iснує в рядку \"aaa;xabbx;abb;ccc;dap\", iнакше false");
            Console.WriteLine("aaa;abb;ccc;dap".Split(';')
                .Any(x => x.Contains("abb")));
            Console.WriteLine();
            Console.WriteLine("7.Отримати найдовше слово в рядку \"aaa;xabbx;abb;ccc;dap\"");
            Console.WriteLine("aaa;xabbx;abb;ccc;dap".Split(';')
                .Where(w => w.Length == "aaa;xabbx;abb;ccc;dap".Split(';').Max(x => x.Length)).First());
            Console.WriteLine();
            Console.WriteLine("8.Обчислити середню довжину слова в рядку \"aaa;xabbx;abb;ccc;dap\"");
            Console.WriteLine("aaa;xabbx;abb;ccc;dap".Split(';')
                .Select(w => w.Length).Average());
            Console.WriteLine();
            Console.WriteLine("9.Вивести найкоротше слово в рядку \"aaa;xabbx;abb;ccc;dap;zh\" у зворотному порядку");
            Console.WriteLine("aaa;xabbx;abb;ccc;dap;zh".Split(';')
                .Where(w => w.Length == "aaa;xabbx;abb;ccc;dap;zh".Split(';').Min(x => x.Length))
                .First()
                .Reverse()
                .ToArray());
            Console.WriteLine();
            Console.WriteLine("10.Вивести true, якщо в першому словi, яке починається з \"aa\", усi лiтери \"b\" (За винятком \"аа\"), інакше false \"baaa;aabb;aaa;xabbx;abb;ccc;dap;zh\"");
            Console.WriteLine("baaa;aabb;aaa;xabbx;abb;ccc;dap;zh".Split(';')
                .Where(w => w.StartsWith("aa"))
                .FirstOrDefault()?
                .Skip(2)
                .All(c => c == 'b') ?? false);
            Console.WriteLine();
            Console.WriteLine("11.Вивести останнє слово в послідовності, за винятком перших двох елементів, які закінчуються на \"bb\" (використовуйте послідовність із 10 завдання");
            Console.WriteLine("baaa;aabb;aaa;xabbx;ccc;dap;zh;abb".Split(';')
               .Where(w => w.EndsWith("bb"))
               .Take(2)
               .Concat("baaa;aabb;aaa;xabbx;abb;ccc;dap;zh".Split(';').Where(w => !w.EndsWith("bb")))
               .LastOrDefault());

        }
    }
}
