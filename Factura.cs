using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Factura;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Factura
{
    public static int ultimoNumeroFactura = 0;// en el futuro se va a guardar en una BDD o archivo.txt

    private int nroFactura;
    private DateTime fecha;
    private Cliente cliente; //permiten null temporalmente
    private Animal animal;
    private Servicios servicio;
    private double precio;

    private static Dictionary<Servicios, double> preciosServicios = new Dictionary<Servicios, double>
    {
        { Servicios.Revision, 2000.00 },
        { Servicios.Cirujia, 10000.00 },
        { Servicios.ControlCompleto, 6000.00 },
        { Servicios.Vacunacion, 4000.00 },
        { Servicios.Laboratorio, 5000.00 }
    };

    public enum Servicios
    {
        Revision,
        Cirujia,
        ControlCompleto,
        Vacunacion,
        Laboratorio,
        NoDefinido
    }

    public Factura(Cliente cliente, Animal animal, Servicios servicio)
    {
        nroFactura = ultimoNumeroFactura;// incrementa en uno el ultimo numero de factura y lo asigna a nroFactura
        fecha = DateTime.Now;// guarda la fecha y hora actual de cuando se creo la factura.
        this.cliente = cliente;
        this.animal = animal;
        this.servicio = servicio;
        CalcularPrecio(servicio);
    }

    public int NroFactura
    {
        set { nroFactura = value; }
        get { return nroFactura; }
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
    }

    public Servicios Servicio
    {
        get { return servicio; }
        set { servicio = value; }
    }

    public double Precio
    {
        get { return precio; }
        set { precio = value; }
    }

    public static Dictionary<Servicios, double> PreciosServicios
    {
        get { return preciosServicios; }
        set { preciosServicios = value; }
    }

    public void CalcularPrecio(Servicios servicio)
    {
        if (preciosServicios.TryGetValue(servicio, out double precioServicio))
        {
            Precio = precioServicio;
        }
    }

    public override string ToString()
    {
        return "---Factura---" + "\nNumero de Factura: " + nroFactura + "\nFecha de factura: " + fecha.ToString("dd/MM/yyyy")
            + "\nCliente: " + cliente.Nombre + ", " +cliente.Apellido + "\nAnimal: " + animal.GetType() + ", " 
            + animal.Nombre + "\nServicio: " + servicio + "\nPrecio: $" + precio;
    }

}