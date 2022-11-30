using System;
using System.Data;

namespace Database
{
    public class PersonsBook
    {
        public int BookId { get;  set; }
        public int PersonId { get;  set; }
        public DateTime DateOfGetting { get;  set; }
        public DateTime DateOfReturn { get;  set; }

        
    }
}
