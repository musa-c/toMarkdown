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
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Messaging;

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
                    if (richTextBox1.SelectionFont.Size != 14)
                    {
                        richTextBox1.Select(firstcharindex, currentlinetext.Length);
                        richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Regular);
                        richTextBox1.SelectionColor = Color.White;
                        richTextBox1.Select(firstcharindex + currentlinetext.Length, 0);
                        richTextBox1.DeselectAll();
                }
                //    else
                //{
                //    richTextBox1.Select(richTextBox1.SelectionStart, 0);
                //    richTextBox1.DeselectAll();
                //}


            };

                if (Regex.Matches(currentlinetext, @"\*\*").Count >= 1)
                {
                    BoldText(firstcharindex, currentlinetext);

                }else if (Regex.Matches(currentlinetext, @"\*").Count >= 1)
                {
                    ItalicText(firstcharindex, currentlinetext);

                }
            }

        }

        private void Heading_1(int firstcharindex, string currentlinetext, float fontSize)
        {
            if (richTextBox1.SelectionFont.Size != fontSize)
            {
                richTextBox1.Select(firstcharindex + 1, currentlinetext.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
                // en son yazdıpımız text'in sonuna atar.
                richTextBox1.Select(firstcharindex, 2);
                richTextBox1.SelectionColor = Color.Gray;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
                richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                richTextBox1.SelectionColor = Color.White;
                AddHorizontalLineToLine(currentline);
                richTextBox1.DeselectAll();
            }
            // currentlinetext.Length mevcut satırdaki text'in uzunluğu
        }
        private void Heading_2(int firstcharindex, string currentlinetext, float fontSize)
        {
            if(richTextBox1.SelectionFont.Size != fontSize)
            {
                richTextBox1.Select(firstcharindex + 2, currentlinetext.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
                // en son yazdıpımız text'in sonuna atar.
                richTextBox1.Select(firstcharindex, 3);
                richTextBox1.SelectionColor = Color.Gray;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
                richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.DeselectAll();
                // currentlinetext.Length mevcut satırdaki text'in uzunluğu
            }
        }
           

        private void Heading_3(int firstcharindex, string currentlinetext, float fontSize)
        {
            if(richTextBox1.SelectionFont.Size != fontSize)
            {
                richTextBox1.Select(firstcharindex + 3, currentlinetext.Length);
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
                // en son yazdıpımız text'in sonuna atar.
                richTextBox1.Select(firstcharindex, 4);
                richTextBox1.SelectionColor = Color.Gray;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, FontStyle.Regular);
                richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.DeselectAll();
                // currentlinetext.Length mevcut satırdaki text'in uzunluğu
            }
        }
        int firstIndex;
        int secondIndex;
        int loop;
        int firstcharindex;
        string currentlinetext = "";
        //int cu
        List<int> indexes = new List<int>();
        private void BoldText(int firstcharindex, string currentlinetext)
        {
            //int cout = Regex.Matches(currentlinetext, @"\*\*").Count;
            Debug.WriteLine("firstcharindex: "+ firstcharindex);
            Debug.WriteLine("this.firstcharindex: "+ this.firstcharindex);

            if (this.firstcharindex != firstcharindex)
            {
                firstIndex = 0;
                secondIndex = 0;
                this.firstcharindex = firstcharindex;
            }
            if (this.currentlinetext != currentlinetext && this.firstcharindex == firstcharindex)
            {
                if (this.currentlinetext.Length > currentlinetext.Length)
                {
                    secondIndex = currentlinetext.LastIndexOf("**");
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.DeselectAll();
                }
                this.currentlinetext = currentlinetext;
            }

            if (currentlinetext.IndexOf("**",secondIndex + 2) != -1)
            {
                firstIndex = currentlinetext.IndexOf("**",0);
                secondIndex = currentlinetext.IndexOf("**", firstIndex + 2);
                    Debug.WriteLine("firstIndex: " + firstIndex);
                    Debug.WriteLine("secondIndex: " + secondIndex);

                    richTextBox1.Select(firstcharindex + firstIndex + 2, secondIndex - firstIndex - 2);
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Bold);
                    richTextBox1.SelectionColor = Color.Green;
                    richTextBox1.Select(firstcharindex + firstIndex, 2);
                    richTextBox1.SelectedText = "";
                    richTextBox1.Select(firstcharindex + secondIndex -2, 2);
                    richTextBox1.SelectedText = "";
                richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.DeselectAll();
            }
        }



        private void ItalicText(int firstcharindex, string currentlinetext)
        {
            int firstIndex = currentlinetext.IndexOf("*");
            int secondIndex = currentlinetext.IndexOf("*", firstIndex + 1);

            if (secondIndex != -1)
            {
                //string boldText = currentlinetext.Substring(firstIndex + 2, secondIndex - firstIndex - 2);
                if (richTextBox1.SelectionFont.Style != FontStyle.Italic)
                {
                    richTextBox1.Select(firstcharindex + firstIndex + 1, secondIndex - firstIndex - 1);
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Italic);
                    richTextBox1.SelectionColor = Color.Green;
                    richTextBox1.Select(firstcharindex + firstIndex, 1);
                    richTextBox1.SelectionColor = Color.Gray;
                    richTextBox1.Select(firstcharindex + secondIndex, 1);
                    richTextBox1.SelectionColor = Color.Gray;
                    richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.DeselectAll();
                }
                else
                {
                    richTextBox1.Select(richTextBox1.SelectionStart, 0);
                    richTextBox1.DeselectAll();
                }
            }
        }
        private void DefaultText(string text)
        {

        }

        private void AddHorizontalLineToLine(int lineNumber, int thickness = 1)
        {
            //richTextBox1.AppendText("──────────────────────────────────────");
        }

        private void richTextBox1_CursorChanged(object sender, EventArgs e)
        {

        }


    }
}
