using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Class_5
{
    public partial class Form1 : Form
    {
        Drob a, b;
        public Form1()
        {
            InitializeComponent();
            a = new Drob("A", AChange, ZeroEvent, OnChanging_A);
            b = new Drob("B", BChange, ZeroEvent, OnChanging_B);
            label13.Text = label14.Text = "";
        }
        void ZeroEvent(Drob sender, DrobArgs e)
        {
            if (MessageBox.Show("Дробь " + sender.name + " получила нулевой знаменатель," +
                "\nкоторый был заменён единицей.\n\n Восстановить прежнее значение дроби?",
                "Обнуление знаменателя", MessageBoxButtons.YesNo) == DialogResult.Yes)
                sender.SetDrob(e.OldP, e.OldQ);
        }
        void OnChanging_A(Drob sender, DrobArgs e)
        {
            if (Math.Abs(sender.Value) > 100)
            {
                sender.SetDrob(e.OldP, e.OldQ);
                AChange(sender, e);
            }
        }
        void OnChanging_B(Drob sender, DrobArgs e)
        {
            if (sender.Q % 2 == 0)
            {
                sender.SetDrob(e.OldP, e.OldQ);
                BChange(sender, e);
            }
        }
        void AChange(Drob sender, EventArgs e)
        {
            if (a != null)
            {
                groupBox1.Text = sender.name;
                textBox1.Text = sender.P.ToString();
                textBox2.Text = sender.Q.ToString();
                textBox3.Text = sender.Value.ToString();
                if (a != null && b != null)
                {
                    label7.Text = a.Compare(b).ToString();
                    label8.Text = b.Compare(a).ToString();
                    if (a.Simple)
                        label13.Text = "Simple";
                    else label13.Text = "NoSimple";
                }
            }
            else
            {
                sender = new Drob();
                textBox1.Text = sender.P.ToString();
                textBox2.Text = sender.Q.ToString();
                textBox3.Text = sender.Value.ToString();
            }
        }

        void BChange(Drob sender, EventArgs e)
        {
            if(b != null)
            {
                groupBox2.Text = sender.name;
                textBox4.Text = sender.P.ToString();
                textBox5.Text = sender.Q.ToString();
                textBox6.Text = sender.Value.ToString();
                if (a != null && b != null)
                {
                    label8.Text = b.Compare(a).ToString();
                    label7.Text = a.Compare(b).ToString();
                    if (b.Simple)
                        label14.Text = "Simple";
                    else label14.Text = "NoSimple";
                }
            }
            else
            {
                sender = new Drob();
                textBox4.Text = sender.P.ToString();
                textBox5.Text = sender.Q.ToString();
                textBox6.Text = sender.Value.ToString();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(a != null)
            {
                if (int.TryParse(textBox1.Text, out int x) && a.Changable)
                {
                    a.P = x;
                }
                else
                    AChange(a, new EventArgs());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (a != null)
            {
                if (int.TryParse(textBox2.Text, out int x) && a.Changable)
                {
                    a.Q = x;
                }
                else
                    AChange(a, new EventArgs());
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (a != null)
            {
                if (e.KeyChar == (char)(13))
                {
                    if (double.TryParse(textBox3.Text, out double x) && a.Changable)
                    {
                        a.Value = x;
                    }
                    else
                        AChange(a, new EventArgs());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            a.Swap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            a.Add(b);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(a.Changable)
            MessageBox.Show(a.ToString());
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (b != null)
            {
                if (int.TryParse(textBox4.Text, out int x) && b.Changable)
                    b.P = x;
                else
                    BChange(b, new EventArgs());
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (b != null)
            {
                if (int.TryParse(textBox5.Text, out int x) && b.Changable)
                    b.Q = x;
                else
                    BChange(b, new EventArgs());
            }
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (b != null)
            {
                if (e.KeyChar == (char)(13))
                {
                    if (double.TryParse(textBox6.Text, out double x) && b.Changable)
                        b.Value = x;
                    else
                        BChange(b, new EventArgs());
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            b.Swap();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            b.Add(a);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(b.Changable)
            MessageBox.Show(b.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            a.Sub(b);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            a.Mult(b);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            b.Sub(a);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            a.Div(b);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            b.Mult(a);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            b.Div(a);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            a.Assign(b);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            a.Changable = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            b.Changable = checkBox2.Checked;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            b.Assign(a);
        }
    }
}
