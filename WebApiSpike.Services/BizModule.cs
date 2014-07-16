using Ninject.Modules;
using WebApiSpike.Biz.Data;
using WebApiSpike.Biz.Entities;
using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Biz
{
	public class BizModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IRepository<Book, int>>().To<FakeBookRepository>();
		}
	}
}