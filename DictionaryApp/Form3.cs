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

            toolTip1.SetToolTip(buttonDiscardChanges, "Discard all changes made before clicking Apply Changes, and then close this window.");
            toolTip2.SetToolTip(buttonApplyChanges, "Add all rows from the table to the actual dictionary.");

            _DictionaryLoad = DictionaryLoad;

            _NorwegianToEnglishDictionary = NorwegianToEnglishDictionary;
            _EnglishToNorwegianDictionary = EnglishToNorwegianDictionary;

            fillTableWithWords();            
        }

        private void fillTableWithWords()
        {
            foreach (var member in _NorwegianToEnglishDictionary)
            {
                dataGridView1.Rows.Add(member.Key, member.Value);
            }
            dataGridView1.Sort(dataGridView1.Columns["NorwegianWords"], ListSortDirection.Ascending);
        }

        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            bool noEmptyCells = true;
            Dictionary<String, String> norwegianToEnglishDictionaryNew = new Dictionary<String, String>();
            Dictionary<String, String> englishToNorwegianDictionaryNew = new Dictionary<String, String>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((row.Cells[0].Value == null) || (row.Cells[0].Value.ToString() == " ") || (row.Cells[1].Value == null) || (row.Cells[1].Value.ToString() == " "))
                    noEmptyCells = false;
            }

            try
            {
                if (noEmptyCells)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!norwegianToEnglishDictionaryNew.ContainsKey(row.Cells[0].Value.ToString()))
                            norwegianToEnglishDictionaryNew.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        if (!englishToNorwegianDictionaryNew.ContainsKey(row.Cells[1].Value.ToString()))
                            englishToNorwegianDictionaryNew.Add(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString());
                    }
                    Form1.DictionarySave(norwegianToEnglishDictionaryNew, englishToNorwegianDictionaryNew);
                    _DictionaryLoad();
                    MessageBox.Show("Changes to dictionary successfully applied.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("There are empty cells, please remove entire row or fill in empty cells.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                //MessageBox.Show("There were duplicate words detected (or null cells). Changes were not applied.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }

        private void editRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.BeginEdit(true);
        }

        private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
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
            dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
            dataGridView1.BeginEdit(true);
        }
    }
}
