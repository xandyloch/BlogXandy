﻿using BlogXandy.db.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogXandy.db.Classes
{
    public class Imagem : ClasseBase
    {
        
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public Byte[] Bytes { get; set; }
        public int IdPost { get; set; }
        
        public virtual Post Post { get;  set; }
    }
}
