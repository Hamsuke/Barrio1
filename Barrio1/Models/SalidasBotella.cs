using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Barrio1.Models;

[Table("salidasbotella")]

public class SalidasBotella : BaseModel
{
    [PrimaryKey("id", false)]
    public int identificador { get; set; }

    [Column("nota")]
    public int id { get; set; }

    [Column("nombre")]
    public string botella { get; set; }

    [Column("cantidad")]
    public int cant { get; set; }

}
