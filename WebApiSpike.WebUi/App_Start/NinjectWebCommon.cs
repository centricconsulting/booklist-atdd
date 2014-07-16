using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Ninject.Syntax;
using WebApiSpike.Services;
using WebApiSpike.Services.Contracts;

[assembly: WebActivator.PreApplicationStartMethod(typeof(WebApiSpike.WebUi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(WebApiSpike.WebUi.App_Start.NinjectWebCommon), "Stop")]

namespace WebApiSpike.WebUi.App_Start
{
	using System;
	using System.Web;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Web.Common;

	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

			RegisterServices(kernel);

			GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

			return kernel;
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			kernel.Bind<IBookService>().To<BookService>();

			kernel.Load(Assembly.Load("WebApiSpike.Services"));
		}
	}

	public class NinjectDependencyScope : IDependencyScope
	{
		private IResolutionRoot resolver;

		internal NinjectDependencyScope(IResolutionRoot resolver)
		{
			Contract.Assert(resolver != null);

			this.resolver = resolver;
		}

		public void Dispose()
		{
			IDisposable disposable = resolver as IDisposable;
			if (disposable != null)
				disposable.Dispose();

			resolver = null;
		}

		public object GetService(Type serviceType)
		{
			if (resolver == null)
				throw new ObjectDisposedException("this", "This scope has already been disposed");

			return resolver.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			if (resolver == null)
				throw new ObjectDisposedException("this", "This scope has already been disposed");

			return resolver.GetAll(serviceType);
		}
	}

	public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver(IKernel kernel)
			: base(kernel)
		{
			this.kernel = kernel;
		}

		public IDependencyScope BeginScope()
		{
			return new NinjectDependencyScope(kernel.BeginBlock());
		}
	}
}
