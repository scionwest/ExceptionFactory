ExceptionFactory 2.0
================
[![Gitter](https://badges.gitter.im/Join Chat.svg)](https://gitter.im/scionwest/ExceptionFactory?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Provides a series of factory methods for creating exceptions in .NET

###2.0 Release Notes

This version of the Exception factory contains some breaking changes from the 1.0 source.

    ExceptionFactory.ThrowExceptionIf<NullReferenceException>(condition == null, "Condition must not be null!");
    
The above `ThrowExceptionIf` method has been renamed to `ThrowIf`

    ExceptionFactory.ThrowIf<NullReferenceException>(condition == null, "Condition must not be null!");

The point of the exception factory is to conditionally throw excpetions; having the method titled `ThrowExceptionIf` felt redundant and was thus renamed.

####Conditional chaining

You can now chain together more than one exception condition.

    ExceptionFactory
        .ThrowIf<NullReferenceException>(condition == null)
        .Or(() => obj != null)
        .Or<InvalidOperationException>(!(obj is string));
        
In the above example, the ExceptionFactory will throw a `NullReferenceException` if the `condition` member is null, or the `obj` member is not null. If both of those checks pass, then the last check will throw an `InvalidOperationException` if the `obj` member is not a string.
 
### Basic conditional Exception throwing

    ExceptionFactory.ThrowIf<NullReferenceException>(condition == null);
  
### Basic conditional Exception throwing with custom message

    ExceptionFactory.ThrowIf<NullReferenceException>(condition == null, "Condition must not be null!");
    
### Throw with custom data

    ExceptionFactory.ThrowIf<NullReferenceException>(
      condition == null,
      "Condition must not be null!",
      new KeyValuePair<string, string>("Date", DateTime.Now.ToString());
      
### Throw Exception from Func

    ExceptionFactory.ThrowIf<NullReferenceException>(
        () => !File.Exists("NonExistantFile.txt"));
        
### Throw Exception from Func with custom Exception Factory delegate

    ExceptionFactory.ThrowIf<ArgumentNullException>(
            () => !File.Exists(fileName),
            () => new ArgumentNullException("fileName", "File does not exist."));
            
### Throw Exception with callback

    // Arrange
    bool testIsPassing = false;

    // Act
    ExceptionFactory
        .Throwif<NullReferenceException>(
            () => !File.Exists("NonExistantFile.txt"))
        .ElseDo(() => testIsPassing = true);
