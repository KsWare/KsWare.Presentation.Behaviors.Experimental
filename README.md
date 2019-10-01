# KsWare.Presentation.Behaviors.Experimental
KsWare.Presentation.Behaviors.Experimental

## FocusFirstElementBehavior
Set focus to first element after load.  
```xml
<Window
    xmlns:ksv="http://ksware.de/Presentation/ViewFramework"
    xmlns:behaviors="http://schemas.microsoft.com/xaml/behaviors">
    <behaviors:Interaction.Behaviors>
        <ksv:FocusFirstElementBehavior />
    </behaviors:Interaction.Behaviors>
    <Grid />
</Window>
```

## IsFocusedPropertyBehavior
Provides a two-way bindable IsFocused property.

```csharp
<behaviors:Interaction.Behaviors>
    <ksv:IsFocusedPropertyBehavior IsElementFocused="{Binding IsFocused}"/>
</behaviors:Interaction.Behaviors>
```