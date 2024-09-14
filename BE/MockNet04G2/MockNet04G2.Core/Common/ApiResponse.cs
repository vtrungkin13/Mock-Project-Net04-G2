using MockNet04G2.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Common
{
    public class ApiResponse<TBody, TError>
    {
        public StatusResponseEnum Status { get; set; }
        public TError? Error { get; set; }
        public TBody? Body { get; set; }


        public ApiResponse() { }

        public ApiResponse(StatusResponseEnum status, TBody? body = default!, TError error = default!)
        {
            Status = status;
            Body = body;
            Error = error;
        }
    }
}
