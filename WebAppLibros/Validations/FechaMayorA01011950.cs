using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppLibros.Validations
{
    public class FechaMayorA01011950 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            DateTime fecha = DateTime.Parse(value.ToString());

            if (DateTime.Compare(fecha, new DateTime(1950, 1, 1)) < 1)
            {
                return new ValidationResult("Solo se aceptan fechas mayores al año 01/01/1950");
            }

            return ValidationResult.Success;
        }


    }
}
