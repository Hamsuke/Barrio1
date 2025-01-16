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
            .Set(b => b.celi, ajustes.celi)
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
            .Set(b => b.celi, ajustesBa.celi)
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

    public async Task<SalidasBotella> GetBotellasNota(int num)
    {
        try
        {
            // Fetch a single record or null if no match
            var response = await _supabaseClient
                .From<SalidasBotella>()
                .Where(x => x.id == num)
                .Single();

            if (response == null)
            {
                Console.WriteLine($"No data found for id {num}.");
            }

            return response;
        }
        catch (Exception ex)
        {
            // Log the exception (use a logger in production)
            Console.WriteLine($"Error fetching Botellas: {ex.Message}");
            return null;
        }
    }

    public async Task<SalidasBarril> GetBarrilesNota(int num)
    {
        try
        {
            // Fetch a single record or null if no match
            var response = await _supabaseClient
                .From<SalidasBarril>()
                .Where(x => x.id == num)
                .Single();

            if (response == null)
            {
                Console.WriteLine($"No data found for id {num}.");
            }

            return response;
        }
        catch (Exception ex)
        {
            // Log the exception (use a logger in production)
            Console.WriteLine($"Error fetching Barriles: {ex.Message}");
            return null;
        }
    }


    //public async Task<SalidasBotella> GetBotellasNota(int num)
    //{
    //    try
    //    {
    //        SalidasBotella response = await _supabaseClient
    //            .From<SalidasBotella>()
    //            .Select(x => new object[]
    //            {
    //                x.id,
    //                x.llane,
    //                x.male,
    //                x.sj,
    //                x.barra,
    //                x.tolo,
    //                x.b21,
    //                x.gen,
    //                x.guasanta,
    //                x.celi
    //            })
    //            .Where(x => x.id == num)
    //            .Single();

    //        return response;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Maneja la excepción (puedes registrar el error o manejarlo de otra manera)
    //        Console.WriteLine($"Error: {ex.Message}");
    //        return null; // O lanza una excepción personalizada o un valor por defecto
    //    }
    //}

    //public async Task<SalidasBarril> GetBarrilesNota(int num)
    //{
    //    try
    //    {
    //        SalidasBarril response = await _supabaseClient
    //            .From<SalidasBarril>()
    //            .Select(x => new object[]
    //            {
    //                x.id,
    //                x.llane,
    //                x.male,
    //                x.sj,
    //                x.barra,
    //                x.tolo,
    //                x.b21,
    //                x.gen,
    //                x.guasanta,
    //                x.celi
    //            })
    //            .Where(x => x.id == num)
    //            .Single();

    //        return response;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Maneja la excepción (puedes registrar el error o manejarlo de otra manera)
    //        Console.WriteLine($"Error: {ex.Message}");
    //        return null; // O lanza una excepción personalizada o un valor por defecto
    //    }
    //}

}