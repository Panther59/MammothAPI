using MammothAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MammothAPI.Services
{
	public interface IReportsService
	{
		Task<List<StoreSaleReport>> GetDataSubmitStatusAsync(DateTime businessDate);
	}
}