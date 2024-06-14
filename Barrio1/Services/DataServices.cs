using Barrio1.Models;
using Supabase;
using Xamarin.Google.Crypto.Tink.Shaded.Protobuf;

namespace Barrio1.Services;


public class DataServices : IDataServices
{
    private readonly Client _supabaseClient;

    private string _user;

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

    public async Task CreateSalida(SalidasBotella salidasBo, SalidasBarril salidasBa)
    {
        await _supabaseClient.From<SalidasBotella>().Insert(salidasBo);
        await _supabaseClient.From<SalidasBarril>().Insert(salidasBa);
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
        if (tmp.clave == u.clave)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task UpdateAlmacen(Botellas ajustes, Barriles ajustesBa)
    {
        ajustes.id = 1;
        await _supabaseClient.From<Botellas>()
            .Where(b => b.id == ajustes.id)
            .Set(b => b.llane, ajustes.llane)
            .Set(b => b.SJ, ajustes.SJ)
            .Set(b => b.male, ajustes.male)
            .Set(b => b.barra, ajustes.barra)
            .Set(b => b.tolo, ajustes.tolo)
            .Set(b => b.b21, ajustes.b21)
            .Set(b => b.gen, ajustes.gen)
            .Set(b => b.guasanta, ajustes.guasanta)
            .Update();

        ajustesBa.id = 1;
        await _supabaseClient.From<Barriles>()
            .Where(b => b.id == ajustesBa.id)
            .Set(b => b.llane, ajustesBa.llane)
            .Set(b => b.SJ, ajustesBa.SJ)
            .Set(b => b.male, ajustesBa.male)
            .Set(b => b.barra, ajustesBa.barra)
            .Set(b => b.tolo, ajustesBa.tolo)
            .Set(b => b.b21, ajustesBa.b21)
            .Set(b => b.gen, ajustesBa.gen)
            .Set(b => b.guasanta, ajustesBa.guasanta)
            .Update();
    }

    public async Task<Botellas> GetInventario()
    {
        var response = await _supabaseClient.From<Botellas>().Get();
        return response.Models.FirstOrDefault();
    }

    public async Task<Barriles> GetBarriles()
    {
        var response = await _supabaseClient.From<Barriles>().Get();
        return response.Models.FirstOrDefault();
    }

    public void SetUsername(string US)
    {
        _user = US;
    }

    public string GetUsername()
    {
        return _user;
    }
}