using System.ComponentModel.DataAnnotations;

namespace AssignPL.ViewModels
{
	public class RegisterViewModel
	{
		public string FName { get; set; }
		public string LName { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invaild Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare("Password", ErrorMessage = "Confirem Password does not match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}
