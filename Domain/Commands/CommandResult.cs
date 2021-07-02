using Domain.Commands.Contracts;
using Domain.Enums;

namespace Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResultStatus Status { get; private set; }
        public string Message { get; private set; }
        public object Body { get; private set; }

        public bool IsSuccess => Status == CommandResultStatus.Success;
        public bool IsInvalidData => Status == CommandResultStatus.InvalidData;
        public bool IsInvalidCommand => Status == CommandResultStatus.InvalidCommand;
        public bool IsException => Status == CommandResultStatus.Exception;

        public CommandResult(CommandResultStatus status, string message, object body)
        {
            Status = status;
            Message = message;
            SetBody(body);
        }

        public CommandResult(CommandResultStatus status, object body)
        {
            Status = status;
            SetDefaultMessage();
            SetBody(body);
        }

        public CommandResult(object body)
        {
            Status = CommandResultStatus.Success;
            SetDefaultMessage();
            SetBody(body);
        }

        private void SetDefaultMessage()
        {
            if (IsInvalidCommand)
                Message = "Invalid Command";
            else if (IsException)
                Message = "Is a Exception";
            else if (IsInvalidData)
                Message = "Is a Invalid Data";
            else
                Message = "Success Operation";
        }

        public void SetBody(object body)
        {
            Body = body;
        }
    }
}
