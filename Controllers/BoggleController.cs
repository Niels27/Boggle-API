using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BoggleWebApi.Models;
using BoggleWebApi.Services;
using System.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoggleController : ControllerBase
    {

        private readonly IBoggleService _boggleService;
      

        public BoggleController(IBoggleService boggleService)
        {
            _boggleService = boggleService ?? throw new ArgumentNullException(nameof(boggleService));

        }



        [HttpGet("GetBoggleBoard")]
        public BoggleBoard GetBoggleBoard() => _boggleService.GetBoggleBoard();

        [HttpGet("GetBoggleBoard{BoggleBoardId}")]
        public ActionResult<BoggleBoard> GetBoggleBoard(Guid BoggleBoardId)
        {
            var BoggleBoard = _boggleService.GetBoggleBoard(BoggleBoardId);

            if (BoggleBoard == null)
            {
                return NotFound();
            }

            return BoggleBoard;
        }

        [HttpGet("IsValidWord/{BoggleBoardId}/{word}")]
        public bool IsValidWord(Guid BoggleBoardId, string word)
        {
            var BoggleBoard = _boggleService.GetBoggleBoard(BoggleBoardId);
      

            if (_boggleService.CheckWordPresent(BoggleBoard, word) && _boggleService.CheckWordExists(word) && word.Length > 2)
            {
                return true;
            }
            else
            {
                return false;
            }   

        }

        [HttpGet("ScoreWord/{word}")]
        public int ScoreWord(string word)
        {

            if (word.Length == 3 || word.Length == 4)
            {
                return 1;
            }
            else if (word.Length == 5)
            {
                return 2;
            }
            else if (word.Length == 6)
            {
                return 3;
            }
            else if (word.Length == 7)
            {
                return 5;
            }
            else if (word.Length >= 8)
            {
                return 11;
            }

            return 0;
        }

    }
}



