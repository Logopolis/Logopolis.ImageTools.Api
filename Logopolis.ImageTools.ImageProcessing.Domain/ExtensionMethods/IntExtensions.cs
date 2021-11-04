namespace Logopolis.ImageTools.ImageProcessing.Domain.ExtensionMethods
{
    public static class IntExtensions
    {
        public static bool HasNonZeroPositiveValue(this int? nullableInt)
        {
            return nullableInt.HasValue && nullableInt.Value > 0;
        }
    }
}
