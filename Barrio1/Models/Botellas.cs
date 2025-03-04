using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Barrio1.Models;

[Table("botellas")]

public class Botellas : BaseModel
{
    [PrimaryKey("id", false)]
    public int id { get; set; }

    [Column("nombre")]
    public string nombreBo { get; set; }

    [Column("cantidad")]
    public int cantidadBo { get; set; }

}