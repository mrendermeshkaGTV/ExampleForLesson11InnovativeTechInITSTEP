using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace Lesson_11
{
    internal class Program
    {
        static void PrintTableStudent(Dictionary<string, student> Data)
        {
            string str = $"Name\t|Group\t|year of education\t|\n";

            foreach (var item in Data.Values)
            {
                str += $"{item.Name}\t|{item.Group}\t|{item.yearOfEducation}\t|\n";
            }
            Console.WriteLine(str);
        }


        static void PrintTableGroup(Dictionary<string, Group> Data)
        {
            string str = $"Name\t|login\t|password\t|current subject\t|\n";

            foreach (var item in Data.Values)
            {
                str += $"{item.Name}\t|{item.login}\t|{item.Password}\t|{item.Current_subject}\t|\n";
            }
            Console.WriteLine(str);
        }
        static void PrintSubjectsList(Dictionary<string, subject> Data)
        {
            string str = "";

            foreach (var item in Data.Values)
            {
                str += $"Name{item.Name}\n";
            }
            Console.WriteLine(str);
        }

        static void Main(string[] args)
        {

            IFirebaseConfig ifc = new FirebaseConfig()
            {
                AuthSecret = "AIzaSyCUDQS7JTVFGUf_NDm0j90oF7uixu3eLhM",
                BasePath = "https://innovative-technology-454d7-default-rtdb.europe-west1.firebasedatabase.app"
            };
            IFirebaseClient client;
            while (true)
            {
                Console.WriteLine("1.Вивід даних\n2.Ввід даних");
                int task = int.Parse(Console.ReadLine());
                int subtask;
                switch (task)
                {
                    case 1:
                        Console.WriteLine("1. Студенти, 2.Групи, 3.Предмети");
                        subtask = int.Parse(Console.ReadLine());
                        switch (subtask)
                        {
                            case 1:
                                client = new FireSharp.FirebaseClient(ifc);
                                FirebaseResponse res = client.Get(@"Student/");
                                Dictionary<string, student> DataStudent = JsonConvert.DeserializeObject<Dictionary<string, student>>(res.Body.ToString());
                                PrintTableStudent(DataStudent);
                                break;
                            case 2:
                                client = new FireSharp.FirebaseClient(ifc);
                                FirebaseResponse resp = client.Get(@"Group/");
                                Dictionary<string, Group> DataGroup = JsonConvert.DeserializeObject<Dictionary<string, Group>>(resp.Body.ToString());
                                PrintTableGroup(DataGroup);
                                break;
                            case 3:
                                client = new FireSharp.FirebaseClient(ifc);
                                FirebaseResponse respz = client.Get(@"Group/");
                                Dictionary<string, Group> DataSubject = JsonConvert.DeserializeObject<Dictionary<string, Group>>(respz.Body.ToString());
                                PrintTableGroup(DataSubject);
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("1. Студенти, 2.Групи, 3.Предмети");
                        subtask = int.Parse(Console.ReadLine());
                        switch (subtask)
                        {
                            case 1:
                                Console.WriteLine("Введіть через ентер групу, ім'я студента та рік навчання");
                                var student = new student
                                {
                                    Group = Console.ReadLine(),
                                    Name = Console.ReadLine(),
                                    yearOfEducation = int.Parse(Console.ReadLine())
                                };
                                client = new FireSharp.FirebaseClient(ifc);
                                client.Set(@"Student/" + student.Name, student);
                                break;
                            case 2:
                                Console.WriteLine("Введіть через ентер назву групи, логін, пароль, предмет який проходить зараз");
                                var group = new Group
                                {
                                    Name = Console.ReadLine(),
                                    login = Console.ReadLine(),
                                    Password = Console.ReadLine(),
                                    Current_subject = Console.ReadLine()
                                };
                                client = new FireSharp.FirebaseClient(ifc);
                                client.Set(@"Group/" + group.Name, group);
                                break;
                            case 3:
                                Console.WriteLine("Введіть через ентер назву предмету");
                                var subject = new subject
                                {
                                    Name = Console.ReadLine()
                                };
                                client = new FireSharp.FirebaseClient(ifc);
                                client.Set(@"subject/" + subject.Name, subject);
                                break;
                        }
                        break;
                }
            }
        }
    }
}
