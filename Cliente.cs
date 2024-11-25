using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Cliente : Persona
{
    private int dni;
    private List <Animal> mascotas;

    public Cliente(string nombre, string apellido, int dni, long numTelefono) : base(nombre, apellido, numTelefono)
    { 
        this.dni = dni;
        mascotas = new List<Animal>();
    }

    public int DNI
    {
        get { return dni; }
        set { this.dni = value; }
    }

    public List<Animal> Mascotas
    {
        get { return mascotas; }
        set { this.mascotas = value; }
    }



    public void VerAnimales()
    {
        int contadorAnimales = 1;
        if(mascotas.Count == 0)
        {
            Console.WriteLine("Este cliente no tiene animales registrados en el sistema\n");
        }
        else
        {
            Console.WriteLine("---Animales del cliente---");
            foreach (Animal animal in mascotas)
            {
                Console.WriteLine("[ " + contadorAnimales +" ] Nombre: " + animal.Nombre + ", Tipo de animal: " + animal.GetType());
                contadorAnimales++;
            }
            Console.WriteLine();
        }
    }

    public override string ToString()
    {
        return "\n---Informacion Cliente---" + base.ToString() + "\nDNI:" + dni;
    }
}