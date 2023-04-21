using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace toMarkdown
{ // 1 KERE SELECT YAPMA ?
    public partial class ToMarkdown : Form
    {

        public ToMarkdown()
        {
            InitializeComponent();
        }

        int currentline; // son değişen satırın indeksi, -1 ile başlatılır
        //bool isTextChanged = false; // metinde değişiklik olup olmadığını kontrol eder
        string text;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //GetFirstCharIndexOfCurrentLine() yöntemi, kontroldeki mevcut satırın ilk karakterinin indexini döndürürken,
            //GetLineFromCharIndex() yöntemi, bir karakter dizininin satır numarasını döndürür.
            int firstcharindex = richTextBox1.GetFirstCharIndexOfCurrentLine();


            currentline = richTextBox1.GetLineFromCharIndex(firstcharindex);

            if (richTextBox1.Lines.Length > currentline)
            {
                string currentlinetext = richTextBox1.Lines[currentline];
             
                Debug.WriteLine(currentlinetext);

                if (currentlinetext.StartsWith("# "))
                {
                    Heading_1(firstcharindex, currentlinetext, 32);
                }
                else if (currentlinetext.StartsWith("## "))
                {
                    Heading_2(firstcharindex, currentlinetext, 24);
                }
                else if (currentlinetext.StartsWith("### "))
                {
                    Heading_3(firstcharindex, currentlinetext, 18);
                }
                else {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Regular);
                      richTextBox1.DeselectAll();
                }
            }

        }

        private void Heading_1(int firstcharindex, string currentlinetext, float fontSize)
        {
            richTextBox1.Select(firstcharindex + currentlinetext.Length - 2, 2);
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
            // en son yazdıpımız text'in sonuna atar.
            richTextBox1.Select(firstcharindex, 2);
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
            richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
            richTextBox1.SelectionColor = Color.White;
            AddHorizontalLineToLine(currentline);
            richTextBox1.DeselectAll();
            // currentlinetext.Length mevcut satırdaki text'in uzunluğu

        }
        private void Heading_2(int firstcharindex, string currentlinetext, float fontSize)
        {
            richTextBox1.Select(firstcharindex + currentlinetext.Length - 3, 3);
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Underline);
            // en son yazdıpımız text'in sonuna atar.
            richTextBox1.Select(firstcharindex, 3);
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
            richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.DeselectAll();
            // currentlinetext.Length mevcut satırdaki text'in uzunluğu
        }

        private void Heading_3(int firstcharindex, string currentlinetext, float fontSize)
        {
            richTextBox1.Select(firstcharindex + currentlinetext.Length - 4, 4);
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Underline);
            // en son yazdıpımız text'in sonuna atar.
            richTextBox1.Select(firstcharindex, 4);
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
            richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.DeselectAll();
            // currentlinetext.Length mevcut satırdaki text'in uzunluğu
        }

        private void DefaultText(string text)
        {

        }

        private void AddHorizontalLineToLine(int lineNumber, int thickness = 1)
        {
            //richTextBox1.AppendText("──────────────────────────────────────");
        }
    }
}
