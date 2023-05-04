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
using System.Windows.Forms.VisualStyles;

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
        //string text;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            //GetFirstCharIndexOfCurrentLine() yöntemi, kontroldeki mevcut satırın ilk karakterinin indexini döndürürken,
            //GetLineFromCharIndex() yöntemi, bir karakter dizininin satır numarasını döndürür.
            int firstcharindex = richTextBox1.GetFirstCharIndexOfCurrentLine();
            
            currentline = richTextBox1.GetLineFromCharIndex(firstcharindex);

            if (richTextBox1.Lines.Length > currentline)
            {
                string currentlinetext = richTextBox1.Lines[currentline];

                if (richTextBox1.Font.Size == 32  || currentlinetext.StartsWith("# "))
                {
                    DefaultText headline_1 = new DefaultText(richTextBox1.SelectionFont.FontFamily, 32, FontStyle.Regular, Color.White, this);
                    headline_1.Headline(firstcharindex, 1, currentlinetext, 32);
                }
                else if (currentlinetext.StartsWith("## "))
                {
                    DefaultText headline_2 = new DefaultText(richTextBox1.SelectionFont.FontFamily, 24, FontStyle.Regular, Color.White, this);
                    headline_2.Headline(firstcharindex, 2, currentlinetext, 24);
                }
                else if (currentlinetext.StartsWith("### "))
                {
                    DefaultText headline_3 = new DefaultText(richTextBox1.SelectionFont.FontFamily, 18, FontStyle.Regular, Color.White, this);
                    headline_3.Headline(firstcharindex, 3, currentlinetext, 18);
                }
                else if (currentlinetext.StartsWith(">"))
                {
                    BlockquoteText(firstcharindex, currentlinetext);

                }
                else if (currentlinetext.Length >= 1 ? char.IsDigit(currentlinetext[0]) &&  currentlinetext.IndexOf(". ") != -1 : false)
                {
                    int IndexDot = currentlinetext.IndexOf(".");
                    string subText = currentlinetext.Substring(0, IndexDot);
                    if(Regex.IsMatch(subText, @"^+?\d+$"))
                    {
                        OrderedList(firstcharindex,currentlinetext);
                    }
                }else if (currentlinetext.Length >= 1 ? (currentlinetext.IndexOf("- ") != -1 || currentlinetext.IndexOf("• ") != -1) : false)
                {
                        UnOrderedList(firstcharindex,currentlinetext);
                }
                else {
                    if (richTextBox1.SelectionFont.Size != 14)
                    {
                        //richTextBox1.Select(firstcharindex, currentlinetext.Length);
                         if (!currentlinetext.StartsWith("•"))
                        {
                            Debug.WriteLine("çıktım");
                            richTextBox1.SelectionIndent = 0;
                        }
                         if(!(currentlinetext.Length > 0 && (richTextBox1.SelectionFont.Size == 32 || richTextBox1.SelectionFont.Size == 24 || richTextBox1.SelectionFont.Size == 18)))
                        {
                            DefaultText defaultText = new DefaultText(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Regular, Color.White, this);
                           defaultText.Default_text(firstcharindex, currentlinetext);
                        }
                }
                };
                if (Regex.Matches(currentlinetext, @"\*\*").Count >= 1)
                {
                    DefaultText defaultText = new DefaultText(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Bold, Color.White, this);
                    defaultText.BoldText(firstcharindex, 2, currentlinetext, currentline);

                }
                else if (Regex.Matches(currentlinetext, @"\*").Count >= 2)
                {
                    DefaultText defaultText = new DefaultText(richTextBox1.SelectionFont.FontFamily, 14, FontStyle.Italic, Color.White, this);
                    defaultText.ItalicText(firstcharindex, 1, currentlinetext, currentline);

                }
            }

        }

  
        private void BlockquoteText(int firstcharindex, string currentlinetext)
        {
            richTextBox1.SelectionIndent = 20;
            richTextBox1.Select(firstcharindex, 10);
            richTextBox1.SelectionBackColor = Color.Gray;
        }

        private void OrderedList(int firstcharindex, string currentlinetext)
        {
            richTextBox1.Select(firstcharindex, currentlinetext.Length);
            richTextBox1.SelectionIndent = 20;
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 13, FontStyle.Regular);
            richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
        }

        private void UnOrderedList(int firstcharindex, string currentlinetext)
        {
            // "• " ifadesindeki boşuk silinince intendt sıfırlanmıyor!
            if (currentlinetext[0] == '-')
            {
                currentlinetext = currentlinetext.TrimStart('-').Insert(0, "•");
                richTextBox1.Select(firstcharindex, currentlinetext.Length);
                richTextBox1.SelectedText = currentlinetext;
                //richTextBox1.Select(firstcharindex, currentlinetext.Length);
                richTextBox1.SelectionIndent = 20;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 13, FontStyle.Regular);
                richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
            }
            else if (currentlinetext.Length == 1 && currentlinetext[0] == '•')
            {
                richTextBox1.Select(firstcharindex, currentlinetext.Length);
                richTextBox1.SelectionIndent = 0;
            }
        }



        //private void DefaultText(string text)
        //{
        //}

        private void AddHorizontalLineToLine(int lineNumber, int thickness = 1)
        {
            //richTextBox1.AppendText("──────────────────────────────────────");
        }

        private void richTextBox1_CursorChanged(object sender, EventArgs e)
        {

        }


    }
}
