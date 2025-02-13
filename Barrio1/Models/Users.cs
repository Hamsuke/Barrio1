using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Barrio1.Models;

[Table("usuarios")]

public class Users : BaseModel
{
    [PrimaryKey("id", false)]
    public string id { get; set; }

    [Column("password")]
    public string clave { get; set; }
}