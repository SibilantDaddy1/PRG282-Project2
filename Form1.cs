using System;
using System.IO;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
           private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (heroes.Count == 0)
                {
                    MessageBox.Show("No heroes to summarize.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int total = heroes.Count;
                double totalAge = 0;
                double totalScore = 0;
                int sCount = 0, aCount = 0, bCount = 0, cCount = 0;

                foreach (var hero in heroes)
                {
                    totalAge += hero.Age;
                    totalScore += hero.ExamScore;

                    if (hero.Rank == "S-Rank") sCount++;
                    else if (hero.Rank == "A-Rank") aCount++;
                    else if (hero.Rank == "B-Rank") bCount++;
                    else if (hero.Rank == "C-Rank") cCount++;
                }

                double avgAge = totalAge / total;
                double avgScore = totalScore / total;

                List<string> lines = new List<string>();
                lines.Add("Summary Report - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                lines.Add("------------------------------------");
                lines.Add($"Total Heroes: {total}");
                lines.Add($"Average Age: {avgAge:F2}");
                lines.Add($"Average Score: {avgScore:F2}");
                lines.Add($"S-Rank Count: {sCount}");
                lines.Add($"A-Rank Count: {aCount}");
                lines.Add($"B-Rank Count: {bCount}");
                lines.Add($"C-Rank Count: {cCount}");

                File.WriteAllLines("summary.txt", lines);

                Form2 summary = new Form2(total, avgAge, avgScore, sCount, aCount, bCount, cCount);
                summary.ShowDialog();

                MessageBox.Show("Summary successfully generated and saved to summary.txt!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating summary: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please select a hero to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox3.Text, out int age) || age <= 0)
                {
                    MessageBox.Show("Age must be a positive number.");
                    return;
                }

                if (!int.TryParse(textBox5.Text, out int score) || score < 0 || score > 100)
                {
                    MessageBox.Show("Exam score must be between 0 and 100.");
                    return;
                }

                Hero heroToUpdate = heroes.FirstOrDefault(h => h.HeroID == textBox1.Text);

                if (heroToUpdate == null)
                {
                    MessageBox.Show("Hero not found in file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                heroToUpdate.Name = textBox2.Text;
                heroToUpdate.Age = age;
                heroToUpdate.Superpower = textBox4.Text;
                heroToUpdate.ExamScore = score;

                heroToUpdate.Rank = heroToUpdate.CalculateRank(score);
                heroToUpdate.ThreatLevel = heroToUpdate.CalculateThreat(heroToUpdate.Rank);

                FileHandler.OverwriteHeroes(heroes);

                LoadHeroesToGrid();

                MessageBox.Show("Hero updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating hero: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells[0].Value.ToString(); // HeroID
            textBox2.Text = row.Cells[1].Value.ToString(); // Name
            textBox3.Text = row.Cells[2].Value.ToString(); // Age
            textBox4.Text = row.Cells[3].Value.ToString(); // Superpower
            textBox5.Text = row.Cells[4].Value.ToString(); // Score

        }
    }
}
