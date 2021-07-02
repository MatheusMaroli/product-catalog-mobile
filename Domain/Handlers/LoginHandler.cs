using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Handlers.Contracts;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class LoginHandler : BaseHandler, IHandler<LoginCommand>
    {
        private IUserRepository _repository;

        public LoginHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(LoginCommand command)
        {
            if ( _repository.Login(command.Email, command.Password))
            {
                return new Commands.CommandResult(null);
            }
            else
                return new Commands.CommandResult(Enums.CommandResultStatus.InvalidData, "Email ou senha invalido");
        }
    }
}
