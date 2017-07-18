﻿#region Copyright
// // -----------------------------------------------------------------------
// // <copyright company="Chinchilla Software Limited">
// // 	Copyright Chinchilla Software Limited. All rights reserved.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using Cqrs.Authentication;
using Cqrs.Configuration;
using Cqrs.Ninject.Configuration;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Cqrs.Ninject.WebApi.Configuration.SimplifiedNinjectWebApi), "Start", Order = 50)]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Cqrs.Ninject.WebApi.Configuration.SimplifiedNinjectWebApi), "Stop", Order = 50)]

namespace Cqrs.Ninject.WebApi.Configuration
{
	public static class SimplifiedNinjectWebApi
	{
		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			Bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			Bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			return new WebApiStartUp(new ConfigurationManager()).CreateKernel();
		}

		private class WebApiStartUp : SimplifiedNinjectStartUp<WebApiModule>
		{
			public WebApiStartUp(IConfigurationManager configurationManager)
				: base(configurationManager)
			{
			}

			#region Overrides of SimplifiedNinjectStartUp<WebHostModule>

			protected override void AddSupplementryModules()
			{
				NinjectDependencyResolver.ModulesToLoad.Insert(2, new SimplifiedSqlModule<SingleSignOnToken>());
			}

			#endregion
		}
	}
}