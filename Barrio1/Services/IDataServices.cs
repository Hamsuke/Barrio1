using Barrio1.Models;

namespace Barrio1.Services
{
    public interface IDataServices
    {
        Task<IEnumerable<Ventas>> GetVentas();
        Task CreateVenta(Ventas venta);
        Task UpdateVenta(Ventas venta);
        Task<bool> Login(Users u);
        Task UpdateAlmacen(Inventario ajustes);
        Task<Inventario> GetInventario();
    }
}
