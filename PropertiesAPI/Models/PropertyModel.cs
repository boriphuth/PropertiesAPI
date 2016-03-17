using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertiesAPI.Models
{
    public class PropertyModel : IValidatableObject
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public decimal? Price { get; set; }

        public string Name { get; set; }

        public string PropertyDescription { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Address))
                yield return new ValidationResult("Address is Required");

            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("Name is Required");

            if (string.IsNullOrEmpty(Address))
                yield return new ValidationResult("PropertyDescription is Required");

            if (!Price.HasValue)
                yield return new ValidationResult("Price is Required");
        }
    }
}