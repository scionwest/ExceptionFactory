ExceptionFactory
================

Provides a series of factory methods for creating exceptions in .NET

### Basic conditional Exception throwing

    ExceptionFactory.ThrowExceptionIf<NullReferenceException>(condition == null);
  
### Basic conditional Exception throwing with custom message

    ExceptionFactory.ThrowExceptionIf<NullReferenceException>(condition == null, "Condition must not be null!");
    
### Throw with custom data

    ExceptionFactory.ThrowExceptionIf<NullReferenceException>(
      condition == null,
      "Condition must not be null!",
      new KeyValuePair<string, string>("Date", DateTime.Now.ToString());
      
### Throw Exception from Func

    ExceptionFactory.ThrowExceptionIf<NullReferenceException>(
        () => !File.Exists("NonExistantFile.txt"));
        
### Throw Exception from Func with custom Exception Factory delegate

    ExceptionFactory.ThrowExceptionIf<ArgumentNullException>(
            () => !File.Exists(fileName),
            () => new ArgumentNullException("fileName", "File does not exist."));
            
### Throw Exception with callback

    // Arrange
    bool testIsPassing = false;

    // Act
    ExceptionFactory
        .ThrowExceptionIf<NullReferenceException>(
            () => !File.Exists("NonExistantFile.txt"))
        .ElseDo(() => testIsPassing = true);
