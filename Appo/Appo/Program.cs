using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using static Appo.Program;

namespace Appo
{
	internal class Program
	{

		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");


			var studio = new Business
			{
				Employees = [
					new Employee
					{
						Name="1"
					},
					new Employee
					{
						Name ="2"
					},
					new Employee
					{
						Name = "3"
					}],
				Ressources = [
					new Ressource(ResourceTypes.Room, 3),
					new Ressource(ResourceTypes.Stool, 6),
					new Ressource(ResourceTypes.Table, 4)],
				Services = [
					new Service(ServiceTypes.Haircut, [ResourceTypes.Room, ResourceTypes.Stool]),
					new Service(ServiceTypes.Shave, [ResourceTypes.Room, ResourceTypes.Stool]),
					new Service(ServiceTypes.Manicure, [ResourceTypes.Table,ResourceTypes.Stool,ResourceTypes.Room]),
					new Service(ServiceTypes.Pedicure, [ResourceTypes.Table,ResourceTypes.Room])
					]
			};

		}
	}

	public class Business
	{
		public List<Employee> Employees { get; set; }

		public required List<Ressource> Ressources { get; set; }

		public List<RessourceManagement> RessourceManagements { get; }

		public List<Service> Services { get; set; }

		public Business()
		{
			RessourceManagements = Ressources.Select(r => new RessourceManagement(r, r.MaxAvaibalbilty)).ToList();
		}
	}


	public class Service
	{
		public Service(ServiceTypes serviceType, List<ResourceTypes> requiredResourcesn)
		{
			Type = serviceType;
			Name = Enum.GetName(typeof(ServiceTypes), serviceType);
			RequiredRessources = requiredResourcesn;
		}

		public ServiceTypes Type { get; set; }
		public string Name { get; set; }

		public List<ResourceTypes> RequiredRessources { get; }

	}

	public class Employee : Ressource
	{
		public Employee() : base(ResourceTypes.Employee, 1)
		{

		}

		public string Name { get; set; }
	}
	/// <summary>
	/// Represents a ressource and the number of times it can be used
	/// Like a shelf where you can only take the same item three times
	/// </summary>
	public class RessourceManagement
	{
		public string Name { get; }
		public List<Ressource> Resources { get; }

		public RessourceManagement(Ressource ressource, int resourceCount)
		{
			Resources = Enumerable.Range(0, resourceCount).Select(i => ressource).ToList();
		}
	}

	public class Ressource
	{
		public Ressource(ResourceTypes ressource, int maxAvailable)
		{
			MaxAvaibalbilty = maxAvailable;
			Type = ressource;
			Name = Enum.GetName(typeof(ResourceTypes), ressource);
		}

		public ResourceTypes Type { get; }
		public string Name { get; }

		public int MaxAvaibalbilty { get; }

		bool IsAvailable { get; set; } = true;
	}

	public enum ServiceTypes
	{
		Haircut,
		Shave,
		Manicure,
		Pedicure,
	}

	public enum ResourceTypes
	{
		Employee,
		Room,
		Stool,
		Table,
	}

}
