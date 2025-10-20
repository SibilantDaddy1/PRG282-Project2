using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperheroesApp
{
    public partial class Form2 : Form
    {
        private int total;
        private double avgAge;
        private double avgScore;
        private int sCount;
        private int aCount;
        private int bCount;
        private int cCount;
        public Form2(int total, double avgAge, double avgScore, int sCount, int aCount, int bCount, int cCount)
        {
            InitializeComponent();

            this.total = total;
            this.avgAge = avgAge;
            this.avgScore = avgScore;
            this.sCount = sCount;
            this.aCount = aCount;
            this.bCount = bCount;
            this.cCount = cCount;
            textBox1.Text = total.ToString();
            textBox2.Text = avgAge.ToString("F2");
            textBox3.Text = avgScore.ToString("F2");
            textBox4.Text = sCount.ToString();
            textBox5.Text = aCount.ToString();
            textBox6.Text = bCount.ToString();
            textBox7.Text = cCount.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
