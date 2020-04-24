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
/* Bug: ponavljanja izmedju dvije liste eng i nor test. Napraviti rand od 20 clanova bez ponavljanja i napraviti od toga dva testa. Vidjeti na kraju da se test pravi od jednog rijecnika, 
   posto su u biti isti rijecnik. */
namespace DictionaryApp
{
    public partial class Form2 : Form
    {
        private Dictionary<String, String> DictionaryCpy = new Dictionary<String, String>();
        private int numberOfWords = 20;
        private List<TextBox> norTextboxList = new List<TextBox>();
        private List<TextBox> engTextboxList = new List<TextBox>();
        private List<Label> norLabelList = new List<Label>();
        private List<Label> engLabelList = new List<Label>();        

        public Form2(Dictionary<String, String> NorwegianToEnglishDictionary)
        {
            InitializeComponent();
            DictionaryCpy = NorwegianToEnglishDictionary;
            InitializeTextBoxList();
            InitializeLabelList();
            CreateTest();
        }

        private void CreateTest()
        {
            int i = 0;
            Dictionary<String, String> randomDict =  RandomWords(DictionaryCpy, numberOfWords);
            foreach(var member in randomDict)
            {
                if(i < numberOfWords/2)
                    norLabelList[i].Text = member.Key;
                else
                    engLabelList[i-10].Text = member.Value;
                i++;
            }
        }

        private void InitializeLabelList()
        {
            norLabelList.Add(labelNor1);
            norLabelList.Add(labelNor2);
            norLabelList.Add(labelNor3);
            norLabelList.Add(labelNor4);
            norLabelList.Add(labelNor5);
            norLabelList.Add(labelNor6);
            norLabelList.Add(labelNor7);
            norLabelList.Add(labelNor8);
            norLabelList.Add(labelNor9);
            norLabelList.Add(labelNor10);

            engLabelList.Add(labelEng1);
            engLabelList.Add(labelEng2);
            engLabelList.Add(labelEng3);
            engLabelList.Add(labelEng4);
            engLabelList.Add(labelEng5);
            engLabelList.Add(labelEng6);
            engLabelList.Add(labelEng7);
            engLabelList.Add(labelEng8);
            engLabelList.Add(labelEng9);
            engLabelList.Add(labelEng10);
        }

        private void InitializeTextBoxList()
        {
            norTextboxList.Add(textBoxNor1);
            norTextboxList.Add(textBoxNor2);
            norTextboxList.Add(textBoxNor3);
            norTextboxList.Add(textBoxNor4);
            norTextboxList.Add(textBoxNor5);
            norTextboxList.Add(textBoxNor6);
            norTextboxList.Add(textBoxNor7);
            norTextboxList.Add(textBoxNor8);
            norTextboxList.Add(textBoxNor9);
            norTextboxList.Add(textBoxNor10);

            engTextboxList.Add(textBoxEng1);
            engTextboxList.Add(textBoxEng2);
            engTextboxList.Add(textBoxEng3);
            engTextboxList.Add(textBoxEng4);
            engTextboxList.Add(textBoxEng5);
            engTextboxList.Add(textBoxEng6);
            engTextboxList.Add(textBoxEng7);
            engTextboxList.Add(textBoxEng8);
            engTextboxList.Add(textBoxEng9);
            engTextboxList.Add(textBoxEng10);
        }

        //For testing only.
        //public void WriteDictInRichTBox()
        //{           
        //    List<String> norWordList = RandomWords(DictionaryCpy, numberOfWords);
        //    for(int i = 0; i < norWordList.Count; i++)
        //    {
        //        richTextBox1.Text += norWordList[i] + "\r\n";
        //    }

        //    List<String> engWordList = RandomWords(englishToNorwegianDictionaryCpy, numberOfWords);
        //    for (int i = 0; i < engWordList.Count; i++)
        //    {
        //        richTextBox1.Text += engWordList[i] + "\r\n";
        //    }
        //}

        private Dictionary<String, String> RandomWords(Dictionary<String, String> DictionaryCpy, int numberOfWords)
        {
            int i = 0;
            Dictionary<String, String> randDictionary = new Dictionary<String, String>();
            Random rand = new Random();
            int size = DictionaryCpy.Count;
            List<String> dictKeys = Enumerable.ToList(DictionaryCpy.Keys);            
            List<String> randWords = new List<String>();            
            int currentRandomValue;
            while (true)
            {
                currentRandomValue = rand.Next(size);
                if (randWords.Contains(dictKeys[currentRandomValue])) continue;                
                randWords.Add(dictKeys[currentRandomValue]);
                i++;
                if (i == numberOfWords) break;
            }

            for(i = 0; i < numberOfWords; i++)
            {
                randDictionary.Add(randWords[i], DictionaryCpy[randWords[i]]);
            }            
            return randDictionary;
        }
    }
}
