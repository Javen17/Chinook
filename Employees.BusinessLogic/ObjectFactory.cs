using Chinook.BusinessLogic.Implementation;
using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Injection;

namespace Chinook.BusinessLogic
{
	/// <summary>
	/// Represent a DI container.
	/// </summary>
	public class ObjectFactory : IObjectFactory
	{
		public static IUnityContainer Container { get; } = new UnityContainer();
		ChinookContext _context;

		public static void Init()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		/// <remarks></remarks>
		public ObjectFactory(ChinookContext context)
		{
			_context = context;

			try
			{
				Container.AddExtension(new Diagnostic());
				Container.RegisterSingleton<IArtistManager, ArtistManager>(new InjectionConstructor(_context));
				Container.RegisterSingleton<IGenreManager, GenreManager>(new InjectionConstructor(_context));
				Container.RegisterSingleton<IAlbumManager, AlbumManager>(new InjectionConstructor(_context));
				Container.RegisterSingleton<ISongManager, SongManager>(new InjectionConstructor(_context));
				Container.RegisterSingleton<IEmployeeManager, EmployeeManager>(new InjectionConstructor(_context));
				Container.RegisterSingleton<IClientManager, ClientManager>(new InjectionConstructor(_context));
			}
			catch (Exception e)
			{
				LogManager.Current.Log.Error("failed composing all objects in the ObjectFactory", e);
				// don't take this out cause if any thing is wrong with unity config this will help the debugger catch it but only if attached
				//		if (Debugger.IsAttached)
				//		Debugger.Break();
			}
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T Resolve<T>() where T : class
		{
			try
			{
				if (Container.IsRegistered<T>())
				{
					T obj = (T)Container.Resolve(typeof(T));
					if (obj is IConfigurable)
					{
						if (!((IConfigurable)obj).Configured)
						{
							((IConfigurable)obj).Configure();
						}
					}

					return obj;
				}
			}
			catch (Exception)
			{
				throw;
			}
			return null;
		}
	}

}
