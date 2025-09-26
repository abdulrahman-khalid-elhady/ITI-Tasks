using System;
using System.Globalization;
class calc()
{
    int a = 5, b = 3;
    public void sum(int x, int y)
    {
        Console.WriteLine(x + y);
    }
    public void sum(float x, float y)
    {
        Console.WriteLine(x + y);
    }
    public void sub(int x, int y)
    {
        Console.WriteLine(x - y);
    }
    public void sub(float x, float y)
    {
        Console.WriteLine(x - y);
    }
    public void mult(int x, int y)
    {
        Console.WriteLine(x * y);
    }
    public void mult(float x, float y)
    {
        Console.WriteLine(x * y);
    }
    public void div(int x, int y)
    {
        if (y != 0) Console.WriteLine(x / y);
        else Console.WriteLine("you can not divide by Zero");
    }
    public void div(float x, float y)
    {
        if (y != 0) Console.WriteLine(x / y);
        else Console.WriteLine("you can not divide by Zero");
    }
    public void displayAll()
    {
        Console.WriteLine("Addition of " + a + " and " + b + " is: ");
        sum(a, b);
        Console.WriteLine("Subtraction of " + a + " and " + b + " is: ");
        sub(a, b);
        Console.WriteLine("Multiplication of " + a + " and " + b + " is: ");
        mult(a, b);
        Console.WriteLine("Division of " + a + " and " + b + " is: ");
        div(a, b);
    }

}

class question
{
    string header, body;
    int mark;
    public question(string h, string b, int m)
    {
        header = h;
        body = b;
        mark = m;
    }
    public void display()
    {
        Console.WriteLine("Marks: " + mark);
        Console.WriteLine(header);
        Console.WriteLine(body);
    }
    public int getMark()
    {
        return mark;
    }
}
class MCQ
{
    question q;
    string[] chooses;
    int correct;
    bool ans = false;
    public MCQ(string h, string b, int m, string[] c, int cr)
    {
        q = new question(h, b, m);
        chooses = c;
        correct = cr;
    }
    public void ask()
    {
        q.display();
        for (int i = 0; i < chooses.Length; i++)
        {
            Console.WriteLine((i + 1) + ") " + chooses[i]);
        }
        Console.WriteLine("choose the correct answer: ");
        int userAnswer = Convert.ToInt32(Console.ReadLine());
        if (userAnswer == correct)
        {
            bool ans = true;
            Console.WriteLine("Correct!");
        }
        else
        {
            Console.WriteLine("Wrong! The correct answer is: " + chooses[correct - 1]);
        }
    }
    public void display()
    {
        q.display();
        for (int i = 0; i < chooses.Length; i++)
        {
            Console.WriteLine((i + 1) + ") " + chooses[i]);
        }
        Console.WriteLine("The correct answer is: " + chooses[correct - 1]);
    }
}
class Program
{

    static void Main()
    {
        //--1
        //    calc obj = new calc();
        //    obj.displayAll();

        //--2
        //MCQ q1 = new MCQ( "Choose the correct answer from the following options:", "What is the capital of France?", 5, new string[] { "Berlin", "Madrid", "Paris", "Rome" }, 3);
        //q1.display();

        //--3
        //MCQ q2 = new MCQ("Choose the correct answer from the following options:", "Which planet is known as the Red Planet?", 5, new string[] { "Earth", "Mars", "Jupiter", "Saturn" }, 2);
        //q2.display();


        //--4
        //Console.WriteLine("Enter number of questions: ");
        //int n = Convert.ToInt32(Console.ReadLine());
        //MCQ[] qs = new MCQ[n];
        //for (int i = 0; i < n; i++)
        //{
        //    Console.WriteLine("Enter header: ");
        //    string h = Console.ReadLine();
        //    Console.WriteLine("Enter body: ");
        //    string b = Console.ReadLine();
        //    Console.WriteLine("Enter marks: ");
        //    int m = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("Enter number of choices: ");
        //    int c = Convert.ToInt32(Console.ReadLine());
        //    string[] chooses = new string[c];
        //    for (int j = 0; j < c; j++)
        //    {
        //        Console.WriteLine("Enter choice " + (j + 1) + ": ");
        //        chooses[j] = Console.ReadLine();
        //    }
        //    Console.WriteLine("Enter correct answer (1-" + c + "): ");
        //    int cr = Convert.ToInt32(Console.ReadLine());
        //    qs[i] = new MCQ(h, b, m, chooses, cr);
        //}
        //for (int i = 0; i < n; i++)
        //{
        //    qs[i].display();
        //}

        //--5 
        //    int score = 0;
        //for (int i = 0; i < n; i++)
        //{
        //    qs[i].ask();
        //    if (qs[i].ans) score += qs[i].q.getMark();

        //}
    }
}
    





