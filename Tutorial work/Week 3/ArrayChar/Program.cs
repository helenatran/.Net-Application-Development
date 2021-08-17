using System;
using System.Collections.Generic;

namespace Week4LabProgramQuestion
{
    class ArrayChar
    {
        private char[] arrayOfChar, numberArray, letterArray, symbolArray, punctuationArray;
        private List<char> numberList, letterList, symbolList, punctuationList;
        public ArrayChar()
        {
            Console.Write("Write a sentence with numbers, symbols and punctuations: ");
            string userInput = Console.ReadLine();
            arrayOfChar = userInput.ToCharArray();
            numberList = new List<char>();
            letterList = new List<char>();
            symbolList = new List<char>();
            punctuationList = new List<char>();
        }
        public void CheckChar()
        {
            foreach (char ch in arrayOfChar)
            {
                if (Char.IsNumber(ch))
                {
                    numberList.Add(ch);
                }
                if (Char.IsLetter(ch))
                {
                    letterList.Add(ch);
                }
                if (Char.IsSymbol(ch))
                {
                    symbolList.Add(ch);
                }
                if (Char.IsPunctuation(ch))
                {
                    punctuationList.Add(ch);
                }
            }
            numberArray = numberList.ToArray();
            letterArray = letterList.ToArray();
            symbolArray = symbolList.ToArray();
            punctuationArray = punctuationList.ToArray();
        }

        private string getCharType(char charType)
        {
            if (charType == 'n')
            {
                if (numberArray.Length == 1 || numberArray.Length == 0)
                    return "number";
                else
                    return "numbers";
            }
            else if (charType == 'l')
            {
                if (letterArray.Length == 1 || letterArray.Length == 0)
                    return "letter";
                else
                    return "letters";
            }
            else if (charType == 's')
            {
                if (symbolArray.Length == 1 || symbolArray.Length == 0)
                    return "symbol";
                else
                    return "symbols";
            }
            else if (charType == 'p')
            {
                if (punctuationArray.Length == 1 || punctuationArray.Length == 0)
                    return "punctuation mark";
                else
                    return "punctuation marks";
            }
            else
                return "undefined type(s)";
        }

        private void DisplayArray(char[] arrayToDisplay, char type)
        {
            string charType = getCharType(type);

            if(arrayToDisplay.Length == 0)
            {
                Console.Write("\nYour sentence has 0 {0}.", charType);
            }
            else
            {
                Console.Write("\nYour sentence has {0} {1}: ", arrayToDisplay.Length, charType);
                for (int loopVar = 0; loopVar < arrayToDisplay.Length; loopVar++)
                {
                    Console.Write("{0} ", arrayToDisplay[loopVar]);
                }
            } 
        }
        public void DisplayLetters()
        {
            DisplayArray(letterArray, 'l');
        }
        public void DisplayNumbers()
        {
            DisplayArray(numberArray, 'n');
        }
        public void DisplaySymbols()
        {
            DisplayArray(symbolArray, 's');
        }
        public void DisplayPunctuations()
        {
            DisplayArray(punctuationArray, 'p');
        }
    }
    class Test
    {
        static void Main(string[] args)
        {
            ArrayChar newArray = new ArrayChar();
            newArray.CheckChar();
            newArray.DisplayLetters();
            newArray.DisplayNumbers();
            newArray.DisplaySymbols();
            newArray.DisplayPunctuations();
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

/**
 * Task output:
 * -------------------------
 * Write a sentence with numbers, symbols and punctuations: Hello!!! 2000 XOXO +-=+

    Your sentence has 9 letters: H e l l o X O X O
    Your sentence has 4 numbers: 2 0 0 0
    Your sentence has 3 symbols: + = +
    Your sentence has 4 punctuation marks: ! ! ! -

 */