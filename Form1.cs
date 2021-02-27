using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6._2.Menus_déroulants__Barre_d_outils_et_Barre_d_Etat
{
    public partial class Form1 : Form
    {
        SaveFileDialog save;
        OpenFileDialog openFileDialog;
        bool ClickButtonSave = false;
        bool fileExiste = false;
        bool TextSaved = false;
        int text_Length;
        FontDialog font;
        ColorDialog color;   


        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFile();

            NewFile();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            SaveFile();

            OpenFile();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ClickButtonSave = true;
            SaveFile();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FontText();  
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            TextColorOrBackColor();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            TextColorOrBackColor();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

                 SaveFile();
                Environment.Exit(0);
        }

        private void SaveFile()
        {
            if (!fileExiste && !ClickButtonSave) // if he has created a new file and he wants to do something without saving the file , then it will show to him a notification whether wants to save the file
            {
                if (!TextSaved && textBox1.Text!=string.Empty)
                if (MessageBox.Show("Do you want save this file ?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    save = new SaveFileDialog();
                    save.ShowDialog();
                    if (save.FileName != string.Empty)
                    {
                            TextSaved = true;
                            using (StreamWriter saveFile = File.CreateText(save.FileName)) // it will delet the previous text and write the new one 
                            {
                                saveFile.Write(textBox1.Text);
                                saveFile.Close();
                            }
                        }
                }
            }
            else if(!TextSaved &&!fileExiste && ClickButtonSave) // if he has created a new file and click on save button , then it will allows him to do what he wants without showing a notification
            {
                ClickButtonSave = false; // if i don't do that then clickbuttonsave it will still true so when i click on close form and without change the text it will lead to here and that is not desired
                save = new SaveFileDialog();
                save.ShowDialog();
                if (save.FileName != string.Empty)
                {
                    using (StreamWriter saveFile = File.CreateText(save.FileName)) // it will delete the previous text and write the new one 
                    {
                        saveFile.Write(textBox1.Text);
                        saveFile.Close();
                    }
                    TextSaved = true;
                }
            }
            else if (fileExiste && !TextSaved && ClickButtonSave ) 
            {
                 ClickButtonSave = false;
                if (openFileDialog.FileName != string.Empty)
                {
                    using (StreamWriter saveFile = File.CreateText(openFileDialog.FileName)) // it will delete the previous text and write the new one 
                    {
                        saveFile.Write(textBox1.Text);
                        saveFile.Close();
                    }
                    TextSaved = true;
                }
            }
            else if(fileExiste && !ClickButtonSave && !TextSaved)
            {
                if(textBox1.Text.Length!=text_Length) // i'm comparing  with the lenght when i opened the file first , if the length changes then it will go through if doesn't the progame it will close 
                if (MessageBox.Show("Do you want to save modification ?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (openFileDialog.FileName != string.Empty)
                            using (StreamWriter saveFile = File.CreateText(openFileDialog.FileName)) // it will delet the previous text and write the new one 
                            {
                                saveFile.Write(textBox1.Text);
                                saveFile.Close();
                            }
                        TextSaved = true;
                }

            }
        }

        private void OpenFile()
        {
            openFileDialog = new OpenFileDialog();

            // openFileDialog.Filter = "type(*.txt)|*.txt|type(*.rtp)|*.rtp";  Idon't to set the EXtention because LoadFile will not get worked
            // openFileDialog.DefaultExt = "rtf";

            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != string.Empty)
            {
                using (StreamReader loadFile = File.OpenText(openFileDialog.FileName)) // it will delet the previous text and write the new one 
                {
                    textBox1.Text = loadFile.ReadToEnd();
                    loadFile.Close();
                }


                text_Length = textBox1.Text.Length; // i made this because , when i open a text file , in textchanged method
                // the variable textsaved it gets false so when i want to get out it will show me a notificaion telling me if i want to save the change even if i didn't 
                // any change , and to fix that problem i compte the length of the text then i will compare it when i want to get out 
                fileExiste = true;
            }
        }

        private void NewFile()
        {
            textBox1.Text = String.Empty;
            fileExiste = false; // to inform that the file is new 

        }

        private void FontText()
        {
            font = new FontDialog();
            font.ShowDialog();
            if (font.Font != null)
                textBox1.Font = font.Font;
        }

        private void TextColorOrBackColor()
        {
            color = new ColorDialog();
            color.Color = textBox1.ForeColor;
            color.ShowDialog();
            if (color.Color != null)
            {
                textBox1.BackColor = color.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Lucida Console", 10);
            toolStripStatusLabel3.Text = DateTime.Now.TimeOfDay.ToString("h\\:mm");
            toolStripStatusLabel2.Text = DateTime.Now.Date.ToString("d");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.TimeOfDay.ToString("h\\:mm");
            toolStripStatusLabel2.Text = DateTime.Now.Date.ToString("d");
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();

            NewFile();
        }

        private void ouvrirFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();

            OpenFile();
        }

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClickButtonSave = true;
            SaveFile();
        }

        private void caractereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontText();
        }

        private void fontDeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextColorOrBackColor();
        }

        private void arrierePlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextColorOrBackColor();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextSaved = false; // i use this because it works even  when i change the color or the font , and to be sure if the text has changed or not
        }
    }

   
}
