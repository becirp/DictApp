using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Editor radi tako sto pokupi sve rijeci iz dataGridViewera i napravi novi rijecnik koji sacuva u fajl.
 * Dozvoljeno je sortiranje rijeci i editovanje, ubacivanje redova i brisanje.
 */

namespace DictionaryApp
{
    public partial class Form3 : Form
    {
        private Action _DictionaryLoad;
        private Dictionary<String, String> _NorwegianToEnglishDictionary;
        private Dictionary<String, String> _EnglishToNorwegianDictionary;
        public Form3(Dictionary<String,String> NorwegianToEnglishDictionary, Dictionary<String, String> EnglishToNorwegianDictionary, Action DictionaryLoad)
        {
            InitializeComponent();

            _DictionaryLoad = DictionaryLoad;

            _NorwegianToEnglishDictionary = NorwegianToEnglishDictionary;
            _EnglishToNorwegianDictionary = EnglishToNorwegianDictionary;

            foreach (var member in NorwegianToEnglishDictionary)
            {
                dataGridView1.Rows.Add(member.Key, member.Value);
            }
            dataGridView1.Sort(dataGridView1.Columns["NorwegianWords"], ListSortDirection.Ascending);            
        }

        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            Dictionary<String, String> norwegianToEnglishDictionaryNew = new Dictionary<String, String>();
            Dictionary<String, String> englishToNorwegianDictionaryNew = new Dictionary<String, String>(); 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if((row.Cells[0].Value != null) || (row.Cells[1].Value != null))
                {
                    norwegianToEnglishDictionaryNew.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                    englishToNorwegianDictionaryNew.Add(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString());
                }
            }
            Form1.DictionarySave(norwegianToEnglishDictionaryNew, englishToNorwegianDictionaryNew);
            _DictionaryLoad();
        }

        private void editRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.BeginEdit(true);
        }

        private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            var hti = dataGridView1.HitTest(e.X, e.Y);
            if ((hti.RowIndex != -1) && (e.Button == MouseButtons.Right))
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[hti.RowIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[hti.RowIndex].Cells[0];
            }
        }

        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = " ";
            row.Cells[1].Value = " ";
            dataGridView1.Rows.Add(row);
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
        }
    }
}
