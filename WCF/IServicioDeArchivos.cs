using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioDeArchivos
    {

        [OperationContract]
        bool cargarUsuarios(string archivo, string tipo);

        [OperationContract]
        List<SolicitanteDTO> FindAllSolicitantes();

        [OperationContract]
        List<ProyectoDTO> FindAllProyectos();

    }
    [DataContract]
    public class SolicitanteDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Ci { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Apellido { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public DateTime FechaNacimiento { get; set; }
        [DataMember]
        public string Celular { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Rol { get; set; }
    }
    [DataContract]
    public class ProyectoDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public double Monto { get; set; }
        [DataMember]
        public double SaldoRestanteFinanciar { get; set; }
        [DataMember]
        public int CuotasAPagar { get; set; }
        [DataMember]
        public double TasaInteresSegunCuota { get; set; }
        [DataMember]
        public double MontoCuotaSinInt { get; set; }
        [DataMember]
        public double MontoTotalConIntereses { get; set; }
        [DataMember]
        public double MontoCuotaIntInc { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public DateTime FechaPresentacion { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public int SolicitanteId { get; set; }
        [DataMember]
        public virtual SolicitanteDTO Solicitante { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string ExpertisSolicitante { get; set; }
        [DataMember]
        public int CantIntegrantes { get; set; }


    }
}
