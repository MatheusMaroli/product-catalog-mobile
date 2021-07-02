using Domain.Commands.Contracts;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test
{

    public class CommandToValidate : CommandsBehaviors.CommandValidatorContext,  ICommand
    {
        public int Id { get; set; }
        public string StringType { get; set; }
        public double? NullValidateValue { get; set; }
        public IEnumerable<string> CollectionValidate { get; set; }
        public IEnumerable<string> CollectionNullValidate { get; set; }

        public override void Validate()
        { 
            NotificationEmptyString(StringType, "StringType", "empty string");
            NotificationIncrementalIdIncorret(Id, "Id", "invalid incremental id");
            NotificationNullValue(NullValidateValue, "NullValidateValue", "invalid null value");
            NotificationNullOrEmptyCollection(CollectionValidate, "CollectionValidate", "empty collection");
            NotificationNullOrEmptyCollection(CollectionNullValidate, "CollectionValidate", "null collection");

        }
    }

    public class CommandToCollectionValidate : CommandsBehaviors.CommandValidatorContext, ICommand
    {
        public IEnumerable<CommandToValidate> CollectionToValidate { get; set; }

        public override void Validate()
        {
            NotificationCollection(CollectionToValidate);
        }
    }


    [TestClass]
    public class CommandBehaviorTest
    {
        [TestMethod]
        public void Should_be_return_all_fail_notification()
        {
            var invalidCommand = new CommandToValidate()
            {
                Id = 0,
                StringType = "",
                NullValidateValue = null,
                CollectionValidate = new List<string>(),
                CollectionNullValidate = null,
            };
            invalidCommand.Validate();
            Assert.IsTrue(invalidCommand.Notifications.Count == 5);
        }

        [TestMethod]
        public void Should_be_valid_notification_command()
        {
            var validCommand = new CommandToValidate()
            {
                Id = 1,
                StringType = "ok",
                NullValidateValue = 1,
                CollectionValidate = new List<string>() {  "Item of Collection" },
                CollectionNullValidate = new List<string>() { "Item of Collection" }
            };
            validCommand.Validate();
            Assert.IsTrue(! validCommand.IsInvalid);
        }

        [TestMethod]
        public void Should_be_return_invalid_collection_notification()
        {
            var invalidCommand = new CommandToCollectionValidate()
            {
                CollectionToValidate = new List<CommandToValidate>()
                {
                    new CommandToValidate()
                    {
                        Id = 0,
                        StringType = "",
                        NullValidateValue = null,
                        CollectionValidate = new List<string>(), 
                        CollectionNullValidate = null
                    }
                }
            };

            invalidCommand.Validate();
            Assert.IsTrue(invalidCommand.Notifications.Count == 5);
        }


        [TestMethod]
        public void Should_be_valid_collection_notification()
        {
            var validCommand = new CommandToCollectionValidate()
            {
                CollectionToValidate = new List<CommandToValidate>()
                {
                    new CommandToValidate()
                    {
                        Id = 1,
                        StringType = "ok",
                        NullValidateValue = 1,
                        CollectionValidate = new List<string>() { "Item of Collection" },
                        CollectionNullValidate = new List<string>() { "Item of Collection" }
                    }
                }
            };
            validCommand.Validate();
            Assert.IsTrue(! validCommand.IsInvalid);
        }


    }

  
}
