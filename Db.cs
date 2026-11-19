using Microsoft.Data.SqlClient;

namespace AdoNetTasks;

static class Db
{
    public const string ConnStr =
        @"Server=DESKTOP-6Q4JS4E\SQLEXPRESS01;Database=SchoolDB;" +
        "Trusted_Connection=True;TrustServerCertificate=True;";
}

class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

class StudentRepository
{
    public List<Student> GetAll()
    {
        var list = new List<Student>();
        using (var con = new SqlConnection(Db.ConnStr))
        {
            var cmd = new SqlCommand(
                "SELECT Id, FirstName, LastName, Age, Email FROM Students ORDER BY LastName, FirstName", con);
            con.Open();
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var s = new Student();
                    s.Id = r.GetInt32(0);
                    s.FirstName = r.GetString(1);
                    s.LastName = r.GetString(2);
                    s.Age = r.GetInt32(3);
                    s.Email = r.GetString(4);
                    list.Add(s);
                }
            }
        }
        return list;
    }

    public void Add(Student s)
    {
        using (var con = new SqlConnection(Db.ConnStr))
        {
            var cmd = new SqlCommand(
                "INSERT INTO Students (FirstName, LastName, Age, Email) VALUES (@fn, @ln, @age, @email)", con);
            cmd.Parameters.AddWithValue("@fn", s.FirstName);
            cmd.Parameters.AddWithValue("@ln", s.LastName);
            cmd.Parameters.AddWithValue("@age", s.Age);
            cmd.Parameters.AddWithValue("@email", s.Email);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
