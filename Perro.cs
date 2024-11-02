using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Perro : Animal
{
    private string raza;
    private string? vacunas; // va una lista en el futuro con todas las vacunas del perro

    public Perro(string nombre, Cliente dueño, DateTime fecNac, double peso, Genero sexo, string raza) : base(nombre, dueño, fecNac, peso, sexo)
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
        return base.ToString() + "\nRaza del Perro: " + raza; // despues tambien va a imprimir la lista con todas las vacunas

    }

}

