using Barrio1.Models;

namespace Barrio1.Services
{
    public interface IDataServices
    {
        Task<IEnumerable<Ventas>> GetVentas(DateTime inicioDeMes, DateTime diaActual);
        Task CreateVenta(Ventas venta);
        Task CreateSalidaBotella(SalidasBotella salida);
        Task CreateSalidaBarril(SalidasBarril salida);
        Task BulkCreateSalidaBarril(IEnumerable<SalidasBarril> salidas);
        Task BulkCreateSalidaBotella(IEnumerable<SalidasBotella> salidas);
        Task UpdateVenta(Ventas venta);
        Task<bool> Login(Users u);
        Task<IEnumerable<Botellas>> GetBotellas();
        Task<IEnumerable<Botellas>> GetBotellasDisp();
        Task<IEnumerable<Barriles>> GetBarriles();
        Task<IEnumerable<Barriles>> GetBarrilesDisp();
        Task<IEnumerable<SalidasBotella>> GetBotellasNota(int num);
        Task<IEnumerable<SalidasBarril>> GetBarrilesNota(int num);
        void SetUsername(string US);
        string GetUsername();
        public Task updateBotella(SalidasBotella salida);
        public Task updateBarril(SalidasBarril salida);
    }
}
