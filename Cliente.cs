using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cliente
{
    private string nombre;
    private string apellido;
    private int dni;
    private int numTelefono;
    private string? mascotas; // aca va a ir una lista en el futuro con todas las mascotas del cliente.

    public Cliente(string nombre, string apellido, int dni, int numTelefono)
    { 
        this.nombre = nombre;
        this.apellido = apellido;
        this.dni = dni;
        this.numTelefono = numTelefono;
    }

    public string Nombre
    {
        get { return nombre; }
        set { this.nombre = value; }
    }

    public string Apellido
    {
        get { return apellido; }
        set { this.apellido = value; }
    }

    public int DNI
    {
        get { return dni; }
        set { this.dni = value; }
    }

    public int NumTelefono
    {
        get { return numTelefono; }
        set { this.numTelefono = value; }
    }

    public void VerAnimales()
    {
        // aca va a ir un foreach para imprimir la lista completa de animal del cliente
    }

    public override string ToString()
    {
        return "\n---Informacion Cliente---" + "\n Nombre: " + nombre + "\n Apellido: " + apellido + "\n DNI:"+ dni 
            + "\n Numero de Telefono: " + numTelefono;
    }
}