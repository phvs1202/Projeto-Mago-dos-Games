using System.ComponentModel.DataAnnotations;

namespace APIJogos.Models
{
    public class usergame
    {
        [Key]
        public int usergameid { get; set; }
        public int userid { get; set; }
        public int jogoid { get; set; }
        public string? status { get; set; }
    }
}
