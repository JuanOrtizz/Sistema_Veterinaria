using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Animal
{
    protected string nombre;
    protected Cliente dueño;
    protected DateTime fecNac;
    protected double peso;
    protected Genero sexo;
    protected List<Consulta> historial;
    public enum Genero {
        Masculino,
        Femenino,
        NoEspecificado
    }

    public Animal(string nombre, Cliente dueño, DateTime fecNac, double peso, Genero sexo)
    {
        this.nombre = nombre;
        this.dueño = dueño;
        this.fecNac = fecNac;
        this.peso = peso;
        this.sexo = sexo;
        historial = new List<Consulta>();
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

    public double Peso 
    {
        get { return peso; }
        set { this.peso= value; }
    }

    public Genero Sexo
    {
        get { return sexo; }
        set { this.sexo = value; }
    }

    public List<Consulta> Historial
    {
        get { return historial; }
    }

    // Metodos para el historial de cada animal
    public void verHistorial()
    {
        Console.WriteLine("---Historial de Consultas---");
        if(historial.Count == 0)
        {
            Console.WriteLine("Este animal no tiene consultas registradas en el sistema\n");
        }
        else
        {
            int contador = 1;
            foreach (Consulta consulta in historial)
            {
                Console.WriteLine("[ "+ contador +" ] Motivo: " + consulta.MotivoConsulta + ", Fecha: " + consulta.Fecha.ToString("dd/MM/yyyy"));
                contador++;
            }
            Console.WriteLine();
        }
       
    }


    public override string ToString()
    {
        return "---Informacion del Animal---" + "\nNombre: " + nombre + "\nDueño: " + dueño.Nombre + ", " + dueño.Apellido +
            "\nFecha de Nacimiento: " + fecNac.ToString("dd/MM/yyyy") + "\nPeso en Kg: " + peso + "\nSexo del animal: " + sexo; 
    }

}

