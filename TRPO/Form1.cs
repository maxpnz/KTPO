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
using System.IO;

namespace TRPO
{
    public partial class Form1 : Form
    {
        int[] Mass = new int[100];
        int i;
        FileInfo file = new FileInfo("array.txt");
        public Form1()
        {
            InitializeComponent();
        }
        public static bool StringIsValid(string str)   //Проверка файла на буквы и пустоту
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^[1-9 ]+$");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Добавление элемента в массив
            if ((textBox2.Text != null) & (textBox2.Text != ""))
            {
                if ((Convert.ToInt32(textBox2.Text) < 30000) & (Convert.ToInt32(textBox2.Text) > 0) & (i < 100))
                {
                    Mass[i] = Convert.ToInt32(textBox2.Text);
                    i++;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = false;
                }
                else
                    MessageBox.Show("Введено недопустимое число или превышено максимальное число элементов!!!");

            }
            else
            {
                MessageBox.Show("Поле ввода пустое");
            }
            Output();
            textBox2.Text = null;

        }
        public void Output()
        {
            //Вывод массив
            label3.Text = null;
            for (int k = 0; k < i; k++)
            {
                label3.Text = label3.Text + Convert.ToString(Mass[k]) + " ";
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ограничение на ввод
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Array.Clear(Mass, 0, Mass.Length);
            i = 0;
            label3.Text = null;
            button1.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = false;

        }

        //Удаление одинаковых элементов
        private void button2_Click(object sender, EventArgs e)
        {
            int[] result = Mass.Reverse().Distinct().Reverse().ToArray();
            label3.Text = null;
            int index = result.Length;
            if (result[index - 1] != 0)
            {
                for (int i = 0; i < result.Length; i++)         //Вывод на в текстбокск результат
                {
                    label3.Text = label3.Text + result[i].ToString() + " ";
                };
            }
            else          //В случае если в конец пишется ноль, не выводим
            {
                for (int i = 0; i < result.Length - 1; i++)         //Вывод на в текстбокск результат
                {
                    label3.Text = label3.Text + result[i].ToString() + " ";
                };
            }
            Array.Clear(Mass, 0, Mass.Length);
            Array.Clear(result, 0, result.Length);
            i = 0;
            button2.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            label3.Text = null;
            bool over = false;
            StreamReader streamReader = new StreamReader("array.txt");
            string filedata = "";
            if (file.Exists == false)
            {
                MessageBox.Show("Файл не был создан");
            }
            else
            {
                while (!streamReader.EndOfStream)   //Взяли строку из файла
                {
                    filedata += streamReader.ReadLine();
                }
                if (StringIsValid(filedata))   //Если в строке нет букв и она не пуста
                {
                    string[] checker = filedata.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  //Делим на элементы
                    int lenght = checker.GetLength(0);
                    for (i = 0; i < checker.Length; i++)
                    {
                        if (checker[i].Length > 5)
                            over = true;
                    }
                    if (lenght < 100)
                    {
                        if (!over)
                        {
                            Mass = checker.Select(int.Parse).ToArray();  // Успешно. Преобразовываем в int и забиваем массив
                            for (int i = 0; i < Mass.Length; i++)         //Вывод на в текстбокск результат
                            {
                                label3.Text = label3.Text + Mass[i].ToString() + " ";
                            }
                            label3.Text = label3.Text + Environment.NewLine;
                            i = lenght;
                            MessageBox.Show("Массив из файла успешно загружен!!!");
                            button1.Enabled = false;
                            button2.Enabled = true;
                            button3.Enabled = true;
                            button4.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("В массиве введены большие числа");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Кол-во элементов в массиве больше 100!!!");
                    };
                }
                else
                {
                    MessageBox.Show("Неверный формат входного файла!!! Удалите буквы из файла или заполните его положительными числами через пробел");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StreamWriter str = new StreamWriter("Out.txt");
            str.WriteLine(label3.Text);
            str.Close();
            MessageBox.Show("Файл полученного массива сохранён");
            button5.Enabled = false;
        }
    }
}
