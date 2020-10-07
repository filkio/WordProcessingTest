using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WordProcessingTest
{
    public partial class Form1 : Form
    {
        public string fileTxt;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All filex(*.*)|*.*";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            fileTxt = System.IO.File.ReadAllText(openFileDialog1.FileName,Encoding.GetEncoding(1251));
            //fileTxt = System.IO.File.ReadAllText(openFileDialog1.FileName);
            label1.Text = openFileDialog1.FileName + "\nФайл загружен успешно!";
            if (!string.IsNullOrEmpty(fileTxt))
            {
                checkBox1.Visible = checkBox2.Visible = button2.Visible = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //в num попадает нажатая клавиша
            char num = e.KeyChar;
            //если не является десятичным числом или не клавиша backspace, то указываем, что событие отработано.
            if (!(Char.IsDigit(num)|| num==(char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = !textBox1.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            //путь к файлу, текст который сохранить
            label2.Text = "Сохранение файла...";
            System.IO.File.WriteAllText(saveFileDialog1.FileName, WordProcessingMethod(fileTxt),Encoding.GetEncoding(1251));
            label2.Text = "Файл сохранён!";

        }
        private string WordProcessingMethod(string ProcessingFileTxt)
        {
            
            if (checkBox1.Checked == true && int.TryParse(textBox1.Text, out int result) && result > 1)
            {
                //удаляем слова длиной от 1 до result-1
                result--;
                ProcessingFileTxt = Regex.Replace(ProcessingFileTxt, "\\b[\\w]{1," + result + "}\\b", string.Empty);
            }
            if (checkBox2.Checked == true)
            {
                //Удаляем знаки препинания
                ProcessingFileTxt = Regex.Replace(ProcessingFileTxt, "\\p{P}", string.Empty);
            }
            return ProcessingFileTxt;
        }
    }
}
