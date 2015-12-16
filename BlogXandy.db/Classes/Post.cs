using BlogXandy.db.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogXandy.db.Classes
{
    public class Post: ClasseBase
    {
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Descricao { get; set; }
        public String Resumo { get; set; }
        public String Titulo { get; set; }
        public Boolean Visivel { get; set; }

        public virtual IList<Visita> visita { get; set; }
        public virtual IList<PostTag> tagpost { get; set; }
        public virtual IList<Comentario> comentario { get; set; }
        public virtual IList<Imagem> imagem { get; set; }
        public virtual IList<Arquivo> arquivo { get; set; }
        
    }
}
