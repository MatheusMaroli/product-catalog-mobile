
using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.CommandsBehaviors;
using System;

namespace Domain.Handlers
{
    public abstract class BaseHandler
    {
        public ICommandResult Execute(ICommand command)
        {
            try
            {
                var typeOfCommand = command.GetType();
                var commandIsCommandValidatorContext = typeOfCommand.IsSubclassOf(typeof(CommandValidatorContext));
                if (commandIsCommandValidatorContext)
                {
                    var notificationValidatorContext = (CommandValidatorContext)command;
                    notificationValidatorContext.Validate();
                    if (notificationValidatorContext.IsInvalid)
                        return new CommandResult(Enums.CommandResultStatus.InvalidCommand, "Invalid Command", notificationValidatorContext.Notifications);
                }
                var methodHandle = this.GetType().GetMethod("Handle", new Type[] { command.GetType() });
                return (ICommandResult)methodHandle.Invoke(this, new object[] { command });
            }
            catch(Exception e)
            {
                return ExceptionResult("Fail to handler execute", e);
            }
            
        }

        protected virtual ICommandResult ExceptionResult(string handlerName, Exception e)
        {
            return new CommandResult(Enums.CommandResultStatus.Exception, $"Fail to execute {handlerName}. Fail stack ===> {e}");
        }
    }
}
