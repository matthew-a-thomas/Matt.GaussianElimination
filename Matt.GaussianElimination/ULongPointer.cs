namespace Matt.GaussianElimination
{
    readonly unsafe struct ULongPointer
    {
        public readonly ulong* Pointer;

        public ULongPointer(ulong* pointer)
        {
            Pointer = pointer;
        }
    }
}