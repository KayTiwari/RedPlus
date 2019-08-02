using RedPlus.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RedPlus
{
    class VM : BaseVM
    {
        private string username;
        private string password;
        private string email;
        private string confirmpassword;
        private string loginname;
        private string loginpass;
        private ObservableCollection<Patient> patients;
        private string firstname;
        private string lastname;
        private int age;
        private BloodType bloodtype;
        private string rhfactor;

        public VM()
        {
            this.Patients = new ObservableCollection<Patient>();
            this.FillWithPatients();
            this.Patients.CollectionChanged += (s, arg) =>
            {
                if (arg.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            };
            this.ClickMeCommand = new SimpleCommand((obj) => true,
                (obj) =>
                {
                    Patient newpt = new Patient()
                    {
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Age = this.Age,
                        BloodType = this.BloodType,
                        RHFactor = this.RHFactor
                    };
                    this.Patients.Add(newpt);
                    Console.WriteLine(newpt);
                    this.AddPtToDB(newpt);
                    this.OnNotify();

                });
        }

        public int Login_Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sql = new SqlConnection("Data Source=D6125;Initial Catalog=MyDB;Integrated Security=True;"))
            {
                sql.Open();
                try
                {
                    string query = "SELECT COUNT(1) FROM UserAuth WHERE Username=@loginname AND Password=@loginpass";
                    SqlCommand cmd = new SqlCommand(query, sql);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@loginname", loginname);
                    cmd.Parameters.AddWithValue("@loginpass", loginpass);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        HomeScreen hm = new HomeScreen();             
                        hm.Show();
                        return 1;

                    }
                    else if (count != 1)
                    {
                        MessageBox.Show("Login failed");
                        return 0;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
            return 0;
        }

        internal int AddPtToDB(Patient pt)
        {
            Console.WriteLine(bloodtype);
            using (SqlConnection sql = new SqlConnection("Data Source=D6125;Initial Catalog=MyDB;Integrated Security=True;"))
            {
                sql.Open();
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    SqlTransaction transaction = sql.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@FirstName", pt.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", pt.LastName);
                    cmd.Parameters.AddWithValue("@Age", pt.Age);
                    cmd.Parameters.AddWithValue("@BloodType", pt.BloodType);
                    cmd.Parameters.AddWithValue("@RHFactor", pt.RHFactor);
                    cmd.CommandText = @"INSERT INTO Patients (FirstName, LastName, Age, BloodType, RHFactor)
                    VALUES (@FirstName, @LastName, @Age, @BloodType, @RHFactor)
                    SELECT SCOPE_IDENTITY()";
                    cmd.ExecuteScalar();
                    transaction.Commit();
                    Console.WriteLine(pt);
                    return 1;
                }
            }
        }

        public void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (this.password != this.confirmpassword)
            {
                MessageBox.Show("Passwords do not match!");
            }
            using (SqlConnection sql = new SqlConnection("Data Source=D6125;Initial Catalog=MyDB;Integrated Security=True;"))
            {
                sql.Open();
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    SqlTransaction transaction = sql.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.CommandText = @"INSERT INTO UserAuth (Username, Password, Email)
                    VALUES (@Username, @Password, @Email)
                    SELECT SCOPE_IDENTITY()";
                    cmd.ExecuteScalar();
                    transaction.Commit();
                    MessageBox.Show("Account successfully created!");
                }
            }
        }

        public void FillWithPatients()
        {
            using (SqlConnection sql = new SqlConnection("Data Source=D6125;Initial Catalog=MyDB;Integrated Security=True;"))
            {
                sql.Open();
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    SqlTransaction transaction = sql.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"SELECT FirstName, LastName, Age, BloodType, RHFactor
                    FROM Patients";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Patient pt = new Patient();
                            pt.FirstName = rdr.GetString(0);
                            pt.LastName = rdr.GetString(1);
                            pt.Age = rdr.GetInt32(2);
                            pt.BloodType = (BloodType)rdr.GetInt32(3);
                            pt.RHFactor = rdr.GetString(4);

                            this.Patients.Add(pt);
                        }
                    }
                }
            }
        }

        public ICommand ClickMeCommand { get; set; }



        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                Console.WriteLine("Username is: " + this.username);
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return this.confirmpassword;
            }
            set
            {
                this.confirmpassword = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public string LoginName
        {
            get
            {
                return this.loginname;
            }
            set
            {
                this.loginname = value;
                Console.WriteLine(loginname);
            }
        }

        public string LoginPass
        {
            get
            {
                return this.loginpass;
            }
            set
            {
                this.loginpass = value;
                Console.WriteLine(loginpass);
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstname;
            }
            set
            {
                this.firstname = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastname;
            }
            set
            {
                this.lastname = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }
        public BloodType BloodType
        {
            get
            {
                return this.bloodtype;
            }
            set
            {
                this.bloodtype = value;
                Console.WriteLine(bloodtype);
            }
        }

        public string RHFactor
        {
            get
            {
                return this.rhfactor;
            }
            set
            {
                this.rhfactor = value;
                Console.WriteLine(rhfactor);
            }
        }



        public ObservableCollection<Patient> Patients
        {
            get
            {
                return this.patients;
            }
            set
            {
                this.patients = value;
                this.OnNotify();
            }
        }
    }
}
