using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HTTPupt;
using Newtonsoft;

namespace Historiales_Medicos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variables
            bool flag = true;
            string option;
            List<Historial> ListaHistoriales;
            //Instrucciones
            while(flag == true)
            {
                Console.WriteLine("............................");
                Console.WriteLine(".-------SALUD PRIMERO------.\n............................\n");
                Console.WriteLine("Por favor ingrese la opción que desea realizar \n 1.Consultar historial cliníco del paciente \n 2.Registrar nuevo paciente. \n 3.Salir");
                Console.WriteLine("++++++++++++++++++++++++++++++++");
                option = Console.ReadLine();
                while (option != "1" && option != "2" && option != "3")
                {
                    Console.WriteLine("++++++++++++++++++++++++++++++++");
                    Console.WriteLine("Opcion no valida");
                    Console.WriteLine("Por favor ingrese la opción que desea realizar \n 1.Consultar historial cliníco del paciente \n 2.Registrar nuevo paciente. \n 3.Salir");
                    Console.WriteLine("++++++++++++++++++++++++++++++++");
                    option = Console.ReadLine();
                }
                switch (option)
                {
                    case "1":
                        {
                            Console.WriteLine("Trabajo en proceso");
                        }
                        break;
                    case "2":
                        {
                            RegistroNuevo(GetFecha(),GetHora());
                        }
                        break;
                    case "3":
                        {
                            flag = false;
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Ha ocurrido un error inesperado porfavor contacte a servicio tecnico");
                        }
                        break;
                }
                Console.Clear();
            }
        }

        static void ConsultarRegistro()
        {

        }

        private static string GetFecha()
        {
            return DateTime.Now.ToString("dd_MM_yy");
        }
        private static string GetHora()
        {
            return DateTime.Now.ToString("hh_mm");
        }

        static void RegistroNuevo(string Fecha,string Hora)
        {
            Historial Paciente = new Historial(Fecha,Hora);
            Console.WriteLine("Ingrese el nombre del paciente");
            Paciente.NombrePaciente = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido paterno del paciente");
            Paciente.ApellidoP = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido materno del paciente");
            Paciente.ApellidoM = Console.ReadLine();
            Console.WriteLine("Ingrese la Edad del paciente");
            Paciente.Edad = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese Fecha de Nacimiento (Formato: 'dd_mm_aa')");
            Paciente.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Ingrese el sexo del paciente");
            Paciente.Sexo = Console.ReadLine();
            Paciente.IdRegistro = GenerarIdRegistro(Paciente);
            Console.WriteLine("Desea ingresar el numero telefonico del paciente? \n 1. Si \n 2. No");
            int flag = int.Parse(Console.ReadLine());
            while (flag != 1 && flag != 2)
            {
                Console.WriteLine("Opcion no valida");
                flag = int.Parse(Console.ReadLine());
            }
            if(flag == 1)
            {
                Console.WriteLine("Ingrese el numero telefonico");
                Paciente.NumeroTelefono = Console.ReadLine();
            }
            Console.WriteLine("Ingrese el tipo de sangre del paciente (Ejemplo: O positivo)");
            Paciente.TipoSangre = Console.ReadLine();
            Console.WriteLine("Ingrese los antecedentes medicos");
            Paciente.AntecedenteMedico = Console.ReadLine();
            Console.WriteLine("Ingrese el diagnostico actual");
            Paciente.Diagnostico = Console.ReadLine();
            Console.WriteLine("Ingrese las indicaciones de tratamiento");
            Paciente.Indicaciones = Console.ReadLine();
            GuardarRegistro(Paciente);
            Console.WriteLine("Registro creado y guardado con exito con la id: \n" + Paciente.IdRegistro + "\n Presione una tecla para continuar");
            Console.ReadKey();
        }
        static void GuardarRegistro(Historial Paciente)
        {
            string NombreArchivo = "Registro" + Paciente.IdRegistro;
            FileStream Archivo = new FileStream(@".\" + NombreArchivo + ".txt", FileMode.CreateNew, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(Archivo);
            String json = JsonConvertidor.Objeto_Json(Paciente);
            Escribir.WriteLine(json);
        }
        static string GenerarIdRegistro(Historial Paciente)
        {
            string id = Char.ToString(Paciente.ApellidoP.FirstOrDefault()) + Char.ToString(Paciente.ApellidoM.FirstOrDefault()) + Paciente.FechaRegistro;
            return id;
        }
    }
}
