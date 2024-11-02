using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Factura
{
    public static int ultimoNumeroFactura = 0;// en el futuro se va a guardar en una BDD o archivo.txt

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

    public Factura()
    {
        nroFactura = ultimoNumeroFactura++;// incrementa en uno el ultimo numero de factura y lo asigna a nroFactura
        fecha = DateTime.Now;// guarda la fecha y hora actual de cuando se creo la factura.
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

    public void CalcularPrecio(Servicios servicio)//metodo temporal ya que cuando se implemente un diccionario ya se van a asignar desde ahi
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

    public void CambiarPrecioProducto() {
        // cuando implemente mapas o diccionarios voy a hacer este metodo
        }


    public override string ToString()
    {
        return "---Factura---" + "\n Numero de Factura: " + nroFactura + "\n Fecha de factura: " + fecha.ToString("dd/MM/yyyy")
            + "\n Cliente: " + cliente.Nombre + ", " +cliente.Apellido + "\n Animal: " + animal.GetType() + ", " 
            + animal.Nombre + "\n Servicio: " + servicio + "\n Precio: " + precio;
    }

}
