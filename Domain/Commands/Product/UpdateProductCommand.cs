using Domain.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Product
{
    public class UpdateProductCommand : CommandsBehaviors.CommandValidatorContext, ICommand
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Img { get; set; }

        public override void Validate()
        {
            NotificationIncrementalIdIncorret(ProductId, "ProductId", "Produto não foi informado");
            NotificationEmptyString(Name, "Name", "Nome do produto não foi informado");
            NotificationEmptyString(Description, "Description", "Descriçao do produto não foi informado");
            if (Price < 0)
                AddNotification("Price", "Preço não pode ser negativo");

            NotificationNullValue(Img, "Img", "Imagem não foi informada");     
        }
    }
}
