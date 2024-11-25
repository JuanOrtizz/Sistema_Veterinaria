using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRegistrosVeterinario
{
    public string RegistrarCliente(string path);
    public string EliminarCliente(string path);
    public string RegistrarAnimal(string path);
    public string EliminarAnimal(string path);
}

