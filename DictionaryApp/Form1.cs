﻿using System;
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
    2. Kad budes pravio Wix install, koristi za pocetak template koji daje wix plugin. Za pojedinacne postavke vidjeti kako je radjeno u DRM-APP. Provjeriti koji od
    GUID-a je fiksan, vjerovatno onaj upgrade id.
    3. Podesiti git.
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
            LoadDictionary();
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
            MessageBox.Show("Dictionary App\r\nVersion: " + Application.ProductVersion);
            
        }

        private void saveDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DictionarySave();
        }

        private void DictionarySave()
        {
            using (StreamWriter file = new StreamWriter("EnglishToNorwegianDictionary.txt"))
                foreach (var entry in EnglishToNorwegianDictionary)
                    file.WriteLine("[{0}_{1}]", entry.Key, entry.Value);
            using (StreamWriter file = new StreamWriter("NorwegianToEnglishDictionary.txt"))
                foreach (var entry in NorwegianToEnglishDictionary)
                    file.WriteLine("[{0}_{1}]", entry.Key, entry.Value);
            ConsoleBox.Text += "\r\nDictionary saved to file.";
        }

        private void LoadDictionary()
        {
            try
            {
                using (StreamReader sr = new StreamReader("EnglishToNorwegianDictionary.txt"))
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
                using (StreamReader sr = new StreamReader("NorwegianToEnglishDictionary.txt"))
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
            catch(Exception e)
            {
                ConsoleBox.Text = "Dictionary file couldn't be read.\r\nSave new dictionary.";
                //ConsoleBox.Text = e.Message;
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Form2 testForm = new Form2(NorwegianToEnglishDictionary, EnglishToNorwegianDictionary);
            testForm.Show();
        }
    }
}
