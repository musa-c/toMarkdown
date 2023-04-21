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
{
    public partial class toMarkdown : Form
    {

        public toMarkdown()
        {
            InitializeComponent();
        }

        int lastChangedLine = -1; // son değişen satırın indeksi, -1 ile başlatılır
        //bool isTextChanged = false; // metinde değişiklik olup olmadığını kontrol eder

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            lastChangedLine = richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());

            if (lastChangedLine != -1 && richTextBox1.Lines.Length > lastChangedLine)
            {
                string text = richTextBox1.Lines[lastChangedLine];
                Debug.WriteLine(lastChangedLine);
                Debug.WriteLine(lastChangedLine + ": " + text.StartsWith("###"));
                //Debug.WriteLine(text);
                // Kontrolde # karakteri var mı diye kontrol et

                if (text.StartsWith("#"))
                {
                    if (text.StartsWith("#"))
                    {
                        Heading(text, 32);
                    }
                    if (text.StartsWith("##"))
                    {
                        Heading(text, 24);
                    }
                    if (text.StartsWith("###"))
                    {
                        Heading(text, 18);
                    }
                }
                else DefaultText(text);

            }

        }

        private void Heading(string text, float fontSize)
        {
            // Tüm metni büyük harfe dönüştür
            richTextBox1.SelectionLength = text.Length;
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, richTextBox1.SelectionFont.Style);
            //richTextBox1.Select(richTextBox1.Text.Length, 0); // metin sonuna atar kullanıcıyı
        }

        private void DefaultText(string text)
        {
            //richTextBox1.SelectionStart = richTextBox1.GetFirstCharIndexFromLine(lastChangedLine);
            richTextBox1.SelectionLength = text.Length;
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, 12, richTextBox1.SelectionFont.Style);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

    }
}
