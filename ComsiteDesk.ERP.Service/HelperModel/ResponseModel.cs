using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public static class ResponseModel
    {
        public static string Status { get; set; }
        public static string Message { get; set; }

        public static string success { get; set; } = "success";
        public static string warning { get; set; } = "warning";
        public static string info { get; set; } = "info";
        public static string danger { get; set; } = "error";
    }
}
