using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IFacturacion
{
    public string CrearFactura(string path);
    public string EliminarFactura(string path);
    public string ImprimirFactura();
    public void MostrarFacturas();
    public string CambiarPrecioServicio();
}
