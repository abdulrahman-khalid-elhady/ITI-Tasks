

public class program
{

    public class Subject
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Subject[] Subjects { get; set; }
    }
    static void Main()
    {
        //--1
        List<int> numbers = new List<int>() { 2, 4, 6, 7, 1, 4, 2, 9, 1 };
        var UNNumbers = numbers.Order().Distinct();
        foreach (var number in UNNumbers)
        {
            Console.WriteLine(number);
        }

        //--2
        foreach (var number in UNNumbers)
        {
            Console.WriteLine($"number : {number} , Multi : {number * number}");
        }

        //--3
        string[] names = { "Tom", "Dick", "Harry", "MARY", "Jay" };
        var Only3letterNames = names.Where(name => name.Length == 3);
        foreach (var name in Only3letterNames)
        {
            Console.WriteLine(name);
        }

        //--4
        var task4=names.Where(s=>s.ToLower().Contains("a"))
                        .OrderBy(s =>s.Length);
        foreach (var name in task4)
        {
            Console.WriteLine(name);
        }

        //--5
        var task5 = names.Where(s => s.ToLower().Contains("a"))
                        .OrderBy(s => s.Length)
                        .Take(2);
        foreach (var name in task5)
        {
            Console.WriteLine(name);
        }

        //--6
        List<Student> students = new List<Student>()
        {
            new Student()
            {
                ID = 1, FirstName = "Ali", LastName = "Mohammed",
                Subjects = new Subject[]
                {
                    new Subject(){ Code=22, Name="EF" },
                    new Subject(){ Code=33, Name="UML" }
                }
            },
            new Student()
            {
                ID = 2, FirstName = "Mona", LastName = "Gala",
                Subjects = new Subject[]
                {
                    new Subject(){ Code=22, Name="EF" },
                    new Subject(){ Code=34, Name="XML" },
                    new Subject(){ Code=25, Name="JS" }
                }
            },
            new Student()
            {
                ID = 3, FirstName = "Yara", LastName = "Yousf",
                Subjects = new Subject[]
                {
                    new Subject(){ Code=22, Name="EF" },
                    new Subject(){ Code=25, Name="JS" }
                }
            },
            new Student()
            {
                ID = 4, FirstName = "Ali", LastName = "Ali",
                Subjects = new Subject[]
                {
                    new Subject(){ Code=33, Name="UML" }
                }
            }
        };
        var q1=students.Select(s=> new 
            {fullName=s.FirstName+" "+s.LastName ,
            NoS=s.Subjects.Count()}).ToList();

        foreach(var s in q1)
        {
            Console.WriteLine(s);
        }
        
        var q2=students.Select(s=> new 
            {s.FirstName,s.LastName})
            .OrderByDescending(s=>s.FirstName)
            .ThenBy(s=>s.LastName).ToList();
        foreach(var s in q2)
        {
            Console.WriteLine(s.FirstName+" "+s.LastName);
        }

        var q3=students.SelectMany(s=>s.Subjects,
            (student,subject)=> new 
            {fullName=student.FirstName+" "+student.LastName,
            subjectName=subject.Name})
            .ToList();
        foreach(var s in q3) { 
            Console.WriteLine(s);
        }

        var q4 = students.SelectMany(s => s.Subjects,
            (student, subject) => new
            {
                fullName = student.FirstName + " " + student.LastName,
                subjectName = subject.Name
            }).GroupBy(s=>s.fullName)
            .ToList();
        foreach (var s in q4)
        {
            Console.WriteLine(s.Key);
            foreach (var sub in s)
            {
                Console.WriteLine("   " + sub.subjectName);
            }
        }
    }
}
