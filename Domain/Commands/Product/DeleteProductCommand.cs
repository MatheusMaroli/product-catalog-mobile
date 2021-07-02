using Domain.Commands.Contracts;

namespace Domain.Commands.Product
{
    public class DeleteProductCommand : CommandsBehaviors.CommandValidatorContext, ICommand
    {
        public int ProductId { get; set; }

        public override void Validate()
        {
            NotificationIncrementalIdIncorret(ProductId, "ProductId", "Produto não foi informado");
        }
    }
}
