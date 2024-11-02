using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CamposCreacionIncorrectosException: Exception
{
    public CamposCreacionIncorrectosException() { }
    public CamposCreacionIncorrectosException(string message): base(message) { }
    public CamposCreacionIncorrectosException(string message, Exception inner): base (message, inner) { }
}

