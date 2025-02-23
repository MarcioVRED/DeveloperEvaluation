using Ambev.DeveloperStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperStore.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public int BranchId { get; private set; }
        public string Name { get; private set; }

        public Branch(int branchId, string name)
        {
            BranchId = branchId;
            Name = name;
        }
    }
}
