using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="Email is required") ]
		[EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }


		[Required(ErrorMessage = "Password is required")]
		//[MinLength(5, ErrorMessage = "Minimum Password length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]

		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare("Password", ErrorMessage = " Confirm Password Doesn't Match Password")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get;set; }

	}
}
