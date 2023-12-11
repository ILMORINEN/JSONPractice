using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONPractice
{
    public class FileReadException : Exception
    {
        public FileReadException() : base("Uncorrect file path") { }
        public FileReadException(string message) : base(message) { }
    }
}
