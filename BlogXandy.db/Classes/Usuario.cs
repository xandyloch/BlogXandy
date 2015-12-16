using BlogXandy.db.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogXandy.db.Classes
{
    public class Usuario : ClasseBase
    {
        public String Login { get; set; }
        public String Nome { get; set; }
        public String Senha { get; set; }

    }
}
