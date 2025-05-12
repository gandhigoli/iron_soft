using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace iRONConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // A 2D matrix where rows indicates the letter and Column number indicates the frequency (0 based indexing).
            // For eg - letters[1, 2] indicates, keyPad number 2 with frequency 3 i.e 'C' (assuming 0 indexing)
            // ^ is used for convinence as some keypard numbers have 4 letters mapping and some has 3 letters mapping
            char[,] letters = { { '^', '^', '^', '^' }, { 'A', 'B', 'C', '^' }, { 'D', 'E', 'F', '^' }, { 'G', 'H', 'I', '^' }, { 'J', 'K', 'L', '^' }, { 'M', 'N', 'O', '^' }, { 'P', 'Q', 'R', 'S' }, { 'T', 'U', 'V', '^' }, { 'W', 'X', 'Y', 'Z' } };

            // Number of input strings
            int numberOfInputs = Convert.ToInt32(Console.ReadLine());
            for (int inputNo = 0; inputNo < numberOfInputs; inputNo++)
            {
                // String input
                string inputString = Console.ReadLine();

                // Function which returns a list of tuple of (keyPard Number, frequency of that number)
                List<(char, int)> data = OldPhonePad(inputString);
                string ans = "";
                for (int i = 0; i < data.Count; i++)
                {
                    ans += letters[data[i].Item1 - '0' - 1, data[i].Item2 - 1];
                }
                Console.WriteLine(ans);
            }
        }

        public static List<(char, int)> OldPhonePad(String input)
        {
            List<(char, int)> data = new List<(char, int)>();
            int count = 1;
            char prevLetter = input[0];
            for (int i = 1; i < input.Length - 1; i++)
            {
                // If prev letter was a space, store the current letter as prevLetter and increment the count
                if (prevLetter == ' ')
                {
                    count = 1;
                    prevLetter = input[i];
                    continue;
                }
                // Spaces are to be ignored directly
                if (input[i] == ' ')
                {
                    data.Add((prevLetter, count));
                    prevLetter = input[i];
                    continue;
                }
                // keep the count of frequency for successive repeating keypard numbers
                if (input[i] == input[i - 1] && prevLetter != '*')
                {
                    count += 1;
                }
                else if (prevLetter != '*')
                {
                    // If successive continuity of number breaks, append the keypard number and its frequency
                    data.Add((prevLetter, count));
                    count = 1;
                }
                else
                {
                    count = 1;
                }
                // Last appended tuple needs to be removed in case of *
                if (input[i] == '*')
                {
                    data.RemoveAt(data.Count - 1);
                    count = 0;
                }
                prevLetter = input[i];
            }
            if (count > 0 && prevLetter != ' ')
            {
                data.Add((prevLetter, count));
            }
            return data;
        }
    }
}
