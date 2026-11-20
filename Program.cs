using System;
using System.Text;

class Bell
{
    public delegate void BellHandler();

    public event BellHandler? Event1;
    public event BellHandler? Event2;

    private readonly int lessonNumber;

    public Bell(int lessonNumber)
    {
        this.lessonNumber = lessonNumber;
    }

    public void AnnounceStart()
    {
        Console.WriteLine($"Дзвінок на {lessonNumber} урок.");
    }

    public void AnnounceEnd()
    {
        Console.WriteLine($"Дзвінок з {lessonNumber} уроку.");
    }

    public void Ring()
    {
        Event1?.Invoke();
        Event2?.Invoke();
    }
}

class Teacher
{
    private readonly string name;

    public Teacher(string name)
    {
        this.name = name;
    }

    public void GoToLesson()
    {
        Console.WriteLine($"Вчитель {name} йде на урок.");
    }

    public void GoToTeachersRoom()
    {
        Console.WriteLine($"Вчитель {name} йде до вчительської.");
    }
}

class Student
{
    private readonly string name;

    public Student(string name)
    {
        this.name = name;
    }

    public void GoToLesson()
    {
        Console.WriteLine($"Учень {name} йде на урок.");
    }

    public void GoToBreak()
    {
        Console.WriteLine($"Учень {name} йде на перерву.");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        int lessonCount;
        while (true)
        {
            Console.Write("Введіть кількість уроків: ");
            if (int.TryParse(Console.ReadLine(), out lessonCount) && lessonCount > 0)
            {
                break;
            }

            Console.WriteLine("Введіть додатне ціле число.");
        }

        Teacher teacher = new Teacher("Вікторія Вікторівна");
        Student student = new Student("Ілля");

        for (int i = 1; i <= lessonCount; i++)
        {
            Console.WriteLine($"\nУрок {i}");

            Bell bell = new Bell(i);

            bell.Event1 += bell.AnnounceStart;
            bell.Event1 += teacher.GoToLesson;
            bell.Event1 += student.GoToLesson;

            bell.Event2 += bell.AnnounceEnd;
            bell.Event2 += teacher.GoToTeachersRoom;
            bell.Event2 += student.GoToBreak;

            bell.Ring();
        }
    }
}
