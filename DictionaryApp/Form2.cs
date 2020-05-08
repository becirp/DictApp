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
/* 1. Napravljen test. Poredi rijec iz labele sa ukucanom rijeci iz tekstboksa. Ako se poklapaju testirana rijec je tacno prevedena. Oznaciti tacne tekstboxe zelenom bojom,
 * a netacne crvenom. Ispisati skor od 20 bodova.
 * 2. Ubaceno da se moze prikazati tacno rjesenje, na rijecima koje su pogresno uradjene.
 * 3. Za sada ne priznaje da zenski rod moze imati muski clan. Mora se znati da je neka rijec zenski rod.
 */

/* Opcionalna kasnija prosirenja: Napraviti da se vodi evidencija skorova uzetih u toku vremena; Staviti opcionalno vremensko ogranicenje na test; Brojac rijeci;
 */
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
        private List<Label> norLabelCorrectionsList = new List<Label>();
        private List<Label> engLabelCorrectionsList = new List<Label>();
        private Dictionary<String, String> randomDict = new Dictionary<String, String>();

        public Form2(Dictionary<String, String> NorwegianToEnglishDictionary)
        {
            InitializeComponent();
            DictionaryCpy = NorwegianToEnglishDictionary;
            InitializeTextBoxList();
            InitializeLabelList();
            CreateTest();
            HideCorrectionLabels();
        }

        private void CreateTest()
        {
            int i = 0;
            randomDict =  RandomWords(DictionaryCpy, numberOfWords);
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

            norLabelCorrectionsList.Add(labelNorCorrection1);
            norLabelCorrectionsList.Add(labelNorCorrection2);
            norLabelCorrectionsList.Add(labelNorCorrection3);
            norLabelCorrectionsList.Add(labelNorCorrection4);
            norLabelCorrectionsList.Add(labelNorCorrection5);
            norLabelCorrectionsList.Add(labelNorCorrection6);
            norLabelCorrectionsList.Add(labelNorCorrection7);
            norLabelCorrectionsList.Add(labelNorCorrection8);
            norLabelCorrectionsList.Add(labelNorCorrection9);
            norLabelCorrectionsList.Add(labelNorCorrection10);

            engLabelCorrectionsList.Add(labelEngCorrection1);
            engLabelCorrectionsList.Add(labelEngCorrection2);
            engLabelCorrectionsList.Add(labelEngCorrection3);
            engLabelCorrectionsList.Add(labelEngCorrection4);
            engLabelCorrectionsList.Add(labelEngCorrection5);
            engLabelCorrectionsList.Add(labelEngCorrection6);
            engLabelCorrectionsList.Add(labelEngCorrection7);
            engLabelCorrectionsList.Add(labelEngCorrection8);
            engLabelCorrectionsList.Add(labelEngCorrection9);
            engLabelCorrectionsList.Add(labelEngCorrection10);
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

        private Dictionary<String, String> RandomWords(Dictionary<String, String> DictionaryCpy, int numberOfWords)
        {
            int i = 0;
            Dictionary<String, String> randDictionary = new Dictionary<String, String>();
            Random rand = new Random();
            int size = DictionaryCpy.Count;
            List<String> dictKeys = Enumerable.ToList(DictionaryCpy.Keys);            
            List<String> randWords = new List<String>();            
            int currentRandomValue;
            //Osigurati da ne moze zaglaviti petlja.
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

        private void buttonCheckTest_Click(object sender, EventArgs e)
        {
            int i = 0;
            int scoreMax = 20;
            int score = 0;
            LockTextBoxes();
            foreach(var member in randomDict)
            {
                if (i < 10)
                {
                    if (member.Value == engTextboxList[i].Text)
                    {
                        engTextboxList[i].BackColor = Color.Green;
                        score++;
                    }
                    else if (member.Value != engTextboxList[i].Text)
                    {
                        engTextboxList[i].BackColor = Color.Red;
                        engLabelCorrectionsList[i].Text = member.Value;
                        engLabelCorrectionsList[i].Visible = true;
                    }
                }
                else if (i >= 10)
                {
                    if (member.Key == norTextboxList[i - 10].Text)
                    {
                        norTextboxList[i - 10].BackColor = Color.Green;
                        score++;
                    }
                    else if (member.Key != norTextboxList[i - 10].Text)
                    {
                        norTextboxList[i - 10].BackColor = Color.Red;
                        norLabelCorrectionsList[i - 10].Text = member.Key;
                        norLabelCorrectionsList[i - 10].Visible = true;
                    }
                }               
                i++;
            }
            labelScore.Text = score.ToString() + "/" + scoreMax.ToString();
            buttonCheckTest.Enabled = false;
        }

        private void LockTextBoxes()
        {
            foreach(var member in norTextboxList)
            {
                member.ReadOnly = true;
            }
            foreach (var member in engTextboxList)
            {
                member.ReadOnly = true;
            }
        }

        private void HideCorrectionLabels()
        {
            foreach(var member in norLabelCorrectionsList)
            {
                member.Visible = false;
            }
            foreach (var member in engLabelCorrectionsList)
            {
                member.Visible = false;
            }
        }
    }
}
