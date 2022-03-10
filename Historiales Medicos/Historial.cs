using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Historiales_Medicos
{
    public class Historial
    {
        public string NombrePaciente;
        public string ApellidoP;
        public string ApellidoM;
        public string IdRegistro;
        public string Sexo;
        public int Edad;
        public string NumeroTelefono = "";
        public string FechaNacimiento;
        public string FechaRegistro;
        public string HoraRegistro;
        public string TipoSangre;
        public string Diagnostico;
        public string Indicaciones;
        public string AntecedenteMedico;

        public Historial()
        {

        }
        public Historial(string FechaRegistro,string HoraRegistro)
        {
            this.FechaRegistro = FechaRegistro;
            this.HoraRegistro = HoraRegistro;
        }
    }
}
