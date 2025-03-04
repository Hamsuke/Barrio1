using Barrio1.Models;
using Supabase;

namespace Barrio1.Services;

public class DataServices : IDataServices
{
    private readonly Client _supabaseClient;

    private string _user;

    public DataServices(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }
    public async Task<IEnumerable<Ventas>> GetVentas(DateTime inicioDeMes, DateTime diaActual)
    {
        var response = await _supabaseClient.From<Ventas>().Where(v => v.dateC >= inicioDeMes && v.dateC <= diaActual).Get();
        return response.Models.OrderByDescending(b => b.id);
    }
    public async Task CreateVenta(Ventas venta)
    {
        await _supabaseClient.From<Ventas>().Insert(venta);
    }
    public async Task CreateSalidaBotella(SalidasBotella salida)
    {
        await _supabaseClient.From<SalidasBotella>().Insert(salida);
    }
    public async Task CreateSalidaBarril(SalidasBarril salida)
    {
        await _supabaseClient.From<SalidasBarril>().Insert(salida);
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
    public async Task updateBotella(SalidasBotella salida)
    {
        var tmp = await _supabaseClient.From<Botellas>()
            .Where(b => b.nombreBo == salida.botella)
            .Single();
        tmp.cantidadBo = tmp.cantidadBo - salida.cant;
        await _supabaseClient.From<Botellas>()
            .Where(b => b.nombreBo == tmp.nombreBo)
            .Set(b => b.cantidadBo, tmp.cantidadBo)
            .Update();
    }
    public async Task updateBarril(SalidasBarril salida)
    {
        var tmp = await _supabaseClient.From<Barriles>()
    .Where(b => b.nombreBa == salida.barril)
    .Single();
        tmp.cantidadBa = tmp.cantidadBa - salida.cant;
        await _supabaseClient.From<Barriles>()
            .Where(b => b.nombreBa == tmp.nombreBa)
            .Set(b => b.cantidadBa, tmp.cantidadBa)
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
    public async Task<IEnumerable<Botellas>> GetBotellas()
    {
        var response = await _supabaseClient.From<Botellas>().Get();
        return response.Models.OrderByDescending(b => b.id);
    }
    public async Task<IEnumerable<Botellas>> GetBotellasDisp()
    {
        var response = await _supabaseClient.From<Botellas>().Get();
        return response.Models.OrderByDescending(b => b.id);
    }
    public async Task<IEnumerable<Barriles>> GetBarriles()
    {
        var response = await _supabaseClient.From<Barriles>().Get();
        return response.Models.OrderByDescending(b => b.id);
    }
    public async Task<IEnumerable<Barriles>> GetBarrilesDisp()
    {
        var response = await _supabaseClient.From<Barriles>().Get();
        return response.Models;
    }
    public void SetUsername(string US)
    {
        _user = US;
    }
    public string GetUsername()
    {
        return _user;
    }
    public async Task<IEnumerable<SalidasBotella>> GetBotellasNota(int num)
    {
        try
        {
            // Fetch a single record or null if no match
            var response = await _supabaseClient
                .From<SalidasBotella>()
                .Where(x => x.id == num)
                .Get();

            if (response == null)
            {
                Console.WriteLine($"No data found for id {num}.");
            }

            return response.Models;
        }
        catch (Exception ex)
        {
            // Log the exception (use a logger in production)
            Console.WriteLine($"Error fetching Botellas: {ex.Message}");
            return null;
        }
    }
    public async Task<IEnumerable<SalidasBarril>> GetBarrilesNota(int num)
    {
        try
        {
            // Fetch a single record or null if no match
            var response = await _supabaseClient
                .From<SalidasBarril>()
                .Where(x => x.id == num)
                .Get();

            if (response == null)
            {
                Console.WriteLine($"No data found for id {num}.");
            }

            return response.Models;
        }
        catch (Exception ex)
        {
            // Log the exception (use a logger in production)
            Console.WriteLine($"Error fetching Barriles: {ex.Message}");
            return null;
        }
    }
}