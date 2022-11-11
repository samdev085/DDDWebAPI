using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notifications
{
    public class Notify
    {
        public Notify()
        {
            Notifications = new List<Notify>();
        }


        [NotMapped]
        public string NameProperty { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public List<Notify> Notifications { get; set; }


        public bool ValidatePropertyString(string value, string nameProperty)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notify 
                {
                    Message = "Required field",
                    NameProperty = nameProperty
                });

                return false;
            }
            return true;
        }

        public bool ValidatePropertyDecimal(decimal value, string nameProperty)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notify
                {
                    Message = "Value must be greater than 0",
                    NameProperty = nameProperty
                });

                return false;
            }
            return true;
        }
    }
}
