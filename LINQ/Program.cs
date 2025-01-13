using System.Diagnostics.Tracing;
using System.Linq;

namespace LInqT
{
    class Actor
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }

    abstract class ArtObject
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }

    class Film : ArtObject
    {
        public int Length { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }

    class Book : ArtObject
    {
        public int Pages { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new List<object>() {
              "Hello",
              new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
              new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
              new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
              new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
              new List<int>() {4, 6, 8, 2},
              new string[] {"Hello inside array"},
              new Film() { Author = "Martin Scorsese", Name= "The Departed", Actors = new List<Actor>() {
                new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22)},
                new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11)},
                new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)}
              }},
              new Film() { Author = "Gus Van Sant", Name = "Good Will Hunting", Actors = new List<Actor>() {
                new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)},
                new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
            }},
              new Book() { Author = "Stephen King", Name="Finders Keepers", Pages = 200},
              "Leonardo DiCaprio"
            };


            Console.WriteLine("1. Виведiть усi елементи, крiм ArtObjects");
            Console.WriteLine(string.Join("\n", data
                .Where(x => x is not ArtObject)  // Вибираємо елементи, що не є ArtObject
                .Select(x =>
                {
                    if (x is IEnumerable<object> collection)  // Якщо елемент є колекцією (списком або масивом)
                    {
                        return string.Join(", ", collection.Select(item => item.ToString()));  // Виводимо елементи колекції
                    }
                    return x.ToString();  // Якщо елемент не колекція, просто виводимо його
                })
            ));
            Console.WriteLine();
            Console.WriteLine("2.Виведiть iмена всiх акторiв");
            Console.WriteLine(string.Join("\n", data
                .OfType<Film>()
                .SelectMany(film=> film.Actors)
                .Select(actor => actor.Name)
                ));
            Console.WriteLine();
            Console.WriteLine("3.Виведiть кiлькiсть акторiв, якi народилися в серпнi");
            Console.WriteLine(data
                .OfType<Film>()
                .SelectMany(film => film.Actors)
                .Where(actor=> actor.Birthdate.Month == 8 )
                .Count());
            Console.WriteLine();
            Console.WriteLine("4.Виведiть два найстарiших iмена акторiв");
            Console.WriteLine(string.Join(" and " ,data
                .OfType<Film>()
                .SelectMany(film => film.Actors)
                .OrderBy(actor => actor.Birthdate)
                .Take(2)
                .Select(name => name.Name)
                ));
            Console.WriteLine();
            Console.WriteLine("5.Вивести кількість книг на авторів");
            Console.WriteLine(string.Join("\n",data
                .OfType<Book>()
                .GroupBy(b => b.Author)
                .Select(g =>  $"{g.Key}: {g.Count()} книг(и)" )
                ));
            Console.WriteLine();
            Console.WriteLine("6.Виведiть кiлькiсть книг на одного автора та фiльмiв на одного режисера");
            Console.WriteLine(string.Join(Environment.NewLine,
                    data.OfType<Book>()
                        .GroupBy(b => b.Author)
                        .Select(g => $"{g.Key}: {g.Count()} книг(и)")
                        .Concat(data.OfType<Film>()
                            .GroupBy(f => f.Author)
                            .Select(g => $"{g.Key}: {g.Count()} фiльм(и)"))
                ));
            Console.WriteLine();
            Console.WriteLine("7.Виведіть, скільки різних букв використано в іменах усіх акторів");
            Console.WriteLine(string.Join("\n",
                data
                .OfType<Film>()
                .Select(film => film.Actors.Select(name=>name.Name.ToLower()))
                .Distinct()
                .Count()
                ));
            Console.WriteLine();
            Console.WriteLine("8.Виведіть назви всіх книг, упорядковані за іменами авторів і кількістю сторінок");
            Console.WriteLine(string.Join("\n",
                data
                .OfType<Book>()
                .OrderBy(name => name.Author)
                .ThenBy(b => b.Pages)
                .Select(name => name.Name)
                ));
            Console.WriteLine();
            Console.WriteLine("9.Виведіть ім'я актора та всі фільми за участю цього актора");
            Console.WriteLine(string.Join("\n",
                data
                .OfType<Film>()
                .SelectMany(film => film.Actors, (film, actor) => new { FilmName = film.Name, ActorName = actor.Name }) // Отримуємо всі фільми з актором
                .GroupBy(fa => fa.ActorName) // Групуємо за іменем актора
                .Select(group => $"{group.Key}:\n" + string.Join("\n", group.Select(f => $"  - {f.FilmName}")))
                ));

            Console.WriteLine();
            Console.WriteLine("10.Виведіть суму загальної кількості сторінок у всіх книгах і всі значення int у всіх послідовностях у даних\r\n\r\n");
            Console.WriteLine(string.Join("\n",
                data
                .OfType<Book>()
                .Sum(book => book.Pages) // Вираховуємо суму сторінок у всіх книгах
                + data
                .OfType<IEnumerable<int>>() // Вибираємо всі послідовності int
                .SelectMany(seq => seq) // Розгортаємо всі послідовності
                .Sum()
                ));
            Console.WriteLine();
            Console.WriteLine("11.Отримати словник з ключем - автор книги, значенням - список авторських книг");
            Console.WriteLine(string.Join("\n",
            data
            .OfType<Book>()
            .GroupBy(b => b.Author) // Групуємо книги за автором
            .ToDictionary
            (
                g => g.Key, // Ключем буде автор
                g => g.Select(b => b.Name).ToList() // Значенням буде список назв книг
            )
            .Select(kv => $"Автор: {kv.Key}\nКниги:\n{string.Join("\n", kv.Value)}")
             ));
            Console.WriteLine();
            Console.WriteLine("12.Вивести всі фільми Метт Деймон, за винятком фільмів з акторами, імена яких представлені в даних у вигляді рядків");
            Console.WriteLine(string.Join("\n",
            data
            .OfType<Film>()
            .Where(film => film.Actors.Any(actor => actor.Name == "Matt Damon")) // Фільтруємо фільми
            .Select(film => film.Name)
             ));

            //Console.WriteLine(string.Join(" ", data
            //    .OfType<object>()
            //    .Where(item => !(item is ArtObject))
            //    .SelectMany(items => new IEnumerable<object>)));

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
