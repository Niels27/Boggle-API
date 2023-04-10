using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoggleWebApi.Models
{
    public class Die
    {
        public char Value { get; set; }
        
        public Die(char value)
        {
            this.Value = value;
        }
    }
   
}
