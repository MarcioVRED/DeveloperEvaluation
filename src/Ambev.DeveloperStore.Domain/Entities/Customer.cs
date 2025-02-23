using Ambev.DeveloperStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperStore.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public int CustomerId { get; private set; }
        public string Name { get; private set; }

        public Customer(int customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }
    }
}
