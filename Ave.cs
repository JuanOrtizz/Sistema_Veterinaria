using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Ave : Animal
{
    private Variedad especie;

    public enum Variedad{ 
        Canario,
        Loro,
        Cata,
        Silvestre,
        Callejero,
        NoEspecificado
    }

    public Ave(string nombre, DateTime fecNac, double peso, Genero sexo, Variedad especie) 
        : base(nombre, fecNac, peso, sexo)
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
        return base.ToString() + "\nEspecie del Ave: " + especie;
    }
}
