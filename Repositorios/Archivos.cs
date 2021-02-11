using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.IO;

namespace Datos
{
    public static class Archivos
    {
        public static bool leerArchivos(string nombreDelArchivo, string tipoEntidad)
        {
            try
            {

                bool retorno = false;
                string baseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "archivos/" + nombreDelArchivo);
                StreamReader sr = new StreamReader(baseFolder);
                string linea = sr.ReadLine();
                #region Usuarios
                if (tipoEntidad == "usuario")
                {

                    while (!String.IsNullOrEmpty(linea))
                    {

                        RepoSolicitante repoSolicitante = new RepoSolicitante();
                        char[] charSeparators = { '|', ' ' };
                        string[] usuarioEnArray = linea.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in usuarioEnArray) s.Trim();

                        if (retorno = repoSolicitante.FindByCi(usuarioEnArray[1]) == null)
                        {
                            int Id = Int32.Parse(usuarioEnArray[0]);
                            string Ci = usuarioEnArray[1];
                            string Nombre = usuarioEnArray[2];
                            string Apellido = usuarioEnArray[3];
                            string Rol = usuarioEnArray[4];
                            DateTime FechaNacimiento = DateTime.Parse(usuarioEnArray[5]);
                            string Celular = usuarioEnArray[6];
                            string Email = usuarioEnArray[7];

                            if (Rol == "Solicitante")
                            {
                                Solicitante solicitante = new Solicitante()
                                {
                                    Id = Id,
                                    Nombre = Nombre,
                                    Apellido = Apellido,
                                    Ci = Ci,
                                    Celular = Celular,
                                    Email = Email,
                                    Rol = Rol,
                                    FechaNacimiento = FechaNacimiento
                                };
                                solicitante.GeneratePassWord();
                                if (solicitante.IsValidUser())
                                {
                                    repoSolicitante.Add(solicitante);
                                }
                                else
                                {
                                    Console.WriteLine("Usuario no es valido para agregar");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usuario NO existe");
                        }
                        retorno = true;
                        linea = sr.ReadLine();
                    }
                }
                #endregion
                #region Proyectos
                if (tipoEntidad == "proyecto")
                {
                    while (!String.IsNullOrEmpty(linea))
                    {
                        RepoSolicitante repoSolicitante = new RepoSolicitante();
                        RepoProyecto repoProyecto = new RepoProyecto();
                        char[] charSeparators = { '|' };
                        string[] proyectoEnArray = linea.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in proyectoEnArray) s.Trim();

                        if (proyectoEnArray[8].ToString().Trim().Equals("APROBADO"))
                        {
                            if (repoSolicitante.FindById(Convert.ToInt32(proyectoEnArray[9])) != null)
                            {
                                if (proyectoEnArray[10].Trim().Equals("P"))
                                {
                                    ProyectoPersonal pPersonal = new ProyectoPersonal()
                                    {
                                        Id = Convert.ToInt32(proyectoEnArray[0]),
                                        Titulo = proyectoEnArray[1],
                                        Descripcion = proyectoEnArray[2],
                                        Monto = Convert.ToDouble(proyectoEnArray[3]),
                                        SaldoRestanteFinanciar = Convert.ToDouble(proyectoEnArray[3]),//Aun no se financio nada
                                        CuotasAPagar = Convert.ToInt32(proyectoEnArray[4]),
                                        TasaInteresSegunCuota = Convert.ToDouble(proyectoEnArray[5]),
                                        Imagen = proyectoEnArray[6],
                                        FechaPresentacion = Convert.ToDateTime(proyectoEnArray[7]).Date,
                                        Estado = "ABIERTO",
                                        SolicitanteId = Convert.ToInt32(proyectoEnArray[9]),
                                        ExpertisSolicitante = proyectoEnArray[11],
                                        Tipo = "P"
                                    };
                                    pPersonal.RealizamosCalculos();

                                    if (pPersonal.ValidoReglasNegocioProyecto())
                                    {
                                        retorno = repoProyecto.Add(pPersonal);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Proyecto no es valido para agregar");
                                    }
                                }
                                else
                                {
                                    ProyectoCooperativo pCooperativo = new ProyectoCooperativo()
                                    {
                                        Id = Convert.ToInt32(proyectoEnArray[0]),
                                        Titulo = proyectoEnArray[1],
                                        Descripcion = proyectoEnArray[2],
                                        Monto = Convert.ToDouble(proyectoEnArray[3]),
                                        SaldoRestanteFinanciar = Convert.ToDouble(proyectoEnArray[3]),//Aun no se financio nada
                                        CuotasAPagar = Convert.ToInt32(proyectoEnArray[4]),
                                        TasaInteresSegunCuota = Convert.ToDouble(proyectoEnArray[5]),
                                        Imagen = proyectoEnArray[6],
                                        FechaPresentacion = Convert.ToDateTime(proyectoEnArray[7]).Date,
                                        Estado = "ABIERTO",
                                        SolicitanteId = Convert.ToInt32(proyectoEnArray[9]),
                                        CantIntegrantes = Convert.ToInt32(proyectoEnArray[11]),
                                        Tipo = "C"
                                    };
                                    pCooperativo.RealizamosCalculos();
                                    if (pCooperativo.ValidoReglasNegocioProyecto())
                                    {
                                        repoProyecto.Add(pCooperativo);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Proyecto no es valido para agregar");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Proyecto: " + proyectoEnArray[1].ToUpper() + " no corresponde a ningun solicitante, " +
                                                    "por lo tanto no se incluira en la base");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Proyecto: " + proyectoEnArray[1].ToUpper() + " no aprobado, no se incluira en la base");
                        }
                        retorno = true;
                        linea = sr.ReadLine();
                    }
                }
                #endregion

                sr.Close();
                return retorno;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
