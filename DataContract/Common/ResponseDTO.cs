using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Common
{
    public class ResponseDTO<T> where T : class
    {
        public T Result { get; set; }

        public bool Status { get; set; }

        public int ResposeID { get; set; }
    }
}
