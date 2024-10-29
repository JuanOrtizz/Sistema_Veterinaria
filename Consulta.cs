using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Consulta
{
    private Cliente? cliente;
    private Animal? animal;
    private DateTime fecha;
    private string motivoConsulta;
    private string diagnostico;

    public Consulta(Cliente cliente, Animal animal, DateTime fecha, string motivoConsulta, string diagnostico)
    {
        this.cliente = cliente;
        this.animal = animal;
        this.fecha = fecha;
        this.motivoConsulta = motivoConsulta;
        this.diagnostico = diagnostico;
    }

    public Cliente Cliente
    {
        get { return cliente; }
        set { cliente = value; }
    }

    public Animal Animal
    {
        get { return animal; }
        set { animal = value; }
    }

    public DateTime Fecha
    {
        get { return fecha; }
        set { fecha = value; }
    }

    public string MotivoConsulta
    {
        get { return motivoConsulta; }
        set { motivoConsulta = value; }
    }

    public string Diagnostico
    {
        get { return diagnostico; }
        set { diagnostico = value; }
    }

    public override string ToString()
    {
        return "---Consulta---\n" + "Cliente: " + cliente.DNI + ", " + cliente.Nombre + "," + cliente.Apellido + "\nAnimal: " + animal.Nombre
            + "\nFecha: " + fecha + "\nMotivo de la consulta: " + motivoConsulta + "\nDiagnostico: " + diagnostico;
    }

}

