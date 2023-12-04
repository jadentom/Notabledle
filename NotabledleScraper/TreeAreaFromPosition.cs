namespace NotabledleScraper
{
    internal static class TreeAreaFromPosition
    {
        const string Scion = "Scion";
        const string Witch = "Witch";
        const string Shadow = "Shadow";
        const string Ranger = "Ranger";
        const string Duelist = "Duelist";
        const string Marauder = "Marauder";
        const string Templar = "Templar";

        /// <summary>
        /// Quick Recovery's group position:
        /// "x": -2628.79,
        /// "y": -3732.47,
        /// Distance from root: 4565.29
        ///
        /// Inspiring Bond's group position:
        /// "x": -2311.98,
        /// "y": -243.095,
        /// Distance form root: 2324.73
        ///
        /// 3445.01 will be used as the Scion ring
        /// </summary>
        const double ScionRingDistSquared = 11868093.9;

        /// <summary>
        /// Large Jewel socket groups. Looks like it's slightly rotated?
        /// x, y, arctan (rad)
        /// -8031.67, -34.185, 0.00425624977
        /// -4483.59, 7763, -1.04704107
        /// -4395.06, -7575.06, 1.04506188
        /// 4405.04, 7808.4, 1.05716103
        /// 4432.23, -7533.79, -1.0390142
        /// 8038.44, 47.815, 0.0059482233
        /// Average abs(arctan) = 1.047069545. Ratio is tan of that
        /// </summary>
        const double WitchAndDuelistRatio = 1.73153889628;

        public static string Get(double x, double y)
        {
            if ((x*x + y*y) < ScionRingDistSquared)
            {
                return Scion;
            }

            if (y > 0)
            {
                if (Math.Abs(y/x) > WitchAndDuelistRatio)
                {
                    return Duelist;
                }
                if (x > 0)
                {
                    return Ranger;
                }
                return Marauder;
            }
            if (Math.Abs(y / x) > WitchAndDuelistRatio)
            {
                return Witch;
            }
            if (x > 0)
            {
                return Shadow;
            }
            return Templar;
        }
    }
}
