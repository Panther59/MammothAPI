using MammothAPI.Data;
using MammothAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammothAPI.Services
{
	public class Mappers : IMappers
	{
		public User MapUser(Users user)
		{
			return new User
			{
				ID = user.Id,
				Email = user.Email,
				LoginName = user.LoginName,
				FirstName = user.FirstName,
				LastName = user.LastName,
				IsActive = user.IsActive,
				Type = (Models.TypeOfUser)user.Type,
				LoginID = user.Login.Id,
			};
		}

		public Product MapProduct(Products product)
		{
			return new Product
			{
				ID = product.Id,
				Group = product.Group != null ? product.Group.Name : null,
				IsActive = product.IsActive,
				Name = product.Name
			};
		}

		public Store MapStore(Stores store)
		{
			return new Store
			{
				ID = store.Id,
				Code = store.Code,
				Name = store.Name,
			};
		}

		public StoreSale MapSale(Sales sale)
		{
			return new StoreSale
			{
				CashDeposite = sale.CashDeposite,
				DamageQuantity = sale.DamageQuantity,
				MissingCrate = sale.MissingCrate,
				MissingQuantity = sale.MissingQuantity,
				OnlineOrder = sale.OnlineOrder,
				OpenOrder = sale.OpenOrder,
				PendingConsignment = sale.PendingConsignment,
				PendingSapphireComplain = sale.PendingSapphireComplain,
				PureMarquee = sale.PureMarquee,
				TotalMarquee = sale.TotalMarquee,
				TotalOnline = sale.TotalOnline,
			};
		}

		public Sales MapSale(StoreSale sale)
		{
			return new Sales
			{
				CashDeposite = sale.CashDeposite,
				DamageQuantity = sale.DamageQuantity,
				MissingCrate = sale.MissingCrate,
				MissingQuantity = sale.MissingQuantity,
				OnlineOrder = sale.OnlineOrder,
				OpenOrder = sale.OpenOrder,
				PendingConsignment = sale.PendingConsignment,
				PendingSapphireComplain = sale.PendingSapphireComplain,
				PureMarquee = sale.PureMarquee,
				TotalMarquee = sale.TotalMarquee,
				TotalOnline = sale.TotalOnline,
			};
		}

		public ProductSale MapSale(ProductSales x)
		{
			return new ProductSale
			{
				ProductID = x.ProductId,
				SaleCount = x.SaleCount
			};
		}
	}
}
