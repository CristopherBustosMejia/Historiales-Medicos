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
            List<Medico> ListaMedicos = new List<Medico>();
            CargarMedicos(ListaMedicos);
            //Instrucciones
            while(flag == true)
            {
                Console.WriteLine("............................");
                Console.WriteLine(".-------SALUD PRIMERO------.\n............................\n");
                Console.WriteLine("Por favor ingrese la opción que desea realizar \n 1. Registrar Medico \n 2. Consultar historial cliníco del paciente \n 3. Registrar nuevo paciente. \n 4. Salir");
                Console.WriteLine("++++++++++++++++++++++++++++++++");
                option = Console.ReadLine();
                while (option != "1" && option != "2" && option != "3" && option != "4")
                {
                    Console.WriteLine("++++++++++++++++++++++++++++++++");
                    Console.WriteLine("Opcion no valida");
                    Console.WriteLine("Por favor ingrese la opción que desea realizar \n 1. Registrar Medico \n 2. Consultar historial cliníco del paciente \n 3. Registrar nuevo paciente. \n 4. Salir");
                    Console.WriteLine("++++++++++++++++++++++++++++++++");
                    option = Console.ReadLine();
                }
                switch (option)
                {
                    case "1":
                        {
                            RegistarMedico();
                        }
                        break;
                    case "2":
                        {
                            ConsultarRegistro();
                        }
                        break;
                    case "3":
                        {
                            RegistroNuevo(GetFecha(),GetHora());
                        }
                        break;
                    case "4":
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

        static Historial ObtenerHistorial()
        {
            string Id;
            Console.WriteLine("Ingrese la Id del paciente:");
            Id = Console.ReadLine();
            Historial Registro = new Historial();
            try
            {
                FileStream Archivo = new FileStream(@".\Registro" + Id + ".txt", FileMode.Open, FileAccess.Read);
                StreamReader Leer = new StreamReader(Archivo);
                String Texto;
                while ((Texto = Leer.ReadLine()) != null)
                {
                    Registro = JsonConvertidor.Json_Objeto<Historial>(Texto);
                }
                Archivo.Close();
                Leer.Close();
                return Registro;
            } catch (Exception ex)
            {
                Console.WriteLine("No se encontro el archivo");
                return null;
            }
        }
        static void ConsultarRegistro()
        {
            Console.WriteLine(".-------SALUD PRIMERO------.\n..........Datos del Paciente..........\n");
            int Flag;
            Historial Paciente = new Historial();
            Paciente = ObtenerHistorial();
            if (Paciente != null)
            {
                Console.WriteLine("Nombre: " + Paciente.NombrePaciente);
                Console.WriteLine("Apellido Paterno: " + Paciente.ApellidoP);
                Console.WriteLine("Apellido Materno: " + Paciente.ApellidoM);
                Console.WriteLine("Edad: " + Paciente.Edad.ToString());
                Console.WriteLine("Sexo: " + Paciente.Sexo);
                Console.WriteLine("Id de registro: " + Paciente.IdRegistro);
                Console.WriteLine("Fecha de registro (dd_mm_aa): " + Paciente.FechaRegistro + "\n");
                Console.WriteLine("Desea ver el historial completo? \n 1. Si \n 2. No");
                Flag = int.Parse(Console.ReadLine());
                while (Flag != 1 && Flag != 2)
                {
                    Console.WriteLine("Opcion invalida");
                    Console.WriteLine("Desea ver el historial completo? \n 1. Si \n 2. No");
                    Flag = int.Parse(Console.ReadLine());
                }
                if (Flag == 1)
                {
                    HistorialCompleto(Paciente);
                }
            }
            Console.WriteLine("Presione una tecla para continuar");
            Console.ReadKey();
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
            Console.WriteLine(".-------SALUD PRIMERO------.\n..........Datos del Paciente..........\n");
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
                Console.WriteLine("Desea ingresar el numero telefonico del paciente? \n 1. Si \n 2. No");
                flag = int.Parse(Console.ReadLine());
            }
            if(flag == 1)
            {
                Console.WriteLine("Ingrese el numero telefonico");
                Paciente.NumeroTelefono = Console.ReadLine();
            }
            Console.WriteLine("Ingrese el tipo de sangre del paciente (Ejemplo: O positivo)");
            Paciente.TipoSangre = Console.ReadLine();
            Console.WriteLine("Ingrese los padecimientos congenitos del paciente");
            Paciente.PadecimientoC = Console.ReadLine();
            Console.WriteLine("Ingrese las lesiones de gravedad recientes (2 años o menos): ");
            Paciente.LesionesR = Console.ReadLine();
            Console.WriteLine("Ingrese las alergias del paciente");
            Paciente.Alergias = Console.ReadLine();
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
            Escribir.Close();
        }
        static string GenerarIdRegistro(Historial Paciente)
        {
            string id = Char.ToString(Paciente.ApellidoP.FirstOrDefault()) + Char.ToString(Paciente.ApellidoM.FirstOrDefault()) + Paciente.FechaRegistro;
            return id;
        }
        static string GenerarIdMedico(Medico Doctor)
        {
            string id = Char.ToString(Doctor.ApellidoP.FirstOrDefault()) + Char.ToString(Doctor.ApellidoM.FirstOrDefault()) + Doctor.AnioIngresion;
            return id;
        }
        static void GuardarMedico(Medico Doctor)
        {
            StreamWriter Escribir = new StreamWriter(@".\Medicos.txt", true);
            String json = JsonConvertidor.Objeto_Json(Doctor);
            Escribir.WriteLine(json);
            Escribir.Close();
        }
        static void RegistarMedico()
        {
            Console.WriteLine(".-------SALUD PRIMERO------.\n..............Datos del Medico............\n");
            bool Flag = false;
            string Confirmacion;
            Medico Doctor = new Medico();
            Console.WriteLine("Ingrese el nombre del medico: ");
            Doctor.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido paterno del medico: ");
            Doctor.ApellidoP = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido materno del medico: ");
            Doctor.ApellidoM = Console.ReadLine();  
            Console.WriteLine("Ingrese la cedula profesional del medico: ");
            Doctor.Cedula = Console.ReadLine();
            Console.WriteLine("Ingrese el año en que ingreso al hospital (Ejemplo: 1999): ");
            Doctor.AnioIngresion = Console.ReadLine();
            Doctor.IdMedico = GenerarIdMedico(Doctor);
            
            Doctor.Password = Console.ReadLine();
            while(Flag == false)
            {
                Console.WriteLine("La contraseña que ingreso es: " + Doctor.Password + "\n Desea guardar esta contraseña? \n 1. Si \n 2. No");
                Confirmacion = Console.ReadLine();
                while(Confirmacion != "1" && Confirmacion != "2")
                {
                    Console.WriteLine("Opcion no valida");
                    Console.WriteLine("Desea guardar esta contraseña? \n 1. Si \n 2. No");
                    Confirmacion = Console.ReadLine();
                }
                if(Confirmacion == "1")
                {
                    Flag = true;
                }
                else
                {
                    Console.WriteLine("Ingrese la contraseña que se guardara \n NOTA: Es muy importante que la contraseña sea de 6 digitos o mas");
                    Doctor.Password = Console.ReadLine();
                }
            }
            GuardarMedico(Doctor);
            Console.WriteLine("Registro creado y guardado con exito con la id: \n" + Doctor.IdMedico + "\n Presione una tecla para continuar");
            Console.ReadKey();
        }
        static void CargarMedicos(List<Medico>ListaMedicos)
        {
            Medico Doctor = new Medico();
            FileStream Archivo =  new FileStream(@".\Medicos.txt", FileMode.Open, FileAccess.Read);
            StreamReader Leer = new StreamReader(Archivo);
            String Texto;
            while ((Texto = Leer.ReadLine()) != null)
            {
                Doctor = JsonConvertidor.Json_Objeto<Medico>(Texto);
                ListaMedicos.Add(Doctor);
            }
            Archivo.Close();
            Leer.Close();
        }
        static void HistorialCompleto(Historial Paciente)
        {
            Console.WriteLine("Trabajo en proceso");
        }
    }
}
