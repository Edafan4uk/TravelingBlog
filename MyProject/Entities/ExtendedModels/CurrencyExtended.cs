using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class CurrencyExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }

        public CurrencyExtended()
        {
                
        }

        public CurrencyExtended(Currency currency)
        {
            Id = currency.Id;
            Name = currency.Name;
        }
    }
}
