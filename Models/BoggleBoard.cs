using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoggleWebApi.Models
{
    public class BoggleBoard
    {
        public Guid BoggleBoardId { get; set; }
        public List<List<Die>> Dice { get; set; }
    }
}
