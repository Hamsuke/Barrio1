using Barrio1.Models;

namespace Barrio1.Services
{
    public interface IDataServices
    {
        Task<IEnumerable<Ventas>> GetVentas();
        Task CreateVenta(Ventas venta);
        Task CreateSalida(SalidasBotella salidaBo, SalidasBarril salidaBa);
        Task UpdateVenta(Ventas venta);
        Task<bool> Login(Users u);
        Task UpdateAlmacen(Botellas ajustes, Barriles ajustesBa);
        Task<Botellas> GetInventario();
        Task<Barriles> GetBarriles();
        Task<SalidasBotella> GetBotellasNota(int num);
        Task<SalidasBarril> GetBarrilesNota(int num);
        void SetUsername(string US);
        string GetUsername();
    }
}
