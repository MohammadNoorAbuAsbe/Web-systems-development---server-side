namespace Lesson_2.Models
{
    public class Student
    {
        string name;
        double age;
        string department;
        static List<Student> studentsList = new List<Student>(); // static keeps the data in the heap , not in the stack

        public Student(string name, double age, string department)
        {
            Name = name;
            Age = age;
            Department = department;
        }

        public string Name { get => name; set => name = value; }
        public double Age { get => age; set => age = value; }
        public string Department { get => department; set => department = value; }

        public Student()
        {
        }

        public int AddMe() {
            studentsList.Add(this);
            return 1;
        }

        public List<Student> GetAll()
        {
            return studentsList;
        }

        public List<Student> GetByDepartmentAndAge(string department, double minAge)
        {
            List<Student> studentsFilter = new List<Student>();
            foreach (var student in studentsList)
            {
                if (student.Department == department && student.Age > minAge)
                {
                    studentsFilter.Add(student);
                }
            }
            return studentsFilter;
        }
    }
}
