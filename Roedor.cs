using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Roedor : Animal
{
     private Variedad especie;

    public enum Variedad
    {
        Hamster,
        Cobayo,
        Chinchilla,
        Rata,
        NoEspecificado
    }

    public Roedor(string nombre, Cliente dueño, DateTime fecNac, double peso, Genero sexo, Variedad especie)
        : base(nombre, dueño, fecNac, peso, sexo)
    {
        this.especie = especie;
    }

    public Variedad Especie
    {
        get { return especie; }
        set { this.especie = value; }
    }

    public override string ToString()
    {
        return base.ToString() + "\nEspecie del Roedor: " + especie;
    }
}

