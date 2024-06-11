namespace PetHealthcare.Server.Helpers
{
	public class Helpers
	{
		public static string GetRole (int roleId)
		{
			switch (roleId)
			{
				case 1:
					return "Customer";
				case 2:
					return "Admin";
				case 3:	
					return "Vet";
				case 4:
					return "Staff";
				default:
					throw new InvalidDataException();
			}
		}
	}
}
