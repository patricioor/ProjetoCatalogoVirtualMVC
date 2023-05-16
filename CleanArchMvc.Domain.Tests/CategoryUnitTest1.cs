using FluentAssertions;
using CleanArchMvc.Domain.Entities;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact]
        public void CreateCategory_WithValidParameteres_ResultObjectValidState()
        {
            Action action = () => new Category(1, "New Category");
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With Invalid Id Value")]
        public void CreateCategory_WithInvalidIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "New Category");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Create Category With Null Name Value")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
        }

        [Fact(DisplayName = "Create Product With Short Name")]
        public void CreateCategory_WithShortName_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters");
        }
    }
}