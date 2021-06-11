﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Validaciones
{
    public class PrimerLetraMayuscula : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value==null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primerLetra = value.ToString()[0].ToString();
            if (primerLetra!=primerLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayuscula");
            }


            return ValidationResult.Success;
        }
        public PrimerLetraMayuscula()
        {
        }
    }
}
