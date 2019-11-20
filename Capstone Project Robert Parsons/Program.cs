﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Capstone_Project_Starting
{
    class Program
    {
        static void Main(string[] args)
        {
            string customWordDataPath = @"HangmanInfo\WordList.txt";
            string wordPackDataPath = @"HangmanInfo\WordPacks.txt";

            MainMenu(customWordDataPath, wordPackDataPath);
        }


        private static void MainMenu(string customWordDataPath, string wordPackDataPath)
        {
            //
            // variable declaration
            //
            bool exitApplication = false;
            char userConvertedResponse;
            bool validResponse;
            bool userDoneWithHangman = false;

            do
            {

                DisplayScreenHeader("Application Menu");

                MainMenuLine("1", "Play Hangman");
                MainMenuLine("2", "Add Custom Words");
                MainMenuLine("3", "Delete Words");
                MainMenuLine("4", "Add/Remove Word Packs");
                MainMenuLine("5", "Quit Application");
                do
                {

                    ConsoleKeyInfo userInput = Console.ReadKey();
                    userConvertedResponse = char.Parse(userInput.KeyChar.ToString().ToLower());
                    validResponse = true;
                    switch (userConvertedResponse)
                    {
                        case ('1'):
                            {
                                do
                                {
                                    userDoneWithHangman = Hangman(customWordDataPath, wordPackDataPath);
                                } while (!userDoneWithHangman);

                                break;
                            }
                        case ('2'):
                            {

                                break;
                            }
                        case ('3'):
                            {

                                break;
                            }
                        case ('4'):
                            {
                                DisplayAddWordPacks(wordPackDataPath);
                                break;
                            }
                        case ('5'):
                            {
                                exitApplication = true;
                                Console.WriteLine();
                                Console.WriteLine("Exiting application, please come again soon!");
                                DisplayContinuePrompt();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine(" Invalid choice. Please select a number 1-X");
                                validResponse = false;
                                break;
                            }
                    }
                } while (!validResponse);
            } while (!exitApplication);

        }

        /// <summary>
        /// Makes a line of the menu, coloring in the first part cyan and the second part white. 
        /// </summary>
        /// <param name="precludingSymbol">Put in a single symbol, typically what you want the user to type in. Ie A, 1, etc...</param>
        /// <param name="line">A description of what typing the prevbious symbol will do.</param>
        private static void MainMenuLine(string precludingSymbol, string line)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{precludingSymbol}] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(line);
        }

        private static void WordPackMenuLine(string precludingNumber, string packName, string wordPackDataPath)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{precludingNumber}] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(packName);
            try
            {
                string onOrOff = File.ReadLines(wordPackDataPath).Skip(Int32.Parse(precludingNumber) - 1).Take(1).First();
                if (onOrOff.Contains("1"))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"  [On] ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (onOrOff.Contains("0"))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"  [Off] ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Initializes word list and produces errors, leading to a cleaner and more helpful experience
        /// </summary>
        /// <param name="customWordDataPath"></param>
        /// <param name="hiddenWords"></param>
        /// <param name="alphabet"></param>
        private static void HangmanWordIntializer(string customWordDataPath, string wordPackDataPath, List<string> hiddenWords, List<char> alphabet)
        {
            bool customWordErrorOccured = ReadWordsFromFile(customWordDataPath, hiddenWords);

            //
            // Gives the user a heads up if the custom words ether include 
            //
            if (customWordErrorOccured == true)
            {
                DisplayScreenHeader("Hangman");
                Console.WriteLine("There has been an error obtaining the custom words");
                Console.WriteLine($"Please make sure that the WordList file is in {customWordDataPath}");
                Console.WriteLine("If it is there, please also make sure that any custom words are only using letters.");
                DisplayContinuePrompt();
            }

            //
            //Adds a word list to the file, if it is on.
            //
            string[] WordPacksInformation = File.ReadAllLines(wordPackDataPath);
            int currentLine = 1;

            foreach (string line in WordPacksInformation)
            {
                if (line.Contains("1"))
                {
                    switch (currentLine)
                    {
                        case (1):
                            {
                                break;
                            }
                        case (2):
                            {
                                break;
                            }
                        case (3):
                            {
                                break;
                            }
                        case (4):
                            {
                                break;
                            }
                        case (5):
                            {
                                break;
                            }
                        case (6):
                            {
                                break;
                            }
                        case (7):
                            {
                                break;
                            }
                        case (8):
                            {
                                break;
                            }
                        case (9):
                            {
                                break;
                            }
                        default:
                            break;
                    }
                }
                currentLine++;
            }

            //
            // Produces words if none can be pulled from other sources
            //
            if (hiddenWords.Count == 0)
            {
                DisplayScreenHeader("Hangman");
                Console.WriteLine("The word list is empty.");
                Console.WriteLine("Although it is recommend that you fix this by adding words to the word list or adding in word packs, Hangman is still playable");
                Console.WriteLine("The word list will now add words so that you can play Hangman.");

                #region FALLBACK_WORDS
                hiddenWords.Add("statuesque");
                hiddenWords.Add("contribute");
                hiddenWords.Add("gold");
                hiddenWords.Add("quack");
                hiddenWords.Add("notice");
                hiddenWords.Add("reset");
                hiddenWords.Add("whip");
                hiddenWords.Add("mundane");
                hiddenWords.Add("gallimaufry");
                #endregion
                DisplayContinuePrompt();

            }
        }

        /// <summary>
        /// Plays a game of hangman
        /// </summary>
        private static bool Hangman(string customWordDataPath, string wordPackDataPath)
        {
            //
            //
            // variable declaration
            //        
            //"changingWord" is the word that is shown, that slowly gets revealed.
            //"hiddenCharacters" is the hidden word converted into an array of characters, to be compared to changingWord
            //"hiddenWord" is the list of all the hangman words -  basic list in case WordList ever gets deleted
            //
            List<string> hiddenWords = new List<string>() { };
            char[] hiddenCharacters;
            char userConvertedResponse;
            bool validUserInput;
            bool completedWord = false;
            bool letterInWord = false;
            int completedCharacter;
            int hiddenCharactersPlacement;
            List<char> userPickedCharacters = new List<char>();
            List<char> alphabet = new List<char>();
            bool userDoneWithHangman;

            AlphabetCreation(alphabet);

            //
            // Updates and Initializes the hiddenWords list from any word packs and any custom words
            // Also gives the user any error information, incase any dataPaths cannot be reached or other reasons
            //
            HangmanWordIntializer(customWordDataPath, wordPackDataPath, hiddenWords, alphabet);

            //
            // Initializes the hidden word, by checking all the user inputted words then randomly picking one from the list
            //
            int randomNumber = RandomNumber(0, hiddenWords.Count);
            char[] changingWord = new char[hiddenWords[randomNumber].Length];

            // Writes down the hidden word, used only for testing
            //WriteLine("{0}", hiddenWords[randomNumber]);

            //
            //Makes the "hiddenCharacters" array into the hidden word
            //
            hiddenCharacters = hiddenWords[randomNumber].ToCharArray(0, hiddenWords[randomNumber].Length);



            DisplayScreenHeader("Hangman");

            //
            //Writes out asterisks for each letter in the hidden word.
            //
            Console.WriteLine();
            Console.Write("Word: ");
            for (int i = 0; i < hiddenWords[randomNumber].Length; i++)
            {
                changingWord[i] = '*';
                Console.Write($"{changingWord[i]}");
            }
            Console.WriteLine();
            Console.WriteLine();

            //
            //The loop that does it all. If all the asterisks are removed, or when the user presses '~' exits the loop
            //
            do
            {
                //
                // Reads the user's input of a letter.
                //
                letterInWord = false;
                Console.Write("Guess a letter >> ");
                //
                // Temporary gives the cursors position, so that displaying the picked letters does not disturb the rest of the layout
                //
                int tempTopCursorPositionGuess = Console.CursorTop;
                int tempLeftCursorPositionGuess = Console.CursorLeft;
                do
                {
                    validUserInput = false;
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    userConvertedResponse = char.Parse(userInput.KeyChar.ToString().ToLower());

                    //
                    // Validation - making sure that the user input is either a letter A-Z or ~ (the exit key)
                    // Pressing ~ exits to the main menu
                    //
                    if (userConvertedResponse == '~')
                    {
                        Console.WriteLine();
                        Console.WriteLine("Exiting hangman!");
                        userDoneWithHangman = true;
                        validUserInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        userDoneWithHangman = false;
                        foreach (char letter in alphabet)
                        {
                            if (letter == userConvertedResponse)
                            {
                                validUserInput = true;
                            }
                        }
                        if (!validUserInput)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please Input a valid letter A-Z.");
                            Console.SetCursorPosition(tempLeftCursorPositionGuess, tempTopCursorPositionGuess);
                        }
                    }

                } while (!validUserInput);



                DisplayScreenHeader("Hangman");

                //
                //Checks every letter to see if it is equal to the user input.
                //If it is equal, sets the letter from an asterisk to the correct letter
                //
                hiddenCharactersPlacement = 0;
                foreach (char c in hiddenCharacters)
                {
                    if (c == userConvertedResponse)
                    {
                        letterInWord = true;
                        changingWord[hiddenCharactersPlacement] = c;
                    }
                    hiddenCharactersPlacement = hiddenCharactersPlacement + 1;
                }

                userPickedCharacters.Add(userConvertedResponse);

                //
                //Says if the letter is in the word or not.
                //
                if (letterInWord == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Yes! {0} is in the word.", userConvertedResponse);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry. {0} is not in the word.", userConvertedResponse);
                }

                //
                //Displays the current progress on the hidden word    
                //
                Console.Write("Word: ");
                for (int i = 0; i < hiddenWords[randomNumber].Length; i++)
                {
                    Console.Write($"{changingWord[i]}");
                }
                Console.WriteLine();
                Console.WriteLine();

                //
                // Temporary gives the cursors position, so that displaying the picked letters does not disturb the rest of the layout
                //
                int tempTopCursorPosition = Console.CursorTop;
                int tempLeftCursorPosition = Console.CursorLeft;

                PickedLetterDisplay(alphabet, userPickedCharacters, tempLeftCursorPosition, tempTopCursorPosition);
                //
                // If all the asterisks are revealed, and thus all the letters have been guessed, it exits the loop.
                //
                completedCharacter = 0;
                foreach (char c in changingWord)
                {
                    if (c == '*')
                    {
                        completedCharacter = completedCharacter + 1;
                    }
                    else
                    {
                        completedCharacter = completedCharacter + 0;
                    }
                }
                if (completedCharacter == 0)
                {
                    completedWord = true;
                }
                else { }
            } while (!completedWord && !userDoneWithHangman);

            if (completedWord)
            {
                //
                //Congratulates the user
                //
                Console.WriteLine("Good job on guessing the hidden word!");
                Console.WriteLine($"The hidden word was {hiddenWords[randomNumber]}.");
                Console.ReadKey();
            }

            return userDoneWithHangman;
        }

        /// <summary>
        /// Shows the screen where users can add to word packs
        /// </summary>
        private static void DisplayAddWordPacks(string wordPackDataPath)
        {
            //
            // variable declaration
            //
            bool exittoMenu = false;
            char userConvertedResponse;
            bool validResponse;

            do
            {

                DisplayScreenHeader("Word Packs");

                WordPackMenuLine("1", "Food Word Pack (20 Words)", wordPackDataPath);
                WordPackMenuLine("2", "Gem Word Pack (20 Words)", wordPackDataPath);
                WordPackMenuLine("3", "Not Implemented", wordPackDataPath);
                WordPackMenuLine("4", "Not Implemented", wordPackDataPath);
                WordPackMenuLine("5", "Not Implemented", wordPackDataPath);
                WordPackMenuLine("6", "Not Implemented", wordPackDataPath);
                WordPackMenuLine("7", "Not Implemented", wordPackDataPath);
                WordPackMenuLine("8", "Not Implemented", wordPackDataPath);
                WordPackMenuLine("9", "Not Implemented", wordPackDataPath);
                MainMenuLine("0", "Exit to Menu");
                do
                {

                    ConsoleKeyInfo userInput = Console.ReadKey();
                    userConvertedResponse = char.Parse(userInput.KeyChar.ToString().ToLower());
                    validResponse = true;
                    switch (userConvertedResponse)
                    {
                        case ('1'):
                            {
                                string onOrOff = File.ReadLines(wordPackDataPath).Skip(0).Take(1).First();
                                string[] previousFile = File.ReadAllLines(wordPackDataPath);
                                if (onOrOff.Contains("0"))
                                {  try
                                    {
                                        previousFile[0] = "FoodPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        previousFile[0] = "FoodPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    catch
                                    {

                                    }
                                }
                                break;
                            }
                        case ('2'):
                            {
                                string line = File.ReadLines(wordPackDataPath).Skip(1).Take(1).First();
                                break;
                            }
                        case ('3'):
                            {

                                break;
                            }
                        case ('4'):
                            {
                                break;
                            }
                        case ('5'):
                            {
                                break;
                            }
                        case ('0'):
                            {
                                exittoMenu = true;
                                break; 
                            }
                        default:
                            {
                                Console.WriteLine(" Invalid choice. Please select a number 1-9");
                                validResponse = false;
                                break;
                            }
                    }
                } while (!validResponse);
            } while (!exittoMenu);
        }

        /// <summary>
        /// Adds in words in the word packs
        /// All the word pack contents rest within this method
        /// </summary>
        /// <param name="hiddenWords"></param>
        private static void HangmanWordPacks(List<string> hiddenWords, int currentLine)
        {
            switch (switch_on)
            {
                default:
            }
        }

        /// <summary>
        /// Sends back a random number between the given lower bound and the upper bound.
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        private static int RandomNumber(int lowerBound, int upperBound)
        {
            Random ranNumberGenerator = new Random();
            int randomNumber;
            randomNumber = ranNumberGenerator.Next(lowerBound, upperBound);

            return randomNumber;
        }

        /// <summary>
        /// Displays letters in a typicaly qwerty keyboard format.
        /// </summary>
        /// <param name="pickedCharacters"></param>
        private static void PickedLetterDisplay(List<char> alphabet, List<char> pickedCharacters, int cursorLeftPosition, int cursorTopPosition)
        {
            //
            // Variable declaration
            //
            bool characterPicked;

            //
            //Sets the cursor position 5 lower than the rest of the hangman game.
            //
            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 5);

            //
            // Writes out all the letters.
            // If the letter is already picked by the user, it instead displays a special character
            // The if/if elses outside of the pickedCharacters foreach is to make the characters look in a keyboard format.
            //
            foreach (char letter in alphabet)
            {
                characterPicked = false;
                if (letter == 'a')
                {
                    Console.Write(" ");
                }
                else if (letter == 'z')
                {
                    Console.Write("  ");
                }

                foreach (char character in pickedCharacters)
                {
                    if (letter == character)
                    {
                        characterPicked = true;
                    }
                }

                if (characterPicked == true)
                {
                    Console.Write("*  ");
                }
                else
                {
                    Console.Write($"{letter}  ");
                }

                if (letter == 'p')
                {
                    Console.WriteLine();
                }
                else if (letter == 'l')
                {
                    Console.WriteLine();
                }
            }

            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition);

        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        private static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Simply adds all letters in the alphabet, in qwerty order.
        /// </summary>
        /// <param name="alphabet"></param>
        private static void AlphabetCreation(List<char> alphabet)
        {
            alphabet.Add('q');
            alphabet.Add('w');
            alphabet.Add('e');
            alphabet.Add('r');
            alphabet.Add('t');
            alphabet.Add('y');
            alphabet.Add('u');
            alphabet.Add('i');
            alphabet.Add('o');
            alphabet.Add('p');
            alphabet.Add('a');
            alphabet.Add('s');
            alphabet.Add('d');
            alphabet.Add('f');
            alphabet.Add('g');
            alphabet.Add('h');
            alphabet.Add('j');
            alphabet.Add('k');
            alphabet.Add('l');
            alphabet.Add('z');
            alphabet.Add('x');
            alphabet.Add('c');
            alphabet.Add('v');
            alphabet.Add('b');
            alphabet.Add('n');
            alphabet.Add('m');
        }

        /// <summary>
        /// Displays the screen header after inputting the header text
        /// </summary>
        /// <param name="headerText"></param>
        private static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***************************************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Reads and adds to the hiddenWord list from the custom words file
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="hiddenWord"></param>
        private static bool ReadWordsFromFile(string dataPath, List<string> hiddenWord)
        {
            bool errorOccured = false;
            
            try
            {
                string[] listOfWords = File.ReadAllLines(dataPath);

                for (int i = 0; i < listOfWords.Length; i++)
                    listOfWords[i] = listOfWords[i].ToLower();

                foreach (string word in listOfWords)
                {
                    if (!hiddenWord.Contains(word))
                    {
                        hiddenWord.Add(word);
                    }
                }
            }
            catch
            {
               errorOccured = true;
            }

            return errorOccured;



        }
    }



}
