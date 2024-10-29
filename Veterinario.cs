using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Factura;

public class Veterinario : IRegistrosVeterinario, IFacturacion, IConsultarInfo, IModificarInfo
{
    private string usuario;
    private string contraseña; // despues la vamos a hashear en el futuro.
    private string nombre;
    private string apellido;
    private string nroTelefono;

    //Constructor
    public Veterinario()
    {
        Console.WriteLine("---Bienvenido al registro del sistema---");
        Console.Write("Ingresa un nombre de usuario: ");
        usuario = Console.ReadLine();
        Console.Write("Ingresa una constraseña: ");
        contraseña = Console.ReadLine();
        Console.Write("Ingresa tu nombre: ");
        nombre = Console.ReadLine();
        Console.Write("Ingresa tu apellido: ");
        apellido = Console.ReadLine();
        Console.Write("Ingresa tu numero de telefono: ");
        nroTelefono = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Te has registrado\n");
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

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public string Apellido
    {
        get { return apellido; }
        set { apellido = value; }
    }

    public string NroTelefono
    {
        get { return nroTelefono; }
        set { nroTelefono = value; }
    }

    //Metodos de Registro y Eliminacion.
    public string RegistrarCliente() //metodo de la interfaz
    {
        Console.WriteLine("---Registrar Cliente---");
        Console.Write("Ingresa el nombre del cliente: ");
        string nombreTemp = Console.ReadLine();
        Console.Write("Ingresa el apellido del cliente: ");
        string apellidoTemp = Console.ReadLine();
        Console.Write("Ingresa el DNI del cliente: ");
        int dniTemp = Convert.ToInt32(Console.ReadLine());
        Console.Write("Ingresa el numero de telefono del cliente: ");
        string nroTemp = Console.ReadLine();

        Cliente cliente = new Cliente(nombreTemp, apellidoTemp, dniTemp, nroTemp);
        Console.WriteLine();
        //se va a guardar en una lista en un futuro.
        return "Cliente registrado con exito \n" + cliente.ToString() + "\n";
    }

    public string RegistrarAnimal() //metodo de la interfaz
    {
        Animal animal = null;
        Console.WriteLine("---Registrar Animal--");
        Console.WriteLine("Selecciona el tipo de animal: ");
        Console.WriteLine("1- Perro");
        Console.WriteLine("2- Gato");
        Console.WriteLine("3- Ave");
        Console.WriteLine("4- Roedor");
        int opcion = 0;
        do
        {
            Console.Write("Coloca tu opcion en numero aqui: ");
            opcion = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (opcion)
            {
                case 1:
                    MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out int pesoTemp, out Animal.Genero sexoTemp);
                    Console.Write("Coloca la raza del perro: ");
                    string razaTemp = Console.ReadLine();
                    animal = new Perro(nombreTemp, cliente, fecNacTemp, pesoTemp, sexoTemp, razaTemp);
                    Console.WriteLine();
                    break;
                case 2:
                    MetodoInternoRegistroAnimal(out string nombreTemp1, out Cliente cliente1, out DateTime fecNacTemp1, out int pesoTemp1, out Animal.Genero sexoTemp1);
                    Console.Write("Coloca la raza del gato: ");
                    string razaTemp1 = Console.ReadLine();
                    animal = new Gato(nombreTemp1, cliente1, fecNacTemp1, pesoTemp1, sexoTemp1, razaTemp1);
                    Console.WriteLine();
                    break;
                case 3:
                    MetodoInternoRegistroAnimal(out string nombreTemp2, out Cliente cliente2, out DateTime fecNacTemp2, out int pesoTemp2, out Animal.Genero sexoTemp2);
                    Console.WriteLine("Coloca la especie del ave: ");
                    Console.WriteLine("1- Canario");
                    Console.WriteLine("2- Loro");
                    Console.WriteLine("3- Cata");
                    Console.WriteLine("4- Silvetre");
                    Console.WriteLine("5- Callejero (Paloma, Gorrion, Tordo, etc.)");
                    int opcion1 = 0;
                    Ave.Variedad especieTemp = Ave.Variedad.NoEspecificado;
                    do
                    {
                        Console.Write("Coloca tu opcion en numero aqui: ");
                        opcion1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcion1)
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
                    } while (opcion1 < 1 || opcion1 > 5);
                    animal = new Ave(nombreTemp2, cliente2, fecNacTemp2, pesoTemp2, sexoTemp2, especieTemp);
                    break;
                case 4:
                    MetodoInternoRegistroAnimal(out string nombreTemp3, out Cliente cliente3, out DateTime fecNacTemp3, out int pesoTemp3, out Animal.Genero sexoTemp3);
                    Console.WriteLine("Selecciona la especie del roedor");
                    Console.WriteLine("1- Hamster");
                    Console.WriteLine("2- Cobayo");
                    Console.WriteLine("3- Chinchilla");
                    Console.WriteLine("4- Rata");
                    int opcion2 = 0;
                    Roedor.Variedad especieTemp1 = Roedor.Variedad.NoEspecificado;
                    do
                    {
                        Console.Write("Coloca tu opcion en numero aqui: ");
                        opcion2 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcion2)
                        {
                            case 1:
                                especieTemp1 = Roedor.Variedad.Hamster;
                                break;
                            case 2:
                                especieTemp1 = Roedor.Variedad.Cobayo;
                                break;
                            case 3:
                                especieTemp1 = Roedor.Variedad.Chinchilla;
                                break;
                            case 4:
                                especieTemp1 = Roedor.Variedad.Rata;
                                break;
                            default:
                                Console.WriteLine("Opcion no valida. Coloca una opcion valida!\n");
                                break;
                        }
                    } while (opcion2 < 1 || opcion2 > 4);
                    animal = new Roedor(nombreTemp3, cliente3, fecNacTemp3, pesoTemp3, sexoTemp3, especieTemp1);
                    break;
                default:
                    Console.WriteLine("Opcion no valida. Coloca una opcion valida!\n");
                    break;
            }
        } while (opcion < 1 || opcion > 4);
        return animal.GetType() + " registrad@ con exito \n";

    }

    public string EliminarAnimal() //metodo de la interfaz
    {
        //Console.Write("Coloca el nombre del cliente del animal a eliminar: ");
        //string nombre = Console.ReadLine();
        // va a realizar una busqueda por el nombre del cliente y va a mostrar todos los animales a su nombre.
        // va a ir un do-while con un switch para elegir que animal de esos eliminar y poner la referencia de ese animal en null
        //Animal animal = animal encontrado por la busqueda.
        //animal = null;
        // el gc va a pasar y eliminar el objeto animal
        return "Metodo sin implementar!!!\n";
    }

    public string EliminarCliente() //metodo de la interfaz
    {
        //Console.Write("Coloca el numero de dni del cliente a eliminar: ");
        //int dni = Convert.ToInt32(Console.ReadLine());
        // va a realizar una busqueda por el numero de dni y va a eliminar el cliente de la lista y luego sacarle la referencia del objeto.
        //Cliente cliente = cliente encontrado por la busqueda.
        //cliente = null;
        // el gc va a pasar y eliminar el objeto cliente
        return "Metodo sin implementar!!!\n";
    }

    //Metodos para Facturacion
    public string EliminarFactura()
    {
        //Console.Write("Coloca el numero de factura a eliminar: ");
        //int numeroFactura = Convert.ToInt32(Console.ReadLine());
        // va a realizar una busqueda por el numero de factura y va a eliminarla de la lista y luego sacarle la referencia dle objeto.
        //Factura factura = factura encontrada por la busqueda.
        //factura = null;
        //El GC va a pasar y eliminar el objeto Factura
        return "Metodo sin implementar!!! \n";
    }//metodo de la interfaz

    public string CrearFactura() //metodo de la interfaz
    {
        Console.WriteLine("---Nueva Factura---");
        Console.Write("Ingresa el numero de factura: "); // en el futuro va a ser automatico, va a ser autoincremental.
        int nroFactTemp = Convert.ToInt32(Console.ReadLine());
        Console.Write("\nIngresa el cliente(apreta enter temporalmente): ");
        Cliente clienteTemp = null; // esta en null para permitir seguir la ejecucion del codigo hasta que veamos listas para validar
        Console.ReadLine();
        Console.Write("\nIngresa el animal(apreta enter temporalmente): ");
        Animal animalTemp = null; // esta en null para permitir seguir la ejecucion del codigo hasta que veamos listas para validar
        Console.ReadLine();
        Console.WriteLine("\nSelecciona el servicio realizado");
        Console.WriteLine("1: Revision ");
        Console.WriteLine("2: Cirujia ");
        Console.WriteLine("3: Control Completo ");
        Console.WriteLine("4: Vacunacion ");
        Console.WriteLine("5: Laboratorio ");
        Servicios servicioTemp = Factura.Servicios.NoDefinido;
        do
        {
            Console.Write("Ingresa la opcion(en numero) aqui: ");
            int opcion = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (opcion)
            {
                case 1:
                    servicioTemp = Factura.Servicios.Revision;
                    break;
                case 2:
                    servicioTemp = Factura.Servicios.Cirujia;
                    break;
                case 3:
                    servicioTemp = Factura.Servicios.ControlCompleto;
                    break;
                case 4:
                    servicioTemp = Factura.Servicios.Vacunacion;
                    break;
                case 5:
                    servicioTemp = Factura.Servicios.Laboratorio;
                    break;
                default:
                    Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                    break;
            }
        } while (servicioTemp == Factura.Servicios.NoDefinido);
        Factura factura = new Factura(nroFactTemp, clienteTemp, animalTemp, servicioTemp);
        Console.WriteLine("El precio total por el servicio es de: $" + factura.Precio + "\n");
        return "Factura realizada con exito \n";
    }

    public string ImprimirFactura()
    {
        //Console.Write("Coloca el numero de factura a buscar: ");
        //int numeroFactura = Convert.ToInt32(Console.ReadLine());
        // va a realizar una busqueda dentro de la estructura de datos con nroFactura
        //Factura factura = a la factura que obtuve por la busqeuda del nroFactura
        //return factura.ToString();
        return "Metodo sin implementar!!!\n";
    }

    public void MostrarFacturas()
    {
        // Cuando veamos la estructura de datos va a imprimirla.
    }

    public string CambiarPrecioServicio()
    {
        int opcionMS = 0;
        do
        {
            Console.WriteLine("---Selecciona el servicio a modificar su precio---");
            Console.WriteLine("1-Revision");
            Console.WriteLine("2-Cirugia");
            Console.WriteLine("3-Control Completo");
            Console.WriteLine("4-Vacunacion");
            Console.WriteLine("5-Laboratorio");
            Console.WriteLine("6-Volver");
            Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
            opcionMS = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (opcionMS)
            {
                case 1:
                    // cuando vea diccionarios va a funcionar esto
                    break;
                case 2:
                    // cuando vea diccionarios va a funcionar esto
                    break;
                case 3:
                    // cuando vea diccionarios va a funcionar esto
                    break;
                case 4:
                    // cuando vea diccionarios va a funcionar esto
                    break;
                case 5:
                    // cuando vea diccionarios va a funcionar esto
                    break;
                case 6:
                    // cuando vea diccionarios va a funcionar esto
                    break;
                default:
                    Console.WriteLine("Opcion no valida. Ingresa una opcion valida\n");
                    break;
            }
        } while (opcionMS != 6);
        return "Volviendo...\n";
    }

    //Metodos para Consultar datos
    public string ConsultarInfoCliente()
    {
        //int opcionConsulta = 0;
        //Console.Write("Coloca el DNI del cliente a obtener su informacion: ");
        //int dni = Convert.ToInt32(Console.ReadLine());
        // realiza la busqueda y va a poner la referencia de ese objeto cliente en otro objeto de tipo Cliente para imprimir su info completa
        // Cliente cliente = lo mencionado anteriormente
        // cliente.ToString();
        //do
        //{
        //    Console.WriteLine("---Que deseas consultar---");
        //    Console.WriteLine("1-Su informacion)");
        //    Console.WriteLine("2-Sus animales");
        //    Console.WriteLine("3-Volver");
        //    Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
        //    opcionConsulta = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine();
        //    switch (opcionConsulta)
        //    {
        //        case 1:
        //            cliente.ToString();
        //            break;
        //        case 2:
        //            // va a imprimir la lista con todos sus animales.
        //            break;
        //        case 3:
        //            Console.WriteLine();
        //            break;
        //        default:
        //            Console.WriteLine("Opcion valida. Ingresa una opcion valida!");
        //            Console.WriteLine();
        //            break;
        //    }
        //} while (opcionConsulta != 3);
        return "Metodo sin implementar!!! \n";
    }

    public string ConsultarInfoAnimal()
    {
        //int opcionConsulta = 0;
        //Console.Write("Coloca el DNI del cliente del animal a obtener su informacion: ");
        //int DNI = Console.ReadLine();
        // va a realizar una busqueda por el DNI del cliente y va a mostrar todos los animales a su nombre.
        // va a ir un do-while para elegir que animal de esos quiere consultar y poner la referencia de ese animal para obtener toda su info
        //Animal animal = animal encontrado por la busqueda.
        //do
        //{
        //    Console.WriteLine("---Que deseas consultar---");
        //    Console.WriteLine("1-Su informacion)");
        //    Console.WriteLine("2-Sus Vacunas");
        //    Console.WriteLine("3-Volver");
        //    Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
        //    opcionConsulta = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine();
        //    switch (opcionConsulta)
        //    {
        //        case 1:
        //            animal.ToString();
        //            break;
        //        case 2:
        //            // va a imprimir la lista con todas sus vacunas.
        //            break;
        //        case 3:
        //            Console.WriteLine();
        //            break;
        //        default:
        //            Console.WriteLine("Opcion valida. Ingresa una opcion valida!");
        //            Console.WriteLine();
        //            break;
        //    }
        //} while (opcionConsulta != 3);
        return "Metodo sin implementar!!!\n";
    }

    // Metodo para simplificar codigo y reutilizar el mismo (actua como una funcion).
    private void MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out int pesoTemp, out Animal.Genero sexoTemp)
    {
        Console.Write("Coloca el nombre del animal: ");
        nombreTemp = Console.ReadLine();
        Console.Write("\nColoca el nombre del dueño: = null temporal (Apreta enter)"); // va a realizar una busqueda en la lista en el futuro
        cliente = null; // colocamos null temporalmente para permitir que siga el flujo del metodo, cuando veamos listas vamos a realizar una busqueda por el nombre para encontrarlo.
        Console.ReadLine();
        Console.Write("\nColoca la fecha de nacimiento del animal (dd/mm/aaaa): ");
        bool flujo = false;
        string fecNacString;
        fecNacTemp = DateTime.MinValue;
        do
        {
            fecNacString = Console.ReadLine();
            if (!DateTime.TryParse(fecNacString, out fecNacTemp)) // tuve que guiarme de chatgpt solo en esta linea de codigo para realizar la validacion. Ya que no sabia como hacerla.
            {
                Console.WriteLine();
                Console.WriteLine("Formato de fecha invalido -> (DD/MM/AAAA).");
                Console.Write("Coloca de nuevo la fecha: ");
            }
            else
                flujo = true;
        } while (!flujo);
        Console.Write("\nColoca el peso del animal en Kg: ");
        pesoTemp = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("\nSelecciona el sexo del animal: ");
        Console.WriteLine("1- Masculino");
        Console.WriteLine("2- Femenino");
        int opcion1 = 0;
        do
        {
            Console.Write("Coloca tu opcion en numero aqui: ");
            opcion1 = Convert.ToInt32(Console.ReadLine());
            sexoTemp = Animal.Genero.NoEspecificado; ;
            Console.WriteLine();
            switch (opcion1)
            {
                case 1:
                    sexoTemp = Animal.Genero.Masculino;
                    break;
                case 2:
                    sexoTemp = Animal.Genero.Femenino;
                    break;
                default:
                    Console.WriteLine("Opcion no valida. Coloca una opcion valida! \n");
                    break;
            }
        } while (opcion1 != 1 && opcion1 != 2);
    }

    //Metodos de modificacion de informacion
    public string ModificarInfoCliente()
    {
        // aca va a realizar una busqueda de un cliente en una coleccion
        // y va a tener la posiblidad de cambiar la informacion de un cliente
        return "";
    }

    public string ModificarInfoAnimal()
    {
        // aca va a realizar una busqueda de un animal en una coleccion
        // y va a tener la posiiblidad de cambiar la informacion de un animal
        return "";
    }

    //Metodos de veterinario para su objeto propio
    public string ModificarInformacion(Veterinario veterinario)
    {
        int opcionMI = 0;
        int opcionInd = 0;
        bool opcionModificacion = false;
 
        do
        {
            Console.WriteLine("---Selecciona la informacion que quieres modificar---");
            Console.WriteLine("1-Toda la informacion");
            Console.WriteLine("2-Una parte");
            Console.WriteLine("3-Volver");
            Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
            opcionMI = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (opcionMI)
            {
                case 1:
                    while (opcionModificacion == false)
                    {
                        Console.Write("Coloca tu nuevo nombre de usuario: ");
                        string nombUsuTemp = Console.ReadLine();
                        if (nombUsuTemp == veterinario.usuario)
                        {
                            Console.WriteLine("No podes poner el mismo nombre de usuario.\n");
                        }
                        else
                        {
                            veterinario.Usuario = nombUsuTemp;
                            Console.WriteLine("Tu nombre de usuario ahora es: " + veterinario.Usuario + "\n");
                            opcionModificacion = true;
                        }
                    }
                    opcionModificacion = false;
                    while (opcionModificacion == false)
                    {
                        Console.Write("Coloca tu nueva contraseña: ");
                        string contTemp = Console.ReadLine();
                        if (contTemp == veterinario.contraseña)
                        {
                            Console.WriteLine("No podes poner la misma contraseña.\n");
                        }
                        else
                        {
                            veterinario.Contraseña = contTemp;
                            Console.WriteLine("Tu contraseña ahora es: " + veterinario.Contraseña + "\n");
                            opcionModificacion = true;
                        }
                    }
                    opcionModificacion = false;
                    while (opcionModificacion == false)
                    {
                        Console.Write("Coloca tu nuevo nombre: ");
                        string nombTemp = Console.ReadLine();
                        if (nombTemp == veterinario.nombre)
                        {
                            Console.WriteLine("No podes poner el mismo nombre.\n");
                        }
                        else
                        {
                            veterinario.Nombre = nombTemp;
                            Console.WriteLine("Tu nombre ahora es: " + veterinario.Nombre + "\n");
                            opcionModificacion = true;
                        }
                    }
                    opcionModificacion = false;
                    while (opcionModificacion == false)
                    {
                        Console.Write("Coloca tu nuevo apellido: ");
                        string apeTemp = Console.ReadLine();
                        if (apeTemp == veterinario.apellido)
                        {
                            Console.WriteLine("No podes poner el mismo apellido.\n");
                        }
                        else
                        {
                            veterinario.Apellido = apeTemp;
                            Console.WriteLine("Tu apellido ahora es: " + veterinario.Apellido + "\n");
                            opcionModificacion = true;
                        }
                    }
                    opcionModificacion = false;
                    while (opcionModificacion == false)
                    {
                        Console.Write("Coloca tu nuevo numero de telefono: ");
                        string numTelTemp = Console.ReadLine();
                        if (numTelTemp == veterinario.NroTelefono)
                        {
                            Console.WriteLine("No podes poner el mismo numero de telefono.\n");
                        }
                        else
                        {
                            veterinario.NroTelefono = numTelTemp;
                            Console.WriteLine("Tu numero de telefono ahora es: " + veterinario.NroTelefono + "\n");
                            opcionModificacion = true;
                        }
                    }
                    Console.WriteLine("Informacion actualizada con exito!\n");
                    break;
                case 2:
                    do
                    {
                        Console.WriteLine("---Selecciona la informacion que quieres modificar---");
                        Console.WriteLine("1-Nombre de usuario");
                        Console.WriteLine("2-Contraseña");
                        Console.WriteLine("3-Nombre"); // estas opciones junto al apellido las ponemos por si por ahi escribio mal el nombre y en el futuro lo quiere cambiar
                        Console.WriteLine("4-Apellido");
                        Console.WriteLine("5-Numero de telefono");
                        Console.WriteLine("6-Volver");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        opcionInd = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcionInd)
                        {
                            case 1:
                                Console.Write("Coloca tu nuevo nombre de usuario: ");
                                string nombUsuTemp = Console.ReadLine();
                                if (nombUsuTemp == veterinario.usuario)
                                {
                                    Console.WriteLine("No podes poner el mismo nombre de usuario.\n");
                                }
                                else
                                {
                                    veterinario.Usuario = nombUsuTemp;
                                    Console.WriteLine("Tu nombre de usuario ahora es: " + veterinario.Usuario + "\n");
                                }
                                break;
                            case 2:
                                Console.Write("Coloca tu nueva contraseña: ");
                                string contTemp = Console.ReadLine();
                                if (contTemp == veterinario.contraseña)
                                {
                                    Console.WriteLine("No podes poner la misma contraseña.\n");
                                }
                                else
                                {
                                    veterinario.Contraseña = contTemp;
                                    Console.WriteLine("Tu contraseña ahora es: " + veterinario.Contraseña + "\n");
                                }
                                break;
                            case 3:
                                Console.Write("Coloca tu nuevo nombre: ");
                                string nombTemp = Console.ReadLine();
                                if (nombTemp == veterinario.nombre)
                                {
                                    Console.WriteLine("No podes poner el mismo nombre.\n");
                                }
                                else
                                {
                                    veterinario.Nombre = nombTemp;
                                    Console.WriteLine("Tu nombre ahora es: " + veterinario.Nombre + "\n");
                                }
                                break;
                            case 4:
                                Console.Write("Coloca tu nuevo apellido: ");
                                string apeTemp = Console.ReadLine();
                                if (apeTemp == veterinario.apellido)
                                {
                                    Console.WriteLine("No podes poner el mismo apellido.\n");
                                }
                                else
                                {
                                    veterinario.Apellido = apeTemp;
                                    Console.WriteLine("Tu apellido ahora es: " + veterinario.Apellido + "\n");
                                }
                                break;
                            case 5:
                                Console.Write("Coloca tu nuevo numero de telefono: ");
                                string numTelTemp = Console.ReadLine();
                                if (numTelTemp == veterinario.NroTelefono)
                                {
                                    Console.WriteLine("No podes poner el mismo numero de telefono.\n");
                                }
                                else
                                {
                                    veterinario.NroTelefono = numTelTemp;
                                    Console.WriteLine("Tu numero de telefono ahora es: " + veterinario.NroTelefono + "\n");
                                }
                                break;
                            case 6:
                                Console.WriteLine("Volviendo...\n");
                                break;
                            default:
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida\n");
                                break;
                        }
                    } while (opcionInd != 6);
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Opcion no valida. Ingresa una opcion valida\n");
                    break;
            }
        } while (opcionMI != 3);
        return "Volviendo...\n";
    }

    public override string ToString()
    {
        return "---Informacion Veterinario---" + "\n Usuario: " + usuario + "\n Nombre: " + nombre + "\n Apellido: " + apellido
            + "\n Numero de Telefono: " + nroTelefono + "\n";
    }
}

