using BoggleWebApi.Models;
using BoggleWebApi.Data;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System;

namespace BoggleWebApi.Data
{
    public class DataHandler : Singleton<DataHandler>
    {
        public List<string> WordList
        {
            get
            {
                if (wordList != null && wordList.Count > 0)
                    return wordList;

                PopulateWordList();

                return wordList;
            }
        }

        public List<BoggleBoard> RegisteredBoggleBoards
        {
            get
            {
                return registeredBoggleBoards;
            }
        }

        private List<BoggleBoard> registeredBoggleBoards = new List<BoggleBoard>();
        private List<string> wordList = new List<string>();

        public void PopulateWordList()
        {

            string f = HttpContext.Current.Server.MapPath("~/Data/lower.lst");


            using (StreamReader r = new StreamReader(f))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    wordList.Add(line);
                }
            }
        }
    }
}