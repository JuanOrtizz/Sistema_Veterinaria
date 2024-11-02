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
    private int nroTelefono;

    //Constructor
    public Veterinario(string usuario, string contraseña, string nombre, string apellido, int nroTelefono)
    {
        this.usuario = usuario;
        this.contraseña = contraseña;
        this.nombre = nombre;
        this.apellido = apellido;
        this.nroTelefono = nroTelefono;
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

    public int NroTelefono
    {
        get { return nroTelefono; }
        set { nroTelefono = value; }
    }

    //Metodos de Registro y Eliminacion.
    public string RegistrarCliente() //metodo de la interfaz IRegistrosVeterinario
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
                if (string.IsNullOrEmpty(nombreTemp) || nombreTemp.Length < 2)
                {
                    throw new CamposCreacionIncorrectosException("El nombre no puede estar vacio y debe tener 2 o mas letras\n");
                }
                else if (!nombreTemp.All(char.IsLetter))
                {
                    throw new CamposCreacionIncorrectosException("El nombre debe contener solo letras\n");
                }

                // cargar datos del apellido del cliente
                Console.Write("Ingresa el apellido del cliente: ");
                string apellidoTemp = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(apellidoTemp) || apellidoTemp.Length < 2)
                {
                    throw new CamposCreacionIncorrectosException("El apellido no puede estar vacio y debe tener 2 o mas letras\n");
                }
                else if (!apellidoTemp.All(char.IsLetter))
                {
                    throw new CamposCreacionIncorrectosException("El apellido debe contener solo letras\n");
                }

                // cargar datos del DNI del cliente
                Console.Write("Ingresa el DNI del cliente: ");
                string dniTemp = Console.ReadLine().Trim();
                int dniNumerico;
                if (string.IsNullOrEmpty(dniTemp) || dniTemp.Length != 8)
                {
                    throw new CamposCreacionIncorrectosException("El DNI no debe estar vacio y debe tener 8 numeros\n");
                }
                else if (!int.TryParse(dniTemp, out dniNumerico))
                {
                    throw new CamposCreacionIncorrectosException("El DNI debe contener unicamente numeros\n");
                }

                // cargar datos del telefono del cliente
                Console.Write("Ingresa el numero de telefono del cliente: ");
                string nroTemp = Console.ReadLine().Trim();
                int nroTempNumerico;
                if (string.IsNullOrEmpty(nroTemp) || nroTemp.Length < 6)
                {
                    throw new CamposCreacionIncorrectosException("El numero de telefono no puede estar vacio y debe tener 6 o mas caracteres\n");
                }
                else if (!int.TryParse(nroTemp, out nroTempNumerico))
                {
                    throw new CamposCreacionIncorrectosException("Tu numero de telefono debe contener solo numeros\n");
                }

                //creacion del cliente
                //se va a guardar en una lista en un futuro.
                cliente = new Cliente(nombreTemp, apellidoTemp, dniNumerico, nroTempNumerico);
                boolRegistroCliente = true;
                Console.WriteLine();
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

    public string RegistrarAnimal() //metodo de la interfaz IRegistrosVeterinario
    {
        Animal animal = null;
        int opcionNumerica = 0;
        // variable para el do-while del metodo interno de cada animal
        bool registroAnimalInterno = false;
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
                        animal = RegistroPerro(registroAnimalInterno);
                        boolRegistroAnimal = true;
                        break;
                    case 2:
                        animal = RegistroGato(registroAnimalInterno);
                        boolRegistroAnimal = true;
                        break;
                    case 3:
                        animal = RegistroAve(registroAnimalInterno);
                        boolRegistroAnimal = true;
                        break;
                    case 4:
                        animal = RegistroRoedor(registroAnimalInterno);
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
                Console.WriteLine("Error: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!boolRegistroAnimal);

        return animal.GetType() + " registrad@ con exito \n";
    }

    // Metodos de registros animales para simplificar codigo y reutilizar el mismo 
    private void MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp)
    {
        // variable para el do-while
        bool boolRegistroAnimalInt = false;

        // variables para que funcione el out de parametros, mas abajo se verifica si siguen en esta condicion
        nombreTemp = "";
        cliente = null;
        fecNacTemp = DateTime.MinValue;
        pesoTemp = 0;
        sexoTemp = Animal.Genero.NoEspecificado;
        do
        {
            Console.WriteLine("---Registrando Animal---");

            // carga el nombre del animal
            Console.Write("Coloca el nombre del animal: ");
            nombreTemp = Console.ReadLine();
            if (string.IsNullOrEmpty(nombreTemp))
            {
                throw new CamposCreacionIncorrectosException("El nombre no puede estar vacio\n");
            }
            else if (!nombreTemp.All(char.IsLetter))
            {
                throw new CamposCreacionIncorrectosException("El nombre debe contener solo letras\n");
            }

            // carga el dueño del animal
            Console.Write("Coloca el DNI del dueño: = null temporal (Apreta enter)"); // va a realizar una busqueda en una coleccion en el futuro
            //cliente = null; colocamos null temporalmente para permitir que siga el flujo del metodo.
            Console.ReadLine(); //AGREGAR CONTROL DE EXCEPCIONES AL IMPLEMENTAR COLECCION

            // carga la fecha de nacimiento del animal
            Console.Write("Coloca la fecha de nacimiento del animal (dd/mm/aaaa): ");
            string fecNacString;
            fecNacString = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(fecNacString))
            {
                throw new CamposCreacionIncorrectosException("La fecha no puede estar vacia\n");
            }
            else if (!DateTime.TryParse(fecNacString, out fecNacTemp))
            {
                throw new CamposCreacionIncorrectosException("Formato de fecha invalido: " + fecNacString + "-> DD/MM/YYYY\n");
            }
            else
                
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
            
        } while (!boolRegistroAnimalInt);
    }

    private Animal RegistroPerro(bool registroAnimal)
    {
        Perro animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

                // cargamos el nombre de la raza del perro
                Console.Write("Coloca la raza del perro: ");
                string razaTemp = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(razaTemp))
                {
                    throw new CamposCreacionIncorrectosException("La raza del perro no puede estar vacia\n");
                }
                else if (!razaTemp.All(char.IsLetter))
                {
                    throw new CamposCreacionIncorrectosException("La raza debe contener solo letras\n");
                }

                //creamos el perro
                // en el futuro lo vamos a agregar a una coleccion
                animalTemp = new Perro(nombreTemp, cliente, fecNacTemp, pesoTemp, sexoTemp, razaTemp);
                registroAnimal = true;
                Console.WriteLine();
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!registroAnimal);
        return animalTemp;
    }

    private Animal RegistroGato(bool registroAnimal)
    {
        Gato animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

                // cargamos el nombre de la raza del gato
                Console.Write("Coloca la raza del gato: ");
                string razaTemp = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(razaTemp))
                {
                    throw new CamposCreacionIncorrectosException("La raza del gato no puede estar vacia\n");
                }
                else if (!razaTemp.All(char.IsLetter))
                {
                    throw new CamposCreacionIncorrectosException("La raza debe contener solo letras\n");
                }

                //creamos el gato
                // en el futuro lo vamos a agregar a una coleccion
                animalTemp = new Gato(nombreTemp, cliente, fecNacTemp, pesoTemp, sexoTemp, razaTemp);
                registroAnimal = true;
                Console.WriteLine();
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!registroAnimal);
        return animalTemp;
    }

    private Animal RegistroAve(bool registroAnimal)
    {
        int opcionNumericaEnums;
        Ave animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

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

                //creamos el ave
                // en el futuro lo vamos a agregar a una coleccion
                animalTemp = new Ave(nombreTemp, cliente, fecNacTemp, pesoTemp, sexoTemp, especieTemp);
                registroAnimal = true;
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!registroAnimal);
        return animalTemp;
    }

    private Animal RegistroRoedor(bool registroAnimal)
    {
        int opcionNumericaEnums;
        Roedor animalTemp = null;
        do
        {
            try
            {
                // llama al metodo para cargar los datos que comparten todos los animales
                MetodoInternoRegistroAnimal(out string nombreTemp, out Cliente cliente, out DateTime fecNacTemp, out double pesoTemp, out Animal.Genero sexoTemp);

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

                //creamos el roedor
                // en el futuro lo vamos a agregar a una coleccion
                animalTemp = new Roedor(nombreTemp, cliente, fecNacTemp, pesoTemp, sexoTemp, especieTemp);
                registroAnimal = true;
            }
            catch (CamposCreacionIncorrectosException e)
            {
                Console.WriteLine("\nError: " + e.Message);
            }
            catch (Exception)// por si se produce un fallo inesperado
            {
                Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
            }
        } while (!registroAnimal);
        return animalTemp;
    }

    public string EliminarAnimal() //metodo de la interfaz IRegistrosVeterinario
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

    public string EliminarCliente() //metodo de la interfaz IRegistrosVeterinario
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
        return "Metodo sin implementar!!!\n";
    }//metodo de la interfaz IFacturacion

    public string CrearFactura() //metodo de la interfaz IFacturacion
    {
        // variable para el do-while
        bool boolCrearFactura = false;
        do
        {
            Factura factura = new Factura();
            factura.Servicio = Factura.Servicios.NoDefinido;
            Console.WriteLine("---Nueva Factura---");
            Console.WriteLine("Numero de factura: " + Factura.ultimoNumeroFactura); //imprime el numero de factura 
            Console.Write("Ingresa el cliente(apreta enter temporalmente): ");
            factura.Cliente = null; // esta en null para permitir seguir la ejecucion del codigo hasta que agregue colecciones para validar
            Console.ReadLine();// va a ir un try-catch
            Console.Write("Ingresa el animal(apreta enter temporalmente): ");
            factura.Animal = null; // esta en null para permitir seguir la ejecucion del codigo hasta que agregue colecciones para validar
            Console.ReadLine();// va a ir un try-catch
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
                            factura.Servicio = Factura.Servicios.Revision;
                            factura.CalcularPrecio(factura.Servicio);
                            break;
                        case 2:
                            factura.Servicio = Factura.Servicios.Cirujia;
                            factura.CalcularPrecio(factura.Servicio);
                            break;
                        case 3:
                            factura.Servicio = Factura.Servicios.ControlCompleto;
                            factura.CalcularPrecio(factura.Servicio);
                            break;
                        case 4:
                            factura.Servicio = Factura.Servicios.Vacunacion;
                            factura.CalcularPrecio(factura.Servicio);
                            break;
                        case 5:
                            factura.Servicio = Factura.Servicios.Laboratorio;
                            factura.CalcularPrecio(factura.Servicio);
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
            } while (factura.Servicio == Factura.Servicios.NoDefinido);
            Console.WriteLine("El precio total por el servicio es de: $" + factura.Precio + "\n");
            if (/*factura.Cliente == null || factura.Animal == null || comentado temporalmente hasta que agregue colecciones*/ factura.Servicio == Factura.Servicios.NoDefinido)
            {
                Factura.ultimoNumeroFactura--;
            }
            else
            {
                boolCrearFactura = true;
            }
        } while (!boolCrearFactura);

        return "Factura realizada con exito \n";
    }

    public string ImprimirFactura() //metodo de la interfaz IFacturacion
    {
        //Console.Write("Coloca el numero de factura a buscar: ");
        //int numeroFactura = Convert.ToInt32(Console.ReadLine());
        // va a realizar una busqueda dentro de la estructura de datos con nroFactura
        //Factura factura = a la factura que obtuve por la busqeuda del nroFactura
        //return factura.ToString();
        return "Metodo sin implementar!!!\n";
    }

    public void MostrarFacturas() //metodo de la interfaz IFacturacion
    {
        // Cuando veamos colecciones se va a implementar el metodo.
        // va a mostrar, el numero de factura, la fecha y el cliente.
        // se va a poder acceder a una de ellas a la vez para mostrar el detalle
    }

    public string CambiarPrecioServicio() //metodo de la interfaz IFacturacion
    {
        int opcionCPNumerica = 0;
        do
        {
            try
            {
                Console.WriteLine("---Selecciona el servicio a modificar su precio---");
                Console.WriteLine("1-Revision");
                Console.WriteLine("2-Cirugia");
                Console.WriteLine("3-Control Completo");
                Console.WriteLine("4-Vacunacion");
                Console.WriteLine("5-Laboratorio");
                Console.WriteLine("6-Volver");
                Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                string opcionCP = Console.ReadLine().Trim();
                if (!int.TryParse(opcionCP, out opcionCPNumerica))
                {
                    throw new SeleccionarOpcionException();
                }
                Console.WriteLine();
                switch (opcionCPNumerica)
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
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Ingresa una opcion valida\n");
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
        } while (opcionCPNumerica != 6);
        return "Volviendo...\n";
    }

    //Metodos para Consultar datos
    public string ConsultarInfoCliente() //metodo de la interfaz IConsultarInfo
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
        return "Metodo sin implementar!!!\n";
    }

    public string ConsultarInfoAnimal() //metodo de la interfaz IConsultarInfo
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

    //Metodos de modificacion de informacion
    public string ModificarInfoCliente() //metodo de la interfaz IModificarInfo
    {
        // aca va a realizar una busqueda de un cliente en una coleccion
        // y va a tener la posiblidad de cambiar la informacion de un cliente
        return "Metodo sin implementar!!!\n";
    }

    public string ModificarInfoAnimal()  //metodo de la interfaz IModificarInfo
    {
        // aca va a realizar una busqueda de un animal en una coleccion
        // y va a tener la posiiblidad de cambiar la informacion de un animal
        return "Metodo sin implementar!!!\n";
    }

    //Metodos de veterinario para su objeto propio
    public string ModificarInformacion(Veterinario veterinario)
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
                        else if (string.IsNullOrEmpty(nombUsuTemp) || nombUsuTemp.Length < 4)
                        {
                            throw new ArgumentException("El nombre de usuario nuevo no puede estar vacio y debe tener al menos 4 caracteres\n");
                        }
                        else
                        {
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
                        else if (string.IsNullOrEmpty(contTemp) || contTemp.Length < 6)
                        {
                            throw new ArgumentException("La contraseña nueva no puede estar vacia y debe tener 6 o mas caracteres\n");
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
                        else if (string.IsNullOrEmpty(nombTemp) || nombTemp.Length < 2)
                        {
                            throw new ArgumentException("Tu nuevo nombre no puede estar vacio y debe tener 2 o mas letras\n");
                        }
                        else if (!nombTemp.All(char.IsLetter))
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
                        else if (string.IsNullOrEmpty(apeTemp) || apeTemp.Length < 2)
                        {
                            throw new ArgumentException("Tu nuevo apellido no puede estar vacio y debe tener 2 o mas letras\n");
                        }
                        else if (!apeTemp.All(char.IsLetter))
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
                        int numTelTempNum;
                        if (string.IsNullOrEmpty(numTelTemp) || numTelTemp.Length < 6)
                        {
                            throw new ArgumentException("Tu numero de telefono no puede estar vacio y debe tener 6 o mas numeros\n");
                        }
                        if (!int.TryParse(numTelTemp, out numTelTempNum))
                        {
                            throw new ArgumentException("Tu nuevo numero de telefono debe contener solo numeros\n");
                        }
                        else if (numTelTempNum == veterinario.NroTelefono)
                        {
                            throw new ArgumentException("No podes poner el mismo numero de telefono\n");
                        }
                        else
                        {
                            veterinario.NroTelefono = numTelTempNum;
                            Console.WriteLine("Tu numero de telefono ahora es: " + veterinario.NroTelefono + "\n");
                        }
                        break;
                    case 6:
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

    public override string ToString()
    {
        return "---Informacion Veterinario---" + "\n Usuario: " + usuario + "\n Nombre: " + nombre + "\n Apellido: " + apellido
            + "\n Numero de Telefono: " + nroTelefono + "\n";
    }
}

