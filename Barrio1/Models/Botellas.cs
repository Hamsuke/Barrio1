using Postgrest.Attributes;
using Postgrest.Models;

namespace Barrio1.Models;

[Table("botellas")]

public class Botellas : BaseModel
{
    [PrimaryKey("id", false)]
    public int id { get; set; }

    [Column("llanerita")]
    public int llane { get; set; }

    [Column("san_juanera")]
    public int SJ { get; set; }

    [Column("maleficio")]
    public int male { get; set; }

    [Column("barranqueña")]
    public int barra { get; set; }

    [Column("toloachi")]
    public int tolo { get; set; }

    [Column("barrio_21")]
    public int b21 { get; set; }

    [Column("genesis")]
    public int gen { get; set; }

    [Column("guasanta")]
    public int guasanta { get; set; }

}