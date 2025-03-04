using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Barrio1.Models;

[Table("barriles")]

public class Barriles : BaseModel
{
    [PrimaryKey("id", false)]
    public int id { get; set; }

    [Column("nombre")]
    public string nombreBa { get; set; }

    [Column("cantidad")]
    public int cantidadBa { get; set; }
}