using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DictionaryApp
{
    public partial class Form3 : Form
    {
        private readonly Action _dictionarySave;
        private Dictionary<String, String> _NorwegianToEnglishDictionary;
        private Dictionary<String, String> _EnglishToNorwegianDictionary;
        public Form3(Dictionary<String,String> NorwegianToEnglishDictionary, Dictionary<String, String> EnglishToNorwegianDictionary)
        {
            InitializeComponent();

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
                norwegianToEnglishDictionaryNew.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                englishToNorwegianDictionaryNew.Add(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString());
            }
            //Kako sacuvati izmjene? Seljacki nacin: kopiraj save metodu u ovu funkciju i pozivaj je. Nisam mogao preko delegata nista.

        }
    }
}
