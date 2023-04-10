using BoggleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleWebApi.Services
{
    public interface IBoggleService
    {
        BoggleBoard GetBoggleBoard();
        BoggleBoard GetBoggleBoard(Guid guid);
        bool CheckWordPresent(BoggleBoard BoggleBoard, string word);
        bool CheckWordExists(string word);
    }
}
