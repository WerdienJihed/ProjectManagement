using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.CustomHelpers
{
	public class DropDownValidator : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			return Guid.TryParse(value?.ToString(), out var id);
		}
	}
}
