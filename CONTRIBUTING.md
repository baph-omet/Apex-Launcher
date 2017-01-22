# Issues
Did you find an issue with the Launcher? Please open an issue report on the [issues page](https://github.com/griffenx/Apex-Launcher/issues). Before submitting, please make sure to follow these guidelines:

1. Only report issues with the _Launcher itself_ here. If you found an issue with Apex within the game, please report it [on the subreddit](https://reddit.com/r/PokemonApex).

2. Do not report duplicate issues. Please take a moment to look over all open and closed issues before reporting to make sure that your issue has not already been reported. If you have information to add to the existing report, please feel free to do so, but otherwise please do not make duplicate reports.

3. Please make your title concise and representitive of your exact issue.

4. Please give as much information as possible when reporting issues. 
    * What were you doing, exactly, when the issue occurred? 
    * What exactly happened that was wrong? Did you get an error message, and if so, what did it say? (When reporting error messages, you don't have to include most of the error. Usually just the stack trace (the bit marked "Exception Text") is necessary, but if in doubt, include the whole thing). 
    * What conditions about your situation might be considered atypical? (First time running the program, messed with internal files, etc.)
    
5. Please make sure to come back and update your issue reports if more information is requested. If your problem is unable to be replicated and you do not provide more information, I'll have to mark your report as inactive and close it.

That being said, don't let this discourage you from making a report. I'd much rather you make an incorrect report than not make a report at all!

You are also allowed (and encouraged) to use the Issues system to make suggestions for the Launcher.

# Pull Requests
If you're interested in contributing code to the project, you are more than welcome to. I ask that you follow some basic code formatting guidelines to keep things consistent.

1. Open braces go on the same line as their respective statement.
    * Yes:

            if (condition) {
                DoSomething();
            }
    
    * No:
    
            if (condition)
            {
                DoSomething();
            }
        
2. Public methods and properties should be named in UpperCamelCase. Private methods and properties should at least be named in lowerCamelCase. I don't care that much about local variables, as long as they're legible.
    * Yes:
    
            public string PropertyName;
    * No:
    
            public string propertyname;
3. Long lines should be split into multi-line statements. Avoid off-screen code and text wrap whenever possible.
4. Strings should be declared as type `string` instead of `String`.

Other common sense things should also apply.
