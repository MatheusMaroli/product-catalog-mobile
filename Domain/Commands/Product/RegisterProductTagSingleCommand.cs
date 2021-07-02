using Domain.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Product
{
    public class RegisterProductTagSingleCommand : CommandsBehaviors.CommandValidatorContext, ICommand
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }


        public override void Validate()
        {
            NotificationIncrementalIdIncorret(ProductId, "ProductId", "Produto não foi informado");
            NotificationIncrementalIdIncorret(TagId, "TagId", "Tag não foi informado");
        }
    }
}
