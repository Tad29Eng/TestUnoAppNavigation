using TestUnoAppNavigation.Enums;

namespace TestUnoAppNavigation.Models;

public record OrderPageShowParam(
    ShowEntityDetailPageVariants ShowVariant,
    int? Code);
