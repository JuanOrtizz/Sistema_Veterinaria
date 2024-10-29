using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Gato: Animal
{
    private string raza;
    private string? vacunas; // va una lista en el futuro con todas las vacunas del gato

    public Gato(string nombre, Cliente dueño, DateTime fecNac, int peso, Genero sexo, string raza) : base(nombre, dueño, fecNac, peso, sexo)
    {
        this.raza = raza;
    }

    public string Raza
    {
        get { return raza; }
        set { this.raza = value; }
    }

    public void VerVacunas()
    {
        //va a tener un foreach para ver todas las vacunas del animal.
    }

    public override string ToString()
    {
        return base.ToString() + "\nRaza del Gato: " + raza; // despues tambien va a imprimir la lista con todas las vacunas

    }
}
