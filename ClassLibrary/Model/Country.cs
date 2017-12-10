
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ClassLibrary.Model
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IsoCode { get; set; }

        [NotMapped]
        public SelectList CountryList { get; set; }
    }
}
