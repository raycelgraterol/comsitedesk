using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketStatusModel : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum statusAlert
    {
        primary,
        secondary,
        success,
        info,
        warning,
        danger,
        dark,
        blue,
        pink,
        light
    }

    public enum statusTicket
    {
        Abierto = 1,
        Cerrado = 2,
        EnProceso = 3,
        Escalado = 4,
        SinAsignar = 5,
        Asignado = 6
    }
}
