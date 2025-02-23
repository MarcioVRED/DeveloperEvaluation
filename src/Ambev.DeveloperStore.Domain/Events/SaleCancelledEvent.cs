using Ambev.DeveloperStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperStore.Domain.Events
{
    public class SaleCancelledEvent
    {
        public Sale Sale { get; }

        public SaleCancelledEvent(Sale sale)
        {
            Sale = sale;
        }
    }
}
