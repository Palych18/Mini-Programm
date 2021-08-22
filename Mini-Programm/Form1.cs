using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Programm
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rnd;
        Dictionary<string, double> metrica;
        
        char[] spicialChars = new char[] {'%', '*', ')', '?', '#', '$', '^', '&', '~'};
        public MainForm()
        {
            InitializeComponent();
            rnd = new Random();
            metrica = new Dictionary<string, double>();
            metrica.Add("мм", 1000);
            metrica.Add("см", 100);
            metrica.Add("дм", 10);
            metrica.Add("м", 1);
            metrica.Add("км", 0.001);
            metrica.Add("мили", 0.00062);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Мини программы содержат ряд небольших программ, которые могут пригодиться в жизни. " +
                "А главное научить меня прграммированию на С#.\nАвтор: Егоров-Клюкин П.В.", "О программе");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            count--;
            lblCount.Text = count.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int n;

            n = rnd.Next(Convert.ToInt32(nudFrom.Value), Convert.ToInt32(nudBefor.Value)+1);
            lblRandom.Text = Convert.ToString(n);
            if (cbRandom.Checked)
            {
                int i = 0;
                while (tbRandom.Text.IndexOf(n.ToString()) != -1)
                {
                    n = rnd.Next(Convert.ToInt32(nudFrom.Value), Convert.ToInt32(nudBefor.Value) + 1);
                    i++;
                    if (i > 1000) break;
                }
                if (i <= 1000) tbRandom.AppendText(n + "\r\n");
            }
            else tbRandom.AppendText(n + "\r\n");
        }

        private void btnRandomClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
            lblRandom.Text = " ";
        }

        private void btnRandomCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(tbRandom.Text);
            }
            catch
            {
                MessageBox.Show("Задайте генерацию!");
            }
        }

        private void tsmiInsertData_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongDateString() + "\n");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongTimeString() + "\n");
        }

        private void tsmiPastePass_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText($"Ваш новый пароль - {Clipboard.GetText()}\n");
        }

        private void btnPastePass_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText($"Ваш новый пароль - {Clipboard.GetText()}\n");
        }

        private void btnPasteData_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongDateString() + "\n");
        }

        private void btnPasteTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongTimeString() + "\n");
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNotepad.SaveFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении.");
            }
        }

        void LoadNotepad()
        {
            try
            {
                rtbNotepad.LoadFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении.");
            }
        }

        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            LoadNotepad();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadNotepad();
            clbPassword.SetItemChecked(0, true);
            clbPassword.SetItemChecked(1, true);
            clbPassword.SetItemChecked(2, true);
            clbPassword.SetItemChecked(3, true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNotepad.SaveFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении.");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadNotepad();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbNotepad.Text = "";
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            rtbNotepad.Text = "";
        }

        private void btnCreatePassword_Click(object sender, EventArgs e)
        {
            string password = "";
            if (clbPassword.CheckedItems.Count == 0)
            {
                tbPassword.Text = "Выберите параметры!";
                return;
            }
            
            for(int i = 0; i < nudPassLenght.Value; i++)
            {
                int n = rnd.Next(0, clbPassword.CheckedItems.Count);
                string s = clbPassword.CheckedItems[n].ToString();
                
                switch (s)
                {
                    case "Цифры": password += rnd.Next(10).ToString();
                        break;
                    case "Прописные буквы": password += Convert.ToChar(rnd.Next(65, 90));
                        break;
                    case "Строчные буквы": password += Convert.ToChar(rnd.Next(97, 122));
                        break;
                    default: password += spicialChars[rnd.Next(spicialChars.Length)];
                        break;
                }

                tbPassword.Text = password;
            }
        }

        private void btnClearPassword_Click(object sender, EventArgs e)
        {
            tbPassword.Text = "";
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(tbPassword.Text.ToString());
            }
            catch
            {
                tbPassword.Text = "Создайте пароль!";
            }
        }

        private void Converter()
        {
            double m1 = metrica[cbFrom.Text];
            double m2 = metrica[cbTo.Text];
            double n = Convert.ToDouble(tbFrom.Text);
            double tb = n* m2 / m1;
            tb = Math.Round(tb, 3);
            tbTo.Text = tb.ToString();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Converter();
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string cb = cbFrom.Text;
            cbFrom.Text = cbTo.Text;
            cbTo.Text = cb;

            Converter();
        }

        private void cbMetrica_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetrica.Text)
            {
                case "Длина":
                    metrica.Clear();
                    metrica.Add("мм", 1000);
                    metrica.Add("см", 100);
                    metrica.Add("дм", 10);
                    metrica.Add("м", 1);
                    metrica.Add("км", 0.001);
                    metrica.Add("мили", 0.00062);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("мм");
                    cbFrom.Items.Add("см");
                    cbFrom.Items.Add("дм");
                    cbFrom.Items.Add("м");
                    cbFrom.Items.Add("км");
                    cbFrom.Items.Add("мили");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("мм");
                    cbTo.Items.Add("см");
                    cbTo.Items.Add("дм");
                    cbTo.Items.Add("м");
                    cbTo.Items.Add("км");
                    cbTo.Items.Add("мили");
                    cbFrom.Text = "м";
                    cbTo.Text = "м";
                    tbFrom.Text = "1";
                    tbTo.Text = "1";
                    break;
                case "Вес":
                    metrica.Clear();
                    metrica.Add("гр", 1);
                    metrica.Add("кг", 0.001);
                    metrica.Add("т", 0.000001);
                    metrica.Add("lb", 0.0022);
                    metrica.Add("oz", 0.035);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("гр");
                    cbFrom.Items.Add("кг");
                    cbFrom.Items.Add("т");
                    cbFrom.Items.Add("lb");
                    cbFrom.Items.Add("oz");                    
                    cbTo.Items.Clear();
                    cbTo.Items.Add("гр");
                    cbTo.Items.Add("кг");
                    cbTo.Items.Add("т");
                    cbTo.Items.Add("lb");
                    cbTo.Items.Add("oz");
                    cbFrom.Text = "гр";
                    cbTo.Text = "гр";
                    tbFrom.Text = "1";
                    tbTo.Text = "1";
                    break;

                default:
                    break;
            }
        }
    }
}
