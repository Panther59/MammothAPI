using MammothAPI.Data;
using MammothAPI.Models;

namespace MammothAPI.Services
{
	public interface IMappers
	{
		Product MapProduct(Products product);
		ProductSale MapSale(ProductSales sale);
		StoreSale MapSale(Sales sale);
		Sales MapSale(StoreSale sale);
		Store MapStore(Stores store);
		User MapUser(Users user);
	}
}