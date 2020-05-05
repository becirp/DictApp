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

/*
    1. Svrha aplikacije je da pomaze u testiranju naucenih rijeci. Treba da omogucava unos novih rijeci od strane korisnika. Oba rjecnika treba da cuva na disku. 
    U aplikaciju se ucitava najnovija verzija rjecnika prilikom pozivanja aplikacije. Testiranje podrazumijeva po 20 nasumicno odabranih parova Eng-Nor i Nor-Eng.
    2. Zasto dva rjecnika: da se moze koristiti contain metoda za brzo trazenje rijeci za obostrani prevod. Za kreiranje samog testa dovoljan jedan rijecnik.
    3. Ubaceno da se odredjena rijec moze naci i ispraviti, ako je neispravno ukucana u tekstbox i dodana u rijecnik. Napraviti preglednik rijecnika pomocu tabele.
    4. Podeseno dock za kontrole kad se povecava i smanjuje forma.
    5. Wix install: ne radi citanje dict fajla kad se on cuva na C:\Program Files. Potrebno je pokrenuti aplikaciju sa administratorskim pravima iz instalacijskog foldera. Preko ikone ili
    bez admin prava aplikacija ne radi korektno. U biti sljedece sto ce se probati jeste da na target masini cuva ove txt fajlove u public folderu. To bi trebalo pomoci oko admin prava.
    Ne mogu instalirati u public. Ne mogu nigdje instalirat, a da imam pristup tom fajlu nakon instalacije. Provjeriti oko ovih eksternih podataka sta se standardno radi.
     */
namespace DictionaryApp
{    
    public partial class Form1 : Form
    {
        private Dictionary<String, String> NorwegianToEnglishDictionary = new Dictionary<String, String>();
        private Dictionary<String, String> EnglishToNorwegianDictionary = new Dictionary<String, String>();
        public Form1()
        {
            InitializeComponent();
            DictionaryLoad();
        }

        private void buttonAddWordPair_Click(object sender, EventArgs e)
        {
            String stringEnglish, stringNorwegian;
            stringEnglish = textBoxEnglish.Text;
            stringNorwegian = textBoxNorwegian.Text;
            try
            {
                NorwegianToEnglishDictionary.Add(stringNorwegian, stringEnglish);
                EnglishToNorwegianDictionary.Add(stringEnglish, stringNorwegian);
                ConsoleBox.Text = "Word pair added to dictionary.";
                DictionarySave();
            }
            catch (ArgumentException)
            {
                ConsoleBox.Text = "Word pair already in dictionary.";
            }
            textBoxEnglish.Clear();
            textBoxNorwegian.Clear();
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            String unknownWord = textBoxUnknownWord.Text;
            try
            {
                ConsoleBox.Text = NorwegianToEnglishDictionary[unknownWord];
            }
            catch(KeyNotFoundException)
            {
                try
                {
                    ConsoleBox.Text = EnglishToNorwegianDictionary[unknownWord];
                }
                catch (KeyNotFoundException)
                {
                    ConsoleBox.Text = "Word not found in dictionary.";
                }               
            }
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: " + Application.ProductVersion + " - April, 2020\r\n" + "Author: Becir Peco");           
        }

        private void saveDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DictionarySave();
        }

        public void DictionarySave()
        {
            string dictRootDir = Path.GetPathRoot(Environment.SystemDirectory);
            string dictPathEng = dictRootDir + "Dictionary App\\EnglishToNorwegianDictionary.txt";
            string dictPathNor = dictRootDir + "Dictionary App\\NorwegianToEnglishDictionary.txt";
            using (StreamWriter file = new StreamWriter(dictPathEng))
                foreach (var entry in EnglishToNorwegianDictionary)
                    file.WriteLine("[{0}_{1}]", entry.Key, entry.Value);
            using (StreamWriter file = new StreamWriter(dictPathNor))
                foreach (var entry in NorwegianToEnglishDictionary)
                    file.WriteLine("[{0}_{1}]", entry.Key, entry.Value);
            ConsoleBox.Text += "\r\nDictionary saved to file.";
        }

        public static void DictionarySave(Dictionary<String, String> _NorwegianToEnglishDictionary, Dictionary<String, String> _EnglishToNorwegianDictionary)
        {
            string dictRootDir = Path.GetPathRoot(Environment.SystemDirectory);
            string dictPathEng = dictRootDir + "Dictionary App\\EnglishToNorwegianDictionary.txt";
            string dictPathNor = dictRootDir + "Dictionary App\\NorwegianToEnglishDictionary.txt";
            using (StreamWriter file = new StreamWriter(dictPathEng))
                foreach (var entry in _EnglishToNorwegianDictionary)
                    file.WriteLine("[{0}_{1}]", entry.Key, entry.Value);
            using (StreamWriter file = new StreamWriter(dictPathNor))
                foreach (var entry in _NorwegianToEnglishDictionary)
                    file.WriteLine("[{0}_{1}]", entry.Key, entry.Value);
        }

        private void DictionaryLoad()
        {
            try
            {
                NorwegianToEnglishDictionary.Clear();
                EnglishToNorwegianDictionary.Clear();
                string dictRootDir = Path.GetPathRoot(Environment.SystemDirectory);
                string dictPathEng = dictRootDir + "Dictionary App\\EnglishToNorwegianDictionary.txt";
                string dictPathNor = dictRootDir + "Dictionary App\\NorwegianToEnglishDictionary.txt";
                using (StreamReader sr = new StreamReader(dictPathEng))
                {
                    string line;
                    string engWord, norWord;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] separatingStrings = { "[", "]", "_"};
                        string text = line;
                        string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                        for(int i = 0; i < (words.Length - 1); i++)
                        {
                            engWord = words[i];
                            norWord = words[i + 1];
                            EnglishToNorwegianDictionary.Add(engWord, norWord);
                        }                       
                    }
                }
                using (StreamReader sr = new StreamReader(dictPathNor))
                {
                    string line;
                    string engWord, norWord;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] separatingStrings = { "[", "]", "_" };
                        string text = line;
                        string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < (words.Length - 1); i++)
                        {
                            norWord = words[i];
                            engWord = words[i + 1];
                            NorwegianToEnglishDictionary.Add(norWord, engWord);
                        }
                    }
                }
                ConsoleBox.Text = "Dictionary Loaded.";
            }
            catch
            {
                ConsoleBox.Text = "Dictionary file couldn't be read.\r\nSave new dictionary.";
                //ConsoleBox.Text = e.Message;
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Form2 testForm = new Form2(NorwegianToEnglishDictionary);
            testForm.Show();
        }

        private void viewDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 dictViewerForm = new Form3(NorwegianToEnglishDictionary, EnglishToNorwegianDictionary, DictionaryLoad);
            dictViewerForm.Show();
        }
    }
}
