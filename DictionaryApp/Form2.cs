using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Kreiran iterator za citanje svih vrijednosti rijecnika. Napraviti random generator za 10 rijeci bez ponavljanja parova. */

namespace DictionaryApp
{
    public partial class Form2 : Form
    {
        private Dictionary<String, String> norwegianToEnglishDictionaryCpy = new Dictionary<String, String>();
        private Dictionary<String, String> englishToNorwegianDictionaryCpy = new Dictionary<String, String>();
        private int numberOfWords = 10;
        public Form2(Dictionary<String, String> NorwegianToEnglishDictionary, Dictionary<String, String> EnglishToNorwegianDictionary)
        {
            InitializeComponent();
            norwegianToEnglishDictionaryCpy = NorwegianToEnglishDictionary;
            englishToNorwegianDictionaryCpy = EnglishToNorwegianDictionary;
            WriteDictInRichTBox();
        }

        public void WriteDictInRichTBox()
        {           
            List<String> norWordList = RandomWords(norwegianToEnglishDictionaryCpy, numberOfWords);
            for(int i = 0; i < norWordList.Count; i++)
            {
                richTextBox1.Text += norWordList[i] + "\r\n";
            }

            List<String> engWordList = RandomWords(englishToNorwegianDictionaryCpy, numberOfWords);
            for (int i = 0; i < engWordList.Count; i++)
            {
                richTextBox1.Text += engWordList[i] + "\r\n";
            }
        }

        private List<string> RandomWords(Dictionary<String, String> norwegianToEnglishDictionaryCpy, int numberOfWords)
        {
            Random rand = new Random();
            int size = norwegianToEnglishDictionaryCpy.Count;
            List<String> englishWords = Enumerable.ToList(norwegianToEnglishDictionaryCpy.Values);
            List<String> randomListEnglishWords = new List<String>();
            int currentRandomValue;
            int[] randomValues = new int[numberOfWords];
            int i = 0;
            while (true)
            {
                currentRandomValue = rand.Next(size);
                if (randomListEnglishWords.Contains(englishWords[currentRandomValue])) continue;                
                randomListEnglishWords.Add(englishWords[currentRandomValue]);
                i++;
                if (i == numberOfWords) break;
            }
            return randomListEnglishWords;
        }
    }
}
