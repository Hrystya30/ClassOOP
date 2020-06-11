using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Класи
{
    class OOP_2
    {
        public enum TimeFrame { Year, TwoYears, Long }
        static void Main(string[] args)
        {
            Team Team1 = new Team("Linuvtsi", 7);
            Team Team2 = new Team("Linuvtsi", 7);
            Console.WriteLine(Team1.Equals(Team2));
            Console.WriteLine(Team1 == Team2);
            Console.WriteLine(string.Format(" MyTeam1: {0}, MyTeam2: {1} ", Team1.GetHashCode(), Team2.GetHashCode()));
            
            try
            {
                Team2.ID = -2;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            ResearchTeam Team3 = new ResearchTeam();
            Team3.AddMembers(new Person[3] { new Person("Khrystyna", "Shcherbanik", new DateTime(2000, 04, 30)), new Person("Olga", "Lavruk", new DateTime(2000, 10, 07)), new Person() });
            Team3.AddPapers(new Paper[2] { new Paper("Null", new Person("Igor", "Vinnychuk", new DateTime(1985, 11, 1)), new DateTime(2016, 11, 13)), new Paper() });
            Console.WriteLine(Team3.getTeamType.ToString());
            
            ResearchTeam Team4 = (ResearchTeam)Team3.DeepCopy();
            Team3.NameOfOrganization = "SoftServe";
            Team3.ID = 7;
            Console.WriteLine(Team3.ToString());
            Console.WriteLine(Team4.ToString());

            foreach (Paper pap in Team3)
            {
                Console.WriteLine(pap);
            }

            foreach (Person per in Team3.MembersWithoutPublication())
            {
                Console.WriteLine(per);
            }
            foreach (Paper pap in Team3.LastPapers(2))
            {
                Console.WriteLine(pap);
            }

            Console.ReadKey();
        }
        interface INameAndCopy
        {
            string Name { get; set; }
            public object DeepCopy();
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

            public int birthMonth
            {
                get { return birthDate.Month; }
                set { birthDate = new DateTime(value, birthDate.Year, birthDate.Day); }
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

            public override bool Equals(object obj)
            {
                if (obj.GetType() != this.GetType()) return false;

                Person person = (Person)obj;
                return (this.FirstName == person.FirstName && this.LastName == person.LastName && this.BirthDate == person.BirthDate);
            }

            public override int GetHashCode()
            {
                int hashcode = 0;

                char[] FirstNameChar = firstName.ToCharArray();
                foreach (char ch in FirstNameChar)
                {
                    hashcode += Convert.ToInt32(ch);
                }
                
                char[] LastNameChar = lastName.ToCharArray();
                foreach (char ch in LastNameChar)
                {
                    hashcode += Convert.ToInt32(ch);
                }

                hashcode += birthDate.Year * birthDate.Month;
                return hashcode;
            }

            public Person DeepCopy()
            {
                Person other = new Person();
                other.BirthDate = this.BirthDate;
                other.FirstName = this.FirstName;
                other.LastName = this.LastName;
                return other;
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
                dateOfPublication = time;
            }
        }


        public class Team:INameAndCopy
        {
            protected string nameOfOrganization;
            protected int idOfOrganization;
            public string NameOfOrganization
            {
                get { return nameOfOrganization; }
                set { nameOfOrganization = value; }
            }

            public int ID
            {
                get { return idOfOrganization; }
                set
                {
                    if (idOfOrganization <= 0)
                    {
                        throw new Exception("Your ID is wrong!");
                    }
                    else
                    {
                        value = idOfOrganization;
                    }
                }
            }

            string INameAndCopy.Name
            {
                get { return string.Format("Team of organisation {0} with registration number {1}", nameOfOrganization, idOfOrganization); }
                set { throw new NotImplementedException(); }
            }

            public Team(string nameoforg, int id)
            {
                nameOfOrganization = nameoforg;
                idOfOrganization = id;
            }

            public Team()
            {
                nameOfOrganization = "Ruta";
                idOfOrganization = 5687;
            }

            public object DeepCopy()
            {
                Team newteam = new Team();
                newteam.NameOfOrganization = this.NameOfOrganization;
                newteam.ID = this.ID;
                return newteam;
            }

            public virtual new string ToString()
            {
                return $"{nameOfOrganization} {idOfOrganization}";
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() != this.GetType()) return false;

                Team team = (Team)obj;
                return (this.NameOfOrganization == team.NameOfOrganization && this.ID == team.ID);
            }

            public override int GetHashCode()
            {
                int hashcode = 0;
                foreach(char ch in nameOfOrganization.ToCharArray())
                {
                    hashcode += (int)Convert.ToInt32(ch);
                }
                hashcode += idOfOrganization;
                return hashcode;
            }
        }

        public class ResearchTeam : Team, INameAndCopy
        {
            private string Theme;
            private TimeFrame ResearchDuration;
            private System.Collections.ArrayList Participants = new ArrayList();
            private System.Collections.ArrayList Publications = new ArrayList();
            
            public System.Collections.ArrayList ListOfPublication
            {
                get { return Publications; }
                set { value = Publications; }
            }
            public System.Collections.ArrayList ListOfParticipants
            {
                get { return Participants; }
                set { value = Participants; }
            }
            public ResearchTeam(string nameoforg, string theme, int idoforg, TimeFrame resdur)
            {
                nameOfOrganization = nameoforg;
                Theme = theme;
                idOfOrganization = idoforg;
                ResearchDuration = resdur;
            }

            public ResearchTeam()
            {
                nameOfOrganization = "Ruta";
                Theme = "C#";
                idOfOrganization = 678;
                ResearchDuration = TimeFrame.TwoYears;
            }

            public Paper LastPaper
            {
                get
                {
                    if(Publications.Count == 0)
                    {
                        return null;
                    }
                    int MaxIndex = 0;
                    DateTime MaxDateTime = ((Paper)Publications[0]).dateOfPublication;
                    for (int i=0; i<Publications.Count; i++)
                    {
                        if (((Paper)Publications[i]).dateOfPublication > MaxDateTime)
                        {
                            MaxIndex = i;
                            MaxDateTime = ((Paper)Publications[i]).dateOfPublication;
                        }
                    }
                    return (Paper)Publications[MaxIndex];
                }
            }

            public void AddPapers(params Paper[] AdditionalPapers)
            {
                Publications.AddRange(AdditionalPapers);
            }

            public void AddMembers(params Person[] AdditionalMembers)
            {
                Participants.AddRange(AdditionalMembers);
            }

            public override string ToString()
            {
                string stringListOfPublications = "";
                foreach (Paper pap in Publications)
                {
                    stringListOfPublications += pap.ToString();
                }

                string stringListOfParticipants = "";
                foreach (Person per in Participants)
                {
                    stringListOfParticipants += per.ToString();
                }

                return base.ToString() + string.Format("Theme: {0}, Duration: {1}, Participants: {2}, Publications: {3}", Theme, ResearchDuration, stringListOfParticipants, stringListOfPublications);
            }

            public string ToShortString()
            {
                return base.ToString() + string.Format("Theme: {0}, Duration: {1}", Theme, ResearchDuration);
            }

            public object DeepCopy()
            {
                ResearchTeam CopyTeam = new ResearchTeam(this.nameOfOrganization, this.Theme, this.idOfOrganization, this.ResearchDuration);
                CopyTeam.ListOfPublication = ListOfPublication;
                CopyTeam.ListOfParticipants = ListOfParticipants;
                return CopyTeam;
            }

            public Team getTeamType
            {
                get { return new Team(NameOfOrganization, ID); }
                set { value.NameOfOrganization = this.NameOfOrganization; value.ID = this.ID; }
            }



            public IEnumerable<Person> MembersWithoutPublication()
            {
                ArrayList AuthorsWithoutP = new ArrayList();
                bool Somebool;
                foreach (Person per in Participants)
                {
                    Somebool = true;
                    foreach (Paper pap in Publications)
                    {
                        if (pap.author == per)
                        {
                            Somebool = false;
                            break;
                        }
                    }
                    if (Somebool)
                    {
                        AuthorsWithoutP.Add(per);
                    }

                }
                for (int i = 0; i < AuthorsWithoutP.Count; i++)
                {
                    yield return (Person)AuthorsWithoutP[i];
                }
            }

            public IEnumerator GetEnumerator()
            {
                foreach  (Paper per in Publications)
                {
                    yield return per.ToString();
                }
            }

            public IEnumerable<Paper> LastPapers(int nyears)
            {
                for (int i = 0; i < Publications.Count; i++)
                {
                    if(((Paper)Publications[i]).dateOfPublication.Year >= (DateTime.Now.Year - nyears))
                    {
                        yield return (Paper)Publications[i];
                    }
                }
            }
        }

    }
}
