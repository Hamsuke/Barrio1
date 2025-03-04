using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Barrio1.Models;

[Table("salidasbarril")]

public class SalidasBarril : BaseModel
{
    [PrimaryKey("id", false)]
    public int identificador { get; set; }

    [Column("nota")]
    public int id { get; set; }

    [Column("nombre")]
    public string barril { get; set; }

    [Column("cantidad")]
    public int cant { get; set; }

}
