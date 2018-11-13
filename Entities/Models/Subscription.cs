using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("Subscription")]
    public class Subscription
    {
        public int UserId { get; set; }
        public int SubcriberId { get; set; }
        public UserExtended UserIdNavigation { get; set; }
        public UserExtended SubscriberIdNavidgation { get; set; }
    }
}
