using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace toMarkdown
{
    public class DefaultText
    {
        private ToMarkdown Form;
        
        public FontFamily Family { get; set; }
        public float FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
        public Color Color { get; set; }
        public Font SelectionFont { get; set; }

        public DefaultText(FontFamily family, float fontSize, FontStyle fontStyle, Color color, ToMarkdown toMarkDown)
        {
            FontSize = fontSize;
            FontStyle = fontStyle;
            Color = color;
            Family = family;
            SelectionFont = new Font(family, FontSize, fontStyle);
            Form = toMarkDown;
        }

        public void SetSelectionFont()
        {
            SelectionFont = new Font(Family, FontSize, FontStyle);
        }

        public void Select(int start, int length)
        {
            Form.richTextBox1.Select(start, length);
        }

        public void Default_text(int firstcharindex, string currentlinetext)
        {
            Form.richTextBox1.SelectionFont = SelectionFont;
            Form.richTextBox1.SelectionColor = Color;
            Select(firstcharindex + currentlinetext.Length, 0);
            Form.richTextBox1.DeselectAll();
        }

        public void Headline(int firstcharindex, int count , string currentlinetext, float fontSize)
        {
            if (Form.richTextBox1.SelectionFont.Size != fontSize)
            {
                Select(firstcharindex + count, currentlinetext.Length);
                Form.richTextBox1.SelectionFont = SelectionFont;
                // en son yazdıpımız text'in sonuna atar.
                Select(firstcharindex, count + 1);
                Form.richTextBox1.SelectedText = "";
                Form.richTextBox1.SelectionFont = SelectionFont;
                Select(currentlinetext.Length + firstcharindex, 0);
                Form.richTextBox1.SelectionColor = Color;
                Form.richTextBox1.DeselectAll();
            }
        }
        private int firstIndexItalic;
        private int secondIndexItalic;
        private int firstcharindexItalic;
        private string currentlinetextItalic = "";
        public void ItalicText(int firstcharindex, int cout, string currentlinetext, int currentLine)
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
                    Form.richTextBox1.SelectionColor = Color;
                    Form.richTextBox1.DeselectAll();
                }
                this.currentlinetextItalic = currentlinetext;
            }
            if (currentlinetext.IndexOf("*", secondIndexItalic + cout >= currentlinetext.Length ? 0 : secondIndexItalic + cout) != -1 && secondIndexItalic != -1)
            {
                firstIndexItalic = currentlinetext.IndexOf("*", 0);
                secondIndexItalic = currentlinetext.IndexOf("*", firstIndexItalic + cout);
                if (secondIndexItalic == -1)
                {
                    secondIndexItalic = 0;
                    return;
                }

                Form.richTextBox1.Select(firstcharindex + firstIndexItalic + cout, secondIndexItalic - firstIndexItalic - cout);
                Form.richTextBox1.SelectionFont = SelectionFont;
                Form.richTextBox1.SelectionColor = Color.Green;
                Form.richTextBox1.Select(firstcharindex + firstIndexItalic, cout);
                Form.richTextBox1.SelectedText = "";
                Form.richTextBox1.Select(firstcharindex + secondIndexItalic - cout, cout);
                Form.richTextBox1.SelectedText = "";
                this.currentlinetextItalic = Form.richTextBox1.Lines[currentLine];
                Form.richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                Form.richTextBox1.SelectionColor = Color;
                Form.richTextBox1.DeselectAll();

            }
        }

        int firstIndexBold;
        int secondIndexBold;
        int firstcharindexBold;
        string currentlinetextBold = "";
        public void BoldText(int firstcharindex, int cout, string currentlinetext, int currentLine)
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
                    Form.richTextBox1.SelectionColor = Color;
                    Form.richTextBox1.DeselectAll();
                }
                this.currentlinetextBold = currentlinetext;
            }

            if (currentlinetext.IndexOf("**", secondIndexBold + 2 > currentlinetext.Length ? 0 : secondIndexBold + 2) != -1 && secondIndexBold != -1)
            {
                firstIndexBold = currentlinetext.IndexOf("**", 0);
                secondIndexBold = currentlinetext.IndexOf("**", firstIndexBold + 2);
                if (secondIndexBold == -1)
                {
                    secondIndexBold = 0;
                    return;
                }

                Form.richTextBox1.Select(firstcharindex + firstIndexBold + 2, secondIndexBold - firstIndexBold - 2);
                Form.richTextBox1.SelectionFont = SelectionFont;
                Form.richTextBox1.SelectionColor = Color.Green;
                Form.richTextBox1.Select(firstcharindex + firstIndexBold, 2);
                Form.richTextBox1.SelectedText = "";
                Form.richTextBox1.Select(firstcharindex + secondIndexBold - 2, 2);
                Form.richTextBox1.SelectedText = "";
                this.currentlinetextBold = Form.richTextBox1.Lines[currentLine];
                Form.richTextBox1.Select(currentlinetext.Length + firstcharindex, 0);
                Form.richTextBox1.SelectionColor = Color;
                Form.richTextBox1.DeselectAll();
            }
        }

    }
}
