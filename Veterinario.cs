using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Factura;

public class Veterinario : Persona, IRegistrosVeterinario, IFacturacion, IConsultarInfo, IModificarInfo, IHistorialAnimal
{

    private string usuario;

    private string contraseña; // despues la vamos a hashear en el futuro.

    private static Dictionary<int, Cliente> clientes;
    private static Dictionary<int, Factura> facturas;

    //Constructor
    public Veterinario() : base("Desconocido", "Desconocido", 0) 
    {
        this.Usuario = "Desconocido";
        this.Contraseña = "Desconocida";
        clientes = new Dictionary<int, Cliente>();
        facturas = new Dictionary<int, Factura>();
    }

    public Veterinario(string usuario, string contraseña, string nombre, string apellido, long numTelefono) : base(nombre, apellido, numTelefono)
    {
        this.usuario = usuario;
        this.contraseña = contraseña;
        clientes = new Dictionary<int, Cliente>();
        facturas = new Dictionary<int, Factura>();
    }

    //Getters y Setters
    public string Usuario
    {
        get { return usuario; }
        set { this.usuario = value; }
    }

    public string Contraseña
    {
        get { return contraseña; }
        set { contraseña = value; }
    }

    //Metodos para el manejo de datos JSON de clientes y facturas
    public void CargarClientesPrograma(string path) 
    {
        if (File.Exists(path))
        {
            try
            {
                Dictionary<int, Cliente> diccionario = Program.archivoJSON.LeerJsonDiccionarioConAnimales<Dictionary<int, Cliente>>(path);

                clientes = diccionario;
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public void CargarFacturasPrograma(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                Dictionary<int, Factura> diccionario = Program.archivoJSON.LeerJsonDiccionarioConAnimales<Dictionary<int, Factura>>(path);
                facturas = diccionario;
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }


    //Metodos de Registro y Eliminacion.
    public string RegistrarCliente(string path) //metodo de la interfaz IRegistrosVeterinario
    {
        Cliente cliente = null;

        // variable para el do-while
        bool boolRegistroCliente = false;
        do
        {
            try
            {
                Console.WriteLine("---Registrar Cliente---");

                // cargar datos del nombre del cliente
                Console.Write("Ingresa el nombre del cliente: ");
                string nombreTemp = Console.ReadLine().Trim();
                if (nombreTemp.Length < 2)
                {
                    throw new CamposCreacionIncorrectosException("El nombre debe tener 2 o mas letras\n");
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(nombreTemp, @"^[a-zA-Z\s]+$"))
                {
                    throw new CamposCreacionIncorrectosException("El nombre debe contener solo letras\n");
                }

                // cargar datos del apellido del cliente
                Console.Write("Ingresa el apellido del cliente: ");
                string apellidoTemp = Console.ReadLine().Trim();
                if (apellidoTemp.Length < 2)
                {
                    throw new CamposCreacionIncorrectosException("El apellido debe tener 2 o mas letras\n");
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(apellidoTemp, @"^[a-zA-Z\s]+$"))// regex para que permita solo letras y espacios)
                {
                    throw new CamposCreacionIncorrectosException("El apellido debe contener solo letras\n");
                }

                // cargar datos del DNI del cliente
                Console.Write("Ingresa el DNI del cliente: ");
                string dniTemp = Console.ReadLine().Trim();
                int dniNumerico;
                if (dniTemp.Length != 8)
                {
                    throw new CamposCreacionIncorrectosException("El DNI debe tener 8 numeros\n");
                }
                else if (!int.TryParse(dniTemp, out dniNumerico))
                {
                    throw new CamposCreacionIncorrectosException("El DNI debe contener unicamente numeros\n");
                }

                // cargar datos del telefono del cliente
                Console.Write("Ingresa el numero de telefono del cliente: ");
                string nroTemp = Console.ReadLine().Trim();
                long nroTempNumerico;
                if (nroTemp.Length < 6)
                {
                    throw new CamposCreacionIncorrectosException("El numero de telefono debe tener 6 o mas caracteres\n");
                }
                else if (!long.TryParse(nroTemp, out nroTempNumerico))
                {
                    throw new CamposCreacionIncorrectosException("Tu numero de telefono debe contener solo numeros\n");
                }

                //creacion del cliente
                if (clientes.ContainsKey(dniNumerico))
                {
                    return "\nEl cliente ya esta registrado\n";
                }
                else
                {
                    cliente = new Cliente(nombreTemp, apellidoTemp, dniNumerico, nroTempNumerico);
                    clientes.Add(cliente.DNI, cliente);
                    try
                    {
                        Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    boolRegistroCliente = true;
                    Console.WriteLine();
                }
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolRegistroCliente);   
        
        return "Cliente registrado con exito \n" + cliente.ToString() + "\n";
    }

    public string RegistrarAnimal(string path) //metodo de la interfaz IRegistrosVeterinario
    {
        Animal animal = null;
        int opcionNumerica = 0;
        // variable para el do-while
        bool boolRegistroAnimal = false;
        do
        {
        Console.WriteLine("---Registrar Animal--");
        Console.WriteLine("Selecciona el tipo de animal: ");
        Console.WriteLine("1- Perro");
        Console.WriteLine("2- Gato");
        Console.WriteLine("3- Ave");
        Console.WriteLine("4- Roedor");
        Console.WriteLine("5- Volver");
            try
            {
                // selecciona la opcion del animal a crear
                Console.Write("Coloca tu opcion en numero aqui: ");
                string opcion = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(opcion) || !int.TryParse(opcion, out opcionNumerica))
                {
                    throw new SeleccionarOpcionException();
                }
                Console.WriteLine();
                switch (opcionNumerica)
                {
                    case 1:
                        animal = RegistroPerro(path);
                        boolRegistroAnimal = true;
                        break;
                    case 2:
                        animal = RegistroGato(path);
                        boolRegistroAnimal = true;
                        break;
                    case 3:
                        animal = RegistroAve(path);
                        boolRegistroAnimal = true;
                        break;
                    case 4:
                        animal = RegistroRoedor(path);
                        boolRegistroAnimal = true;
                        break;
                    case 5:
                        boolRegistroAnimal = true;
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Coloca una opcion valida!\n");
                        break;
                }
            }
            catch (SeleccionarOpcionException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (CamposCreacionIncorrectosException e) // agregamos esta excepcion por las dudas 
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolRegistroAnimal);
        if(animal == null)
        {
            return "Volviendo...\n";
        }
        return animal.GetType() + " registrad@ con exito \n\n" + animal.ToString() + "\n";
    }

    public string EliminarAnimal(string path) //metodo de la interfaz IRegistrosVeterinario
    {
        Cliente cliente = null;
        Animal animal = null;
        bool boolEliminarAnimal = false;
        do
        {
            try
            {
                if (clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema, por lo tanto tampoco hay animales\n";
                }
                Console.WriteLine("---Eliminar animal---");
                cliente = validarCliente(cliente);
                
                if (cliente == null)
                {
                    return "Volviendo...\n";
                }
                else
                {
                    Console.WriteLine();
                    if (cliente.Mascotas.Count == 0)
                    {
                        return "Este cliente no tiene animales registrados en el sistema\n";
                    }
                    else
                    {
                        animal = validarAnimal(cliente, animal);
                        int indice = cliente.Mascotas.IndexOf(animal);
                        cliente.Mascotas.RemoveAt(indice);
                        animal = null;
                        try
                        {
                            Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                        }
                        catch (NotSupportedException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        boolEliminarAnimal = true;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("\nFallo la eliminacion del animal, intentalo de nuevo\n");
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }

        } while (!boolEliminarAnimal);
        return "\nAnimal eliminado con exito!\n";
    }

    public string EliminarCliente(string path) //metodo de la interfaz IRegistrosVeterinario
    {
        Cliente cliente = null;
        bool boolEliminarCliente = false;
        do
        {
            try
            {
                if(clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema\n";
                }
                else
                {
                    Console.WriteLine("---Eliminar Cliente---");
                    cliente = validarCliente(cliente);

                    if (cliente == null)
                    {
                        return "Volviendo...\n";
                    }
                    else
                    {
                        Console.WriteLine();
                        int dni = cliente.DNI;
                        clientes.Remove(dni);
                        cliente = null;
                        try
                        {
                            Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                        }
                        catch (NotSupportedException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        boolEliminarCliente = true;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolEliminarCliente);
       return "Cliente eliminado con exito!\n";
    }

    //Metodos para Facturacion
    public string CrearFactura(string path) //metodo de la interfaz IFacturacion
    {
        // variable para el do-while
        bool boolCrearFactura = false;
        //variables para la factura
        Factura factura = null;      
        Cliente cliente = null;
        Animal animal = null;
        Factura.Servicios servicio = Factura.Servicios.NoDefinido;
        do
        {
            try
            {

                Console.WriteLine("---Nueva Factura---");
                //incrementamos en 1 la variable estatica de factura
                ultimoNumeroFactura++;
                Console.WriteLine("Numero de factura: " + Factura.ultimoNumeroFactura); //imprime el numero de factura 

                // permitimos que ingrese el cliente por su dni
                cliente = validarCliente(cliente);
                if (cliente == null) // si cliente es null retorna el volviendo
                {
                    ultimoNumeroFactura--;
                    return "Volviendo...\n";
                }

                if (cliente.Mascotas.Count == 0) //si el cliente no tiene animales registrados larga este return
                {
                    ultimoNumeroFactura--;
                    return "\nEste cliente no tiene animales registrados en el sistema\n";
                }
                else // si el cliente tiene animales registrados ejecuta este bloque
                {
                    Console.WriteLine();
                    animal = validarAnimal(cliente, animal); // asigna el animal de la lista del cliente a la variable animal para crear una factura
                    Console.WriteLine();
                }

                // permite elegir el servicio
                do
                {
                    try
                    {
                        Console.WriteLine("Selecciona el servicio realizado");
                        Console.WriteLine("1: Revision ");
                        Console.WriteLine("2: Cirujia ");
                        Console.WriteLine("3: Control Completo ");
                        Console.WriteLine("4: Vacunacion ");
                        Console.WriteLine("5: Laboratorio ");
                        Console.Write("Ingresa la opcion(en numero) aqui: ");
                        string opcion = Console.ReadLine().Trim();
                        int opcionNumerica;
                        if (!int.TryParse(opcion, out opcionNumerica))
                        {
                            throw new SeleccionarOpcionException();
                        }
                        Console.WriteLine();
                        switch (opcionNumerica)
                        {
                            case 1:
                                servicio = Factura.Servicios.Revision;
                                break;
                            case 2:
                                servicio = Factura.Servicios.Cirujia;
                                break;
                            case 3:
                                servicio = Factura.Servicios.ControlCompleto;
                                break;
                            case 4:
                                servicio = Factura.Servicios.Vacunacion;
                                break;
                            case 5:
                                servicio = Factura.Servicios.Laboratorio;
                                break;
                            default:
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                break;
                        }
                    }
                    catch (SeleccionarOpcionException e)
                    {
                        Console.WriteLine("\nError: " + e.Message);
                    }
                    catch (Exception)// por si se produce un fallo inesperado
                    {
                        Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
                    }
                } while (servicio == Factura.Servicios.NoDefinido); // cuando se le asigne un valor que no sea el NoDefinido a servicio sale del bucle

                //si alguna de estas condiciones es true, larga la excepcion, esta la coloco por cualquier falla que pueda tener.
                if (cliente == null || animal == null || servicio == Factura.Servicios.NoDefinido)
                {
                    throw new InvalidOperationException("Hay datos invalidos en la factura, intentalo de nuevo\n");
                }
                else// si no crea la factura y sale del bucle
                {
                    factura = new Factura(cliente, animal, servicio);
                    facturas.Add(factura.NroFactura, factura);
                    Program.archivoJSON.EscribirJsonDiccionarioFacturas<Factura>(path, facturas);
                    boolCrearFactura = true;
                }
            }   
            catch(InvalidOperationException e)
            {
                Console.WriteLine("\nError: " + e.Message);
                ultimoNumeroFactura--;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
                ultimoNumeroFactura--;
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
                ultimoNumeroFactura--;
            }
        } while (!boolCrearFactura);

        //retorna el mensaje e imprime la factura
        return "Factura realizada con exito!\n\n" + factura.ToString() + "\n" ;
    }

    public string EliminarFactura(string path)
    {
        Factura factura = null;
        bool boolEliminarFactura = false;
        do
        {
            try
            {
                if (facturas.Count == 0)
                {
                    return "No hay facturas registradas en el sistema\n";
                }
                else
                {
                    Console.WriteLine("---Eliminar factura---");
                    factura = validarFactura(factura);

                    if (factura == null)
                    {
                        return "Volviendo...\n";
                    }
                    else
                    {
                        Console.WriteLine();
                        int numeroFact = factura.NroFactura;
                        facturas.Remove(numeroFact);
                        factura = null;
                        Program.archivoJSON.EscribirJsonDiccionarioFacturas<Factura>(path, facturas);
                        boolEliminarFactura = true;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }

        } while (!boolEliminarFactura);

        return "Factura eliminada con exito!\n";
    }

    public string ImprimirFactura() //metodo de la interfaz IFacturacion
    {
        Factura factura = null;
        bool boolImprimirFact = false;
        do
        {
            try
            {
                if (facturas.Count == 0)
                {
                    return "No hay facturas registradas en el sistema\n";
                }
                else
                {
                    factura = validarFactura(factura);
                    if (factura == null)
                    {
                        return "Volviendo...\n";
                    }
                    else
                    {
                        Console.WriteLine();
                        boolImprimirFact = true;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolImprimirFact);
        return factura.ToString() + "\n";
    }

    public void MostrarFacturas() //metodo de la interfaz IFacturacion
    {
        if (facturas.Count == 0)
        {
            Console.WriteLine("No hay facturas registradas en el sistema\n");
        }
        else
        {
            var facturasOrdenadas = facturas.OrderByDescending(entry => entry.Value.Fecha);

            Console.WriteLine("---Facturas Registradas---");
            foreach (KeyValuePair<int, Factura> entry in facturasOrdenadas)
            {
                Factura factura = entry.Value;
                Console.WriteLine("Numero de factura: " + factura.NroFactura + ", Fecha: " + factura.Fecha + ", Cliente: " + factura.Cliente.Apellido + ", " + factura.Cliente.Nombre);
            }
            Console.WriteLine();
        }
    }

    public string CambiarPrecioServicio() //metodo de la interfaz IFacturacion
    {
        bool boolCambiarPrecioServicio = false;
        int opcionNumerica;
        double precioNuevoNumerico;
        string precio;
        do
        {
            try
            {
                Console.WriteLine("---Cambiar Precio Servicios---");
                Console.WriteLine("1-Revision");
                Console.WriteLine("2-Cirujia");
                Console.WriteLine("3-Control Completo");
                Console.WriteLine("4-Vacunacion");
                Console.WriteLine("5-Laboratorio");
                Console.WriteLine("6-Volver");
                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                string opcion = Console.ReadLine();
                if (!int.TryParse(opcion, out opcionNumerica))
                {
                    throw new SeleccionarOpcionException();
                }
                switch (opcionNumerica)
                {
                    case 1:
                        Console.Write("Coloca el precio nuevo para [REVISION]: ");
                        precio = Console.ReadLine().Trim();
                        if (!double.TryParse(precio, out precioNuevoNumerico))
                        {
                            throw new ArgumentException("El precio nuevo debe ser numerico\n");
                        }
                        else if(precioNuevoNumerico <= 0)
                        {
                            throw new ArgumentException("El precio nuevo debe ser mayor a 0\n");
                        }
                        else if (precioNuevoNumerico == Factura.PreciosServicios[Servicios.Revision])
                        {
                            throw new ArgumentException("El precio nuevo no puede ser el mismo\n");
                        }
                        else
                        {
                            Factura.PreciosServicios[Servicios.Revision] = precioNuevoNumerico;
                            Console.WriteLine("\nEl precio del servicio Revision ahora es : $" + precioNuevoNumerico + "\n");
                        }
                        break;
                    case 2:
                        Console.Write("Coloca el precio nuevo para [Cirujia]: ");
                        precio = Console.ReadLine().Trim();
                        if (!double.TryParse(precio, out precioNuevoNumerico))
                        {
                            throw new ArgumentException("El precio nuevo debe ser numerico\n");
                        }
                        else if (precioNuevoNumerico <= 0)
                        {
                            throw new ArgumentException("El precio nuevo debe ser mayor a 0\n");
                        }
                        else if (precioNuevoNumerico == Factura.PreciosServicios[Servicios.Cirujia])
                        {
                            throw new ArgumentException("El precio nuevo no puede ser el mismo\n");
                        }
                        else
                        {
                            Factura.PreciosServicios[Servicios.Cirujia] = precioNuevoNumerico;
                            Console.WriteLine("\nEl precio del servicio Cirujia ahora es : $" + precioNuevoNumerico + "\n");
                        }
                        break;
                    case 3:
                        Console.Write("Coloca el precio nuevo para [Control Completo]: ");
                        precio = Console.ReadLine().Trim();
                        if (!double.TryParse(precio, out precioNuevoNumerico))
                        {
                            throw new ArgumentException("El precio nuevo debe ser numerico\n");
                        }
                        else if (precioNuevoNumerico <= 0)
                        {
                            throw new ArgumentException("El precio nuevo debe ser mayor a 0\n");
                        }
                        else if (precioNuevoNumerico == Factura.PreciosServicios[Servicios.ControlCompleto])
                        {
                            throw new ArgumentException("El precio nuevo no puede ser el mismo\n");
                        }
                        else
                        {   
                            Factura.PreciosServicios[Servicios.ControlCompleto] = precioNuevoNumerico;
                            Console.WriteLine("\nEl precio del servicio Control Completo ahora es : $" + precioNuevoNumerico + "\n");
                        }
                        break;
                    case 4:
                        Console.Write("Coloca el precio nuevo para [Vacunacion]: ");
                        precio = Console.ReadLine().Trim();
                        if (!double.TryParse(precio, out precioNuevoNumerico))
                        {
                            throw new ArgumentException("El precio nuevo debe ser numerico\n");
                        }
                        else if (precioNuevoNumerico <= 0)
                        {
                            throw new ArgumentException("El precio nuevo debe ser mayor a 0\n");
                        }
                        else if (precioNuevoNumerico == Factura.PreciosServicios[Servicios.Vacunacion])
                        {
                            throw new ArgumentException("El precio nuevo no puede ser el mismo\n");
                        }
                        else
                        {
                            Factura.PreciosServicios[Servicios.Vacunacion] = precioNuevoNumerico;
                            Console.WriteLine("\nEl precio del servicio Vacunacion ahora es : $" + precioNuevoNumerico + "\n");
                        }
                        break;
                    case 5:
                        Console.Write("Coloca el precio nuevo para [Laboratorio]: ");
                        precio = Console.ReadLine().Trim();
                        if (!double.TryParse(precio, out precioNuevoNumerico))
                        {
                            throw new ArgumentException("El precio nuevo debe ser numerico\n");
                        }
                        else if (precioNuevoNumerico <= 0)
                        {
                            throw new ArgumentException("El precio nuevo debe ser mayor a 0\n");
                        }
                        else if (precioNuevoNumerico == Factura.PreciosServicios[Servicios.Laboratorio])
                        {
                            throw new ArgumentException("El precio nuevo no puede ser el mismo\n");
                        }
                        else
                        {
                            Factura.PreciosServicios[Servicios.Laboratorio] = precioNuevoNumerico;
                            Console.WriteLine("\nEl precio del servicio Laboratorio ahora es : $" + precioNuevoNumerico + "\n");
                        }
                        break;
                    case 6:
                        Console.WriteLine();
                        boolCambiarPrecioServicio = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida. Ingresa una opcion valida!\n");
                        break;
                }
            }
            catch (SeleccionarOpcionException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolCambiarPrecioServicio);
        return "Volviendo...\n";
    }

    //Metodos para Consultar datos

    public void ConsultarClientes() // ordenados por DNI
    {

        if (clientes.Count == 0)
        {
            Console.WriteLine("No hay clientes registrados en el sistema\n");
        }
        else
        {
            var clientesOrdenados = clientes.OrderBy(entry => entry.Key);
            int contador = 1;

            Console.WriteLine("---Clientes Registrados---");
            foreach (KeyValuePair<int, Cliente> entry in clientesOrdenados)
            {
                Cliente cliente = entry.Value;
                Console.WriteLine("[ " + contador + " ] DNI: " + cliente.DNI + ", Nombre: " + cliente.Apellido + ", " + cliente.Nombre);
                contador++;
            }
            Console.WriteLine();
        }
        
    }

    public string ConsultarInfoCliente() //metodo de la interfaz IConsultarInfo
    {
        Cliente cliente = null;
        bool boolConsultarInfo = false;
        do
        {
            try
            {
                if (clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema\n";
                }
                else
                {
                    Console.WriteLine("---Consultar Informacion Cliente---");
                    cliente = validarCliente(cliente);

                }
                if (cliente == null)
                {
                    return "Volviendo...\n";
                }
                else
                {
                    Console.WriteLine();
                    bool consultarInfoInterno = false;
                    do
                    {
                        try
                        {
                            Console.WriteLine("---Que deseas consultar---");
                            Console.WriteLine("1-Su informacion");
                            Console.WriteLine("2-Sus animales");
                            Console.WriteLine("3-Volver");
                            Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                            string opcion = Console.ReadLine().Trim();
                            int opcionNum;
                            if (!int.TryParse(opcion, out opcionNum))
                            {
                                throw new SeleccionarOpcionException();
                            }
                            switch (opcionNum)
                            {
                                case 1:
                                    Console.WriteLine(cliente.ToString());
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    cliente.VerAnimales();
                                    break;
                                case 3:
                                    consultarInfoInterno = true;
                                    boolConsultarInfo = true;
                                    break;
                                default:
                                    Console.WriteLine("\nOpcion valida. Ingresa una opcion valida!\n");
                                    break;
                            }
                        }
                        catch (SeleccionarOpcionException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                        }
                    } while (!consultarInfoInterno);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolConsultarInfo);
        Console.WriteLine();
        return "Volviendo...\n";
    }

    public string ConsultarInfoAnimal() //metodo de la interfaz IConsultarInfo
    {
        bool boolConsultarInfo = false;
        Cliente cliente = null;
        Animal animal = null;
        do
        {
            try
            {
                if (clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema, por lo tanto tampoco hay animales\n";
                }
                else
                {
                    Console.WriteLine("---Consultar Informacion Animal---");
                    cliente = validarCliente(cliente);
                }
                if (cliente == null)
                {
                    return "Volviendo...\n";
                }
                else
                {
                    Console.WriteLine();
                    if (cliente.Mascotas.Count == 0)
                    {
                        return "Este cliente no tiene animales registrados en el sistema\n";
                    }
                    else
                    {
                        animal = validarAnimal(cliente, animal);
                        bool boolConsInfoAnimal = false;
                        Console.WriteLine();
                        do
                        {
                            try
                            {
                                Console.WriteLine("---Que deseas consultar---");
                                Console.WriteLine("1-Su informacion detallada");
                                Console.WriteLine("2-Su Historial");
                                Console.WriteLine("3-Volver");
                                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                                string opcion = Console.ReadLine().Trim();
                                int opcionNum;
                                Console.WriteLine();
                                if (!int.TryParse(opcion, out opcionNum))
                                {
                                    throw new SeleccionarOpcionException();
                                }
                                switch (opcionNum)
                                {
                                    case 1:
                                        Console.WriteLine(animal.ToString());
                                        Console.WriteLine();
                                        break;
                                    case 2:
                                        animal.verHistorial();
                                        break;
                                    case 3:
                                        boolConsultarInfo = true;
                                        boolConsInfoAnimal = true;
                                        break;
                                    default:
                                        Console.WriteLine("Opcion valida. Ingresa una opcion valida!\n");
                                        break;
                                }
                            }
                            catch (SeleccionarOpcionException e)
                            {
                                Console.WriteLine("Error: " + e.Message);
                            }
                        } while (!boolConsInfoAnimal);                 
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolConsultarInfo);
        return "Volviendo...\n";
    }

    //Metodos de modificacion de informacion
    public string ModificarInfoCliente(string path) //metodo de la interfaz IModificarInfo
    {
        Cliente cliente = null;
        bool boolModificarInfoCliente = false;
        bool boolMICI;
        do
        {
            try
            {
                if (clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema\n";
                }
                else
                {
                    Console.WriteLine("---Modificar Datos Clientes---");
                    cliente = validarCliente(cliente);
                }
                if (cliente == null)
                {
                    return "Volviendo...\n";
                }
                else
                {
                    Console.WriteLine();
                    ModificarInfoClienteInterno(cliente, path);
                    boolModificarInfoCliente = true; 
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolModificarInfoCliente);
        return "Volviendo...\n";
    }

    public string ModificarInfoAnimal(string path)  //metodo de la interfaz IModificarInfo
    {
        Cliente cliente = null;
        Animal animal = null;
        bool boolModificarInfoAnimal = false;
        do
        {
            try
            {
                if (clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema, por lo tanto tampoco hay animales\n";
                }
                Console.WriteLine("---Modificar Informacion Animal---");
                cliente = validarCliente(cliente);

                if (cliente == null)
                {
                    return "Volviendo...\n";
                }
                else
                {
                    Console.WriteLine();
                    if (cliente.Mascotas.Count == 0)
                    {
                        return "Este cliente no tiene animales registrados en el sistema\n";
                    }
                    else
                    {
                        animal = validarAnimal(cliente, animal);
                        Console.WriteLine();
                        ModificarInfoAnimalInterno(cliente, animal, path);
                        boolModificarInfoAnimal = true;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("\nFallo la eliminacion del animal, intentalo de nuevo\n");
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }

        } while (!boolModificarInfoAnimal);
        return "Volviendo...\n";
    }

    //Metodo para el historial de los animales
    public string ActualizarHistorial(string path) //metodo de la interfaz IHistorialAnimal
    {
        Cliente cliente = null;
        Animal animal = null;
        bool boolActualizarHistorial = false;
        int opcionMenuNum;
        bool boolAH = false;
        string mensaje = "";

        do
        {
            try
            {
                if (clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema, por lo tanto tampoco hay animales\n";
                }
                cliente = validarCliente(cliente);
                if (cliente == null)
                {
                    return "Volviendo...\n";
                }
                else
                {
                    Console.WriteLine();
                    if (cliente.Mascotas.Count == 0)
                    {
                        return "Este cliente no tiene animales registrados en el sistema\n";
                    }
                    else
                    {
                        // cargar el animal del cliente para la consulta
                        animal = validarAnimal(cliente, animal);
                        Console.WriteLine();
                    }
                }
                
                do
                {
                    try
                    {
                        Console.WriteLine("---Actualizar Historial de un animal---");
                        Console.WriteLine("1-Agregar consulta");
                        Console.WriteLine("2-Eliminar consulta");
                        Console.WriteLine("3-Vaciar historial");
                        Console.WriteLine("4-Volver");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        string opcionMenu = Console.ReadLine().Trim();
                        Console.WriteLine();
                        if (!int.TryParse(opcionMenu, out opcionMenuNum))
                        {
                            throw new SeleccionarOpcionException();
                        }
                        switch (opcionMenuNum)
                        {
                            case 1:
                                mensaje = AgregarConsulta(cliente, animal, path);
                                Console.WriteLine();
                                break;
                            case 2:
                                mensaje = EliminarConsulta(cliente, animal, path);
                                break;
                            case 3:
                                mensaje = VaciarHistorial(cliente, animal, path);
                                break;
                            case 4:
                                boolAH = true;
                                boolActualizarHistorial = true;
                                break;
                            default:
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                break;
                        }
                        if (!string.IsNullOrEmpty(mensaje)) 
                        {
                            Console.WriteLine(mensaje);
                        }
                        
                    }
                    catch (SeleccionarOpcionException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                    catch (Exception)// por si se produce un fallo inesperado
                    {
                        Console.WriteLine("Se produjo un error, intentalo de nuevo\n");
                    }
                } while (!boolAH);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolActualizarHistorial);
        return "Volviendo...\n";
    }

    //Metodo para las vacunas de perros y gatos
    public string ModificarVacunas(string path)
    {
        bool boolModificarInformacion = false;
        int opcionNum;
        Animal animal = null;
        Cliente cliente = null;
        object animalCasteado = null;
        do
        {
            try
            {
                if (clientes.Count == 0)
                {
                    return "No hay clientes registrados en el sistema, por lo tanto tampoco hay animales\n";
                }
                Console.WriteLine("---Modificar Informacion Animal---");
                cliente = validarCliente(cliente);

                if (cliente == null)
                {
                    return "Volviendo...\n";
                }
                else
                {
                    Console.WriteLine();
                    if (cliente.Mascotas.Count == 0)
                    {
                        return "Este cliente no tiene animales registrados en el sistema\n";
                    }
                    else
                    {
                        animal = validarAnimal(cliente, animal);
                        Console.WriteLine();
                    }
                    if (animal is Perro)
                    {
                        animalCasteado = (Perro)animal;
                    }else if (animal is Gato)
                    {
                        animalCasteado = (Gato)animal;
                    }
                    if(animalCasteado is Perro || animalCasteado is Gato)
                    {
                        bool boolVacunasAnimal = false;
                        do
                        {
                            try
                            {
                                Console.WriteLine("---Modificar Historial de Vacunas---");
                                Console.WriteLine("1-Ver vacunas");
                                Console.WriteLine("2-Agregar vacuna");
                                Console.WriteLine("3-Eliminar vacuna");
                                Console.WriteLine("4-Modificar vacuna");
                                Console.WriteLine("5-Volver");
                                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                                string opcion = Console.ReadLine().Trim();
                                Console.WriteLine();
                                if (!int.TryParse(opcion, out opcionNum))
                                {
                                    throw new SeleccionarOpcionException();
                                }
                                switch (opcionNum)
                                {
                                    case 1:
                                        VerVacunas(animalCasteado);
                                        break;
                                    case 2:
                                        AgregarVacuna(animalCasteado, path);
                                        break;
                                    case 3:
                                        EliminarVacuna(animalCasteado, path);
                                        break;
                                    case 4:
                                        ModificarVacuna(animalCasteado, path);
                                        break;
                                    case 5:
                                        boolVacunasAnimal = true;
                                        boolModificarInformacion = true;
                                        break;
                                    default:
                                        Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                        break;
                                }
                            }
                            catch (SeleccionarOpcionException e)
                            {
                                Console.WriteLine("Error: " + e.Message);
                            }
                        } while (!boolVacunasAnimal);
                    }
                    else
                    {
                        Console.WriteLine("No se puede aplicar vacunas a este animal, por lo tanto no tiene historial de las mismas\n");
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolModificarInformacion);
        return "Volviendo...\n";
    }

    //Metodos de veterinario para su objeto propio
    public string ModificarInformacion(Veterinario veterinario, string path)
    {
        int opcionMINumerica = 0;
        do
        {
            try
            {
                Console.WriteLine("---Selecciona la informacion que quieres modificar---");
                Console.WriteLine("1-Nombre de usuario");
                Console.WriteLine("2-Contraseña");
                Console.WriteLine("3-Nombre"); // estas opciones junto al apellido las ponemos por si por ahi escribio mal el nombre y en el futuro lo quiere cambiar
                Console.WriteLine("4-Apellido");
                Console.WriteLine("5-Numero de telefono");
                Console.WriteLine("6-Volver");
                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                string opcionMI = Console.ReadLine().Trim();
                if (!int.TryParse(opcionMI, out opcionMINumerica))
                {
                    throw new SeleccionarOpcionException();
                }
                Console.WriteLine();
                switch (opcionMINumerica)
                {
                    case 1:
                        Console.Write("Coloca tu nuevo nombre de usuario: ");
                        string nombUsuTemp = Console.ReadLine().Trim();
                        if (nombUsuTemp == veterinario.usuario)
                        {
                            throw new ArgumentException("No podes poner el mismo nombre de usuario\n");
                        }
                        else if (nombUsuTemp.Length < 4)
                        {
                            throw new ArgumentException("El nombre de usuario debe tener al menos 4 caracteres\n");
                        }
                        else if (Program.veterinarios.ContainsKey(nombUsuTemp))
                        {
                            throw new ArgumentException("El nombre de usuario " + nombUsuTemp + " ya esta en uso\n");
                        }
                        else
                        {
                            //modificamos la clave del diccionario para acceder al veterinario
                            Veterinario valor = Program.veterinarios[veterinario.Usuario];

                            // agrega una nueva clave con el mismo valor
                            Program.veterinarios.Add(nombUsuTemp, valor);

                            // elimina la key antigua
                            Program.veterinarios.Remove(veterinario.Usuario);

                            //modifica el usuario del veterinario en el objeto
                            veterinario.Usuario = nombUsuTemp;
                            Console.WriteLine("Tu nombre de usuario ahora es: " + veterinario.Usuario + "\n");
                        }
                        break;
                    case 2:
                        Console.Write("Coloca tu nueva contraseña: ");
                        string contTemp = Console.ReadLine().Trim();
                        if (contTemp == veterinario.contraseña)
                        {
                            throw new ArgumentException("No podes poner la misma contraseña\n");
                        }
                        else if (contTemp.Length < 6)
                        {
                            throw new ArgumentException("La contraseña debe tener 6 o mas caracteres\n");
                        }
                        else
                        {
                            veterinario.Contraseña = contTemp;
                            Console.WriteLine("Tu contraseña ahora es: " + veterinario.Contraseña + "\n");
                        }
                        break;
                    case 3:
                        Console.Write("Coloca tu nuevo nombre: ");
                        string nombTemp = Console.ReadLine().Trim();
                        if (nombTemp == veterinario.nombre)
                        {
                            throw new ArgumentException("No podes poner el mismo nombre\n");
                        }
                        else if (nombTemp.Length < 2)
                        {
                            throw new ArgumentException("Tu nombre debe tener 2 o mas letras\n");
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(nombTemp, @"^[a-zA-Z\s]+$"))// regex para que permita solo letras y espacios
                        {
                            throw new ArgumentException("Tu nuevo nombre debe contener solo letras\n");
                        }
                        else
                        {
                            veterinario.Nombre = nombTemp;
                            Console.WriteLine("Tu nombre ahora es: " + veterinario.Nombre + "\n");
                        }
                        break;
                    case 4:
                        Console.Write("Coloca tu nuevo apellido: ");
                        string apeTemp = Console.ReadLine().Trim();
                        if (apeTemp == veterinario.apellido)
                        {
                            throw new ArgumentException("No podes poner el mismo apellido\n");
                        }
                        else if (apeTemp.Length < 2)
                        {
                            throw new ArgumentException("Tu apellido debe tener 2 o mas letras\n");
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(apeTemp, @"^[a-zA-Z\s]+$"))// regex para que permita solo letras y espacios
                        {
                            throw new ArgumentException("Tu nuevo apellido debe contener solo letras\n");
                        }
                        else
                        {
                            veterinario.Apellido = apeTemp;
                            Console.WriteLine("Tu apellido ahora es: " + veterinario.Apellido + "\n");
                        }
                        break;
                    case 5:
                        Console.Write("Coloca tu nuevo numero de telefono: ");
                        string numTelTemp = Console.ReadLine().Trim();
                        long numTelTempNum;
                        if (numTelTemp.Length < 6)
                        {
                            throw new ArgumentException("Tu numero de telefono debe tener 6 o mas numeros\n");
                        }
                        if (!long.TryParse(numTelTemp, out numTelTempNum))
                        {
                            throw new ArgumentException("Tu nuevo numero de telefono debe contener solo numeros\n");
                        }
                        else if (numTelTempNum == veterinario.NumTelefono)
                        {
                            throw new ArgumentException("No podes poner el mismo numero de telefono\n");
                        }
                        else
                        {
                            veterinario.NumTelefono = numTelTempNum;
                            Console.WriteLine("Tu numero de telefono ahora es: " + veterinario.NumTelefono + "\n");
                        }
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                        break;
                }
                try
                {
                    Program.archivoJSON.EscribirJsonDiccionarioVeterinarios<Veterinario>(path, Program.veterinarios);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (SeleccionarOpcionException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (opcionMINumerica != 6);
        return "Volviendo...\n";
    }

    //METODOS INTERNOS PARA LA REUTILIZACION Y MODULARIZACION DEL CODIGO
    //Utilizado en los metodos MetodoInternoRegistroAnimal, EliminarAnimal, EliminarCliente, CrearFactura, ConsultarInfoCliente, ConsultarInfoAnimal,
    //ModificarInfoCliente, ModificarInfoAnimal y ModificarInfoAnimalInterno
    private Cliente validarCliente(Cliente cliente)
    {
        Console.Write("Coloca el DNI del cliente: ");
        string dni = Console.ReadLine().Trim();
        int dniNum = 0;
        if (dni.Length != 8)
        {
            throw new ArgumentException("El DNI debe tener 8 numeros\n");
        }
        else if (!int.TryParse(dni, out dniNum))
        {
            throw new ArgumentException("El DNI debe contener unicamente numeros\n");
        }
        if (clientes.TryGetValue(dniNum, out Cliente value))
        {
            cliente = value; // asigna el cliente que obtiene de value a la variable cliente
        }
        else
        {
            int opcionIntNum = 0;

            // le avisa que no hay ningun cliente con ese dni y le da la opcion de volver a intentar o de cancelar la accion
            Console.WriteLine("\nNo hay ningun cliente registrado con el DNI: " + dni + "\n");
            do
            {
                try
                {
                    Console.WriteLine("---¿Que deseas hacer?---");
                    Console.WriteLine("1-Volver a intentar");
                    Console.WriteLine("2-Salir al menu");
                    Console.Write("Coloca tu opcion en numero aqui: ");
                    string opcionInt = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(opcionInt) || !int.TryParse(opcionInt, out opcionIntNum))
                    {
                        throw new SeleccionarOpcionException();
                    }
                    switch (opcionIntNum)
                    {
                        case 1:
                            if (opcionIntNum == 1)
                            {
                                throw new ArgumentException("No habia ningun cliente registrado con ese DNI, intenta de nuevo\n");
                            }
                            break;
                        case 2:
                            Console.WriteLine();
                            //si selecciona esta retorna al metodo que lo llamo donde verifica si es null algun valor, y si es asi retorna al menu del metodo
                            break;
                        default:
                            Console.WriteLine("\nOpcion no valida. Coloca una opcion valida!\n");
                            break;
                    }
                }
                catch (SeleccionarOpcionException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                }
            } while (opcionIntNum != 1 && opcionIntNum != 2);
        }
        return cliente;
    }

    //Utilizado por los metodos
    private Animal validarAnimal(Cliente cliente, Animal animal)
    {
        bool animalConsulta = false;
        do
        {
            try
            {
                cliente.VerAnimales();
                Console.Write("Coloca el indice del animal: ");
                string indice = Console.ReadLine().Trim();
                if (!int.TryParse(indice, out int indiceNum))
                {
                    throw new ArgumentException("El indice debe ser numerico\n");
                }
                else if (indiceNum <= 0)
                {
                    throw new ArgumentException("El indice debe ser mayor a 0\n");
                }
                indiceNum -= 1;
                if (indiceNum > cliente.Mascotas.Count - 1)
                {
                    throw new ArgumentException("El indice que proporcionaste no es valido\n");
                }
                else
                {
                    animal = cliente.Mascotas[indiceNum];
                    animalConsulta = true;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
        } while (!animalConsulta);
        return animal;
    }

    //Utilizados en el metodo registrarAnimal
    private Animal RegistroPerro(string path)
    {
        bool boolRegistroPerro = false;
        Perro animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

                if (!string.IsNullOrEmpty(nombreTemp) && cliente != null &&
                fecNacTemp != DateTime.MinValue && pesoTemp != 0 && sexoTemp != Animal.Genero.NoEspecificado)
                {
                    // cargamos el nombre de la raza del perro
                    Console.Write("Coloca la raza del perro: ");
                    string razaTemp = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(razaTemp))
                    {
                        throw new CamposCreacionIncorrectosException("La raza del perro no puede estar vacia\n");
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(razaTemp, @"^[a-zA-Z\s]+$"))// regex para que permita solo letras y espacios
                    {
                        throw new CamposCreacionIncorrectosException("La raza debe contener solo letras\n");
                    }

                    //creamos el perro y lo agregamos a la lista de mascotas del cliente
                    animalTemp = new Perro(nombreTemp, fecNacTemp, pesoTemp, sexoTemp, razaTemp);
                    cliente.Mascotas.Add(animalTemp);

                    // Guardar los cambios en el archivo
                    try
                    {
                        Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //try { Program.archivoJSON.EscribirJson(Program.archivoClientes, cliente); }
                    //catch (JsonException e) { Console.WriteLine(e.Message); }


                    boolRegistroPerro = true;
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolRegistroPerro);
        return animalTemp;
    }

    private Animal RegistroGato(string path)
    {
        bool boolRegistroGato = false;
        Gato animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

                if (!string.IsNullOrEmpty(nombreTemp) && cliente != null &&
                fecNacTemp != DateTime.MinValue && pesoTemp != 0 && sexoTemp != Animal.Genero.NoEspecificado)
                {
                    // cargamos el nombre de la raza del gato
                    Console.Write("Coloca la raza del gato: ");
                    string razaTemp = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(razaTemp))
                    {
                        throw new CamposCreacionIncorrectosException("La raza del gato no puede estar vacia\n");
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(razaTemp, @"^[a-zA-Z\s]+$"))
                    {
                        throw new CamposCreacionIncorrectosException("La raza debe contener solo letras\n");
                    }

                    //creamos el gato y lo agregamos a la lista de mascotas del cliente
                    animalTemp = new Gato(nombreTemp, fecNacTemp, pesoTemp, sexoTemp, razaTemp);
                    cliente.Mascotas.Add(animalTemp);

                    try
                    {
                        Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    boolRegistroGato = true;
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolRegistroGato);
        return animalTemp;
    }

    private Animal RegistroAve(string path)
    {
        int opcionNumericaEnums;
        bool boolRegistroAve = false;
        Ave animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

                if (!string.IsNullOrEmpty(nombreTemp) && cliente != null &&
                fecNacTemp != DateTime.MinValue && pesoTemp != 0 && sexoTemp != Animal.Genero.NoEspecificado)
                {
                    // cargamos la especie del ave mediante entrada por consola numerica de un ENUM
                    Console.WriteLine("Coloca la especie del ave: ");
                    Console.WriteLine("1- Canario");
                    Console.WriteLine("2- Loro");
                    Console.WriteLine("3- Cata");
                    Console.WriteLine("4- Silvetre");
                    Console.WriteLine("5- Callejero (Paloma, Gorrion, Tordo, etc.)");
                    string opcionEnum;
                    Ave.Variedad especieTemp = Ave.Variedad.NoEspecificado;
                    do
                    {
                        try
                        {
                            Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                            opcionEnum = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(opcionEnum) || !int.TryParse(opcionEnum, out opcionNumericaEnums))
                            {
                                throw new SeleccionarOpcionException();
                            }
                            Console.WriteLine();
                            switch (opcionNumericaEnums)
                            {
                                case 1:
                                    especieTemp = Ave.Variedad.Canario;
                                    break;
                                case 2:
                                    especieTemp = Ave.Variedad.Loro;
                                    break;
                                case 3:
                                    especieTemp = Ave.Variedad.Cata;
                                    break;
                                case 4:
                                    especieTemp = Ave.Variedad.Silvestre;
                                    break;
                                case 5:
                                    especieTemp = Ave.Variedad.Callejero;
                                    break;
                                default:
                                    Console.WriteLine("Opcion no valida. Coloca una opcion valida!\n");
                                    break;
                            }
                        }
                        catch (SeleccionarOpcionException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                        }
                    } while (especieTemp == Ave.Variedad.NoEspecificado);

                    //creamos el ave y la agregamos a la lista de mascotas del cliente
                    animalTemp = new Ave(nombreTemp, fecNacTemp, pesoTemp, sexoTemp, especieTemp);
                    cliente.Mascotas.Add(animalTemp);

                    try
                    {
                        Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    boolRegistroAve = true;
                }
                else
                {
                    break;
                }
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolRegistroAve);
        return animalTemp;
    }

    private Animal RegistroRoedor(string path)
    {
        bool boolRegistroRoedor = false;
        int opcionNumericaEnums;
        Roedor animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

                if (!string.IsNullOrEmpty(nombreTemp) && cliente != null &&
                fecNacTemp != DateTime.MinValue && pesoTemp != 0 && sexoTemp != Animal.Genero.NoEspecificado)
                {
                    // cargamos la especie del roedor mediante entrada por consola numerica de un ENUM
                    Console.WriteLine("Selecciona la especie del roedor");
                    Console.WriteLine("1- Hamster");
                    Console.WriteLine("2- Cobayo");
                    Console.WriteLine("3- Chinchilla");
                    Console.WriteLine("4- Rata");
                    string opcionEnum;
                    Roedor.Variedad especieTemp = Roedor.Variedad.NoEspecificado;
                    do
                    {
                        try
                        {
                            Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                            opcionEnum = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(opcionEnum) || !int.TryParse(opcionEnum, out opcionNumericaEnums))
                            {
                                throw new SeleccionarOpcionException();
                            }
                            Console.WriteLine();
                            switch (opcionNumericaEnums)
                            {
                                case 1:
                                    especieTemp = Roedor.Variedad.Hamster;
                                    break;
                                case 2:
                                    especieTemp = Roedor.Variedad.Cobayo;
                                    break;
                                case 3:
                                    especieTemp = Roedor.Variedad.Chinchilla;
                                    break;
                                case 4:
                                    especieTemp = Roedor.Variedad.Rata;
                                    break;
                                default:
                                    Console.WriteLine("Opcion no valida. Coloca una opcion valida!\n");
                                    break;
                            }
                        }
                        catch (SeleccionarOpcionException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                        }

                    } while (especieTemp == Roedor.Variedad.NoEspecificado);

                    //creamos el roedor y lo agregamos a la lista de mascotas del cliente
                    animalTemp = new Roedor(nombreTemp, fecNacTemp, pesoTemp, sexoTemp, especieTemp);
                    cliente.Mascotas.Add(animalTemp);

                    try
                    {
                        Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    boolRegistroRoedor = true;
                }
                else
                {
                    break;
                }
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolRegistroRoedor);
        return animalTemp;
    }

    //Utilizado dentro de cada metodo interno para registrar los 4 tipos de animales
    private void MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp)
    {
        // variable para el do-while y para la busqueda del cliente
        bool boolRegistroAnimalInt = false;

        // variables para que funcione el out de parametros, mas abajo se verifica si siguen en esta condicion
        nombreTemp = "";
        cliente = null;
        fecNacTemp = DateTime.MinValue;
        pesoTemp = 0;
        sexoTemp = Animal.Genero.NoEspecificado;
        do
        {
            try
            {
                Console.WriteLine("---Registrando Animal---");

                // carga el nombre del animal
                Console.Write("Coloca el nombre del animal: ");
                nombreTemp = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(nombreTemp))
                {
                    throw new CamposCreacionIncorrectosException("El nombre no puede estar vacio\n");
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(nombreTemp, @"^[a-zA-Z\s]+$"))
                {
                    throw new CamposCreacionIncorrectosException("El nombre debe contener solo letras\n");
                }

                // carga el dueño del animal

                cliente = validarCliente(cliente); // simplifico el codigo y la reutilizacion del mismo.
                if (cliente == null)
                {
                    return;
                }


                // carga la fecha de nacimiento del animal
                Console.Write("Coloca la fecha de nacimiento del animal (dd/mm/aaaa): ");
                string fecNacString;
                fecNacString = Console.ReadLine().Trim();
                if (fecNacString.Length != 10)
                {
                    throw new CamposCreacionIncorrectosException("La fecha de nacimiento debe tener 10 caracteres contando ambas barras (/)\n");
                }
                else if (!DateTime.TryParse(fecNacString, out fecNacTemp))
                {
                    throw new CamposCreacionIncorrectosException("Formato de fecha invalido: " + fecNacString + "-> DD/MM/YYYY\n");
                }

                // carga el peso del animal
                Console.Write("Coloca el peso del animal en Kg: ");
                string pesoString = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(pesoString))
                {
                    throw new CamposCreacionIncorrectosException("El peso del animal no puede estar vacio\n");
                }
                else if (!double.TryParse(pesoString, out pesoTemp) || pesoTemp <= 0)
                {
                    throw new CamposCreacionIncorrectosException("El peso del animal debe ser numerico y mayor a 0\n");
                }

                // carga el sexo del animal
                Console.WriteLine("Selecciona el sexo del animal: ");
                Console.WriteLine("1- Masculino");
                Console.WriteLine("2- Femenino");
                int opcionNumerica = 0;
                do
                {
                    try
                    {
                        Console.Write("Coloca tu opcion en numero aqui: ");
                        string opcion = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(opcion) || !int.TryParse(opcion, out opcionNumerica))
                        {
                            throw new SeleccionarOpcionException();
                        }
                        switch (opcionNumerica)
                        {
                            case 1:
                                sexoTemp = Animal.Genero.Masculino;
                                break;
                            case 2:
                                sexoTemp = Animal.Genero.Femenino;
                                break;
                            default:
                                Console.WriteLine("\nOpcion no valida. Coloca una opcion valida!\n");
                                break;
                        }

                    }
                    catch (SeleccionarOpcionException e)
                    {
                        Console.WriteLine("\nError: " + e.Message);
                    }
                    catch (Exception)// por si se produce un fallo inesperado
                    {
                        Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
                    }
                } while (opcionNumerica != 1 && opcionNumerica != 2);
                if (!string.IsNullOrEmpty(nombreTemp) || /*cliente == null esto es temporal ||*/
                    fecNacTemp != DateTime.MinValue || pesoTemp != 0 || sexoTemp != Animal.Genero.NoEspecificado)
                {
                    boolRegistroAnimalInt = true;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
        } while (!boolRegistroAnimalInt);
    }

    //Utilizado dentro los metodos EliminarFactura e ImprimirFactura
    private Factura validarFactura(Factura factura)
    {
        Console.Write("Coloca el numero de la factura: ");
        string nroFactura = Console.ReadLine();
        if (!int.TryParse(nroFactura, out int nroFacturaNum))
        {
            throw new ArgumentException("El numero de la factura debe contener unicamente numeros\n");
        }
        if (facturas.TryGetValue(nroFacturaNum, out Factura value))
        {
            factura = value; // asigna la factura que obtiene de value a la variable factura
        }
        else
        {
            int opcionIntNum = 0;

            // le avisa que no hay ninguna factura con ese numero y le da la opcion de volver a intentar o de cancelar la eliminacion.
            Console.WriteLine("\nNo hay ninguna factura registrada con el numero: " + nroFacturaNum + "\n");
            do
            {
                try
                {
                    Console.WriteLine("---¿Que deseas hacer?---");
                    Console.WriteLine("1-Volver a intentar");
                    Console.WriteLine("2-Salir al menu");
                    Console.Write("Coloca tu opcion en numero aqui: ");
                    string opcionInt = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(opcionInt) || !int.TryParse(opcionInt, out opcionIntNum))
                    {
                        throw new SeleccionarOpcionException();
                    }
                    switch (opcionIntNum)
                    {
                        case 1:
                            if (opcionIntNum == 1)
                            {
                                throw new ArgumentException("No habia ninguna factura registrado con ese numero, intenta de nuevo\n");
                            }
                            break;
                        case 2:
                            Console.WriteLine();
                            // si selecciona esta retorna al metodo que lo llamo donde verifica si es null algun valor, y si es asi retorna al menu del metodo
                            break;
                        default:
                            Console.WriteLine("\nOpcion no valida. Coloca una opcion valida!\n");
                            break;
                    }
                }
                catch (SeleccionarOpcionException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                }
            } while (opcionIntNum != 1 && opcionIntNum != 2);
        }
        return factura;
    }

    //Utilizado dentro del metodo ModificarInfoCliente
    private void ModificarInfoClienteInterno(Cliente cliente, string path)
    {
        bool boolMICI = false;
        int opcionNum;
        do
        {
            try
            {
                Console.WriteLine("---¿Que dato deseas modificar?---");
                Console.WriteLine("1-Su nombre");
                Console.WriteLine("2-Su apellido");
                Console.WriteLine("3-Su DNI");
                Console.WriteLine("4-Su numero de telefono");
                Console.WriteLine("5-Volver");
                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                string opcion = Console.ReadLine().Trim();
                Console.WriteLine();
                if (!int.TryParse(opcion, out opcionNum))
                {
                    throw new SeleccionarOpcionException();
                }
                switch (opcionNum)
                {
                    case 1:
                        Console.Write("Ingresa el nuevo nombre del cliente: ");
                        string nombreTemp = Console.ReadLine().Trim();
                        if (nombreTemp.Length < 2)
                        {
                            throw new ArgumentException("El nombre debe tener 2 o mas letras\n");
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(nombreTemp, @"^[a-zA-Z\s]+$"))// regex para que permita solo letras y espacios
                        {
                            throw new ArgumentException("El nombre debe contener solo letras\n");
                        }
                        else if (cliente.Nombre == nombreTemp)
                        {
                            throw new ArgumentException("No podes poner el mismo nombre: " + nombreTemp + "\n");
                        }
                        else
                        {
                            cliente.Nombre = nombreTemp;
                            Console.WriteLine("El nuevo nombre del cliente es: " + cliente.Nombre + "\n");
                        }
                        break;
                    case 2:
                        Console.Write("Ingresa el nuevo apellido del cliente: ");
                        string apellidoTemp = Console.ReadLine().Trim();
                        if (apellidoTemp.Length < 2)
                        {
                            throw new ArgumentException("El apellido debe tener 2 o mas letras\n");
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(apellidoTemp, @"^[a-zA-Z\s]+$"))// regex para que permita solo letras y espacios
                        {
                            throw new ArgumentException("El apellido debe contener solo letras\n");
                        }
                        else if (cliente.Apellido == apellidoTemp)
                        {
                            throw new ArgumentException("No podes poner el mismo apellido: " + apellidoTemp + "\n");
                        }
                        else
                        {
                            cliente.Apellido = apellidoTemp;
                            Console.WriteLine("El nuevo apellido del cliente es: " + cliente.Apellido + "\n");
                        }
                        break;
                    case 3:
                        Console.Write("Ingresa el nuevo DNI del cliente: ");
                        string dniTemp = Console.ReadLine().Trim();
                        int dniNumerico;
                        if (dniTemp.Length != 8)
                        {
                            throw new ArgumentException("El DNI debe tener 8 numeros\n");
                        }
                        else if (!int.TryParse(dniTemp, out dniNumerico))
                        {
                            throw new ArgumentException("El DNI debe contener unicamente numeros\n");
                        }
                        else if (cliente.DNI == dniNumerico)
                        {
                            throw new ArgumentException("No podes poner el mismo DNI: " + dniNumerico + "\n");
                        }
                        else
                        {
                            if (clientes.ContainsKey(dniNumerico))
                            {
                                throw new ArgumentException("Ya existe un cliente con el DNI: " + dniNumerico + "\n");
                            }
                            else
                            {
                                //modificamos la clave del diccionario para acceder al cliente
                                Cliente valor = clientes[cliente.DNI];

                                // agrega una nueva clave con el mismo valor
                                clientes.Add(dniNumerico, valor);

                                // elimina la key antigua
                                clientes.Remove(cliente.DNI);

                                //modifica el dni del cliente en el objeto
                                cliente.DNI = dniNumerico;
                                Console.WriteLine("El nuevo DNI del cliente es: " + cliente.DNI + "\n");
                            }
                        }
                        break;
                    case 4:
                        Console.Write("Ingresa el nuevo numero de telefono del cliente: ");
                        string nroTemp = Console.ReadLine().Trim();
                        long nroTempNumerico;
                        if (nroTemp.Length < 6)
                        {
                            throw new ArgumentException("El numero de telefono debe tener 6 o mas caracteres\n");
                        }
                        else if (!long.TryParse(nroTemp, out nroTempNumerico))
                        {
                            throw new ArgumentException("Tu numero de telefono debe contener solo numeros\n");
                        }
                        else if (cliente.NumTelefono == nroTempNumerico)
                        {
                            throw new ArgumentException("No podes poner el mismo numero de telefono: " + nroTempNumerico + "\n");
                        }
                        else
                        {
                            cliente.NumTelefono = nroTempNumerico;
                            Console.WriteLine("El nuevo numero de telefono del cliente es: " + cliente.NumTelefono + "\n");
                        }
                        break;
                    case 5:
                        boolMICI = true;
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                        break;
                }
                try
                {
                    Program.archivoJSON.EscribirJsonDiccionario < Cliente>(path, clientes);
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (SeleccionarOpcionException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolMICI);
    }

    //Utilizado dentro del metodo ModificarInfoAnimal
    private void ModificarInfoAnimalInterno(Cliente cliente, Animal animal, string path)
    {
        int opcionNum = 0;
        bool boolCambiarDueño = false;
        Cliente clienteRemoverAnimal;
        do
        {
            try
            {
                Console.WriteLine("---¿Que dato deseas modificar?---");
                Console.WriteLine("1-Su nombre");
                Console.WriteLine("2-Su dueño");
                Console.WriteLine("3-Su peso");
                Console.WriteLine("4-Volver");
                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                string opcion = Console.ReadLine().Trim();
                Console.WriteLine();
                if (!int.TryParse(opcion, out opcionNum))
                {
                    throw new SeleccionarOpcionException();
                }
                switch (opcionNum)
                {
                    case 1:
                        Console.Write("Ingresa el nuevo nombre del animal: ");
                        string nombreTemp = Console.ReadLine().Trim();
                        if (nombreTemp.Length < 2)
                        {
                            throw new ArgumentException("El nombre debe tener 2 o mas letras\n");
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(nombreTemp, @"^[a-zA-Z\s]+$"))
                        {
                            throw new ArgumentException("El nombre debe contener solo letras\n");
                        }
                        else if (animal.Nombre == nombreTemp)
                        {
                            throw new ArgumentException("No podes poner el mismo nombre: " + nombreTemp + "\n");
                        }
                        else
                        {
                            animal.Nombre = nombreTemp;
                            Console.WriteLine("El nuevo nombre del cliente es: " + animal.Nombre + "\n");
                        }
                        break;
                    case 2:
                        do
                        {
                            try
                            {
                                Cliente nuevoCliente = null;
                                Console.WriteLine("A continuacion coloca el DNI del nuevo dueño");
                                nuevoCliente = validarCliente(nuevoCliente);
                                if (nuevoCliente == null)
                                {
                                    Console.WriteLine("Volviendo...\n");
                                }
                                else
                                {
                                    if (cliente == nuevoCliente)
                                    {
                                        throw new ArgumentException("No podes asignarle el mismo dueño al animal\n");
                                    }
                                    else
                                    {
                                        // elimino el animal de la lista del anterior dueño
                                        cliente.Mascotas.Remove(animal);

                                        // agrego a la lista de mascotas del nuevo dueño, el animal
                                        nuevoCliente.Mascotas.Add(animal);

                                        Console.WriteLine("El nuevo dueño del animal es: " + nuevoCliente.Apellido + ", " + nuevoCliente.Nombre + "\n");
                                        boolCambiarDueño = true;
                                    }
                                }
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("\nError: " + e.Message);
                            }
                            catch (Exception)// por si se produce un fallo inesperado
                            {
                                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
                            }
                        } while (!boolCambiarDueño);
                        break;
                    case 3:
                        Console.Write("Ingresa el nuevo peso del animal: ");
                        string pesoString = Console.ReadLine().Trim();
                        double pesoTemp;
                        if (!double.TryParse(pesoString, out pesoTemp) || pesoTemp <= 0)
                        {
                            throw new ArgumentException("El peso del animal debe ser numerico y mayor a 0\n");
                        }
                        else if (animal.Peso == pesoTemp)
                        {
                            throw new ArgumentException("No podes poner el mismo peso: " + pesoTemp + "\n");
                        }
                        else
                        {                       
                            animal.Peso = pesoTemp;
                            Console.WriteLine("El nuevo peso del animal es: " + animal.Peso + "\n");
                        }
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                        break;
                }
                try
                {
                    Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (SeleccionarOpcionException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (opcionNum != 4);
    }

    //Utilizados para modificar el historial de un animal, dentro del metodo ActualizarHistorial
    private string AgregarConsulta(Cliente cliente, Animal animal, string path)
    {
        Consulta consulta = null;
        bool boolInterno = false;
        do
        {
            try
            {
                Console.WriteLine("---Agregar consulta---");
                // cargar el motivo de la consulta
                Console.Write("Ingresa el motivo de la consulta: ");
                string motivo = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(motivo))
                {
                    throw new CamposCreacionIncorrectosException("El motivo de la consulta no puede estar vacio\n");
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(motivo, @"^[a-zA-Z\s]+$"))// regex para que permita solo letras y espacios
                {
                    throw new CamposCreacionIncorrectosException("El motivo de la consulta debe contener solo letras\n");
                }

                // cargar el diagnostico de la consulta
                Console.Write("Ingresa el diagnostico de la consulta: ");
                string diagnostico = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(diagnostico))
                {
                    throw new CamposCreacionIncorrectosException("El diagnostico de la consulta no puede estar vacio\n");
                }

                consulta = new Consulta(motivo, diagnostico);
                animal.Historial.Add(consulta);
                try
                {
                    Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.Message);
                }
                boolInterno = true;
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolInterno);
        return "Consulta creada con exito!\n\n" + consulta.ToString();
    }

    private string EliminarConsulta(Cliente cliente, Animal animal, string path)
    {
        bool boolInterno = false;
        Consulta consulta;
        int indiceConsulta;
        do
        {
            try
            {
                Console.WriteLine("---Eliminar consulta---");
                if (animal.Historial.Count == 0)
                {
                    return "El animal no tiene consultas registradas en el sistema\n";
                }
                else
                {
                    animal.verHistorial();
                    Console.Write("Coloca el indice de la consulta a eliminar: ");
                    string consultaString = Console.ReadLine().Trim();
                    if (!int.TryParse(consultaString, out indiceConsulta))
                    {
                        throw new ArgumentException("El indice de la consulta no puede estar vacio y debe contener solo numeros\n");
                    }
                    indiceConsulta -= 1;
                    if (indiceConsulta < 0 || indiceConsulta > animal.Historial.Count - 1)
                    {
                        throw new ArgumentOutOfRangeException("El indice de la consulta que proporcionaste es invalido\n");
                    }
                    else
                    {
                        consulta = animal.Historial[indiceConsulta];
                        animal.Historial.RemoveAt(indiceConsulta);
                        consulta = null;

                        try
                        {
                            Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                        }
                        catch (NotSupportedException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        boolInterno = true;
                    }
                }
                
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolInterno);
        return "Consulta eliminada con exito!\n";
    }

    private string VaciarHistorial(Cliente cliente, Animal animal, string path)
    {
        if (animal.Historial.Count == 0)
        {
            return "No se puede vaciar el historial, ya que se encuentra vacio\n";
        }
        else
        {
            animal.Historial.Clear();
            try
            {
                Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return "Historial vaciado con exito!\n";
    }

    //Utilizados para modificar la lista de vacunas de un animal (VER, AGREGAR, ELIMINAR Y MODIFICAR)
    private void VerVacunas(object animalCasteado)
    {
        // verifico si  animalCasteado es Perro o Gato
        if (animalCasteado is Perro perro)
        {
            perro.VerVacunas();
        }
        else if (animalCasteado is Gato gato)
        {
            gato.VerVacunas();
        }
    }

    private void AgregarVacuna(object animalCasteado, string path)
    {
        Gato gato = null;
        Perro perro = null;
        if (animalCasteado is Perro p)
        {
            perro = p;
        }
        else if (animalCasteado is Gato g)
        {
            gato = g;
        }
        bool boolAgregarVacuna = false;
        DateTime fechaVacunaDate;
        do
        {
            try
            {
                Console.WriteLine("---Agregar Vacuna---");
                Console.Write("Coloca el nombre de la vacuna: ");
                string nombreVacuna = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(nombreVacuna))
                {
                    throw new CamposCreacionIncorrectosException("El nombre de la vacuna no puede estar vacio\n");
                }
                Console.Write("Coloca la fecha de vacunacion (dd/mm/aaaa): ");
                string fechaVacuna = Console.ReadLine().Trim();
                if (fechaVacuna.Length != 10)
                {
                    throw new CamposCreacionIncorrectosException("La fecha de vacunacion debe tener 10 caracteres contando ambas barras (/)\n");
                }
                else if (!DateTime.TryParse(fechaVacuna, out fechaVacunaDate))
                {
                    throw new CamposCreacionIncorrectosException("Formato de fecha invalido: " + fechaVacuna + "-> DD/MM/YYYY\n");
                }
                string vacuna = nombreVacuna + ", Fecha: " + fechaVacunaDate.ToString("dd/MM/yyyy");
                if (!string.IsNullOrEmpty(vacuna))
                {
                    if (perro != null) // si es un Perro agregar la vacuna
                    {
                        perro.Vacunas.Add(vacuna);
                        try
                        {
                            Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                        }
                        catch (NotSupportedException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine();
                    }
                    else if (gato != null) // si es un Gato agregar la vacuna
                    {
                        gato.Vacunas.Add(vacuna);
                        try
                        {
                            Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                        }
                        catch (NotSupportedException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine();
                    }
                    boolAgregarVacuna = true;
                }
                else
                {
                    throw new CamposCreacionIncorrectosException("Ocurrio un error intentalo de nuevo\n");
                }
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolAgregarVacuna);
    }

    private void EliminarVacuna(object animalCasteado, string path)
    {
        // verifico si  animalCasteado es Perro o Gato
        if (animalCasteado is Perro perro)
        {
            // Si es Perro, procedemos
            EliminarVacunaInterno(perro.Vacunas, path);
        }
        else if (animalCasteado is Gato gato)
        {
            // Si es Gato, procedemos
            EliminarVacunaInterno(gato.Vacunas, path);
        }
    }

    private void ModificarVacuna(object animalCasteado, string path)
    {
        // verifico si  animalCasteado es Perro o Gato
        if (animalCasteado is Perro perro)
        {
            // Si es Perro, procedemos
            ModificarVacunaInterno(perro.Vacunas, path);
        }
        else if (animalCasteado is Gato gato)
        {
            // Si es Gato, procedemos
            ModificarVacunaInterno(gato.Vacunas, path);
        }
    }

    //Utilizados dentro de EliminarVacuna y ModificarVacuna
    private void EliminarVacunaInterno(List<string> vacunas, string path)
    {

        bool boolEliminarVacuna = false;
        int indiceVacunaNum;
        if (vacunas.Count == 0)
        {
            Console.WriteLine("El animal no tiene vacunas registradas en el sistema\n");
            return;
        }
        do
        {
            try
            {
                Console.WriteLine("---Eliminar Vacuna---");
                Console.WriteLine("---Vacunas del animal---");
                int contador = 1;
                foreach (string vacuna in vacunas)
                {
                    Console.WriteLine("[ " + contador + " ] Vacuna: " + vacuna);
                    contador++;
                }
                Console.WriteLine();
                Console.Write("Coloca el indice de la vacuna a eliminar: ");
                string indiceVacuna = Console.ReadLine().Trim();
                Console.WriteLine();
                if (!int.TryParse(indiceVacuna, out indiceVacunaNum))
                {
                    throw new ArgumentException("El indice de la vacuna a eliminar debe ser numerico\n");
                }
                indiceVacunaNum -= 1;
                if (indiceVacunaNum < 0 || indiceVacunaNum > vacunas.Count - 1)
                {
                    throw new ArgumentException("El indice de la vacuna que proporcionaste es invalido\n");
                }
                else
                {
                    vacunas.RemoveAt(indiceVacunaNum);
                    try
                    {
                        Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    boolEliminarVacuna = true;
                    Console.WriteLine("Vacuna eliminada con exito!\n");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolEliminarVacuna);
    }

    private void ModificarVacunaInterno(List<string> vacunas, string path)
    {
        bool boolModificarVacuna = false;
        int indiceVacunaNum;
        int opcionNum;
        if (vacunas.Count == 0)
        {
            Console.WriteLine("El animal no tiene vacunas registradas en el sistema\n");
            return;
        }
        do
        {
            try
            {
                Console.WriteLine("---Modificar Vacuna---");
                Console.WriteLine("---Vacunas del animal---");
                int contador = 1;
                foreach (string vacuna in vacunas)
                {
                    Console.WriteLine("[ " + contador + " ] Vacuna: " + vacuna);
                    contador++;
                }
                Console.WriteLine();
                Console.Write("Coloca el indice de la vacuna a modificar: ");
                string indiceVacuna = Console.ReadLine().Trim();
                if (!int.TryParse(indiceVacuna, out indiceVacunaNum))
                {
                    throw new ArgumentException("El indice de la vacuna a modificar debe ser numerico\n");
                }
                indiceVacunaNum -= 1;
                if (indiceVacunaNum < 0 || indiceVacunaNum > vacunas.Count - 1)
                {
                    throw new ArgumentException("El indice de la vacuna que proporcionaste es invalido\n");
                }
                else
                {
                    Console.WriteLine();
                    bool boolMV = false;
                    do
                    {
                        try
                        {
                            Console.WriteLine("---Modificar---");
                            Console.WriteLine("1-Nombre Vacuna");
                            Console.WriteLine("2-Fecha Vacuna");
                            Console.WriteLine("3-Volver");
                            Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                            string opcion = Console.ReadLine().Trim();
                            Console.WriteLine();
                            if (!int.TryParse(opcion, out opcionNum))
                            {
                                throw new SeleccionarOpcionException();
                            }
                            switch (opcionNum)
                            {
                                case 1:
                                    Console.Write("Coloca el nuevo nombre de la vacuna: ");
                                    string nuevoNombre = Console.ReadLine().Trim();
                                    if (string.IsNullOrEmpty(nuevoNombre))
                                    {
                                        throw new ArgumentException("El nombre de la vacuna no puede estar vacio\n");
                                    }
                                    //guardo el string de la vacuna en ese indice
                                    string vacunaOriginal = vacunas[indiceVacunaNum];

                                    // divido la cadena para separar el nombre de la vacuna y la fecha
                                    string[] partes = vacunaOriginal.Split(new string[] { ", Fecha: " }, StringSplitOptions.None);
                                    if (partes.Length == 2) // veo si se pudo separar
                                    {
                                        // creo el nuevo string recuperando la vacuna pero poniendo el nombre nuevo
                                        string modificada = nuevoNombre + ", Fecha: " + partes[1];
                                        vacunas[indiceVacunaNum] = modificada; // reemplazo el elemento.
                                        boolModificarVacuna = true;
                                        Console.WriteLine("Vacuna modificada con exito!\n");
                                    }
                                    break;

                                case 2:
                                    DateTime fechaNuevaDate;
                                    Console.Write("Coloca la nueva fecha de vacunacion (dd/mm/aaaa): ");
                                    string nuevaFecha = Console.ReadLine().Trim();
                                    if (nuevaFecha.Length != 10)
                                    {
                                        throw new ArgumentException("La fecha de vacunacion debe tener 10 caracteres contando ambas barras (/)\n");
                                    }
                                    else if (!DateTime.TryParse(nuevaFecha, out fechaNuevaDate))
                                    {
                                        throw new ArgumentException("Formato de fecha invalido: " + nuevaFecha + "-> DD/MM/YYYY\n");
                                    }
                                    //guardo el string de la vacuna en ese indice
                                    string vacunaOriginal1 = vacunas[indiceVacunaNum];

                                    // divido la cadena para separar el nombre de la vacuna y la fecha
                                    string[] partes1 = vacunaOriginal1.Split(new string[] { ", Fecha: " }, StringSplitOptions.None);
                                    if (partes1.Length == 2) // veo si se pudo separar
                                    {
                                        // creo el nuevo string recuperando la vacuna pero poniendo la fecha nueva
                                        string modificada = partes1[0] + ", Fecha: " + fechaNuevaDate.ToString("dd/MM/yyyy");
                                        vacunas[indiceVacunaNum] = modificada; // reemplazo el elemento.
                                        boolModificarVacuna = true;
                                        Console.WriteLine("Vacuna modificada con exito!\n");
                                    }
                                    break;

                                case 3:
                                    Console.WriteLine("Volviendo...\n");
                                    boolMV = true;
                                    boolModificarVacuna = true;
                                    break;

                                default:
                                    Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                    break;
                            }
                            try
                            {
                                Program.archivoJSON.EscribirJsonDiccionario<Cliente>(path, clientes);
                            }
                            catch (NotSupportedException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        catch (SeleccionarOpcionException e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                    } while (!boolMV);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolModificarVacuna);
    }

    public override string ToString()
    {
        return "---Informacion Veterinario---" + "\nUsuario: " + usuario + base.ToString() + "\n";
    }

    public string CambiarDireccionArchivos()
    {
        string destinoCompleto = "";
        bool boolCambiarDireccion = false;
        do
        {
            try
            {
                Console.WriteLine("--- Cambiar direccion de archivos ---");
                Console.Write("Coloca la nueva direccion a donde quieres mover los archivos: ");
                string nuevaDireccion = Console.ReadLine().Trim();

                // verifico que la nueva direccion exista
                if (!Directory.Exists(nuevaDireccion))
                {
                    return "La direccion proporcionada no existe\n";
                }
                else
                {
                    // obtengo el nombre de la carpeta original y la ruta completa de destino
                    string nombreDirectorioDestino = Path.GetFileName(Program.carpetaArchivos);
                    destinoCompleto = Path.Combine(nuevaDireccion, nombreDirectorioDestino);

                    // verifico si ya existe un directorio con el mismo nombre en la nueva ubicacion
                    if (Directory.Exists(destinoCompleto))
                    {
                        return "Ya existe un directorio con el mismo nombre en la nueva ubicacion\n";
                    }

                    // muevo la carpeta a la nueva direccion
                    Directory.Move(Program.carpetaArchivos, destinoCompleto);

                    // Actualizar la ruta en el archivo de configuracion
                    File.WriteAllText(Program.archivoConfiguracion, destinoCompleto);

                    boolCambiarDireccion = true;
                    
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("No tenes permisos para mover los archivos a la nueva ubicacion\n");
            }
            catch (IOException)
            {
                Console.WriteLine("Ocurrio un error al mover los archivos. Verifica si la carpeta esta en uso o abierta\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Ocurrio un error inesperado. Intentalo de nuevo\n");
            }
        } while (!boolCambiarDireccion);
        return "La nueva direccion de los archivos es: " + destinoCompleto + "\n";
    }

    public string EliminarTodaInformacion(string rutaClientes, string rutaFacturas)
    {
        bool boolBorrarInfo = false;
        do
        {
            try
            {
                Console.WriteLine("¿Estas seguro que queres borrar toda la informacion?");
                Console.WriteLine("1-Si");
                Console.WriteLine("2-No, volver");
                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                string opcionMenu = Console.ReadLine().Trim();
                int opcionMenuNumerica;
                if (!int.TryParse(opcionMenu, out opcionMenuNumerica))
                {
                    throw new SeleccionarOpcionException();
                }
                Console.WriteLine();
                string opcionIntMenu;
                int opcionIntMenuNum = 0;
                switch (opcionMenuNumerica)
                {
                    case 1:
                        do
                        {
                            try
                            {
                                Console.WriteLine("Seguro?");
                                Console.WriteLine("1-Si");
                                Console.WriteLine("2-No, volver");
                                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                                opcionIntMenu = Console.ReadLine().Trim();
                                if (!int.TryParse(opcionIntMenu, out opcionIntMenuNum))
                                {
                                    throw new SeleccionarOpcionException();
                                }
                                Console.WriteLine();
                                switch (opcionIntMenuNum)
                                {
                                    case 1:
                                        File.Delete(rutaClientes);
                                        File.Delete(rutaFacturas);
                                        Console.WriteLine("Reinicia la aplicacion para ver los cambios\n");
                                        return "Se borraron todos los datos (clientes, facturas, animales) de la aplicacion\n";;
                                    case 2:
                                        Console.WriteLine("Volviendo...\n");
                                        break;
                                    default:

                                        Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                        break;
                                }
                            }
                            catch (SeleccionarOpcionException e)
                            {
                                Console.WriteLine("\nError: " + e.Message);
                            }
                        } while (opcionIntMenuNum != 2);
                        break;

                    case 2:
                        boolBorrarInfo = true;
                        break;


                    default:
                        Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                        break;
                }
            }
            catch (SeleccionarOpcionException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolBorrarInfo);
     return "Volviendo...\n";
    }
}