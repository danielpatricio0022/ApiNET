using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApiNet.model;
using JetBrains.Annotations;
using Xunit;

namespace ApiNet.Tests.model;

[TestSubject(typeof(TaskDTO))]
public class TaskDTOTest
{
    [Fact]
    public void TaskDTO_Validation_WhenNameIsEmpty()
    {
        // Arrangee
        var task = new TaskDTO
        {
            name = "",
            description = "Description",
            done = false,
            date = DateTime.Now
        };

        // when
        var validationResults = new List<ValidationResult>(); // list of validation results
        var validationContext = new ValidationContext(task, null, null);
                                         //object to validate, injection null and dictionary of items null
                                         
        Validator.TryValidateObject(task, validationContext, validationResults, true);

        // Assert
        Assert.Contains(validationResults, v => v.ErrorMessage == "Name is required" && 
                                                              v.MemberNames.Contains("name"));
        

    }
    
    
    
    

}