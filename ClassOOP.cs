using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Класи
{
    class ClassOOP
    {
        public enum TimeFrame { Year, TwoYears, Long }
        static void Main(string[] args)
        {
            Person first = new Person("Petro", "Gora", new DateTime(2000, 5, 23));
            Console.WriteLine(first.ToString());
            Console.WriteLine(first.ToShortString());
            Person second = new Person();
            Console.WriteLine(second.ToString());
            Console.WriteLine(second.ToShortString());

            ResearchTeam third = new ResearchTeam();
            ResearchTeam team = new ResearchTeam();
            Paper[] papers = new Paper[2];
            papers[0] = new Paper("pub_4", new Person("Anton", "Bahok", new DateTime(1980, 4, 24)), new DateTime(1887, 6, 1));
            papers[1] = new Paper("pub_5", new Person("Petro", "Bahok", new DateTime(1980, 4, 24)), new DateTime(1889, 8, 21));
            team.AddPapers(papers);
            foreach (Paper paper in team.list)
            {
                Console.WriteLine(paper.nameOfPublication);
                Console.WriteLine(paper.Time.ToShortDateString());
            }

            Console.WriteLine(third.ToShortString());
            Console.WriteLine(team.ToString());

        }

        public class Person
        {
            private string firstName;
            private string lastName;
            private System.DateTime birthDate;

            public string FirstName
            {
                get { return firstName; }
                set { firstName = value; }
            }
            public string LastName
            {
                get { return lastName; }
                set { lastName = value; }
            }
            public DateTime BirthDate
            {
                get { return this.birthDate; }
                set { birthDate = value; }
            }
            public int birthYear
            {
                get { return birthDate.Year; }
                set { birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
            }

            public Person(string firstname, string lastname, DateTime birthdate)
            {
                firstName = firstname;
                lastName = lastname;
                birthDate = birthdate;
            }

            public Person()
            {
                firstName = "Khrystyna";
                lastName = "Shcherbanik";
                birthDate = new DateTime(2000, 4, 30);
            }

            public override string ToString()
            {
                return $"Your firstname is {firstName} \nYour lastname is {lastName} \nYour birthday is {birthDate.ToShortDateString()}";
            }

            public string ToShortString()
            {
                return $"{firstName} {lastName}";
            }
        }

        public class Paper
        {
            public string nameOfPublication { get; set; }
            public Person author { get; set; }
            public System.DateTime dateOfPublication { get; set; }

            public Paper(string Name, Person Author, DateTime dateofpublication)
            {
                nameOfPublication = Name;
                author = Author;
                dateOfPublication = dateofpublication;
            }

            public Paper()
            {
                nameOfPublication = "Ruta";
                author = new Person("Petro", "Gora", new DateTime(2000, 5, 23));
                dateOfPublication = new DateTime(2000, 6, 24);
            }
            public override string ToString()
            {
                return $"{nameOfPublication}, {author}, {dateOfPublication.ToShortDateString()}";
            }
            public Paper(string publication, DateTime time)
            {
                nameOfPublication = publication;
                Time = time;
            }
            public string publication;

            public DateTime Time;
        }

        public class ResearchTeam : List<Paper>
        {
            private string nameOfResearch;
            private string nameOfOrganization;
            private int Number;
            private TimeFrame Last;
            private List<Paper> Papers = new List<Paper>() { new Paper("pub_1", new Person("Anton", "Bahok", new DateTime(1980, 4, 24)), new DateTime(1881, 6, 2)), new Paper("pub_2", new Person("Anton", "Bahok", new DateTime(1980, 4, 24)), new DateTime(1883, 6, 2)), new Paper("pub_3", new Person("Anton", "Bahok", new DateTime(1980, 4, 24)), new DateTime(1885, 6, 2)) };

            public string NameOfResearch
            {
                get { return nameOfResearch; }
            }
            public string NameOfOrganization
            {
                get { return nameOfOrganization; }
            }
            public int number
            {
                get { return Number; }
            }
            public TimeFrame last
            {
                get { return Last; }
            }
            public List<Paper> list
            {
                get { return Papers; }
                set { Papers = value; }
            }

            public Paper Paper
            {
                get { return Count == 0 ? null : this.OrderByDescending(p => p.dateOfPublication).FirstOrDefault(); }
            }
            public bool this[TimeFrame frame]
            {
                get { return frame == Last; }
            }
            public ResearchTeam(string nameofser, string nameoforg, int number, TimeFrame last)
            {
                nameOfResearch = nameofser;
                nameOfOrganization = nameoforg;
                Number = number;
                Last = last;
            }
            public ResearchTeam()
            {
                nameOfResearch = "vision";
                nameOfOrganization = "VisionStudio";
                Number = 5;
                Last = TimeFrame.TwoYears;
            }

            public TimeFrame this[int ind]
            {
                get
                {
                    if (ind == 1)
                    {
                        return TimeFrame.Year;
                    }
                    else if (ind == 2)
                    {
                        return TimeFrame.TwoYears;
                    }
                    else
                        return TimeFrame.Long;
                }
            }

            public void AddPapers(Paper[] papers)
            {
                Papers.AddRange(papers);
            }

            public override string ToString()
            {
                return string.Join(",", Papers.Select(p => p.nameOfPublication));
            }
            public string ToShortString()
            {
                return $"{nameOfResearch},{nameOfOrganization},{Number},{Last}";
            }
        }
    }
}
