using Barrio1.Models;
using Supabase;

namespace Barrio1.Services;

public class DataServices : IDataServices
{
    private readonly Client _supabaseClient;

    public DataServices(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }
    public async Task<IEnumerable<Ventas>> GetVentas()
    {
        var response = await _supabaseClient.From<Ventas>().Get();
        return response.Models.OrderByDescending(b => b.id);
    }

    public async Task CreateVenta(Ventas venta)
    {
        await _supabaseClient.From<Ventas>().Insert(venta);
    }

    public async Task UpdateVenta(Ventas venta)
    {
        var tmp = await _supabaseClient.From<Ventas>()
            .Select(x => new object[] { x.id, x.pago })
            .Where(x => x.id == venta.id)
            .Single();
        venta.pago = venta.pago + tmp.pago;
        if (venta.pago == venta.costo)
        {
            venta.est = true;
            venta.dateP = DateTime.Now;
        }
        await _supabaseClient.From<Ventas>()
            .Where(b => b.id == venta.id)
            .Set(b => b.id, venta.id)
            .Set(b => b.name, venta.name)
            .Set(b => b.vendor, venta.vendor)
            .Set(b => b.dateC, venta.dateC)
            .Set(b => b.dateP, venta.dateP)
            .Set(b => b.est, venta.est)
            .Set(b => b.pago, venta.pago)
            .Set(b => b.costo, venta.costo)
            .Update();
    }

    public async Task<bool> Login(Users u)
    {
        Users tmp = await _supabaseClient.From<Users>()
        .Where(b => b.id == u.id).Single();
        if (tmp == u)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task UpdateAlmacen(Salidas ajustes)
    {
        var tmp = await _supabaseClient.From<Inventario>().Get();

        await _supabaseClient.From<Inventario>()
            .Where(b => b.id == ajustes.id)
            .Set(b => b.llane, ajustes.llane)
            .Set(b => b.SJ, ajustes.SJ)
            .Set(b => b.male, ajustes.male)
            .Set(b => b.barra, ajustes.barra)
            .Set(b => b.tolo, ajustes.tolo)
            .Set(b => b.b21, ajustes.b21)
            .Set(b => b.gen, ajustes.gen)
            .Set(b => b.guasanta, ajustes.guasanta)
            .Set(b => b.aguac, ajustes.aguac)
            .Update();
    }
}