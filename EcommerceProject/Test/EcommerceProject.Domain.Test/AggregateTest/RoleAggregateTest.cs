using EcommerceProject.Domain.AggregatesRoot.RoleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.Domain.Test.AggregateTest
{
    public class RoleAggregateTest
    {
        [Fact]
        public void GivenInformation_WhenCreatingRole_ThenItShouldBeCreated()
        {
            var name = UserRole.Customer;

            var role = new Role(name);

            Assert.Equal(name, role.Name);
        } 
    }
}
