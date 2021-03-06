using System.Linq;
using Mono.Cecil;

public partial class ModuleWeaver
{

    public void CheckForNormalAttribute(IMemberDefinition memberDefinition)
    {
        var skip = memberDefinition
            .CustomAttributes
            .Any(x => x.AttributeType.Name == "DoNotWarnAboutObsoleteUsageAttribute");
        if (skip)
        {
            return;
        }

        var obsoleteExAttribute = memberDefinition
            .CustomAttributes
            .FirstOrDefault(x => x.AttributeType.Name == "ObsoleteAttribute");
        if (obsoleteExAttribute == null)
        {
            return;
        }
        var warning = $"The member `{memberDefinition.FullName}` has an ObsoleteAttribute. Consider replacing it with an ObsoleteExAttribute.";
        LogWarning(warning);
    }
}