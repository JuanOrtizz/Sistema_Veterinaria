using static System.Runtime.InteropServices.JavaScript.JSType;

class Program // mostramos la clase junto con el main.
{
    public static Dictionary<string, Veterinario> veterinarios = new Dictionary<string, Veterinario>();

    static void Main()
    {
        
        bool boolInicioSistema = false;
        int opcionInicioSistNum;
        Veterinario veterinario = null;
        bool boolSistema = false;
        do
        {

            do
            {
                try
                {
                    Console.WriteLine("---Bienvenido---");
                    Console.WriteLine("1-Iniciar Sesion");
                    Console.WriteLine("2-Registrarse");
                    Console.WriteLine("3-Salir del sistema");
                    Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                    string opcionInicioSist = Console.ReadLine().Trim();
                    Console.WriteLine();
                    if (!int.TryParse(opcionInicioSist, out opcionInicioSistNum))
                    {
                        throw new SeleccionarOpcionException();
                    }
                    switch (opcionInicioSistNum)
                    {
                        case 1:
                            bool login = false;
                            Console.WriteLine("---Bienvenido al sistema---");
                            Console.WriteLine("---Inicia Sesion---");
                            int contadorLogin = 0;
                            int contadorBloquearPrograma = 0;
                            string usuarioLogin = "";
                            string contraseñaLogin = "";
                            do
                            {
                                try
                                {
                                    //momentaneamente si todavia no se registro, va a lanzar lo siguiente
                                    if (veterinarios.Count == 0)
                                    {
                                        Console.WriteLine("No hay veterinarios registrados en el sistema, registrate!\n");
                                        break;
                                    }

                                    //ingresa el usuario su nombre de usuario
                                    Console.Write("Coloca tu usuario: "); // aca va a verificar en una bdd o archivo txt/xml/json, si existe el nombre de usuario y si existe verifica si la contraseña es valida
                                    usuarioLogin = Console.ReadLine().Trim();
                                    if (string.IsNullOrEmpty(usuarioLogin))
                                    {
                                        throw new ArgumentException("Tu usuario no puede estar vacio\n");
                                    }

                                    // ingresa el usuario su contraseña
                                    Console.Write("Coloca tu contraseña: ");
                                    contraseñaLogin = Console.ReadLine().Trim();
                                    if (string.IsNullOrEmpty(contraseñaLogin))
                                    {
                                        throw new ArgumentException("Tu contraseña no puede estar vacia\n");
                                    }
                                    Console.WriteLine();
                                    veterinarios.TryGetValue(usuarioLogin, out Veterinario value);

                                    if (!veterinarios.ContainsKey(usuarioLogin) || contraseñaLogin != value.Contraseña)
                                    {
                                        if(contadorBloquearPrograma < 3)
                                        {
                                            contadorLogin++;
                                            if (contadorLogin < 5)
                                            {
                                                Console.WriteLine("Credenciales incorrecta");
                                                Console.WriteLine("Coloca bien tus credenciales.");
                                                Console.WriteLine("Te quedan " + (5 - contadorLogin) + " intentos. \n");
                                            }
                                            else //si no puede iniciar sesion, le da este mensaje por consola
                                            {
                                                contadorBloquearPrograma++;
                                                Console.WriteLine("Demasiados intentos fallidos. Vuelve a intentarlo en 30 segundos\n");
                                                
                                                //cambio el color a rojo en la consola
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("¡ADVERTENCIA! Si tenes muchos intentos fallidos, el tiempo de espera va a aumentar. ");
                                                // pongo de nuevo el color en blanco 
                                                Console.ResetColor();

                                                for (int i = 30; i > 0; i--)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                    Console.Write("\rEspera " + i + " segundos..."); // \r regresa al inicio y la sobrescribe
                                                    System.Threading.Thread.Sleep(1000); // pausa de 1 segundo
                                                    Console.ResetColor();
                                                }

                                                Console.WriteLine("\nPodes intentar iniciar sesion de nuevo.\n");
                                                contadorLogin = 0; //reinicio el contador
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Demasiados intentos fallidos. Vuelve a intentarlo en ");
                                            for (int i = 120; i > 0; i--)
                                            {
                                                int minutos = i / 60;
                                                int segundos = i % 60;
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.Write("\rEspera " + minutos.ToString("D2") + ":" + segundos.ToString("D2") + " minutos..."); // \r regresa al inicio y la sobrescribe
                                                System.Threading.Thread.Sleep(1000); // pausa de 1 segundo
                                                Console.ResetColor();
                                            }
                                            Console.WriteLine("\nPodes intentar iniciar sesion de nuevo.\n");
                                            contadorBloquearPrograma--;
                                        }
                                        
                                    }
                                    else
                                    {
                                        veterinario = veterinarios[usuarioLogin];
                                        Console.WriteLine("Iniciaste sesion!\n");
                                        login = true;
                                        boolInicioSistema = true;
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
                            } while (!login);
                            break;
                        case 2:
                            bool registroBool = false;
                            do
                            {
                                try
                                {
                                    Console.WriteLine("---Bienvenido al registro del sistema---");

                                    // Guarda el nombre de usuario en una variable para luego crear el objeto Veterinario
                                    Console.Write("Ingresa un nombre de usuario: ");
                                    string usuarioTemp = Console.ReadLine().Trim();
                                    if (string.IsNullOrEmpty(usuarioTemp))
                                    {
                                        throw new CamposCreacionIncorrectosException("El nombre de usuario no puede estar vacio\n");
                                    }
                                    else if (usuarioTemp.Length < 4)
                                    {
                                        throw new CamposCreacionIncorrectosException("El nombre de usuario debe tener al menos 4 caracteres\n");
                                    }
                                    else if (veterinarios.ContainsKey(usuarioTemp))
                                    {
                                        throw new CamposCreacionIncorrectosException("El nombre de usuario " + usuarioTemp +" ya esta en uso\n");
                                    }

                                    // Guarda la contraseña en una variable para luego crear el objeto Veterinario
                                    Console.Write("Ingresa una constraseña: ");
                                    string contraseñaTemp = Console.ReadLine().Trim();
                                    if (contraseñaTemp.Length < 6)
                                    {
                                        throw new CamposCreacionIncorrectosException("La contraseña debe tener 6 o mas caracteres\n");
                                    }

                                    // Guarda el nombre en una variable para luego crear el objeto Veterinario
                                    Console.Write("Ingresa tu nombre: ");
                                    string nombreTemp = Console.ReadLine().Trim();
                                    if (nombreTemp.Length < 2)
                                    {
                                        throw new CamposCreacionIncorrectosException("Tu nombre debe tener 2 o mas letras\n");
                                    }
                                    else if (!nombreTemp.All(char.IsLetter))
                                    {
                                        throw new CamposCreacionIncorrectosException("Tu nombre debe contener solo letras\n");
                                    }

                                    // Guarda el apellido en una variable para luego crear el objeto Veterinario
                                    Console.Write("Ingresa tu apellido: ");
                                    string apellidoTemp = Console.ReadLine().Trim();
                                    if (apellidoTemp.Length < 2)
                                    {
                                        throw new CamposCreacionIncorrectosException("Tu apellido debe tener 2 o mas letras\n");
                                    }
                                    else if (!apellidoTemp.All(char.IsLetter))
                                    {
                                        throw new CamposCreacionIncorrectosException("Tu apellido debe contener solo letras\n");
                                    }

                                    // Guarda el numero de telefono en una variable para luego crear el objeto Veterinario
                                    Console.Write("Ingresa tu numero de telefono: ");
                                    string nroTelefonoString = Console.ReadLine().Trim();
                                    long nroTelefonoTemp;
                                    if (nroTelefonoString.Length < 6)
                                    {
                                        throw new CamposCreacionIncorrectosException("Tu numero de telefono debe tener 6 o mas numeros\n");
                                    }
                                    else if (!long.TryParse(nroTelefonoString, out nroTelefonoTemp))
                                    {
                                        throw new CamposCreacionIncorrectosException("Tu numero de telefono debe contener solo numeros\n");
                                    }
                                    Console.WriteLine();

                                    // Crea el objeto Veterinario
                                    Veterinario veterinarioRegistro = new Veterinario(usuarioTemp, contraseñaTemp, nombreTemp, apellidoTemp, nroTelefonoTemp);
                                    Console.WriteLine("Te has registrado\n");
                                    veterinarios.Add(usuarioTemp, veterinarioRegistro);

                                    registroBool = true;
                                }
                                catch (CamposCreacionIncorrectosException e)
                                {
                                    Console.WriteLine("\nError: " + e.Message);
                                }
                                catch (Exception)// por si se produce un fallo inesperado
                                {
                                    Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
                                }
                            } while (!registroBool);
                            break;
                        case 3:
                            int opcionSalirNum = 0;
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Estas seguro que queres salir?");
                                    Console.WriteLine("1-Si");
                                    Console.WriteLine("2-Volver");
                                    Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                                    string opcionSalir = Console.ReadLine().Trim();
                                    if (!int.TryParse(opcionSalir, out opcionSalirNum))
                                    {
                                        throw new SeleccionarOpcionException();
                                    }
                                    Console.WriteLine();
                                    switch (opcionSalirNum)
                                    {
                                        case 1:
                                            Console.WriteLine("Hasta luego!");
                                            Environment.Exit(0);
                                            break;
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
                                catch (Exception)// por si se produce un fallo inesperado
                                {
                                    Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
                                }
                            } while (opcionSalirNum != 2);
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
                catch (Exception)// por si se produce un fallo inesperado
                {
                    Console.WriteLine("\nSe produjo un error, intentalo de nuevo\n");
                }
            } while (!boolInicioSistema);


            // menu con opciones del veterinario una vez logeado
            if (veterinario != null)
            {
                bool menuSalir = false;
                do
                {
                    try
                    {
                        Console.WriteLine("---Menu de Opciones---");
                        Console.WriteLine("1-Registros");
                        Console.WriteLine("2-Facturacion");
                        Console.WriteLine("3-Eliminacion");
                        Console.WriteLine("4-Consultar datos");
                        Console.WriteLine("5-Modificar datos");
                        Console.WriteLine("6-Gestion de historiales y vacunas");
                        Console.WriteLine("7-Cerrar sesion");
                        Console.Write("Coloca tu opcion (NUMERICA) aqui: ");
                        string opcionMenu = Console.ReadLine().Trim();
                        int opcionMenuNumerica;
                        if (!int.TryParse(opcionMenu, out opcionMenuNumerica))
                        {
                            throw new SeleccionarOpcionException();
                        }
                        Console.WriteLine();

                        Func<string> veterinarioDelegate = null; //delegate Func para metodos con retorno string
                        int opcionIntMenuNum = 0;
                        string opcionIntMenu;
                        switch (opcionMenuNumerica)
                        {
                            case 1:
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("---Menu Interno Registros---");
                                        Console.WriteLine("1-Registrar Cliente");
                                        Console.WriteLine("2-Registrar Animal");
                                        Console.WriteLine("3-Volver");
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
                                            string mensaje = veterinarioDelegate(); // llamamos al delegado e imprimimos el mensaje return
                                            Console.WriteLine(mensaje);
                                        }
                                    }
                                    catch (SeleccionarOpcionException e)
                                    {
                                        Console.WriteLine("\nError: " + e.Message);
                                    }
                                } while (opcionIntMenuNum != 3);
                                break;

                            case 2:
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("---Menu Interno Facturacion---");
                                        Console.WriteLine("1-Crear nueva factura");
                                        Console.WriteLine("2-Imprimir factura");
                                        Console.WriteLine("3-Eliminar factura");
                                        Console.WriteLine("4-Visualizar todas las facturas");
                                        Console.WriteLine("5-Cambiar precio de un servicio");
                                        Console.WriteLine("6-Volver");
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
                                                veterinario.MostrarFacturas();
                                                break;
                                            case 5:
                                                veterinarioDelegate = veterinario.CambiarPrecioServicio;
                                                break;
                                            case 6:
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
                                            string mensaje = veterinarioDelegate(); // llamamos al delegado e imprimimos el mensaje return
                                            Console.WriteLine(mensaje);
                                        }
                                    }
                                    catch (SeleccionarOpcionException e)
                                    {
                                        Console.WriteLine("\nError: "+ e.Message);
                                    }
                               
                                } while (opcionIntMenuNum != 6);
                                break;

                            case 3:
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("---Menu Interno Eliminacion---");
                                        Console.WriteLine("1-Eliminar Cliente");
                                        Console.WriteLine("2-Eliminar Animal");
                                        Console.WriteLine("3-Volver"); ;
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
                                                Console.WriteLine("Opcion no valida. Ingresa una opcion valida!" + "\n");
                                                break;
                                        }
                                        if (veterinarioDelegate != null)
                                        {
                                            string mensaje = veterinarioDelegate(); // llamamos al delegado e imprimimos el mensaje return
                                            Console.WriteLine(mensaje);
                                        }
                                    }
                                    catch (SeleccionarOpcionException e)
                                    {
                                        Console.WriteLine("\nError: " + e.Message);
                                    }
                                
                                } while (opcionIntMenuNum != 3);
                                break;

                            case 4:
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("---Menu Interno Consultas---");
                                        Console.WriteLine("1-Consultar tu Informacion (Veterinario)");
                                        Console.WriteLine("2-Consultar Informacion de un cliente");
                                        Console.WriteLine("3-Consultar Informacion de un animal");
                                        Console.WriteLine("4-Volver");
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
                                            string mensaje = veterinarioDelegate(); // llamamos al delegado e imprimimos el mensaje return
                                            Console.WriteLine(mensaje);
                                        }
                                    }
                                    catch (SeleccionarOpcionException e)
                                    {
                                        Console.WriteLine("\nError: " + e.Message);
                                    }
                                } while (opcionIntMenuNum != 4);
                                break;

                            case 5:
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("---Menu Interno Modificacion Datos---");
                                        Console.WriteLine("1-Modificar tus datos");
                                        Console.WriteLine("2-Modificar datos de un cliente");
                                        Console.WriteLine("3-Modificar datos de un animal");
                                        Console.WriteLine("4-Volver");
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
                                                string mensaje = veterinario.ModificarInformacion(veterinario);
                                                Console.WriteLine(mensaje);
                                                break;
                                            case 2:
                                                veterinarioDelegate = veterinario.ModificarInfoCliente;
                                                break;
                                            case 3:
                                                veterinarioDelegate = veterinario.ModificarInfoAnimal;
                                                break;
                                            case 4:
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
                                            string mensaje = veterinarioDelegate(); // llamamos al delegado e imprimimos el mensaje return
                                            Console.WriteLine(mensaje);
                                        }
                                    }
                                    catch (SeleccionarOpcionException e)
                                    {
                                        Console.WriteLine("\nError: " + e.Message);
                                    }

                                } while (opcionIntMenuNum != 4);
                                break;

                            case 6:
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("---Menu Interno Gestion Historiales y Vacunas---");
                                        Console.WriteLine("1-Actualizar las vacunas de un animal");
                                        Console.WriteLine("2-Actualizar el historial de un animal");
                                        Console.WriteLine("3-Volver");
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
                                                veterinarioDelegate = veterinario.ModificarVacunas;
                                                break;
                                            case 2:
                                                veterinarioDelegate = veterinario.ActualizarHistorial;
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
                                            string mensaje = veterinarioDelegate(); // llamamos al delegado e imprimimos el mensaje return
                                            Console.WriteLine(mensaje);
                                        }
                                    }
                                    catch (SeleccionarOpcionException e)
                                    {
                                        Console.WriteLine("\nError: " + e.Message);
                                    }

                                } while (opcionIntMenuNum != 3);
                                break;

                            case 7:
                                bool boolSalir = false;
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("Estas seguro que queres salir?");
                                        Console.WriteLine("1-Si");
                                        Console.WriteLine("2-Volver");
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
                                                Console.WriteLine("Hasta luego " + veterinario.Nombre + "!\n");
                                                menuSalir = true;
                                                boolSalir = true;
                                                veterinario = null;
                                                break;
                                            case 2:
                                                Console.WriteLine("Volviendo...\n");
                                                boolSalir = true;
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

                                } while (!boolSalir);
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
                } while (!menuSalir);
            }
        } while (!boolSistema);
    }
}