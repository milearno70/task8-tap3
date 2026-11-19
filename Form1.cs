using Microsoft.Data.SqlClient;

namespace AdoNetTasks;


public partial class Form1 : Form
{
    StudentRepository repo = new StudentRepository();

    public Form1()
    {
        InitializeComponent();
    }

    void Form1_Load(object sender, EventArgs e)
    {
        LoadList();
    }

    void LoadList()
    {
        try
        {
            dgv.DataSource = repo.GetAll();
            dgv.Columns["Id"].Visible = false;
        }
        catch (SqlException ex)
        {
            MessageBox.Show("Verilənlər bazası xətası: " + ex.Message);
        }
    }

    void btnAdd_Click(object sender, EventArgs e)
    {
        string first = txtFirst.Text.Trim();
        string last = txtLast.Text.Trim();
        string email = txtEmail.Text.Trim();

        
        if (first == "" || last == "")
        {
            MessageBox.Show("Ad və soyad boş ola bilməz.");
            return;
        }

        
        int age;
        if (!int.TryParse(txtAge.Text, out age) || age <= 0)
        {
            MessageBox.Show("Yaş müsbət tam ədəd olmalıdır.");
            return;
        }

        if (email == "")
        {
            MessageBox.Show("E-poçt boş ola bilməz.");
            return;
        }

        try
        {
            var s = new Student();
            s.FirstName = first;
            s.LastName = last;
            s.Age = age;
            s.Email = email;

            repo.Add(s);
            LoadList();  

            txtFirst.Clear();
            txtLast.Clear();
            txtAge.Clear();
            txtEmail.Clear();
        }
        catch (SqlException ex) when (ex.Number == 2627)
        {
           
            MessageBox.Show("Bu e-poçt artıq mövcuddur");
        }
        catch (SqlException ex)
        {
            MessageBox.Show("Verilənlər bazası xətası: " + ex.Message);
        }
    }
}
