using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ArchivosJSON
{

    // metodo para la escritura en el JSON de los veterinarios
    public void EscribirJsonDiccionarioVeterinarios<T>(string path, Dictionary<string, Veterinario> diccionario)
    {
        string json = JsonSerializer.Serialize(diccionario, new JsonSerializerOptions
        {
            WriteIndented = true,  // Activamos la indentación para que sea más legible
        });
        File.WriteAllText(path, json);
    }
    // metodo para la escritura en el JSON de las facturas
    public void EscribirJsonDiccionarioFacturas<T>(string path, Dictionary<int, Factura> diccionario)
    {
        string json = JsonSerializer.Serialize(diccionario, new JsonSerializerOptions
        {
            WriteIndented = true,  // Activamos la indentación para que sea más legible
        });
        File.WriteAllText(path, json);
    }

    // metodo para la lectura de veterinarios
    public T LeerJsonDiccionario<T>(string path)
    {
        string contenido = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(contenido);
    }

    // metodo para la escritura  en el JSON de los clientes y la informacion de sus mascotas
    public void EscribirJsonDiccionario<T>(string path,Dictionary<int,Cliente> diccionario) 
    {
        foreach (Cliente cliente in diccionario.Values)
        {
            foreach (var mascota in cliente.Mascotas)
            {
                // Esto asegura que cada mascota tenga un campo 'Tipo' con el nombre de la clase de la subclase
                if (mascota is Perro perro)
                {
                    // Si la mascota es un Perro, se puede agregar explícitamente el campo 'Tipo'
                    mascota.Tipo = "Perro";
                }
                else if (mascota is Gato gato)
                {
                    mascota.Tipo = "Gato";
                }
                else if (mascota is Ave ave)
                {
                    mascota.Tipo = "Ave";
                }
                else if (mascota is Roedor roedor)
                {
                    mascota.Tipo = "Roedor";
                }
            }
        }
        string json = JsonSerializer.Serialize(diccionario, new JsonSerializerOptions
        {
            WriteIndented = true,  // Activamos la indentación para que sea más legible
            Converters = { new AnimalConvertidor() } // Asegurarse de que el JsonConverter esté incluido
        });
        File.WriteAllText(path, json);
        // Serializar las mascotas con el tipo correcto
        
    }


    // metodo para la lectura de facturas y clientes para deserealizar el tipo de animal
    public T LeerJsonDiccionarioConAnimales<T>(string path)
    {
        string contenido = File.ReadAllText(path); 
        return JsonSerializer.Deserialize<T>(contenido, new JsonSerializerOptions
        {
            Converters = { new AnimalConvertidor() } // Registra el convertidor
        });
    }


}
