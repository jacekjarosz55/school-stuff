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
using System.Windows;
using System.Reflection.Metadata;

namespace Notatnik
{
    public partial class Oknonotatnik : Form
    {
        private string path = "";
        private int krok = 5;
        private bool is_control = false;
        private float defaultFontSize;

        public Oknonotatnik(string[] args)
        {
            InitializeComponent();
            powiększenieToolStripMenuItem.Tag = krok;
            pomniejszToolStripMenuItem.Tag = -krok;
            
            

            textBoxNotatnik.MouseWheel += textBoxNotatnik_MouseWheel;
            defaultFontSize = textBoxNotatnik.Font.Size;
            if(args != null && args.Length > 0 && File.Exists(args[0]))
            {
                OtworzPlik(args[0]);
                
            }

        }

        #region sus
        private void textBoxNotatnik_MouseWheel(object sender, MouseEventArgs e)
        {
            if (is_control)
            {
                if (e.Delta > 0) ZmienRozmiarCzcionki(krok);
                else ZmienRozmiarCzcionki(-krok);
            }
           
        }
        #endregion





        #region Zdarzenia menu Plik

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!textBoxNotatnik.Modified || ZapiszPlikPytanie() != DialogResult.Cancel)
            {
                CzyscNotatnik();
                return;
            }
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZapiszPlikJako();
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZapiszPlikPytanie(false);
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxNotatnik.Modified && ZapiszPlikPytanie() == DialogResult.Cancel)
            {
                return;
            }
            Close();
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxNotatnik.Modified && ZapiszPlikPytanie() == DialogResult.Cancel)
                return;

            OtworzPlik();
        }


        #endregion

        #region Zdarzenia menu Edycja

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxNotatnik.Undo();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxNotatnik.Cut();
        }

        private void kopjujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxNotatnik.Copy();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxNotatnik.Paste();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxNotatnik.SelectedText = "";
        }

        private void zaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxNotatnik.SelectAll();
        }

        private void dataIGodzinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxNotatnik.Paste(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
        }

        #endregion

        #region Metody zapisu/odczytu pliku

        private void UstawPasekGornyOkna()
        {
            Text = "Notatnik";
            if (path != "")
                Text += " - " + Path.GetFileName(path);
        }

        private DialogResult ZapiszPlik()
        {
            File.WriteAllText(path, textBoxNotatnik.Text);
            textBoxNotatnik.Modified = false;
            UstawPasekGornyOkna();

            return DialogResult.Yes;
        }

        private DialogResult ZapiszPlikJako()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                ZapiszPlik();
                return DialogResult.Yes;
            }
            return DialogResult.No;
        }

        private DialogResult ZapiszPlikPytanie(bool czyPokazacPytanie = true)
        {
            DialogResult jakZamknietoOkno = DialogResult.Yes;
            if (czyPokazacPytanie)
                jakZamknietoOkno = MessageBox.Show("Plik nie zapisany.\nCzy zapisać?", "Uwaga!!!", MessageBoxButtons.YesNoCancel);

            if (jakZamknietoOkno == DialogResult.Yes)
            {
                if (path != "")
                    return ZapiszPlik();

                return ZapiszPlikJako();
            }
            return jakZamknietoOkno;
        }

        private void OtworzPlik(string scieszka = "")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki|*.*";
            if (scieszka != "" || openFileDialog.ShowDialog() == DialogResult.OK)
            {

                path = scieszka == "" ? openFileDialog.FileName : scieszka;
                textBoxNotatnik.Text = File.ReadAllText(path);
                textBoxNotatnik.Modified = false;
                UstawPasekGornyOkna();
            }
        }

        private void CzyscNotatnik()
        {
            textBoxNotatnik.Text = "";
            textBoxNotatnik.Modified = false;
            path = "";
            UstawPasekGornyOkna();
        }


        #endregion

        #region Metody menu widok

        private void ZmienRozmiarCzcionki(int krok)
        {
            FontFamily fontFamily = textBoxNotatnik.Font.FontFamily;
            float size = textBoxNotatnik.Font.Size + krok;
            FontStyle fontStyle = textBoxNotatnik.Font.Style;
            GraphicsUnit graphicsUnit = textBoxNotatnik.Font.Unit;
            byte gdiCharSet = textBoxNotatnik.Font.GdiCharSet;

            if (size <= 0)
                return;
            Font font = new Font(fontFamily, size, fontStyle, graphicsUnit, gdiCharSet);
            textBoxNotatnik.Font = font;
            fontchange();

        }

        #endregion


        #region zdarzenia widok

        private void zmienRozmiarTekstuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int krokZmiany;
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && int.TryParse(menuItem.Tag.ToString(), out krokZmiany))
                ZmienRozmiarCzcionki(krokZmiany);
        }
        #endregion
        #region idk 
        private void textBoxNotatnik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                is_control = true;
            }
            
        }

        private void textBoxNotatnik_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                is_control = false;
            }
        }

        private void fontchange()
        {
            float powienkszenie = textBoxNotatnik.Font.Size / defaultFontSize * 100;
            toolStripStatusLabelPowienkszenie.Text = $"Powiększenie: {powienkszenie} %";
        }

        private void zawijanieWierszyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zawijanieWierszyToolStripMenuItem.Checked = !zawijanieWierszyToolStripMenuItem.Checked;
            textBoxNotatnik.WordWrap = zawijanieWierszyToolStripMenuItem.Checked;
        }

        private void czciąkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = textBoxNotatnik.Font;
            if(font.ShowDialog() == DialogResult.OK)
            {
                textBoxNotatnik.Font = font.Font;
            }
        }
        #endregion

        #region Zdarzenia drag n drop

        


        private void textBoxNotatnik_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;


                Point pozycja = textBoxNotatnik.PointToClient(Cursor.Position);
                int index = textBoxNotatnik.GetCharIndexFromPosition(pozycja);

                textBoxNotatnik.SelectionStart = index;
                textBoxNotatnik.SelectionLength = 0;
                textBoxNotatnik.Refresh();
                textBoxNotatnik.Focus();
            }
            else
                e.Effect = DragDropEffects.None;
        }
        

        #endregion


    private void textBoxNotatnik_DragDrop(object sender, DragEventArgs e)
        {  
            if (e.Data.GetDataPresent(DataFormats.Text))
            {

                string text = e.Data.GetData(DataFormats.Text) as string;
                if (text != null)
                {
                    textBoxNotatnik.Paste(text);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Length > 0)
                {
                    if (File.Exists(files[0]))
                    {
                        if(ZapiszPlikPytanie(textBoxNotatnik.Modified) == DialogResult.Yes)
                        OtworzPlik(files[0]);
                    }
                }
            }
        }

    }
}
