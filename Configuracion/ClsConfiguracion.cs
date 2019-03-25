using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuracion
{
    public class ClsConfiguracion
    {

        public string getConectioString
        {

            get { return ConfigurationManager.AppSettings["ConnectionString"]; }

        }

    }
}
