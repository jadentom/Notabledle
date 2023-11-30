namespace Notabledle.NotableModel.PropertyEnums
{
    public struct OilRecipe
    {
        public OilRecipe(BlightOil largestOil, BlightOil secondOil, BlightOil thirdOil)
        {
            if (largestOil < secondOil)
            {
                throw new ArgumentException($"Largest {largestOil} < Second {secondOil}");
            }

            if (secondOil < thirdOil)
            {
                throw new ArgumentException($"Second {secondOil} < Third {thirdOil}");
            }

            LargestOil = largestOil;
            SecondOil = secondOil;
            ThirdOil = thirdOil;
        }

        public BlightOil LargestOil { get; }
        public BlightOil SecondOil { get; }
        public BlightOil ThirdOil { get; }

        public override string ToString()
        {
            return $"{LargestOil}, {SecondOil}, {ThirdOil}";
        }
    }
}
