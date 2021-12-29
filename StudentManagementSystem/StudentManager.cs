using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;

namespace StudentManagementSystem
{
    public class StudentManager
    {
        SqlConnection _sqlConnection = new SqlConnection(@"server = (localdb)\mssqllocaldb; initial catalog = StudentManagementSystem; integrated security = true");

        public List<Student> GetAll()
        {
            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from StudentManagement", _sqlConnection);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<Student> students = new List<Student>();

            while (sqlDataReader.Read())
            {
                Student student = new Student
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    Name = sqlDataReader["Name"].ToString(),
                    Surname = sqlDataReader["Surname"].ToString(),
                    DateOfBirth = Convert.ToDateTime(sqlDataReader["DateOfBirth"]),
                    Nationality = sqlDataReader["Nationality"].ToString(),
                    Gender = sqlDataReader["Gender"].ToString(),
                    Adress = sqlDataReader["Adress"].ToString()
                };

                students.Add(student);
            }

            sqlDataReader.Close();
            _sqlConnection.Close();

            return students;
        }

        public void Add(Student student)
        {
            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Insert into StudentManagement values(@id, @name, @surname, @dateOfBirth, @nationality, @gender, @adress)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", student.Id);
            sqlCommand.Parameters.AddWithValue("@name", student.Name);
            sqlCommand.Parameters.AddWithValue("@surname", student.Surname);
            sqlCommand.Parameters.AddWithValue("@dateOfBirth", student.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@nationality", student.Nationality);
            sqlCommand.Parameters.AddWithValue("@gender", student.Gender);
            sqlCommand.Parameters.AddWithValue("@adress", student.Adress);

            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void Update(Student student)
        {
            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Update StudentManagement set Name = @name, Surname = @surname, DateOfBirth = @dateOfBirth, Nationality = @nationality, Gender = @gender, Adress = @adress where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", student.Name);
            sqlCommand.Parameters.AddWithValue("@surname", student.Surname);
            sqlCommand.Parameters.AddWithValue("@dateOfBirth", student.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@nationality", student.Nationality);
            sqlCommand.Parameters.AddWithValue("@gender", student.Gender);
            sqlCommand.Parameters.AddWithValue("@adress", student.Adress);
            sqlCommand.Parameters.AddWithValue("@id", student.Id);

            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void Delete(int id)
        {
            ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Delete from StudentManagement where Id = @id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void ConnectionControl()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }
    }
}
