﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class ProyectoCooperativo: Proyecto
    {
        public int CantIntegrantes { get; set; }
        
        public override bool ValidoReglasNegocioProyecto()
        {
            return (base.ValidoReglasNegocioProyecto()) ? true : false;
        }

        public override string ToString()
        {
            string delimitador = " | ";
            return base.ToString()+delimitador+Tipo.ToString()+delimitador+ CantIntegrantes.ToString();
        }

    }
}
