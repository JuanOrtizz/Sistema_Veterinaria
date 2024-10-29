class Program // mostramos la clase junto con el main.
{
    static void Main()
    {
        //Registrarse, lo hacemos temporal a esto ya que no queda almacenado en ningun lado los datos con las credenciales.
        Veterinario veterinario = new Veterinario();

        //Inicio de sesion
        bool login = false;
        Console.WriteLine("---Bienvenido al sistema---");
        Console.WriteLine("---Inicia Sesion---");
        int contadorLogin = 0;
        string usuarioLogin = "";
        string contraseñaLogin = "";
        do
        {
            Console.Write("Coloca tu usuario: ");
            usuarioLogin = Console.ReadLine();
            Console.Write("Coloca tu contraseña: ");
            contraseñaLogin = Console.ReadLine();
            Console.WriteLine();
            if (veterinario.Usuario == usuarioLogin && veterinario.Contraseña == contraseñaLogin)
            {
                Console.WriteLine("Iniciaste sesion!");
                Console.WriteLine();
                login = true;
            }
            else
            {
                contadorLogin++;
                if (contadorLogin < 5)
                {
                    Console.WriteLine("Usuario o contraseña incorrecto");
                    Console.WriteLine("Coloca bien tus credenciales.");
                    Console.WriteLine("Te quedan " + (5 - contadorLogin)  + " intentos.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Demasiados intentos fallidos. Vuelve a intentarlo mas tarde.");
                    Environment.Exit(0);
                }
            }
        } while (!login);

        // menu con opciones del veterinario una vez logeado
        bool menuSalir = false;
        do
        {
            Console.WriteLine("---Menu de Opciones---");
            Console.WriteLine("1-Registros");
            Console.WriteLine("2-Facturacion");
            Console.WriteLine("3-Eliminacion");
            Console.WriteLine("4-Consultar datos");
            Console.WriteLine("5-Modificar datos");
            Console.WriteLine("6-Salir del sistema");
            Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
            int opcionMenu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Func<string> veterinarioDelegate = null; //delegate Func para metodos con retorno string
            int opcionIntMenu = 0;

            switch (opcionMenu)
            {
                case 1:    
                    do {
                        Console.WriteLine("---Menu Interno Registros---");
                        Console.WriteLine("1-Registrar Cliente");
                        Console.WriteLine("2-Registrar Animal");
                        Console.WriteLine("3-Volver");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        opcionIntMenu = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcionIntMenu)
                        {
                            case 1:
                                veterinarioDelegate = veterinario.RegistrarCliente;
                                break;
                            case 2:
                                veterinarioDelegate = veterinario.RegistrarAnimal;
                                break;
                            case 3:                               
                                veterinarioDelegate = null;
                                Console.WriteLine("Volviendo...\n");
                                break;
                            default:
                                veterinarioDelegate = null;
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                            break;
                        }
                        if (veterinarioDelegate != null)
                        {
                            string mensaje = veterinarioDelegate(); // llamamos al metodo e imprimimos el mensaje
                            Console.WriteLine(mensaje);
                        }
                    } while (opcionIntMenu != 3);
                break;

                case 2:
                    do
                    {
                        Console.WriteLine("---Menu Interno Facturacion---");
                        Console.WriteLine("1-Crear nueva factura");
                        Console.WriteLine("2-Imprimir factura (NO DISPONIBLE)");
                        Console.WriteLine("3-Eliminar factura (NO DISPONIBLE)");
                        Console.WriteLine("4-Visualizar todas las facturas (NO DISPONIBLE)");
                        Console.WriteLine("5-Volver");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        opcionIntMenu = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcionIntMenu)
                        {
                            case 1:
                                veterinarioDelegate = veterinario.CrearFactura;
                                break;
                            case 2:
                                veterinarioDelegate = veterinario.ImprimirFactura;
                                break;
                            case 3:
                                veterinarioDelegate = veterinario.EliminarFactura;
                                break;
                            case 4:
                                veterinarioDelegate = null;
                                Console.WriteLine("Metodo sin implementar!!!\n");
                                // Cuando tengamos las list, set, map o queue voy a entrar a esta para ver la informacion.
                                break;
                            case 5:
                                veterinarioDelegate = null;
                                Console.WriteLine("Volviendo...\n");
                                break;
                            default:
                                veterinarioDelegate = null;
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                break;
                        }
                        if (veterinarioDelegate != null)
                        {
                            string mensaje = veterinarioDelegate(); // llamamos al metodo e imprimimos el mensaje
                            Console.WriteLine(mensaje);
                        }
                    } while (opcionIntMenu != 5);
                    break;

                case 3:
                    do
                    {
                        Console.WriteLine("---Menu Interno Eliminacion---");
                        Console.WriteLine("1-Eliminar Cliente (NO DISPONIBLE)");
                        Console.WriteLine("2-Eliminar Animal (NO DISPONIBLE)");
                        Console.WriteLine("3-Volver");;
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        opcionIntMenu = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcionIntMenu)
                        {
                            case 1:
                                veterinarioDelegate = veterinario.EliminarCliente;
                                break;  
                            case 2:
                                veterinarioDelegate = veterinario.EliminarAnimal;
                                break;
                            case 3:
                                veterinarioDelegate = null;
                                Console.WriteLine("Volviendo...\n");
                                break;
                            default:
                                veterinarioDelegate = null;
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!" +"\n");
                                break;
                        }
                        if (veterinarioDelegate != null)
                        {
                            string mensaje = veterinarioDelegate(); // llamamos al metodo e imprimimos el mensaje
                            Console.WriteLine(mensaje);
                        }
                    } while (opcionIntMenu != 3);
                    break;

                case 4:
                    do
                    {
                        Console.WriteLine("---Menu Interno Consultas---");
                        Console.WriteLine("1-Consultar tu Informacion (Veterinario)");
                        Console.WriteLine("2-Consultar Informacion de un cliente (NO DISPONIBLE)");
                        Console.WriteLine("3-Consultar Informacion de un animal (NO DISPONIBLE)");
                        Console.WriteLine("4-Volver");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        opcionIntMenu = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcionIntMenu)
                        {
                            case 1:
                                veterinarioDelegate = veterinario.ToString;
                                break;
                            case 2:
                                veterinarioDelegate = veterinario.ConsultarInfoCliente;
                                break;
                            case 3:
                                veterinarioDelegate = veterinario.ConsultarInfoAnimal;
                                break;
                            case 4:
                                veterinarioDelegate = null;
                                Console.WriteLine("Volviendo...\n");
                                break;
                            default:
                                veterinarioDelegate = null;
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida\n");
                                break;
                        }
                        if (veterinarioDelegate != null)
                        {
                            string mensaje = veterinarioDelegate(); // llamamos al metodo e imprimimos el mensaje
                            Console.WriteLine(mensaje);
                        }
                    } while (opcionIntMenu != 4);
                    break;

                case 5:
                    do
                    {
                        Console.WriteLine("---Menu Interno Modificacion Datos---");
                        Console.WriteLine("1-Modificar tus datos");
                        Console.WriteLine("2-Modificar datos de un cliente");
                        Console.WriteLine("3-Modificar datos de un animal");
                        Console.WriteLine("4-Volver");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        opcionIntMenu = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcionIntMenu)
                        {
                            case 1:
                                string mensaje = veterinario.ModificarInformacion(veterinario);
                                Console.WriteLine(mensaje);
                                break;
                            case 2:
                                
                                break;
                            case 3:
                                
                                break;
                            case 4:
                                Console.WriteLine("Volviendo...\n");
                                break;
                            default:
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                break;
                        }
                    } while (opcionIntMenu != 4);
                    break;
                case 6:
                    do
                    {
                        Console.WriteLine("Estas seguro que queres salir?");
                        Console.WriteLine("1-Si");
                        Console.WriteLine("2-Volver al menu");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        opcionIntMenu = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (opcionIntMenu)
                        {
                            case 1:
                                Console.WriteLine("Hasta luego!");
                                menuSalir = true;
                                Environment.Exit(0);
                                break;
                            case 2:
                                Console.WriteLine("Volviendo...\n");
                                break;
                            default:
                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                                break;
                        }
                    } while (opcionIntMenu != 2);
                    break;

                default:
                    Console.WriteLine("Opcion no valida. Ingresa una opcion valida!\n");
                    break;
            }
        } while (!menuSalir);
    }
}