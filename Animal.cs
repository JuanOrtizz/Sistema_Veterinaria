using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Animal: IHistorialAnimal
{
    protected string nombre;
    protected Cliente dueño;
    protected DateTime fecNac;
    protected int peso;
    protected Genero sexo;
    public enum Genero {
        Masculino,
        Femenino,
        NoEspecificado
    }

    public Animal(string nombre, Cliente dueño, DateTime fecNac, int peso, Genero sexo)
    {
        this.nombre = nombre;
        this.dueño = dueño;
        this.fecNac = fecNac;
        this.peso = peso;
        this.sexo = sexo;
    }

    public string Nombre
    {
        get { return nombre; }
        set { this.nombre= value; }
    }

    public Cliente Dueño 
    {
        get { return dueño; }
        set { this.dueño = value; }
    }

    public DateTime FecNac
    {
        get { return fecNac; }
        set { this.fecNac = value; }
    }

    public int Peso 
    {
        get { return peso; }
        set { this.peso= value; }
    }

    public Genero Sexo
    {
        get { return sexo; }
        set { this.sexo = value; }
    }

    // Metodos para el historial de cada animal
    public void verHistorial()
    {
        //aca se va a implementar cuando tengamos lista.
    }

    public void actualizarHistorial()
    {
        //aca vamos a agregar una consulta cuando tengamos listas. 
    }

    public override string ToString()
    {
        return "---Informacion del Animal---" + "\nNombre: " + nombre + "\nDueño: " + dueño.Nombre + ", " + dueño.Apellido +
            "\nFecha de Nacimiento: " + fecNac.ToString("dd/MM/yyyy") + "\nPeso en Kg: " + peso + "\nSexo del animal: " + sexo; 
    }
}

