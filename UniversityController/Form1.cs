using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversityController
{


    public partial class Form1 : Form
    {
        public static int userStatus = 0;
        static List<Button> buttons = new List<Button>();
        static int page = 1;
        Button backButton = new Button();
        Button saveButton = new Button();
        Button addButton = new Button();
        Button deleteButton = new Button();
        DataGridView dataGridView = new DataGridView();

        static string selectedTable = "";

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-NCJ80S5\\SQLEXPRESS2002;Trusted_Connection=Yes;DataBase=SystemOfKPIManager;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }

        private void StudentSelectButton_Click(object sender, EventArgs e)
        {
            SelectUserStatus(2, false);

        }
        private void TeacherSelectButton_Click(object sender, EventArgs e)
        {
            SelectUserStatus(1, true);
        }

        void SelectUserStatus(int status, bool acces)
        {
            page++;

            saveButton.Enabled = acces;
            addButton.Enabled = acces;
            deleteButton.Enabled = acces;
            dataGridView.ReadOnly = !acces;

            userStatus = status;
            TeacherSelectButton.Visible = false;
            StudentSelectButton.Visible = false;
            ShowTables();
        }

        private void ShowTables()
        {
            buttons.Clear();
            DataTable dt_user = Select("SELECT * FROM sys.objects WHERE type in (N'U')"); // получаем данные из таблицы
            backButton.Visible = true;

            saveButton.Visible = false;
            addButton.Visible = false;
            deleteButton.Visible = false;

            backButton.Size = new Size(this.Size.Width - 35, this.Size.Height / dt_user.Rows.Count);
            if (!backButton.Created)
            {
                CreateButton(ref backButton, "Back", new Size(this.Size.Width - 35, this.Size.Height / dt_user.Rows.Count), new Point(0, 0), ClickOnBackButton);
                backButton.BackColor = Color.Gray;
                Controls.Add(backButton);
            }

            for (int i = 0; i < dt_user.Rows.Count-3; i++)
            {
                Button button = new Button();
                CreateButton(ref button, Convert.ToString(dt_user.Rows[i][0]), new Size(this.Size.Width - 35, this.Size.Height / dt_user.Rows.Count), new Point(0, (this.Size.Height / dt_user.Rows.Count) * (buttons.Count + 1)), ClickOnTableButton);
                buttons.Add(button);
            }

            foreach (var item in buttons)
            {
                Controls.Add(item);
            }
        }

        private void ShowStatusChooseButtons(int status)
        {
            userStatus = status;
            TeacherSelectButton.Visible = true;
            StudentSelectButton.Visible = true;

            foreach (var item in buttons)
            {
                item.Visible = false;
            }
        }

        delegate void ClickOnButtonHandler(object sender, EventArgs e);

        void CreateButton(ref Button button, string text, Size size, Point location, ClickOnButtonHandler clickOnButton)
        {
            button.Text = text;
            button.Size = size;
            button.Location = location;
            button.Click += clickOnButton.Invoke;
        }

        void ClickOnTableButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            page++;
            //  MessageBox.Show(Convert.ToString(page)); // выводим данные

            foreach (var item in buttons)
            {
                item.Visible = false;
            }
            ShowFieldsInTable(button.Text, button);
        }

        void ClickOnBackButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (page > 1)
                page--;
            //  MessageBox.Show(Convert.ToString(page)); // выводим данные

            switch (page)
            {
                case 1:
                    backButton.Visible = false;
                    ShowStatusChooseButtons(0);
                    break;
                case 2:
                    dataGridView.Visible = false;
                    ShowTables();
                    break;
                case 3:
                    ShowFieldsInTable(button.Text, button);
                    break;
            }
        }
        void ClickOnSaveButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-NCJ80S5\\SQLEXPRESS2002;Trusted_Connection=Yes;DataBase=SystemOfKPIManager;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = $"SELECT * FROM [dbo].[{selectedTable}]";                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            DataTable dt_user = (DataTable)dataGridView.DataSource;

            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.Update(dt_user);

            dt_user.Clear();
            sqlDataAdapter.Fill(dt_user);
        }
        void ClickOnAddButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            DataTable dt_user = (DataTable)dataGridView.DataSource;
            DataRow row = dt_user.Rows[0].Table.NewRow();
            dt_user.Rows.Add(row);
        }
        void ClickOnDeleteButton(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                dataGridView.Rows.Remove(row);
            }
        }



        private void ShowFieldsInTable(string nameTable, Button button)
        {
            DataTable dt_user = Select($"SELECT * FROM [dbo].[{nameTable}]"); // получаем данные из таблицы

            selectedTable = nameTable;

            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AllowUserToAddRows = false;


            saveButton.Visible = true;
            addButton.Visible = true;
            deleteButton.Visible = true;
            backButton.Size = new Size(backButton.Size.Width / 4, backButton.Size.Height);

            if (!saveButton.Created)
            {
                CreateButton(ref saveButton, "Save", new Size(backButton.Size.Width, backButton.Size.Height), new Point(backButton.Size.Width, 0), ClickOnSaveButton);
                saveButton.BackColor = Color.Gray;
                Controls.Add(saveButton);
            }
            if (!addButton.Created)
            {
                CreateButton(ref addButton, "Add", new Size(backButton.Size.Width, backButton.Size.Height), new Point(backButton.Size.Width * 2, 0), ClickOnAddButton);
                addButton.BackColor = Color.Gray;
                Controls.Add(addButton);
            }
            if (!deleteButton.Created)
            {
                CreateButton(ref deleteButton, "Delete", new Size(backButton.Size.Width, backButton.Size.Height), new Point(backButton.Size.Width * 3, 0), ClickOnDeleteButton);
                deleteButton.BackColor = Color.Gray;
                Controls.Add(deleteButton);
            }


            if (!dataGridView.Visible)
                dataGridView.Visible = true;

            dataGridView.DataSource = dt_user;

            // MessageBox.Show(Convert.ToString(dataGridView.ColumnCount)); // выводим данные
            dataGridView.Size = new Size(this.Size.Width - 35, this.Size.Height - button.Size.Height);
            dataGridView.Location = new Point(0, button.Size.Height);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!dataGridView.Created)
            {
                Controls.Add(dataGridView);
            }
        }
    }
}
