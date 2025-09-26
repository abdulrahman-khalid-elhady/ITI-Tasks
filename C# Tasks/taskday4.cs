using System;
#region task1
class company
{
    public string name;
    public List<department> departments = new List<department>();
    public List<employee> employees = new List<employee>();
    public List<instructor> instructors = new List<instructor>();
    public company(string name)
    {
        this.name = name;
    }

}
class department
{
    public string name;
    public department(string name)
    {
        this.name = name;
    }
    List<string> employee = new List<string>();
}
class employee
{
    public string name;
    public department dept;
    public List<string> courses = new List<string>();
    public void addcourse(string course)
    {
        courses.Add(course);
    }
    public employee(string name)
    {
        this.name = name;
        this.dept =new department("IT");
    }
    public void display()
    {
        Console.WriteLine(name);
        Console.WriteLine(dept);
        foreach (var course in courses)
        {
            Console.WriteLine(course);
        }
    }
}
class engine
{
   public void start()
    {
        Console.WriteLine("vooo");
    }
}
class car
{
    engine engine = new engine();
    public void start()
    {
        engine.start();
    }
}

#endregion

#region task2
class person
{
    public string name;
    public int age;
    public person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

}
class student:person
{
    public int studentid= idGenerator.genId();
    public List<course> Scourses = new List<course>();
    public List<grade> grades = new List<grade>();

    public student(string name, int age):base(name,age)
    {
        this.studentid = idGenerator.genId();
    }
    public void enrollCourse(course c)
    {
        c.students.Add(this);
        Scourses.Add(c);
        switch(c.CL)
        {
            case courseLevel.beginner:
                Console.WriteLine("Good luck starting out!");
                break;
            case courseLevel.intermediate:
                Console.WriteLine("Keep going, you're making progress!");
                break;
            case courseLevel.advanced:
                Console.WriteLine("This will be challenging!");
                break;
        }
    }
    
}
class instructor:person
{
    public int instructorid;
    public course Mc;
    public instructor(string name, int age):base(name,age)
    {
        this.instructorid = idGenerator.genId();
    }
    public void techCourse(course c)
    {
        c.ins= this;
        this.Mc = c;
    }
    public void introduce()
    {
        Console.WriteLine($"Instructor Name: {name}");
        Console.WriteLine($"Instructor Age: {age}");
        Console.WriteLine($"Instructor ID: {instructorid}");
        Console.WriteLine($"Course Name: {Mc.courseName}");
        Console.WriteLine("Students Enrolled:");
        foreach (var student in Mc.students)
        {
            Console.WriteLine(student.name);
        }

    }
}
class course
{
    public string courseName;
    public instructor ins;
    public courseLevel CL;

    public course(string courseName, instructor ins,courseLevel CL)
    {
        this.courseName = courseName;
        this.ins = ins;
        this.CL = CL;
    }
    public List<student> students = new List<student>();
}
#endregion

#region task3
abstract class SHAPES
{
    public abstract double area();
    public abstract void draw();
}
class circle:SHAPES
{
    double radius;
    public circle(double radius)
    {
        this.radius = radius;
    }
    public override double area()
    {
        return Math.PI * radius * radius;
    }
    public override void draw()
    {
        Console.WriteLine(" ***");
        Console.WriteLine("*   *");
        Console.WriteLine(" ***");
    }
}
class rectangle:SHAPES
{
    double length;
    double width;
    public rectangle(double length, double width)
    {
        this.length = length;
        this.width = width;
    }
    public override double area()
    {
        return length * width;
    }
        public override void draw()
    {
        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < width; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
        }

    }
#endregion

#region task4
static class idGenerator
{
    static int id=1;
    public static int genId()
    {
        return id++;
    }
} 
class grade
{
    public List<int> g;
    public static int operator +(grade s1,grade s2)
    {
        int sum = 0;
        foreach (var i in s1.g)
        {
            sum += i;
        }
        foreach (var i in s2.g)
        {
            sum += i;
        }
        return sum;
    }
    public static bool operator ==(grade s1, grade s2)
    {
        int sum1 = 0,sum2=0;
        foreach (var i in s1.g)
        {
            sum1 += i;
        }
        foreach (var i in s2.g)
        {
            sum2 += i;
        }
        return sum1 == sum2;
    }
    public static bool operator !=(grade s1, grade s2)
    {
        int sum1 = 0, sum2 = 0;
        foreach (var i in s1.g)
        {
            sum1 += i;
        }
        foreach (var i in s2.g)
        {
            sum2 += i;
        }
        return sum1 != sum2;
    }

}
#endregion

#region task5
enum courseLevel
{
    beginner,
    intermediate,
    advanced
}
#endregion
public class program
    {
    public static void Main(string[] args)
    {
        //employee emp = new employee("John");
        //emp.addcourse("C#");
        //emp.display();
        //List<SHAPES> shapes = new List<SHAPES>
        //{
        //    new circle(3),
        //    new rectangle(3,5)
        //};
        //foreach (var shape in shapes)
        //{
        //    Console.WriteLine(shape.area());
        //    shape.draw();
        //}

        //create system

        company myCompany = new company("TechCorp");
        department itDept = new department("IT");
        myCompany.departments.Add(itDept);
        employee emp1 = new employee("Alice");
        myCompany.employees.Add(emp1);
        instructor instr1 = new instructor("Bob", 35);
        myCompany.instructors.Add(instr1); 
        course course1 = new course("C# Programming", instr1, courseLevel.beginner);
        student stud1 = new student("Charlie", 20);
        stud1.enrollCourse(course1);
        instr1.techCourse(course1);
        grade grade1 = new grade { g = new List<int> { 85, 90, 78 } };
        stud1.grades.Add(grade1);
        // Generate report
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Company: {myCompany.name}");
        Console.WriteLine("------------------------------------");
        foreach (var dept in myCompany.departments)
        {
            Console.WriteLine($"Department: {dept.name}");
            int empCount = myCompany.employees.Count(employee => employee.dept.name == dept.name);
            Console.WriteLine($"Number of Employees: {empCount}");
        }
        Console.WriteLine("------------------------------------");
        foreach (var instr in myCompany.instructors)
        {
            instr.introduce();
        }
        Console.WriteLine("------------------------------------");
        foreach (var stud in new List<student> { stud1 })
        {
            Console.WriteLine($"Student Name: {stud.name}");
            Console.WriteLine("Enrolled Courses:");
            foreach (var c in stud.Scourses)
            {
                Console.WriteLine(c.courseName);
            }
            int totalGrades = stud.grades.Sum(g => g.g.Sum());
            Console.WriteLine($"total gardes : {totalGrades}");
        }


    }
}


