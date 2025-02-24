using Ambev.DeveloperStore.Domain.Enums;
using Ambev.DeveloperStore.Domain.Specifications;
using Ambev.DeveloperStore.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperStore.Unit.Domain.Specifications
{
    public class ActiveUserSpecificationTests
    {
        [Theory]
        [InlineData(UserStatus.Active, true)]
        [InlineData(UserStatus.Inactive, false)]
        [InlineData(UserStatus.Suspended, false)]
        public void IsSatisfiedBy_ShouldValidateUserStatus(UserStatus status, bool expectedResult)
        {
            // Arrange
            var user = ActiveUserSpecificationTestData.GenerateUser(status);
            var specification = new ActiveUserSpecification();

            // Act
            var result = specification.IsSatisfiedBy(user);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
