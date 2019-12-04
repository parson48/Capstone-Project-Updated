using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Capstone_Project_Starting
{
    class Program
    {
        //************************************
        //Title: Hangman
        //Application Type: Console
        //Description: Lets the user play hangman, letting them add in custom words or using pre-made word packs.
        //Author: Robert Parsons
        //Date Created: 11/18/2019
        //Last Modified: 12/2/2019
        //************************************

        static void Main(string[] args)
        {
            //
            // Sets the text color to white, because I think it looks better that way.
            //
            Console.ForegroundColor = ConsoleColor.White;

            string customWordDataPath = @"HangmanInfo\WordList.txt";
            string wordPackDataPath = @"HangmanInfo\WordPacks.txt";

            DisplayOpeningScreen();

            MainMenu(customWordDataPath, wordPackDataPath);
        }

        /// <summary>
        /// Has the code for the main menu of the application
        /// </summary>
        /// <param name="customWordDataPath"></param>
        /// <param name="wordPackDataPath"></param>
        private static void MainMenu(string customWordDataPath, string wordPackDataPath)
        {
            //
            // variable declaration
            //
            bool exitApplication = false;
            char userConvertedResponse;
            bool validResponse;

            //
            // Creates a list with all the letters of the alphabet, used to double check that words and inputs use only letters
            //
            List<char> alphabet = new List<char>();
            AlphabetCreation(alphabet);

            //
            // The menu itself, it will stay within this loop until the user exits the application
            //
            do
            {

                DisplayScreenHeader("Application Menu");

                MainMenuLine("1", "Play Hangman");
                MainMenuLine("2", "How to Play");
                MainMenuLine("3", "Add Custom Words");
                MainMenuLine("4", "Delete Words");
                MainMenuLine("5", "Add/Remove Word Packs");
                MainMenuLine("6", "Quit Application");
                do
                {

                    ConsoleKeyInfo userInput = Console.ReadKey();
                    userConvertedResponse = char.Parse(userInput.KeyChar.ToString().ToLower());
                    validResponse = true;
                    switch (userConvertedResponse)
                    {
                        case ('1'):
                            {
                                Hangman(customWordDataPath, wordPackDataPath, alphabet);
                                break;
                            }
                        case ('2'):
                            {
                                DisplayHowToPlay();
                                break;
                            }
                        case ('3'):
                            {
                                DisplayAddCustomWords(customWordDataPath, alphabet);

                                break;
                            }
                        case ('4'):
                            {
                                DisplayRemoveCustomWords(customWordDataPath);
                                break;
                            }
                        case ('5'):
                            {
                                DisplayAddWordPacks(wordPackDataPath);
                                break;
                            }
                        case ('6'):
                            {
                                exitApplication = true;
                                Console.WriteLine();
                                Console.WriteLine("Exiting application, please come again soon!");
                                DisplayContinuePrompt();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine(" Invalid choice. Please select a number 1-6.");
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

        /// <summary>
        /// Makes a line for the word pack menu, similar to the MainMenuLine, but adding in the status of it being on or off
        /// </summary>
        /// <param name="precludingNumber"></param>
        /// <param name="packName"></param>
        /// <param name="wordPackDataPath"></param>
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
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"  [ERROR] - click this option to attempt to fix it.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"  [ERROR] ");
                    Console.ForegroundColor = ConsoleColor.White;
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
            //
            // Empties out the hidden words list, can be replaced if wish to have it check to make sure there isn't duplicate words.
            //
            hiddenWords.Clear();

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
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (2):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (3):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (4):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (5):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (6):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (7):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (8):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        case (9):
                            {
                                HangmanWordPacks(hiddenWords, currentLine);
                                break;
                            }
                        default:
                            break;
                    }
                }
                currentLine++;
            }

            //
            // Adds custom words to the words list
            // 
            bool customWordErrorOccured;
            try
            {
                customWordErrorOccured = ReadWordsFromFile(customWordDataPath, hiddenWords);
            }
            catch
            {
                customWordErrorOccured = false;
            }

            //
            // Gives the user a heads up if the custom words aren't included.
            //
            if (customWordErrorOccured == true)
            {
                DisplayScreenHeader("Hangman");
                Console.WriteLine("There has been an error obtaining the custom words.");
                Console.WriteLine($"Please make sure that the WordList file is in {customWordDataPath}");
                Console.WriteLine("If it is there, please also make sure that any custom words are only using letters.");
                DisplayContinuePrompt();
            }

            //
            // Produces words if none can be pulled from other sources
            //
            if (hiddenWords.Count == 0)
            {
                DisplayScreenHeader("Hangman");
                Console.WriteLine("The word list is empty.");
                Console.WriteLine("Although it is recommended that you fix this by adding words to the word list or adding in word packs, Hangman is still playable.");
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
        private static void Hangman(string customWordDataPath, string wordPackDataPath, List<char> alphabet)
        {
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
            bool completedWord;
            bool letterInWord;
            int completedCharacter;
            int hiddenCharactersPlacement;
            List<char> userPickedCharacters = new List<char>();
            bool userDoneWithHangman;
            int incorrectGuesses;
            bool userLostHangman = false;
            const int MAX_INCORRECT_GUESSES = 7;

            //
            // Updates and Initializes the hiddenWords list from any word packs and any custom words
            // Also gives the user any error information, incase any dataPaths cannot be reached or other reasons
            //
            HangmanWordIntializer(customWordDataPath, wordPackDataPath, hiddenWords, alphabet);

            do
            {
                //
                // Resets the game & variables
                //
                userPickedCharacters.Clear();
                completedWord = false;
                letterInWord = false;
                incorrectGuesses = 0;
                userLostHangman = false;

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
                            Console.WriteLine("Exiting hangman!".PadRight(Console.WindowWidth-1));
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
                                try
                                {
                                    Console.SetCursorPosition(tempLeftCursorPositionGuess, tempTopCursorPositionGuess);
                                }
                                catch
                                {
                                    Console.Write("Guess a letter >> ");
                                }

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

                    //
                    // Checks to make sure a letter was not already picked.
                    // If it was already picked, it tells the user and DOES NOT PENALIZE THE USER
                    //
                    if (userPickedCharacters.Contains(userConvertedResponse))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{userConvertedResponse} was already guessed.");
                    }
                    else
                    {
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
                            incorrectGuesses++;
                        }
                    }

                    if (incorrectGuesses == MAX_INCORRECT_GUESSES)
                    {
                        completedWord = true;
                        userLostHangman = true;
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
                    // Temporary gives the cursors potition, so that displayed the hangman does not disturb the reset of the layout
                    //
                    tempTopCursorPosition = Console.CursorTop;
                    tempLeftCursorPosition = Console.CursorLeft;

                    HangmanGuyDisplay(tempLeftCursorPosition, tempTopCursorPosition, incorrectGuesses);

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

                if (completedWord && !userLostHangman)
                {
                    //
                    //Congratulates the user
                    //
                    Console.WriteLine("Good job on guessing the hidden word!");
                    Console.WriteLine($"The hidden word was {hiddenWords[randomNumber]}.");
                    Console.ReadKey();
                }
                else if (userLostHangman)
                {
                    Console.WriteLine("You did not guess the hidden word in time.");
                    Console.WriteLine($"The hidden word was {hiddenWords[randomNumber]}.");
                    Console.ReadKey();
                }
                else if (!userDoneWithHangman)
                {
                    Console.WriteLine($"The hidden word was {hiddenWords[randomNumber]}.");
                    Console.ReadKey();
                }

            } while (userDoneWithHangman == false);
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
            try
            {
                File.ReadAllLines(wordPackDataPath);
                do
                {

                    DisplayScreenHeader("Word Packs");

                    WordPackMenuLine("1", "Vegetable Pack (20 Words)", wordPackDataPath);
                    WordPackMenuLine("2", "Gem Pack (20 Words)", wordPackDataPath);
                    WordPackMenuLine("3", "Mountain Moons Pack (20 Words)", wordPackDataPath);
                    WordPackMenuLine("4", "Fruit Pack (20 Words)", wordPackDataPath);
                    WordPackMenuLine("5", "Ancient Empires Pack (20 Words)", wordPackDataPath);
                    WordPackMenuLine("6", "Instrument Pack (20 Words)", wordPackDataPath);
                    WordPackMenuLine("7", "'Word of the Day' Pack A (20 Words)", wordPackDataPath);
                    WordPackMenuLine("8", "'Word of the Day' Pack B (20 Words)", wordPackDataPath);
                    WordPackMenuLine("9", "'Word of the Day' Pack C (20 Words)", wordPackDataPath);

                    MainMenuLine("~", "Exit to Menu");
                    
                    //
                    // This determines what is shown to the user. If the pack has a 1, it is on, otherwise it is a 0 and off.
                    // If the file exists but has differing information (ie no 0), it will override the first 9 lines
                    //
                    do
                    {

                        //
                        // Single Key input
                        //
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
                                    {

                                        previousFile[0] = "VegetablePack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[0] = "VegetablePack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('2'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(1).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[1] = "GemPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[1] = "GemPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('3'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(2).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[2] = "MoonMountainPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[2] = "MoonMountainPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('4'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(3).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[3] = "FruitPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[3] = "FruitPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('5'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(4).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[4] = "AncientEmpirePack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[4] = "AncientEmpirePack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('6'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(5).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[5] = "InstrumentPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[5] = "InstrumentPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('7'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(6).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[6] = "WOTDUnoPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[6] = "WOTDUnoPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('8'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(7).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[7] = "WOTDDosPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[7] = "WOTDDosPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('9'):
                                {
                                    string onOrOff = File.ReadLines(wordPackDataPath).Skip(8).Take(1).First();
                                    string[] previousFile = File.ReadAllLines(wordPackDataPath);

                                    if (onOrOff.Contains("0"))
                                    {

                                        previousFile[8] = "WOTDTresPack: 1";
                                        File.WriteAllLines(wordPackDataPath, previousFile);

                                    }
                                    else
                                    {
                                        previousFile[8] = "WOTDTresPack: 0";
                                        File.WriteAllLines(wordPackDataPath, previousFile);
                                    }
                                    break;
                                }
                            case ('~'):
                                {
                                    exittoMenu = true;
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine(" Invalid choice. Please select a number 1-9 or ~");
                                    validResponse = false;
                                    break;
                                }
                        }
                    } while (!validResponse);
                } while (!exittoMenu);
            }
            catch
            {
                //
                //This block is used if the file cannot be reached. Does not fix the problem
                //
                DisplayScreenHeader("Word Packs");

                Console.WriteLine("Error!");
                Console.WriteLine($"The program cannot reach the word packs file at {wordPackDataPath}");
                Console.WriteLine("Please try making sure the file exists");
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// Adds in words in the word packs
        /// All the word pack contents rest within this method
        /// </summary>
        /// <param name="hiddenWords"></param>
        private static void HangmanWordPacks(List<string> hiddenWords, int currentLine)
        {
            switch (currentLine)
            {
                //Vegetables
                case (1):
                    {
                        hiddenWords.Add("cucumber");
                        hiddenWords.Add("artichoke");
                        hiddenWords.Add("rutabaga");
                        hiddenWords.Add("radicchio");
                        hiddenWords.Add("celery");
                        hiddenWords.Add("cabbage");
                        hiddenWords.Add("arugula");
                        hiddenWords.Add("arrowroot");
                        hiddenWords.Add("eggplant");
                        hiddenWords.Add("lettuce");
                        hiddenWords.Add("asparagus");
                        hiddenWords.Add("cauliflower");
                        hiddenWords.Add("mushroom");
                        hiddenWords.Add("potato");
                        hiddenWords.Add("shallot");
                        hiddenWords.Add("spinach");
                        hiddenWords.Add("zucchini");
                        hiddenWords.Add("turnip");
                        hiddenWords.Add("squash");
                        hiddenWords.Add("pumpkin");
                        break;
                    }
                //Gemstones
                case (2):
                    {
                        hiddenWords.Add("chrysolite");
                        hiddenWords.Add("sapphire");
                        hiddenWords.Add("garnet");
                        hiddenWords.Add("pearl");
                        hiddenWords.Add("tanzanite");
                        hiddenWords.Add("topaz");
                        hiddenWords.Add("amethyst");
                        hiddenWords.Add("spinel");
                        hiddenWords.Add("aquamarine");
                        hiddenWords.Add("ruby");
                        hiddenWords.Add("citrine");
                        hiddenWords.Add("diamond");
                        hiddenWords.Add("emerald");
                        hiddenWords.Add("kunzite");
                        hiddenWords.Add("opal");
                        hiddenWords.Add("zircon");
                        hiddenWords.Add("peridot");
                        hiddenWords.Add("sardonyx");
                        hiddenWords.Add("jasper");
                        hiddenWords.Add("carnelian");
                        break;
                    }
                //Mountain Moons
                case (3):
                    {
                        hiddenWords.Add("huygens");
                        hiddenWords.Add("hadley");
                        hiddenWords.Add("bradley");
                        hiddenWords.Add("penck");
                        hiddenWords.Add("blanc");
                        hiddenWords.Add("wolff");
                        hiddenWords.Add("vitruvius");
                        hiddenWords.Add("piton");
                        hiddenWords.Add("pico");
                        hiddenWords.Add("maraldi");
                        hiddenWords.Add("agnes");
                        hiddenWords.Add("dilip");
                        hiddenWords.Add("herodotus");
                        hiddenWords.Add("ardeshir");
                        hiddenWords.Add("esam");
                        hiddenWords.Add("moro");
                        hiddenWords.Add("usov");
                        hiddenWords.Add("dieter");
                        hiddenWords.Add("argaeus");
                        hiddenWords.Add("hansteen");

                        break;
                    }
                //Fruits
                case (4):
                    {
                        hiddenWords.Add("apple");
                        hiddenWords.Add("pear");
                        hiddenWords.Add("orange");
                        hiddenWords.Add("grapefruit");
                        hiddenWords.Add("lime");
                        hiddenWords.Add("nectarines");
                        hiddenWords.Add("apricot");
                        hiddenWords.Add("peach");
                        hiddenWords.Add("plum");
                        hiddenWords.Add("banana");
                        hiddenWords.Add("strawberry");
                        hiddenWords.Add("passionfruit");
                        hiddenWords.Add("watermelon");
                        hiddenWords.Add("tomatoe");
                        hiddenWords.Add("avocado");
                        hiddenWords.Add("blueberry");
                        hiddenWords.Add("raspberry");
                        hiddenWords.Add("kiwi");
                        hiddenWords.Add("cantaloupe");
                        hiddenWords.Add("mandarin");
                        break;
                    }
                //Ancient Empires
                case (5):
                    {
                        hiddenWords.Add("akkadian");
                        hiddenWords.Add("babylonian");
                        hiddenWords.Add("egyptian");
                        hiddenWords.Add("mitanni");
                        hiddenWords.Add("hittite");
                        hiddenWords.Add("carthaginian");
                        hiddenWords.Add("kushite");
                        hiddenWords.Add("median");
                        hiddenWords.Add("achaemenid");
                        hiddenWords.Add("nanda");
                        hiddenWords.Add("macedonian");
                        hiddenWords.Add("mauryan");
                        hiddenWords.Add("seleucid");
                        hiddenWords.Add("ptolemaic");
                        hiddenWords.Add("parthian");
                        hiddenWords.Add("armenian");
                        hiddenWords.Add("shunga");
                        hiddenWords.Add("pontic");
                        hiddenWords.Add("roman");
                        hiddenWords.Add("aksumite");
                        break;
                    }
                //Instruments
                case (6):
                    {
                        hiddenWords.Add("flute");
                        hiddenWords.Add("keyboard");
                        hiddenWords.Add("cowbell");
                        hiddenWords.Add("clarient");
                        hiddenWords.Add("guitar");
                        hiddenWords.Add("saxophone");
                        hiddenWords.Add("kazoo");
                        hiddenWords.Add("viola");
                        hiddenWords.Add("maracas");
                        hiddenWords.Add("fiddle");
                        hiddenWords.Add("accordion");
                        hiddenWords.Add("harmonica");
                        hiddenWords.Add("tambourine");
                        hiddenWords.Add("ocarina");
                        hiddenWords.Add("piccolo");
                        hiddenWords.Add("vibraphone");
                        hiddenWords.Add("bagpipes");
                        hiddenWords.Add("ukulele");
                        hiddenWords.Add("trumpet");
                        hiddenWords.Add("drums");
                        break;
                    }
                //Word of the day - Pack 1
                case (7):
                    {
                        hiddenWords.Add("apocryphal");
                        hiddenWords.Add("dilapidated");
                        hiddenWords.Add("fraught");
                        hiddenWords.Add("sobriquet");
                        hiddenWords.Add("chilblain");
                        hiddenWords.Add("posthaste");
                        hiddenWords.Add("aphorism");
                        hiddenWords.Add("teleological");
                        hiddenWords.Add("gambit");
                        hiddenWords.Add("incongruous");
                        hiddenWords.Add("officious");
                        hiddenWords.Add("recondite");
                        hiddenWords.Add("fortitude");
                        hiddenWords.Add("heterodox");
                        hiddenWords.Add("sempiternal");
                        hiddenWords.Add("retinue");
                        hiddenWords.Add("comestible");
                        hiddenWords.Add("incognito");
                        hiddenWords.Add("pointillistic");
                        hiddenWords.Add("lyric");
                        break;
                    }
                //Word of the day - Pack 2
                case (8):
                    {
                        hiddenWords.Add("mitigate");
                        hiddenWords.Add("sawbones");
                        hiddenWords.Add("futhark");
                        hiddenWords.Add("divulge");
                        hiddenWords.Add("redound");
                        hiddenWords.Add("caustic");
                        hiddenWords.Add("scapegoat");
                        hiddenWords.Add("blandish");
                        hiddenWords.Add("exoteric");
                        hiddenWords.Add("wheedle");
                        hiddenWords.Add("belfry");
                        hiddenWords.Add("hobbyhorse");
                        hiddenWords.Add("maunder");
                        hiddenWords.Add("knackered");
                        hiddenWords.Add("fiduciary");
                        hiddenWords.Add("coruscate");
                        hiddenWords.Add("undulate");
                        hiddenWords.Add("lackadaisical");
                        hiddenWords.Add("respite");
                        hiddenWords.Add("phantasm");
                        break;
                    }
                //Word of the day - Pack 3
                case (9):
                    {
                        hiddenWords.Add("prodigious");
                        hiddenWords.Add("hypermnesia");
                        hiddenWords.Add("ephemeral");
                        hiddenWords.Add("asperity");
                        hiddenWords.Add("trivial");
                        hiddenWords.Add("stratagem");
                        hiddenWords.Add("footle");
                        hiddenWords.Add("incipient");
                        hiddenWords.Add("darling");
                        hiddenWords.Add("regale");
                        hiddenWords.Add("countermand");
                        hiddenWords.Add("palimpsest");
                        hiddenWords.Add("tenacious");
                        hiddenWords.Add("remittance");
                        hiddenWords.Add("pungle");
                        hiddenWords.Add("scavenger");
                        hiddenWords.Add("disparage");
                        hiddenWords.Add("commemorate");
                        hiddenWords.Add("lacuna");
                        hiddenWords.Add("disbursement");
                        break;
                    }

                default:
                    {
                        break;
                    }
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
        /// Shows the hanged man.
        /// </summary>
        /// <param name="cursorLeftPosition"></param>
        /// <param name="cursorTopPosition"></param>
        /// <param name="incorrectGuesses"></param>
        private static void HangmanGuyDisplay(int cursorLeftPosition, int cursorTopPosition, int incorrectGuesses)
        {
            const int CURSOR_LEFT_BUFFER = 55;

            //
            // This try makes sure the program does not crash if the window is resized too small.
            // If the window is too small intially, then it just makes the hangman below the alphabet display (7 below initial position)
            //
            try
            {
                Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 2);
                switch (incorrectGuesses)
                {
                    case (0):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (1):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (2):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (3):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("/|   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (4):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (5):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("  \\  | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (6):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("/ \\  | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (7):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" x  ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("/ \\  | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 3);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 4);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 5);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 6);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition + CURSOR_LEFT_BUFFER, cursorTopPosition + 7);
                            Console.WriteLine("_____|_");
                            break;
                        }
                }
            }
            catch 
            {
                Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 7);
                switch (incorrectGuesses)
                {
                    case (0):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (1):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (2):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (3):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("/|   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (4):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (5):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("  \\  | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (6):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine(" o   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("/ \\  | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    case (7):
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" x  ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("/|\\  | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("/ \\  | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(" _____ ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 8);
                            Console.WriteLine(" |   | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 9);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 10);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 11);
                            Console.WriteLine("     | ");
                            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition + 12);
                            Console.WriteLine("_____|_");
                            break;
                        }
                }
            }




            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition);
        }

        /// <summary>
        /// Display continue prompt
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
        private static bool ReadWordsFromFile(string customDataPath, List<string> hiddenWord)
        {
            bool errorOccured = false;

            try
            {
                string[] listOfWords = File.ReadAllLines(customDataPath);

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

        /// <summary>
        /// Adds custom words to the WordList file, if the word only contains letters of the alphabet
        /// </summary>
        /// <param name="customWordDataPath"></param>
        /// <param name="alphabet"></param>
        private static void DisplayAddCustomWords(string customWordDataPath, List<char> alphabet)
        {
            string newWord;
            bool invalidWord;
            bool exitToMenu = false;
            DisplayScreenHeader("Adding Custom Words");

            Console.WriteLine("Simply write in any words, using letters a-z, to add the words to the word pack!");
            Console.WriteLine("Press ~ when you wish to exit to the menu.");

            //
            // Reads the input to add the word.
            //
            do
            {
                newWord = Console.ReadLine().ToLower();

                if (newWord == "~")
                {
                    exitToMenu = true;
                }
                else
                {
                    invalidWord = true;
                    foreach (char wordLetter in newWord)
                    {
                        invalidWord = true;
                        foreach (char letter in alphabet)
                        {
                            if (wordLetter == letter)
                            {
                                invalidWord = false;
                                break;
                            }
                        }
                        if (invalidWord)
                        {
                            break;
                        }
                    }
                    if (invalidWord)
                    {
                        Console.WriteLine("Invalid word! Please type in a word only with letters of the alphabet.");
                    }
                    else
                    {
                        try
                        {
                            File.AppendAllText(customWordDataPath, newWord);
                            File.AppendAllText(customWordDataPath, "\n");
                            Console.Write($"  {newWord} added!");
                            Console.WriteLine();
                        }
                        catch
                        {
                            Console.WriteLine("Application could not write to to the file.");
                            Console.WriteLine("Make sure that the application can access the file and try again.");
                        }
                    }
                }
            } while (!exitToMenu);

            Console.WriteLine("Exiting to the menu!");
            DisplayContinuePrompt();

        }

        /// <summary>
        /// Shows the user all their custom words, and lets them delete the words by typing them in.
        /// </summary>
        /// <param name="customWordDataPath"></param>
        /// <param name="alphabet"></param>
        private static void DisplayRemoveCustomWords(string customWordDataPath)
        {
            bool doneWithRemoving = false;

            DisplayScreenHeader("Removing Custom Words");

            //
            // The main loop for removing custom words
            // Provided the file can be reached, it displays all the custom words and lets the user type in the one they want deleted.
            //
            do
            {

                try
                {
                    //
                    // Creates a list by looking at the word list file.
                    //
                    List<string> allCustomWords = File.ReadAllLines(customWordDataPath).ToList();

                    if (allCustomWords.Count == 0)
                    {
                        Console.WriteLine("There are no custom words.");
                        Console.WriteLine("As such, no custom words can be removed.");
                        DisplayContinuePrompt();
                        doneWithRemoving = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Current Words:");
                        foreach (string word in allCustomWords)
                        {
                            Console.WriteLine(word);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Please type in the word you would like to remove.");
                        Console.WriteLine("If you are done, type in ~ to exit.");

                        //
                        // Checks the user input. If it is the same as a custom word, it removes the custom word.
                        //
                        string userInput = Console.ReadLine().ToLower();
                        if (userInput == "~")
                        {
                            doneWithRemoving = true;
                        }
                        else
                        {
                            foreach (string word in allCustomWords)
                            {
                                if (userInput == word.ToLower())
                                {
                                    allCustomWords.Remove(word);
                                    break;
                                }
                            }
                            File.WriteAllLines(customWordDataPath, allCustomWords);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("The file could not be reached.");
                    Console.WriteLine("Please make sure you can access and modify a file at the data path.");
                    Console.WriteLine($"The datapath is {customWordDataPath}");
                    doneWithRemoving = true;
                    DisplayContinuePrompt();
                }
            } while (!doneWithRemoving);
        }

        /// <summary>
        /// Shows the user how to play hangman.
        /// </summary>
        private static void DisplayHowToPlay()
        {
            //
            //Simply a wall of text that shows the user how to play hangman.
            //
            DisplayScreenHeader("How to Play");

            Console.WriteLine("Playing hangman is quit simple.");
            Console.WriteLine("The computer picks a word, and displays the word, replacing unguessed letters with *");
            Console.WriteLine("Simply guess a letter a-z that you think is in the picked word");
            Console.WriteLine("If you are correct, the displayed word replaces any * with the correct letter");
            Console.WriteLine("However, incorrect guesses further detail the hangman, with enough incorrect guesses losing you the game");
            Console.WriteLine("You win if you guess all the letters of the word before the hangman is finished.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the opening screen that pops up when you start the application.
        /// </summary>
        private static void DisplayOpeningScreen()
        {
            DisplayScreenHeader("Welcome!");

            Console.WriteLine();
            Console.WriteLine("This application will let you play hangman!");
            Console.WriteLine("For more information on how to play hangman, goto the 'How to Play' section on the continuing screen.");
            Console.WriteLine("This application also lets you add in custom words or add in packs of words.");
            Console.WriteLine("In most screens, entering in '~' will bring you back to the main menu.");

            DisplayContinuePrompt();
        }
    }



}