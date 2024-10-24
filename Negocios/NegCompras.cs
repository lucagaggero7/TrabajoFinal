using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegCompras
    {
        DatosCompras objDatosCompras = new DatosCompras();

        public int Compra(string accion, int idUsuario , string listaProuductos)
        {
            return objDatosCompras.Compra(accion, idUsuario, listaProuductos);
        }
    }
}
