using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Factura
{
    private int nroFactura;
    private DateTime fecha;
    private Cliente? cliente; //permiten null temporalmente
    private Animal? animal;
    private Servicios servicio;
    private double precio;

    public enum Servicios
    {
        Revision,
        Cirujia,
        ControlCompleto,
        Vacunacion,
        Laboratorio,
        NoDefinido
    }

    public Factura(int nroFactura, Cliente cliente, Animal animal, Servicios servicio)
    {
        this.nroFactura = nroFactura;
        fecha = DateTime.Now; // guarda la fecha y hora actual de cuando se creo la factura.
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

    public override string ToString()
    {
        return "---Factura---" + "\n Numero de Factura: " + nroFactura + "\n Fecha de factura: " + fecha.ToString("dd/MM/yyyy")
            + "\n Cliente: " + cliente.Nombre + ", " +cliente.Apellido + "\n Animal: " + animal.GetType() + ", " 
            + animal.Nombre + "\n Servicio: " + servicio + "\n Precio: " + precio;
    }

    public void CalcularPrecio(Servicios servicio)
    {
        switch (servicio)
        {
            case Servicios.Revision:
                precio = 2000.00;
            break;

            case Servicios.Cirujia:
                precio = 10000.00;
            break;

            case Servicios.ControlCompleto:
                precio = 6000.00;
            break;

            case Servicios.Vacunacion:
                precio = 4000.00;
            break;

            case Servicios.Laboratorio:
                precio = 5000.00;
            break;
        }         
    }



}
