using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_FileIO_NTier.Models;

namespace Demo_FileIO_NTier
{
    public class CsvDataService : IDataService
    {
        private string _dataFilePath;

        public CsvDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }

        public IEnumerable<Character> ReadAll()
        {
            List<string> characterStrings = new List<string>();
            List<Character> characters = new List<Character>();

            try
            {
                StreamReader sr = new StreamReader(_dataFilePath);
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        characterStrings.Add(sr.ReadLine());
                    }
                }
                foreach (string charString in characterStrings)
                {
                    characters.Add(CharacterObjectBuilder(charString));
                }
            }
            catch (Exception)
            {

                throw;
            }

            return characters;

        }

        private Character CharacterObjectBuilder(string characterString)
        {
            const char DELINEATOR = ',';
            string[] properties = characterString.Split(DELINEATOR);

            Character character = new Character()
            {
                Id = Convert.ToInt32(properties[0]),
                LastName = properties[1],
                FirstName = properties[2],
                Address = properties[3],
                City = properties[4],
                State = properties[5],
                Zip = properties[6],
                Age = Convert.ToInt32(properties[7]),
                Gender = (Character.GenderType)Enum.Parse(typeof(Character.GenderType), properties[8])
            };

            return character;

        }

        private string CharacterStringBuilder(Character characterObject)
        {
            const string DELINEATOR = ",";
            string characterString;

            characterString =
                characterObject.Id + DELINEATOR +
                characterObject.LastName + DELINEATOR +
                characterObject.FirstName + DELINEATOR +
                characterObject.Address + DELINEATOR +
                characterObject.City + DELINEATOR +
                characterObject.State + DELINEATOR +
                characterObject.Zip + DELINEATOR +
                characterObject.Age + DELINEATOR +
                characterObject.Gender;

            return characterString;

        }

        public void WriteAll(IEnumerable<Character> characters)
        {
            try
            {
                StreamWriter sw = new StreamWriter(_dataFilePath);
                using (sw)
                {
                    StringBuilder sb = new StringBuilder(_dataFilePath);
                    sb.Clear();
                    foreach (Character c in characters)
                    {
                        sb.AppendLine(CharacterStringBuilder(c));
                    }
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
