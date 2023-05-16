using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = " Create Product With Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Bolsa", "Bolsa Escolar", 30.1m, 10, "x.jpg");
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Invalid Id Value")]
        public void CreateProduct_WithInvalidIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Bolsa", "Bolsa Escolar", 30.1m, 10, "x.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid id value");
        }

        [Fact(DisplayName = "Create Product With Null Name Value")]
        public void CreateProduct_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, null, "Bolsa Escolar", 30.1m, 10, "x.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
        }

        [Fact(DisplayName = "Create Product With Short Name")]
        public void CreateProduct_WithShortName_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Bo", "Bolsa Escoler", 30.1m, 10, "x.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create Product With Null Description Value")]
        public void CreateProduct_WithNullDescriptionValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, "Bolsa", null, 30.1m, 10, "x.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid description. Description is required");
        }

        [Fact(DisplayName = "Create Product With Short Description")]
        public void CreateProduct_WithShortDescription_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Bolsa", "Bols", 30.1m, 10, "x.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid description. Too short, minimum 5 characters");
        }

        [Fact(DisplayName = "Create Product With Invalid Price Value")]
        public void CreateProduct_WithInvalidPriceValue_DomainExceptionInvalidPrice()
        {
            Action action = () => new Product(1, "Bolsa", "Bolsa Escolar", -30.1m, 10, "x.jpg");
            action.Should()
            .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid price value");
        }

        [Fact(DisplayName = "Create Product With Invalid Stock Value")]
        public void CreateProduct_WithInvalidStockValue_DomainExceptionInvalidStock()
        {
            Action action = () => new Product(1, "Bolsa", "Bolsa Escolar", 30.1m, -10, "x.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid stock value");
        }

        [Fact(DisplayName = "Create Product With Invalid Image Value")]
        public void CreateProduct_WithInvalidImageValue_DomainExceptionInvalidImage()
        {
            Action action = () => new Product(1, "Bolsa", "Bolsa Escolar", 30.1m, 10, "012345678901234567890123456789" +
                       "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                       "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                       "0123456789012345678901234567890123456.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid image name, too long, maximum 250 characters");
        }

        [Fact(DisplayName = "Create Product With Null Image Name Value")]
        public void CreateProduct_WithNullImageNameValue_DomainException()
        {
            Action action = () => new Product(1, "Bolsa", "Bolsa Escolar", 30.1m, 10, null);
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Empty Image Name")]
        public void CreateProduct_WithEmptyImageName_DomainException()
        {
            Action action = () => new Product(1, "Bolsa", "Bolsa Escolar", 30.1m, 10, "");
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }
        //Quando for necessário testar a introdução do parâmetro
        [Theory]
        //Valor que o parametro irá receber (value)
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Bolsa", "Bolsa Escolar", 30.1m, value, "x.jpg");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid stock value");
        }
    }
}
