using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CP
    {
        public string DescEstado { get; set; }
        public int CodEstado { get; set; }
        public int CodMunicipio { get; set; }
        public string DescMunicipio { get; set; }
        public List<DatosCP> results  { get; set; }
    }

    public class DatosCP
    {
        public int ID { get; set; }
        public string DescripcionColonia { get; set; }
        public string CodigoColonia { get; set; }
        public List<object> Colonias { get; set; }
    }
}


