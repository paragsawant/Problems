using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        [Required]
        [Display(Name = "Pet Owner Name")]
        public string PetOwnerName { get; set; }
        [Required]
        [Display(Name = "Policy Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime PolicyDate { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        private IList<Pet> _pets = new List<Pet>();
        public IList<Pet> Pets
        {
            get { return _pets; }
            set { _pets = value; }
        }
    }
}
