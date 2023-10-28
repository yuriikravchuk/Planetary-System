public static class ExtentionMethods
{
    public static double Remap(this double from, double fromMin, double fromMax, double toMin, double toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;
        var normal = fromAbs / fromMaxAbs;
        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;
        var to = toAbs + toMin;
        return to;
    }
}