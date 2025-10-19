using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SuperheroesApp
{

    public partial class Form1 : Form
    {
        private List<Hero> heroes = new List<Hero>();
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadHeroesToGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox3.Text, out int age) || age <= 0)
                {
                    MessageBox.Show("Age must be a valid positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox4.Text, out int score) || score < 0 || score > 100)
                {
                    MessageBox.Show("Exam Score must be between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create hero and save
                Hero hero = new Hero(textBox1.Text, textBox2.Text, age, textBox4.Text, score);
                FileHandler.SaveHero(hero);

                MessageBox.Show("Superhero added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadHeroesToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding superhero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadHeroesToGrid()
        {
            heroes = FileHandler.LoadHeroes();
            dataGridView1.Rows.Clear();

            foreach (var hero in heroes)
            {
                dataGridView1.Rows.Add(hero.HeroID, hero.Name, hero.Age, hero.Superpower,
                    hero.ExamScore, hero.Rank, hero.ThreatLevel);
            }
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Hero ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Age";
            dataGridView1.Columns[3].Name = "Superpower";
            dataGridView1.Columns[4].Name = "Exam Score";
            dataGridView1.Columns[5].Name = "Rank";
            dataGridView1.Columns[6].Name = "Threat Level";
            dataGridView1.AllowUserToAddRows = false;
        }

    }
}
