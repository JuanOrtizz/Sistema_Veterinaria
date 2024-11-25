using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public abstract class Persona
{

    protected string nombre;

    protected string apellido;

    protected long numTelefono;

    public Persona(string nombre, string apellido, long numTelefono)
    {
        this.nombre = nombre;
        this.apellido = apellido;
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

    public long NumTelefono
    {
        get { return numTelefono; }
        set { this.numTelefono = value; }
    }

    public override string ToString()
    {
        return "\nNombre: " + nombre + "\nApellido: " + apellido + "\nNumero de Telefono: " + numTelefono;
    }
}

