using System.Threading.Tasks;

namespace Demo.BingFa.Service
{
    public interface IGoodsService
    {
        Task CreateGoodsAsync(string goodsName,double price,int stock);
        Task UpdateStockAsync(int goodsid, int stock);

        bool CheckStock(int goodsid);
        
    }
}
