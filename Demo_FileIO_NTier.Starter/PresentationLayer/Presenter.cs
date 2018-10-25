using Demo_FileIO_NTier.Models;
using Demo_FileIO_NTier.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_FileIO_NTier
{
    class Presenter
    {

        static CharacterBLL _characterBLL;

        public Presenter(CharacterBLL characterBLL)
        {
            _characterBLL = characterBLL;
            ManageApplicationLoop();
        }

        private void ManageApplicationLoop()
        {
            DisplayWelcomeScreen();
            DisplayListOfCharacters();
            DisplayClosingScreen();
        }

        static void DisplayHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t\t{headerText}");
            Console.WriteLine();
        }

        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }

        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t\tWelcome");

            DisplayContinuePrompt();
        }

        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t\tGood bye!");

            DisplayContinuePrompt();
        }

        private void DisplayCharacterTable(List<Character> characters)
        {
            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("Id".PadRight(8));
            columnHeader.Append("Full Name".PadRight(25));

            Console.WriteLine(columnHeader.ToString());

            characters = characters.OrderBy(c => c.Id).ToList();

            foreach (Character character in characters)
            {
                StringBuilder characterInfo = new StringBuilder();

                characterInfo.Append(character.Id.ToString().PadRight(8));
                characterInfo.Append(character.FullName().PadRight(8));

                Console.WriteLine(characterInfo.ToString());
            }

        }

        private void DisplayListOfCharacters()
        {
            bool success;
            string message;

            List<Character> characters = _characterBLL.GetCharacter(out success, out message) as List<Character>;
            characters = characters.OrderBy(c => c.Id).ToList();

            DisplayHeader("List of Characters");

            if (success)
            {
                DisplayCharacterTable(characters);
            } else
            {
                Console.WriteLine(message);
            }

            DisplayContinuePrompt();
        }

    }
}
