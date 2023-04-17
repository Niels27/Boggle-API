using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoggleWebApi.Models;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BoggleWebApi.Services
{
    public class BoggleService : IBoggleService
    {
        private static List<BoggleBoard> BoggleBoards = new List<BoggleBoard>();
        private readonly List<string> _wordList;
        static Random rnd = new Random();

        public BoggleService()
        {
            _wordList = File.ReadAllLines("lower.lst", Encoding.UTF8).ToList(); //loads file with all dutch words
        }
        //all possible characters in boggle, represented in a list of 16 lists containing 6 characters
        List<List<char>> letters = new List<List<char>>
        {
            new List<char> {'R', 'I', 'F', 'O', 'B', 'X'},
            new List<char> {'I', 'F', 'E', 'H', 'E', 'Y'},
            new List<char> {'D', 'E', 'N', 'O', 'W', 'S'},
            new List<char> {'U', 'T', 'O', 'K', 'N', 'D'},
            new List<char> {'H', 'M', 'S', 'R', 'A', 'O'},
            new List<char> {'L', 'U', 'P', 'E', 'T', 'S'},
            new List<char> {'A', 'C', 'I', 'T', 'O', 'A'},
            new List<char> {'Y', 'L', 'G', 'K', 'U', 'E'},
            new List<char> {'Q', 'B', 'M', 'J', 'O', 'A'},
            new List<char> {'E', 'H', 'I', 'S', 'P', 'N'},
            new List<char> {'V', 'E', 'T', 'I', 'G', 'N'},
            new List<char> {'B', 'A', 'L', 'I', 'Y', 'T'},
            new List<char> {'E', 'Z', 'A', 'V', 'N', 'D'},
            new List<char> {'R', 'A', 'L', 'E', 'S', 'C'},
            new List<char> {'U', 'W', 'I', 'L', 'R', 'G'},
            new List<char> {'P', 'A', 'C', 'E', 'M', 'D'}
        };


        //Create a boggleboard with shuffled dice
        public BoggleBoard GetBoggleBoard()
        {
            List<Die> rolledDice = new() { }; //a list of die that will be ''rolled''

            foreach (List<char> letterList in letters) //do this 16 times
            {
                var die = new Die(letterList[index: rnd.Next(5)]); //assign a random character to the die out of the 6 options in a letter list
                rolledDice.Add(die); //add the die to the list of rolled dice
            }

            rolledDice = Shuffle(rolledDice); //randomize the order of the 16 dice

            List<List<Die>> dice = new() //a list of 4 lists containing 4 rolled dice, essentially the boggle board

            {
                new List<Die> { rolledDice[0], rolledDice[1], rolledDice[2], rolledDice[3] },
                new List<Die> { rolledDice[4], rolledDice[5], rolledDice[6], rolledDice[7] },
                new List<Die> { rolledDice[8], rolledDice[9], rolledDice[10], rolledDice[11] },
                new List<Die> { rolledDice[12], rolledDice[13], rolledDice[14], rolledDice[15] }

            };

            //create a boggleboard with an unique Id and 16 rolled dice 
            var BoggleBoard = new BoggleBoard
            {
                BoggleBoardId = Guid.NewGuid(),
                Dice = dice
            };

            BoggleBoards.Add(BoggleBoard); //Add board to list of boards so we could retrieve it later

            return BoggleBoard;
        }

        //Get the requested boggleboard by Id from a list of all created boggleboards
        public BoggleBoard GetBoggleBoard(Guid BoggleBoardId)
        {
            BoggleBoard BoggleBoard = null;

            foreach (BoggleBoard bb in BoggleBoards)
            {
                if (bb.BoggleBoardId == BoggleBoardId)
                {
                    BoggleBoard = bb;
                    break;
                }
            }
            return BoggleBoard;
        }

        
        // Define a method called "Shuffle" that takes in a List of "Die" objects and returns a shuffled version of the list.
        public static List<Die> Shuffle(List<Die> list)
        {
            int n = list.Count;  
            while (n > 1) 
            {
                n--; 
                // Generate a random integer "k" between 0 and "n" 
                int k = rnd.Next(n + 1);

                // Retrieve the element at index "k" from the list and assign it to a variable called "value".
                Die value = list[k];

                // Swap the element at index "k" with the element at index "n".
                list[k] = list[n];

                // Swap the element at index "n" with "value".
                list[n] = value;
            }

            // Return the shuffled list.
            return list;
        }

        //Checks whether the word exists on the boggle board;for each die check if a letter on a die is in a word, if so remove it from the word 
        public bool CheckWordPresent(BoggleBoard board, string word)
        {
            foreach (var die in board.Dice.SelectMany(row => row)) //flattens nested list into single sequence of die objects
            {
                if (word.Contains(die.Value))
                {
                    word = word.Remove(word.IndexOf(die.Value), 1);
                }
            }
            //if the word becomes 0 it means the word is possible, and returns true
            return string.IsNullOrEmpty(word);
        }


        //checks if the word is in the dutch language
        public bool CheckWordExists(string word)
        {
            if (_wordList.Contains(word.ToLower()))
            {
                return true;
            }
            else { return false; }
            
        }
    }
}
