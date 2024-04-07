﻿using Postgrest.Attributes;
using Postgrest.Models;

namespace Barrio1.Models;

[Table("ventas")]

public class Ventas : BaseModel
{
    [PrimaryKey("nota", false)]
    public int id { get; set; }

    [Column("cliente")]
    public string name { get; set; }

    [Column("vendedor")]
    public string vendor { get; set; }

    [Column("fecha_creacion")]
    public DateTime dateC { get; set; }

    [Column("fecha_pago")]
    public DateTime dateP { get; set; }

    [Column("estado")]
    public bool est { get; set; }

    [Column("pago")]
    public float pago { get; set; }

    [Column("costo")]
    public float costo { get; set; }
}