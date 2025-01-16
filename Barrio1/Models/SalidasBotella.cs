using Postgrest.Attributes;
using Postgrest.Models;

namespace Barrio1.Models;

[Table("salidasbotella")]

public class SalidasBotella : BaseModel
{
    [PrimaryKey("id", false)]
    public int identificador { get; set; }

    [Column("nota")]
    public int id { get; set; }

    [Column("llanerita")]
    public int llane { get; set; }

    [Column("san_juanera")]
    public int sj { get; set; }

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

    [Column("celi")]
    public int celi { get; set; }

}
