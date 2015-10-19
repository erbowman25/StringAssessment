////////////////////////////////////////////////////////////////////////////////////////
/// Author: Eric Bowman
/// Date Octorber 18th, 2014
/// 
/// 
/// This application parses a sentence and replaces each word with the following: 
/// first letter, number of distinct characters between first and last character, and last letter.  
/// 
/// Button: 'Run' will run parse the input and display the results in the output.
/// Button: 'Clear' will clear both text boxes.
///  
/// 
////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StringAssessment
{
    public partial class StringAssessment : Form
    {
        public StringAssessment()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            InputTextBox.Clear();
            OutputTextBox.Clear();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            var tmp = ParseInputString(InputTextBox.Text);
            OutputTextBox.Text = tmp;
        }


        private string ParseInputString(string inputString)
        {
            if (inputString == "")
                return inputString;

            var tempCharArray = inputString.ToCharArray();
            var regExLetter = new Regex("[^A-Za-z]");
            var regExNonSpaceChar = new Regex("[^\\S]");
             
            var indexList = new List<int>();
            for (var i = 0; i < inputString.Length; i++)
            {
                if (regExNonSpaceChar.IsMatch(tempCharArray[i].ToString()) ||
                    regExLetter.IsMatch(tempCharArray[i].ToString()))
                {
                    indexList.Add(i);
                }
            }

            if (indexList.Count == 0) return GetNewString(inputString);

            //Get first substring
            var outputText = GetNewString(inputString.Substring(0, indexList[0]));
            //Get first non letter
            outputText = outputText + inputString.Substring(indexList[0], 1);

            for (var i = 0; i < indexList.Count - 1; i++)
            {
                var index = indexList[i] + 1;
                var length = indexList[i + 1] - indexList[i] - 1;
                var newOutput = GetNewString(inputString.Substring(index, length));
                outputText = outputText + newOutput + inputString.Substring(index + length, 1);
            }

            //Get last substring if it's a letter
            if ( indexList[indexList.Count - 1] + 1 != inputString.Length)
                outputText = outputText + GetNewString(inputString.Substring(indexList[indexList.Count - 1]));


            return outputText;
        }


        private string GetNewString(string inputString)
        {
            if (inputString.Length < 2) return inputString;
            var innerString = inputString.Substring(1, inputString.Length - 2);
            var tempCharArray = innerString.ToCharArray();
            tempCharArray = tempCharArray.Distinct().ToArray();

            var ouputString = inputString.Substring(0, 1) + tempCharArray.Length +
                              inputString.Substring(inputString.Length - 1, 1);

            return ouputString;
        }
    }
}