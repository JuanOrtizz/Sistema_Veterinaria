using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SeleccionarOpcionException : Exception
{
    public SeleccionarOpcionException(): base("La opcion ingresada no puede estar vacia y debe ser numerica\n") { }
    public SeleccionarOpcionException(string message) :  base(message) { }
    public SeleccionarOpcionException(string message, Exception inner): base (message, inner) { }
}

