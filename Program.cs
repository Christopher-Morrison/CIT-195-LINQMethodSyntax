using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LINQMethodSyntax
{
    class Program
    {
        public class Student{
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition {get;set;}
        }
        public class StudentClubs
        {
            public int StudentID { get; set; }
            public string ClubName { get; set; }
        }
        public class StudentGPA
        {
            public int StudentID { get; set;}
            public double GPA    { get; set;}
        }
        static void Main(string[] args)
        {
            // Student collection
            IList < Student > studentList = new List < Student >() { 
                    new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                    new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                    new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                    new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                    new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                    new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                    new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
            };
            // Student GPA Collection
            IList < StudentGPA > studentGPAList = new List < StudentGPA > () {
                    new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                    new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                    new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                    new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                    new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                    new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                    new StudentGPA() { StudentID = 7,  GPA=1.0 }
                };
            // Club collection
            IList < StudentClubs > studentClubList = new List < StudentClubs >() {
                new StudentClubs() {StudentID=1, ClubName="Photography" },
                new StudentClubs() {StudentID=1, ClubName="Game" },
                new StudentClubs() {StudentID=2, ClubName="Game" },
                new StudentClubs() {StudentID=5, ClubName="Photography" },
                new StudentClubs() {StudentID=6, ClubName="Game" },
                new StudentClubs() {StudentID=7, ClubName="Photography" },
                new StudentClubs() {StudentID=3, ClubName="PTK" },
            };

            // a) Group by GPA and display the student's IDs
            var result = studentGPAList.GroupBy(s=>s.GPA);
            foreach (var r in result){
                Console.WriteLine($"GPA: {r.First().GPA}");
                foreach (var student in r){
                    Console.WriteLine(student.StudentID);
                }
            }

            // b) Sort by Club, then group by Club and display the student's IDs
            var club = studentClubList.OrderBy(s=> s.ClubName).GroupBy(s=> s.ClubName);
            foreach (var c in club){
                Console.WriteLine($"{c.First().ClubName} Club");
                foreach (var student in c){
                    Console.WriteLine(student.StudentID);
                }
            }

            //c) Count the number of students with a GPA between 2.5 and 4.0
            var students = studentGPAList.Where(s=> s.GPA >= 2.5 && s.GPA <= 4);
            Console.WriteLine($"\nNumber of students with gpa between 2.5 and 4.0 is: {students.Count()}");

            //d) Average all student's tuition
            var aveTuition = studentList.Average(s=> s.Tuition);
            Console.WriteLine($"\nAverage student tuition: {aveTuition}");

            //e) Find the student paying the most tuition and display their name, major and tuition.
            var highTuition = studentList.Max(s=> s.Tuition);
            Console.WriteLine("\nStudents paying the highest tuition");
            foreach (var student in studentList) {
                if (student.Tuition == highTuition)
                Console.WriteLine(student.StudentName + " $" + student.Tuition);
            }

            // f) Join the student list and student GPA list on student ID and display the student's name, major and gpa
            var studentInfo = studentList.Join(studentGPAList,
                                student => student.StudentID,
                                gpa => gpa.StudentID,
                                (student, gpa) => new
                                {
                                    student.StudentName,
                                    student.Major,
                                    StudentGpa= gpa.GPA
                                });
            Console.WriteLine("\nStudent's Name, Major, and GPA: ");
            foreach (var student in studentInfo){
                Console.WriteLine($"Name: {student.StudentName}, Major: {student.Major}, GPA: {student.StudentGpa}");
            }

            //g) Join the student list and student club list. 
            //Display the names of only those students who are in the Game club.
            var innerJoin = studentList.Join(studentClubList,
                            student => student.StudentID,
                            club => club.StudentID,
                            (student, club) => new{
                                student.StudentName,
                                club.ClubName
                            });
            Console.WriteLine("\nStudents in the Game club: ");
            foreach ( var student in innerJoin){
                if (student.ClubName == "Game")
                Console.WriteLine($"{student.StudentName}");
            }

        }
    }
}