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

            };

                if (Regex.Matches(currentlinetext, @"\*\*").Count >= 1)
                {
                    BoldText(firstcharindex, currentlinetext);

                }else if (Regex.Matches(currentlinetext, @"\*").Count >= 2)
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
        int firstIndexBold;
        int secondIndexBold;
        int firstcharindexBold;
        string currentlinetextBold = "";
        //int cu
        private void BoldText(int firstcharindex, string currentlinetext)
        {
        

            if (this.firstcharindexBold != firstcharindex)
            {
                firstIndexBold = 0;
                secondIndexBold = 0;
                this.firstcharindexBold = firstcharindex;
            }
            if (this.currentlinetextBold != currentlinetext && this.firstcharindexBold == firstcharindex)
            {
                if (this.currentlinetextBold.Length > currentlinetext.Length)
                {
                    secondIndexBold = currentlinetext.LastIndexOf("**");
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.DeselectAll();
                }
                this.currentlinetextBold = currentlinetext;
            }

            if (currentlinetext.IndexOf("**", secondIndexBold + 2 > currentlinetext.Length ? 0 : secondIndexBold + 2) != -1 && secondIndexBold != -1)
            {
                firstIndexBold = currentlinetext.IndexOf("**",0);
                secondIndexBold = currentlinetext.IndexOf("**", firstIndexBold + 2);
                if (secondIndexBold == -1)
                {
                    secondIndexBold = 0;
                    return;
                }


                richTextBox1.Select(firstcharindex + firstIndexBold + 2, secondIndexBold - firstIndexBold - 2);
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Bold);
                    richTextBox1.SelectionColor = Color.Green;
                    richTextBox1.Select(firstcharindex + firstIndexBold, 2);
                    richTextBox1.SelectedText = "";
                    richTextBox1.Select(firstcharindex + secondIndexBold - 2, 2);
                    richTextBox1.SelectedText = "";
                    this.currentlinetextBold = richTextBox1.Lines[currentline];
                    richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                    richTextBox1.SelectionColor = Color.White;
                richTextBox1.DeselectAll();
            }
        }


        int firstIndexItalic;
        int secondIndexItalic;
        int firstcharindexItalic;
        string currentlinetextItalic = "";

        private void ItalicText(int firstcharindex, string currentlinetext)
        {

            if (this.firstcharindexItalic != firstcharindex)
            {
                firstIndexItalic = 0;
                secondIndexItalic = 0;
                this.firstcharindexItalic = firstcharindex;
            }
            if (this.currentlinetextItalic != currentlinetext && this.firstcharindexItalic == firstcharindex)
            {
                if (this.currentlinetextItalic.Length > currentlinetext.Length)
                {
                    firstIndexItalic = currentlinetext.LastIndexOf("*");
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.DeselectAll();
                }
                this.currentlinetextItalic = currentlinetext;
            }
            if (currentlinetext.IndexOf("*", secondIndexItalic + 1 >= currentlinetext.Length ? 0 : secondIndexItalic + 1) != -1 && secondIndexItalic != -1)
            {
                firstIndexItalic = currentlinetext.IndexOf("*", 0);
                secondIndexItalic = currentlinetext.IndexOf("*", firstIndexItalic + 1);
                if (secondIndexItalic == -1)
                {
                    secondIndexItalic = 0;
                    return;
                }

                richTextBox1.Select(firstcharindex + firstIndexItalic + 1, secondIndexItalic - firstIndexItalic - 1);
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Italic);
                richTextBox1.SelectionColor = Color.Green;
                richTextBox1.Select(firstcharindex + firstIndexItalic, 1);
                richTextBox1.SelectedText = "";
                richTextBox1.Select(firstcharindex + secondIndexItalic - 1, 1);
                richTextBox1.SelectedText = "";
                this.currentlinetextItalic = richTextBox1.Lines[currentline];
                richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.DeselectAll();

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
