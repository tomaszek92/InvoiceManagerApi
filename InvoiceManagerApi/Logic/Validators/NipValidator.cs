using FluentValidation.Validators;

namespace InvoiceManagerApi.Logic.Validators
{
    public class NipValidator : PropertyValidator
    {
        public const string ErrorMessage = "Nip is not valid";

        public NipValidator() : base(ErrorMessage)
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var nip = context.PropertyValue as string;

            return IsValid(nip);
        }

        public bool IsValid(string nip)
        {
            if (nip == null || nip.Length != 10)
            {
                return false;
            }

            var weight = new int[] { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            var sum = 0;
            var controlNumber = int.Parse(nip[9].ToString());
            var weightCount = weight.Length;
            for (var i = 0; i < weightCount; i++)
            {
                sum += int.Parse(nip[i].ToString()) * weight[i];
            }

            return sum % 11 == controlNumber;
        }
    }
}
