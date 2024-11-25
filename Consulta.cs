using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Consulta
{
    private DateTime fecha;
    private string motivoConsulta;
    private string diagnostico;

    public Consulta(string motivoConsulta, string diagnostico)
    {
        fecha = DateTime.Now;
        this.motivoConsulta = motivoConsulta;
        this.diagnostico = diagnostico;
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
        return "---Consulta---" +  "\nFecha: " + fecha + "\nMotivo de la consulta: " + motivoConsulta + "\nDiagnostico: " + diagnostico + "\n";
    }

}

